using Task1.Interfaces;

namespace Task1.Models;

public record File(string Name, IFolder ParentFolder): IFileSystemItem
{
    public override string ToString()
    {
        return $"File: {Name}. ParentFolder: {ParentFolder.Name}";
    }
}
