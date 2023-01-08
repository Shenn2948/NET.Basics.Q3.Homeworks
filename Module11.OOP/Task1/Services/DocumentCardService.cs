using Task1.Models;
using Task1.StorageContext;

namespace Task1.Services;

public class DocumentCardService
{
    private readonly IStorageContext _context;

    public DocumentCardService(IStorageContext context)
    {
        _context = context;
    }

    public IEnumerable<DocumentCard> GetAll()
    {
        return _context.DocumentCards;
    }

    public DocumentCard? GetByDocNumber(int docNumber)
    {
        return _context.DocumentCards.FirstOrDefault(card => card.DocNumber == docNumber);
    }
}