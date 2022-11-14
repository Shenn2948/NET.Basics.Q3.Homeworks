using System.Reflection;
using Task1.Attributes;
using Task1.Models;
using Task2.Providers;

namespace Task1;

public class ConfigurationComponentBase
{
    private readonly Dictionary<Type, Func<string, object>> _typeMapping = new()
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

    private readonly Dictionary<PropertyInfo, IConfigurationProvider> _providerMap;

    public ConfigurationComponentBase()
    {
        _providerMap = GetPropertyInfoProviderMap();
    }

    public void SaveSettings(Settings settings)
    {
        foreach ((PropertyInfo propertyInfo, IConfigurationProvider provider) in _providerMap)
        {
            if (_typeMapping.ContainsKey(propertyInfo.PropertyType))
            {
                provider.SaveSetting(propertyInfo.Name, propertyInfo.GetValue(settings));
            }
        }
    }

    public Settings LoadSettings()
    {
        Settings settings = new();

        foreach ((PropertyInfo propertyInfo, IConfigurationProvider provider) in _providerMap)
        {
            string settingValue = provider.LoadSetting(propertyInfo.Name);

            if (!_typeMapping.TryGetValue(propertyInfo.PropertyType, out var mapFunc))
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
        IEnumerable<Assembly> assemblies = GetAssembliesToCheckForConfigProviders();
        Type[] types = assemblies.Select(GetTypesThatImplementIConfigurationProvider).SelectMany(type => type).ToArray();

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

    private static IEnumerable<Assembly> GetAssembliesToCheckForConfigProviders()
    {
        foreach (string file in Directory.GetFiles(@".\Plugins", "*.dll"))
        {
            yield return Assembly.LoadFrom(Directory.GetCurrentDirectory() + file);
        }

        yield return Assembly.GetExecutingAssembly();
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