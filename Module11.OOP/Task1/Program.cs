using Task1.Services;
using Task1.StorageContext;

IStorageContext fileSystemStorage = new FileSystemStorageContext();
var docService = new DocumentCardService(fileSystemStorage);

Console.WriteLine("Document cards app");

string? userInput = string.Empty;

while (userInput?.Trim().ToLower() != "q")
{
    Console.WriteLine("\nPlease enter the search number or 'q' to quit");
    userInput = Console.ReadLine();
    if (int.TryParse(userInput, out var documentNumber))
    {
        var card = docService.GetByDocNumber(documentNumber);
        
        if(card == null)
        {
            Console.WriteLine("not found");
            continue;
        }

        Console.WriteLine($"Found a record with Type: {card.DocType}, Number: {card.DocNumber}");
        Console.WriteLine();
        Console.WriteLine(card.Document.GetInfo());
    }
}
