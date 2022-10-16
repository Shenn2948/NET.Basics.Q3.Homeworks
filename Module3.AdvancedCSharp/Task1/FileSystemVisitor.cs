namespace Task1;

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

        while (stack.Count > 0)
        {
            FolderViewModel currentNode = stack.Pop();
            IEnumerable<FolderViewModel> foldersByParent = currentNode.Folder.GetFolders()
                                                                             .Where(_folderPredicate)
                                                                             .Select(folder => new FolderViewModel { Folder = folder });

            foreach (FolderViewModel childFolder in foldersByParent)
            {
                currentNode.Directories.Add(childFolder);
                stack.Push(childFolder);
            }

            currentNode.Files = currentNode.Folder.GetFiles()
                                                  .Where(_filePredicate)
                                                  .ToList();

            yield return currentNode;
        }
    }
}
