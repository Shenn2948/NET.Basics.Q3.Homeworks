using System.Configuration;

namespace Task1;

public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
{
    public void SaveSetting(string key, object value)
    {
        try
        {
            var configMap = new ExeConfigurationFileMap { ExeConfigFilename = "app.config" };
            var configFile = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None, true);

            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value.ToString());
            }
            else
            {
                settings[key].Value = value.ToString();
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
        catch (ConfigurationErrorsException)
        {
            Console.WriteLine("Error writing app settings");
        }
    }

    public string LoadSetting(string key)
    {
        string result = null;
        try
        {
            result = ConfigurationManager.AppSettings[key];
        }
        catch (ConfigurationErrorsException)
        {
            Console.WriteLine("Error reading app settings");
        }

        return result;
    }
}