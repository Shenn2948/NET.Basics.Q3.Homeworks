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
        return _directory.GetFiles().Select(file => new File { Name = file.Name });
    }

    public IEnumerable<IFolder> GetFolders()
    {
        return _directory.GetDirectories().Select(dir => new Folder(dir.FullName));
    }
}
