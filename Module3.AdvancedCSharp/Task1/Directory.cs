namespace Task1;

public class Folder
{
    public required DirectoryInfo DirectoryInfo { get; init; }

    public List<Folder> Directories { get; init; } = new();

    public List<FileInfo> Files { get; set; } = new();

    public bool HasDirectories => Directories.Count > 0;

    public bool HasFiles => Files.Count > 0;

    public static Folder MapFromDirectoryInfo(DirectoryInfo directoryInfo)
    {
        return new Folder() { DirectoryInfo = directoryInfo };
    }

    public override string ToString()
    {
        List<string> subFolders = Directories.Select(x => x.DirectoryInfo.Name).ToList();
        List<string> files = Files.Select(x => x.Name).ToList();

        string subFoldersText = subFolders.Count > 0 ? $"Subfolders: {string.Join(", ", subFolders)}" : "Subfolders: 0";
        string filesText = Files.Count > 0 ? $"Files: {string.Join(", ", files)}" : "Files: 0";

        return $"Folder: {DirectoryInfo.Name}, {subFoldersText}, {filesText}";
    }
}
