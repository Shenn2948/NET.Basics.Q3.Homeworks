namespace Task1;

public class FileConfigurationProvider : IConfigurationProvider
{
    public void SaveSetting(string key, object value)
    {
        var keyValuePairs = File.ReadAllLines("app-config.txt").Select(GetPair).ToDictionary(x => x.Key, x => x.Value);

        if (!keyValuePairs.ContainsKey(key))
        {
            keyValuePairs.Add(key, value.ToString());
        }
        else
        {
            keyValuePairs[key] = value.ToString();
        }

        string[] updatedLines = keyValuePairs.Select(x => $"[{x.Key}]: {x.Value}").ToArray();

        File.WriteAllLines("app-config.txt", updatedLines);
    }

    public string LoadSetting(string key)
    {
        var fileInfo = new FileInfo("app-config.txt");
        if (!fileInfo.Exists)
        {
            return null;
        }

        var keyValuePairs = File.ReadAllLines("app-config.txt").Select(GetPair).ToDictionary(x => x.Key, x => x.Value);

        return keyValuePairs.FirstOrDefault(pair => pair.Key == key).Value;
    }

    private static KeyValuePair<string, string> GetPair(string source)
    {
        string[] parts = source.Split(':');

        if (parts.Length != 2) return default;

        string key = string.IsNullOrWhiteSpace(parts[0]) ? null : parts[0].Trim('[', ']');
        string value = string.IsNullOrWhiteSpace(parts[1]) ? null : parts[1].Trim();

        return new KeyValuePair<string, string>(key, value);
    }
}