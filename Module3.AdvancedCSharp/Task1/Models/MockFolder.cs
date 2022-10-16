namespace Task1;

public class MockFolder : IFolder
{
    public MockFolder(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public bool Exists => true;

    public MockFolder[] SubFolders { get; set; } = Array.Empty<MockFolder>();

    public File[] Files { get; set; } = Array.Empty<File>();

    public IEnumerable<File> GetFiles()
    {
        return Files;
    }

    public IEnumerable<IFolder> GetFolders()
    {
        return SubFolders;
    }
}