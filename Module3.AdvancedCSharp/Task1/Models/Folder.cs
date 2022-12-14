using Task1.Interfaces;

namespace Task1.Models;

public class Folder : IFolder
{
    private readonly DirectoryInfo _directory;

    public Folder(string path)
    {
        _directory = new DirectoryInfo(path);

        Exists = _directory.Exists;
        Name = _directory.Name;
    }

    public string Name { get; }

    public bool Exists { get; }

    public IEnumerable<File> GetFiles()
    {
        return _directory.GetFiles().Select(file => new File(file.Name, this));
    }

    public IEnumerable<IFolder> GetFolders()
    {
        return _directory.GetDirectories().Select(dir => new Folder(dir.FullName));
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
