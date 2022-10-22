using Task1.Delegates;
using Task1.Interfaces;
using Task1.ViewModels;
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

    public event EventHandler? SearchStarted;

    public event EventHandler? SearchFinished;

    public event FileSystemItemFoundHandler<IFolder>? DirectoryFound;

    public event FileSystemItemFoundHandler<File>? FileFound;

    public event FileSystemItemFoundHandler<IFolder>? FilteredDirectoryFound;

    public event FileSystemItemFoundHandler<File>? FilteredFileFound;

    public IEnumerable<FolderViewModel> Traverse(IFolder folder)
    {
        if (!folder.Exists)
        {
            Console.WriteLine("Provided directory does not exist in filesystem.");
            yield break;
        }

        var stack = new Stack<FolderViewModel>();
        var root = new FolderViewModel { Folder = folder };
        stack.Push(root);

        SearchStarted?.Invoke(this, EventArgs.Empty);

        while (stack.Count > 0)
        {
            FolderViewModel currentNode = stack.Pop();
            IEnumerable<IFolder> foldersByParent = currentNode.Folder.GetFolders();
            FileSystemItemsFilterArgs<IFolder> folderArgs = BuildFolderFilterArgs(currentNode, stack);

            if (!TryFilterItems(foldersByParent, folderArgs))
            {
                yield break;
            }

            IEnumerable<File> files = currentNode.Folder.GetFiles();
            var finalFileList = new List<File>();
            FileSystemItemsFilterArgs<File> fileArgs = BuildFileFilterArgs(finalFileList);

            if (!TryFilterItems(files, fileArgs))
            {
                yield break;
            }

            currentNode.Files = finalFileList;

            yield return currentNode;
        }

        SearchFinished?.Invoke(this, EventArgs.Empty);
    }

    private static bool TryFilterItems<T>(IEnumerable<T> items, FileSystemItemsFilterArgs<T> args) where T : IFileSystemItem
    {
        foreach (T item in items)
        {
            var beforeFilter = args.FoundHandler?.Invoke(item);
            if (beforeFilter?.AbortSearch ?? false)
            {
                return false;
            }

            bool excludeFromResult = beforeFilter?.ExcludeFromResult ?? false;
            bool isFiltered = args.Predicate?.Invoke(item) ?? false;
            if (excludeFromResult || isFiltered)
            {
                continue;
            }

            var afterFilter = args.FilteredFoundHandler?.Invoke(item);
            if (afterFilter?.AbortSearch ?? false)
            {
                return false;
            }

            if (afterFilter?.ExcludeFromResult ?? false)
            {
                continue;
            }

            args.CallBack?.Invoke(item);
        }

        return true;
    }

    private FileSystemItemsFilterArgs<File> BuildFileFilterArgs(ICollection<File> finalFileList)
    {
        return new FileSystemItemsFilterArgs<File>
        {
            Predicate = _filePredicate, FoundHandler = FileFound, FilteredFoundHandler = FilteredFileFound, CallBack = finalFileList.Add
        };
    }

    private FileSystemItemsFilterArgs<IFolder> BuildFolderFilterArgs(FolderViewModel currentNode, Stack<FolderViewModel> stack)
    {
        return new FileSystemItemsFilterArgs<IFolder>
        {
            Predicate = _folderPredicate,
            FoundHandler = DirectoryFound,
            FilteredFoundHandler = FilteredDirectoryFound,
            CallBack = childFolder =>
            {
                var childFolderVM = new FolderViewModel { Folder = childFolder };
                currentNode.Directories.Add(childFolderVM);
                stack.Push(childFolderVM);
            }
        };
    }
}