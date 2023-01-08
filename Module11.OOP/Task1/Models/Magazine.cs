namespace Task1.Models;

public record Magazine : IDocument
{
    public string Title { get; set; }
    public string Publisher { get; set; }
    public int ReleaseNumber { get; set; }
    public DateTime PublishedDate { get; set; }

    public string GetInfo()
    {
        return this.ToString();
    }
}
