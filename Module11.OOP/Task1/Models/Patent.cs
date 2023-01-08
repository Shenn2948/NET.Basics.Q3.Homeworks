namespace Task1.Models;

public record Patent: IDocument
{
    public string Title { get; set; }
    public string Authors { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string UniqueId { get; set; }

    public string GetInfo()
    {
        return this.ToString();
    }
}
