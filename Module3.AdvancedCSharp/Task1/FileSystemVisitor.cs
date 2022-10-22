using System.Data;
using Task1.Delegates;
using Task1.Interfaces;
using File = Task1.Models.File;

namespace Task1;

public class FileSystemVisitor
{
    private readonly Func<IFolder, bool>? _folderPredicate;
    private readonly Func<File, bool>? _filePredicate;

    public FileSystemVisitor(Func<IFolder, bool>? folderPredicate = null, Func<File, bool>? filePredicate = null)
    {
        _folderPredicate = folderPredicate;
        _filePredicate = filePredicate;
    }

    public event EventHandler<EventArgs>? SearchStarted;

    public event EventHandler<EventArgs>? SearchFinished;

    public event FileSystemItemFoundHandler<IFolder>? DirectoryFound;

    public event FileSystemItemFoundHandler<File>? FileFound;

    public event FileSystemItemFoundHandler<IFolder>? FilteredDirectoryFound;

    public event FileSystemItemFoundHandler<File>? FilteredFileFound;

    public IEnumerable<IFileSystemItem> Traverse(IFolder root)
    {
        if (!root.Exists)
        {
            Console.WriteLine("Provided directory does not exist in filesystem.");
            yield break;
        }

        var stack = new Stack<IFolder>();
        stack.Push(root);

        SearchStarted?.Invoke(this, EventArgs.Empty);

        while (stack.Count > 0)
        {
            IFolder currentFolder = stack.Pop();
            IEnumerable<IFolder> foldersByParent = currentFolder.GetFolders();

            if (!TryFilterItems(foldersByParent, BuildFolderFilterArgs(stack)))
            {
                yield return currentFolder;

                foreach (IFolder folder in stack)
                {
                    yield return folder;
                }

                yield break;
            }

            IEnumerable<File> files = currentFolder.GetFiles();
            var filteredFiles = new List<File>();

            if (!TryFilterItems(files, BuildFileFilterArgs(filteredFiles)))
            {
                foreach (File folderFile in filteredFiles)
                {
                    yield return folderFile;
                }

                yield break;
            }

            yield return currentFolder;

            foreach (File folderFile in filteredFiles)
            {
                yield return folderFile;
            }
        }

        SearchFinished?.Invoke(this, EventArgs.Empty);
    }

    private static bool TryFilterItems<T>(IEnumerable<T> items, FileSystemItemsFilterArgs<T> args) where T : IFileSystemItem
    {
        foreach (T item in items)
        {
            var beforeFilter = args.FoundHandler?.Invoke(item);
            bool notExclude = !beforeFilter?.ExcludeFromResult ?? true;
            bool abort = beforeFilter?.AbortSearch ?? false;

            bool isFiltered = args.Predicate?.Invoke(item) ?? false;

            if (isFiltered)
            {
                var filteredFoundArgs = args.FilteredFoundHandler?.Invoke(item);
                notExclude = !filteredFoundArgs?.ExcludeFromResult ?? true;
                abort = filteredFoundArgs?.AbortSearch ?? false;
            }

            if (notExclude)
            {
                args.CallBack?.Invoke(item);
            }

            if (abort)
            {
                return false;
            }
        }

        return true;
    }

    private FileSystemItemsFilterArgs<File> BuildFileFilterArgs(ICollection<File> folderFiles)
    {
        return new FileSystemItemsFilterArgs<File>
        {
            Predicate = _filePredicate, FoundHandler = FileFound, FilteredFoundHandler = FilteredFileFound, CallBack = folderFiles.Add
        };
    }

    private FileSystemItemsFilterArgs<IFolder> BuildFolderFilterArgs(Stack<IFolder> stack)
    {
        return new FileSystemItemsFilterArgs<IFolder>
        {
            Predicate = _folderPredicate,
            FoundHandler = DirectoryFound,
            FilteredFoundHandler = FilteredDirectoryFound,
            CallBack = stack.Push
        };
    }
}