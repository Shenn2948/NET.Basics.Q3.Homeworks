using System.Reflection;

namespace Task1;

public class ConfigurationComponentBase
{
    private static readonly Dictionary<Type, Func<string, object>> TypeMapping = new()
    {
        {
            typeof(int), (x) =>
            {
                if (int.TryParse(x, out int result))
                {
                    return result;
                }

                return null;
            }
        },
        {
            typeof(float), (x) =>
            {
                if (float.TryParse(x, out float result))
                {
                    return result;
                }

                return null;
            }
        },
        { typeof(string), (x) => x },
        {
            typeof(TimeSpan), (x) =>
            {
                if (TimeSpan.TryParse(x, out var result))
                {
                    return result;
                }

                return null;
            }
        }
    };

    public static void SaveSettings(Settings settings)
    {
        Dictionary<PropertyInfo, IConfigurationProvider> map = GetPropertyInfoProviderMap();

        foreach ((PropertyInfo propertyInfo, IConfigurationProvider provider) in map)
        {
            if (TypeMapping.ContainsKey(propertyInfo.PropertyType))
            {
                provider.SaveSetting(propertyInfo.Name, propertyInfo.GetValue(settings));
            }
        }
    }

    public static Settings LoadSettings()
    {
        Settings settings = new();

        Dictionary<PropertyInfo, IConfigurationProvider> map = GetPropertyInfoProviderMap();

        foreach ((PropertyInfo propertyInfo, IConfigurationProvider provider) in map)
        {
            string settingValue = provider.LoadSetting(propertyInfo.Name);

            if (!TypeMapping.TryGetValue(propertyInfo.PropertyType, out var mapFunc))
            {
                continue;
            }

            object result = mapFunc(settingValue);

            if (result != null)
            {
                propertyInfo.SetValue(settings, result);
            }
        }

        return settings;
    }

    private static Dictionary<PropertyInfo, IConfigurationProvider> GetPropertyInfoProviderMap()
    {
        Dictionary<PropertyInfo, IConfigurationProvider> providerMap = new();

        IEnumerable<PropertyInfo> properties = typeof(Settings).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                               .Where(prop => prop.IsDefined(typeof(ConfigurationItemAttribute), false));
        Assembly asm = Assembly.GetExecutingAssembly();
        Type[] types = GetTypesThatImplementIConfigurationProvider(asm).ToArray();

        foreach (PropertyInfo propertyInfo in properties)
        {
            var attribute = propertyInfo.GetCustomAttribute<ConfigurationItemAttribute>(false);
            if (attribute == null) continue;
            IConfigurationProvider providerInstance = GetProviderInstanceByKind(types, attribute.ProviderKind);

            Console.WriteLine($"> Property:'{propertyInfo.Name}', provider kind:'{attribute.ProviderKind}.'");

            if (providerInstance == null) continue;

            providerMap.Add(propertyInfo, providerInstance);
        }

        return providerMap;
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
}