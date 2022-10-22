using Task1.Interfaces;

namespace Task1.Delegates;

internal class FileSystemItemsFilterArgs<T> where T : IFileSystemItem
{
    public required FileSystemItemFoundHandler<T>? FoundHandler { get; init; }

    public required FileSystemItemFoundHandler<T>? FilteredFoundHandler { get; init; }

    public Func<T, bool>? Predicate { get; set; }

    public Action<T>? CallBack { get; set; }
}