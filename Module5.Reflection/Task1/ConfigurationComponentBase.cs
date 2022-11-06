using System.Reflection;

namespace Task1;

public class ConfigurationComponentBase
{
    public static void SaveSettings(Settings settings)
    {
        // get all settings properties which marked with [ConfigurationItem] attribute
        // for-each property get provider type from the [ConfigurationItem] attribute
        //    check this assembly for providers of IConfigurationProvider type
        //    check other assembly (task 2)
        //    create provider instance of IConfigurationProvider type

        //    save setting prop value using provider instance
        ProcessSettings(settings, (provider, propertyInfo) =>
        {

            provider.SaveSetting(propertyInfo.Name, propertyInfo.GetValue(settings));
        });
    }

    public static void LoadSettings(Settings settings)
    {
        // get all settings properties which marked with [ConfigurationItem] attribute
        // for-each property get provider type from the [ConfigurationItem] attribute
        //    check this assembly for providers of IConfigurationProvider type
        //    check other assembly (task 2)
        //    create provider instance of IConfigurationProvider type

        //    load setting prop value using provider instance
        ProcessSettings(settings, (provider, propertyInfo) =>
        {
            var setting = provider.LoadSetting(propertyInfo.Name);
            propertyInfo.SetValue(settings, setting);
        });
    }

    private static IEnumerable<Type> GetTypesThatImplementIConfigurationProvider(Assembly assembly)
    {
        return assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IConfigurationProvider)));
    }

    private static Type GetProviderTypeByKind(IEnumerable<Type> types, ProviderKind providerType)
    {
        return types.FirstOrDefault(type => providerType switch
                {
                    ProviderKind.File => type == typeof(FileConfigurationProvider),
                    ProviderKind.ConfigurationManager => type == typeof(ConfigurationManagerConfigurationProvider),
                    _ => false,
                });
    }

    private static IConfigurationProvider GetProviderInstanceByKind(IEnumerable<Type> types, ProviderKind providerKind)
    {
        Type providerType = GetProviderTypeByKind(types, providerKind);
        return providerType == null ? null : Activator.CreateInstance(providerType) as IConfigurationProvider;
    }

    private static void ProcessSettings(Settings settings, Action<IConfigurationProvider, PropertyInfo> onPropertyFound)
    {
        IEnumerable<PropertyInfo> properties = settings.GetType()
                                                       .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                       .Where(prop => prop.IsDefined(typeof(ConfigurationItemAttribute), false));
        Assembly asm = Assembly.GetExecutingAssembly();
        IEnumerable<Type> types = GetTypesThatImplementIConfigurationProvider(asm);

        foreach (PropertyInfo propertyInfo in properties)
        {
            var attribute = propertyInfo.GetCustomAttribute<ConfigurationItemAttribute>(false);
            IConfigurationProvider providerInstance = GetProviderInstanceByKind(types, attribute.ProviderKind);

            Console.WriteLine($"> Property:'{propertyInfo.Name}', provider kind:'{attribute.ProviderKind}.'");
            onPropertyFound(providerInstance, propertyInfo);
        }
    }
}
