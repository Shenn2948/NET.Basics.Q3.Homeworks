using Task1.Interfaces;

namespace Task1.Delegates;

public delegate FileSystemItemFoundEventArgs FileSystemItemFoundHandler<in T>(T foundItem) where T : IFileSystemItem;

public class FileSystemItemFoundEventArgs
{
    public bool AbortSearch { get; set; }

    public bool ExcludeFromResult { get; set; }

    public static FileSystemItemFoundEventArgs Empty = new ();
}