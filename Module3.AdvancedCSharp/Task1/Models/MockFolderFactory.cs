namespace Task1;

public class MockFolderFactory
{
    public static MockFolder GetTestFolder()
    {
        return new MockFolder("temp")
        {
            Files = new[] { new File { Name = "nlog-test.txt" }, new File { Name = "New Text Document.txt" } },
            SubFolders = new[] { new MockFolder("test1") {
                Files = new[] { new File { Name = "New Text Document.txt" } },
                SubFolders = new[] { new MockFolder("sub1") {
                    Files = new[] { new File { Name = "New Text Document.txt" }, new File { Name = "New Text Document - Copy.txt" } }
                }, new MockFolder("sub2") {
                    Files = new[] { new File { Name = "New Text Document.txt" }, new File { Name = "New Text Document - Copy.txt" } }
                } }
            }, new MockFolder("test2") {
                Files = new[] { new File { Name = "New Text Document.txt" } }
            } }
        };
    }
}
