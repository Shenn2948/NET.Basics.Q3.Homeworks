using Task1;
using Task1.Models;

var mockSettings = new Settings()
{
    Name = "DEV",
    ServiceId = Guid.NewGuid(),
    AutoClearHistoryTime = new TimeSpan(1, 30, 0),
    Precision = 1.5f,
    ResponseTimeoutSeconds = 5
};

var configuration = new ConfigurationComponentBase();

var settings = configuration.LoadSettings();

configuration.SaveSettings(mockSettings);

Console.WriteLine("press any key to exit...");
Console.ReadKey();