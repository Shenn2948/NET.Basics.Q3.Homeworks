namespace Task2.Providers;

public interface IConfigurationProvider
{
    void SaveSetting(string key, object value);
    string LoadSetting(string key);
}