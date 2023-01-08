using Task1.Models;

namespace Task1.Storage;

public interface IStorage
{
    DocumentCard? GetDocumentCardByNumber(int docNumber);
}
