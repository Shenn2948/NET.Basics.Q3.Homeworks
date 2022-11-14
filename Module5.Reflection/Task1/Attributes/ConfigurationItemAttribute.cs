using Task1.Models;

namespace Task1.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigurationItemAttribute : Attribute
{
    public ConfigurationItemAttribute(string settingName, ProviderKind providerKind)
    {
        SettingName = settingName;
        ProviderKind = providerKind;
    }

    public string SettingName { get; }
    public ProviderKind ProviderKind { get; }
}