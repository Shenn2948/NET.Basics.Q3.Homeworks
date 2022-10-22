using Task1.Interfaces;
using File = Task1.Models.File;

namespace Task1.Tests.Models;

public record MockFolder(string Name) : IFolder
{
    public bool Exists => true;

    public List<MockFolder> SubFolders { get; set; } = new();

    public List<File> Files { get; set; } = new();

    public IEnumerable<File> GetFiles()
    {
        return Files;
    }

    public IEnumerable<IFolder> GetFolders()
    {
        return SubFolders;
    }

    public override string ToString()
    {
        List<string> subFolders = GetFolders().Select(x => x.Name).ToList();
        List<string> files = GetFiles().Select(x => x.Name).ToList();

        string subFoldersText = subFolders.Count > 0 ? $"Subfolders: {string.Join(", ", subFolders)}" : "Subfolders: 0";
        string filesText = files.Count > 0 ? $"Files: {string.Join(", ", files)}" : "Files: 0";

        return $"Folder: {Name}, {subFoldersText}, {filesText}";
    }
}