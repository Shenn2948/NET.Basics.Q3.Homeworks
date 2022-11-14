using Task1.Attributes;

namespace Task1.Models;

public class Settings
{
    [ConfigurationItem(nameof(ServiceId), ProviderKind.File)] // should not work according to the requirements
    public Guid ServiceId { get; set; }

    [ConfigurationItem(nameof(ResponseTimeoutSeconds), ProviderKind.File)]
    public int ResponseTimeoutSeconds { get; set; }

    [ConfigurationItem(nameof(Precision), ProviderKind.ConfigurationManager)]
    public float Precision { get; set; }

    [ConfigurationItem(nameof(Name), ProviderKind.File)]
    public string Name { get; set; }

    [ConfigurationItem(nameof(AutoClearHistoryTime), ProviderKind.ConfigurationManager)]
    public TimeSpan AutoClearHistoryTime { get; set; }
}