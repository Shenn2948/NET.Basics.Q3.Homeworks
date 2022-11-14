using Task1;

var mockSettings = new Settings()
{
    Name = "DEV",
    ServiceId = Guid.NewGuid(),
    AutoClearHistoryTime = new TimeSpan(1, 30, 0),
    Precision = 1.5f,
    ResponseTimeoutSeconds = 5
};

var settings = ConfigurationComponentBase.LoadSettings();

ConfigurationComponentBase.SaveSettings(mockSettings);

Console.WriteLine("press any key to exit...");
Console.ReadKey();