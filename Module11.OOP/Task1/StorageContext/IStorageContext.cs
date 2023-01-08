using Task1.Models;

namespace Task1.StorageContext;

public interface IStorageContext
{
    ICollection<DocumentCard> DocumentCards { get; }
}
