using Task1.Interfaces;

namespace Task1.Models;

public class File: IFileSystemItem
{
    public required string Name { get; init; }
}
