using File = Task1.Models.File;

namespace Task1.Interfaces;

public interface IFolder: IFileSystemItem
{
    bool Exists { get; }
    IEnumerable<IFolder> GetFolders();
    IEnumerable<File> GetFiles();
}