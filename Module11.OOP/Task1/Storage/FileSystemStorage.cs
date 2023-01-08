using System.Text.Json;

using Task1.Models;

namespace Task1.Storage;

public class FileSystemStorage : IStorage
{
    record DocumentCardFileMetadata(int DocNumber, DocType DocType, string File);

    public DocumentCard? GetDocumentCardByNumber(int docNumber)
    {
        var fileMetadata = GetFiles().Select(MapFileToDocumentCardFileMetadata)
                                     .FirstOrDefault(doc => doc.DocNumber == docNumber);

        if (fileMetadata == null) return null;

        IDocument? doc = MapFileToDocument(fileMetadata);
        return new DocumentCard(docNumber, fileMetadata.DocType, doc!);
    }

    private static string[] GetFiles()
    {
        return Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Documents", "*.json");
    }

    private DocumentCardFileMetadata MapFileToDocumentCardFileMetadata(string file)
    {
        string[] split = Path.GetFileNameWithoutExtension(file).Split("_");
        int docNumber = int.Parse(split[1]);
        DocType docType = Enum.Parse<DocType>(split[0], true);

        return new DocumentCardFileMetadata(docNumber, docType, file);
    }

    private static IDocument? MapFileToDocument(DocumentCardFileMetadata fileMetadata)
    {
        string text = File.ReadAllText(fileMetadata.File);
        return fileMetadata.DocType switch
        {
            DocType.Patent => JsonSerializer.Deserialize<Patent>(text),
            DocType.Book => JsonSerializer.Deserialize<Book>(text),
            DocType.LocalisedBook => JsonSerializer.Deserialize<LocalizedBook>(text),
            DocType.Magazine => JsonSerializer.Deserialize<Magazine>(text),
            _ => throw new ArgumentException("Unable to convert doc content by doc type"),
        };
    }
}