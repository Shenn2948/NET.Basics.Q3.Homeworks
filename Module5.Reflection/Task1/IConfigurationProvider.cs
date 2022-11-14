namespace Task1;

public interface IConfigurationProvider
{
    void SaveSetting(string key, object value);
    string LoadSetting(string key);
}