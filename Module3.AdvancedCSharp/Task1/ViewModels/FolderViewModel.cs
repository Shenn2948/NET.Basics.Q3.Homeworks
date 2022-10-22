using Task1.Interfaces;
using Task1.Models;
using File = Task1.Models.File;

namespace Task1.ViewModels;

public class FolderViewModel
{
    public required IFolder Folder { get; init; }

    public List<FolderViewModel> Directories { get; init; } = new();

    public List<File> Files { get; set; } = new();

    public override string ToString()
    {
        List<string> subFolders = Directories.Select(x => x.Folder.Name).ToList();
        List<string> files = Files.Select(x => x.Name).ToList();

        string subFoldersText = subFolders.Count > 0 ? $"Subfolders: {string.Join(", ", subFolders)}" : "Subfolders: 0";
        string filesText = Files.Count > 0 ? $"Files: {string.Join(", ", files)}" : "Files: 0";

        return $"Folder: {Folder.Name}, {subFoldersText}, {filesText}";
    }
}
