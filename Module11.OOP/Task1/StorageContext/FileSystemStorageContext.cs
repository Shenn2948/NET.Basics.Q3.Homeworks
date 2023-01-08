using System.Text.Json;

using Task1.Models;

namespace Task1.StorageContext;

public class FileSystemStorageContext : IStorageContext
{
    public FileSystemStorageContext()
    {
        DocumentCards = GetFiles().Select(MapFileToCard).ToList();
    }

    public ICollection<DocumentCard> DocumentCards { get; }

    private static string[] GetFiles()
    {
        return Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Documents", "*.json");
    }

    private DocumentCard MapFileToCard(string file)
    {
        string[] split = Path.GetFileNameWithoutExtension(file).Split("_");
        string text = File.ReadAllText(file);

        int docNumber = int.Parse(split[1]);
        DocType docType = Enum.Parse<DocType>(split[0], true);

        IDocument? doc = docType switch
        {
            DocType.Patent => JsonSerializer.Deserialize<Patent>(text),
            DocType.Book => JsonSerializer.Deserialize<Book>(text),
            DocType.LocalisedBook => JsonSerializer.Deserialize<LocalizedBook>(text),
            _ => throw new ArgumentException("Unable to convert doc content by doc type"),
        };

        return new DocumentCard(docNumber, docType, doc!);
    }
}
