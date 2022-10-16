namespace Task1;

public delegate FileSystemItemFoundEventArgs DirectoryFoundHandler(IFolder foundFolder);
public delegate FileSystemItemFoundEventArgs FileFoundHandler(File foundFile);

public class FileSystemVisitor
{
    private readonly Func<IFolder, bool> _folderPredicate;
    private readonly Func<File, bool> _filePredicate;

    public FileSystemVisitor(Func<IFolder, bool>? folderPredicate = null,
                             Func<File, bool>? filePredicate = null)
    {
        _folderPredicate = folderPredicate ?? ((folder) => true);
        _filePredicate = filePredicate ?? ((file) => true);
    }


    public event EventHandler? SearchStarted;
    public event EventHandler? SearchFinished;
    public event DirectoryFoundHandler? DirectoryFound;
    public event FileFoundHandler? FileFound;
    public event DirectoryFoundHandler? FilteredDirectoryFound;
    public event FileFoundHandler? FilteredFileFound;

    public IEnumerable<FolderViewModel> Traverse(IFolder folder)
    {
        if (!folder.Exists)
        {
            Console.WriteLine($"Provided directory does not exist in filesystem.");
            yield break;
        }

        var stack = new Stack<FolderViewModel>();
        var root = new FolderViewModel { Folder = folder };
        stack.Push(root);

        SearchStarted?.Invoke(this, EventArgs.Empty);

        while (stack.Count > 0)
        {
            FolderViewModel currentNode = stack.Pop();
            var foldersByParent = currentNode.Folder.GetFolders();

            foreach (IFolder childFolder in foldersByParent)
            {
                FileSystemItemFoundEventArgs? nonFilteredResult = DirectoryFound?.Invoke(childFolder);
                if (nonFilteredResult?.AbortSearch ?? false)
                {
                    yield break;
                }

                bool excludeFromResult = nonFilteredResult?.ExcludeFromResult ?? false;
                if (excludeFromResult || !_folderPredicate(childFolder))
                {
                    continue;
                }

                FileSystemItemFoundEventArgs? filteredResult = FilteredDirectoryFound?.Invoke(childFolder);
                if (filteredResult?.AbortSearch ?? false)
                {
                    yield break;
                }

                if (filteredResult?.ExcludeFromResult ?? false)
                {
                    continue;
                }

                var childFolderVM = new FolderViewModel { Folder = folder };
                currentNode.Directories.Add(childFolderVM);
                stack.Push(childFolderVM);
            }

            IEnumerable<File> files = currentNode.Folder.GetFiles();
            var finalFileList = new List<File>();

            foreach (File file in files)
            {
                FileSystemItemFoundEventArgs? nonFilteredResult = FileFound?.Invoke(file);
                if (nonFilteredResult?.AbortSearch ?? false)
                {
                    yield break;
                }

                bool excludeFromResult = nonFilteredResult?.ExcludeFromResult ?? false;
                if (excludeFromResult || !_filePredicate(file))
                {
                    continue;
                }

                FileSystemItemFoundEventArgs? filteredResult = FilteredFileFound?.Invoke(file);
                if (filteredResult?.AbortSearch ?? false)
                {
                    yield break;
                }

                if (filteredResult?.ExcludeFromResult ?? false)
                {
                    continue;
                }

                finalFileList.Add(file);
            }

            currentNode.Files = finalFileList;

            yield return currentNode;
        }

        SearchFinished?.Invoke(this, EventArgs.Empty);
    }
}

public class FileSystemItemFoundEventArgs
{
    public bool AbortSearch { get; set; }

    public bool ExcludeFromResult { get; set; }
}
