namespace Task1;

public class FileSystemVisitor
{
    private readonly Func<DirectoryInfo, bool>? _folderPredicate;
    private readonly Func<FileInfo, bool>? _filePredicate;

    public FileSystemVisitor(Func<DirectoryInfo, bool>? folderPredicate = null,
                             Func<FileInfo, bool>? filePredicate = null)
    {
        _folderPredicate = folderPredicate;
        _filePredicate = filePredicate;
    }

    public IEnumerable<Folder> Traverse(string path)
    {
        var directory = new DirectoryInfo(path);
        if (!directory.Exists)
        {
            Console.WriteLine($"Directory with path: {path} does not exist.");
            yield break;
        }

        var stack = new Stack<Folder>();
        var root = Folder.MapFromDirectoryInfo(directory);
        stack.Push(root);

        while (stack.Count > 0)
        {
            var currentNode = stack.Pop();
            var foldersByParent = currentNode.DirectoryInfo.GetDirectories()
                                                           .Where(_folderPredicate)
                                                           .Select(Folder.MapFromDirectoryInfo);

            foreach (var childFolder in foldersByParent)
            {
                currentNode.Directories.Add(childFolder);
                stack.Push(childFolder);
            }

            currentNode.Files = currentNode.DirectoryInfo.GetFiles()
                                                         .Where(_filePredicate)
                                                         .ToList();

            yield return currentNode;
        }
    }
}
