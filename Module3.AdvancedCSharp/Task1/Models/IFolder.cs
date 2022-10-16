namespace Task1;

public interface IFolder
{
    string Name { get; }
    bool Exists { get; }
    IEnumerable<IFolder> GetFolders();
    IEnumerable<File> GetFiles();
}