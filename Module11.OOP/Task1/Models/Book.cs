namespace Task1.Models;

public record Book: IDocument
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public DateTime DatePublished { get; set; }
    public int NumberOfPages { get; set; }
    public string Publisher { get; set; }

    public string GetInfo()
    {
        return this.ToString();
    }
}
