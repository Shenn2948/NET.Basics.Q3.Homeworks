namespace Task1;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
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