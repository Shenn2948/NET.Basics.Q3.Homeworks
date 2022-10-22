using File = Task1.Models.File;

namespace Task1.Tests.Models;

public class MockFolderFactory
{
    public static MockFolder GetTestFolder()
    {
        var root = new MockFolder("temp");
        root.Files.AddRange(new[] { new File("nlog-test.txt", root), new File("New Text Document.txt", root) });

        var test1 = new MockFolder("test1");
        test1.Files.Add(new File("New Text Document.txt", test1));

        var sub1 = new MockFolder("sub1");
        sub1.Files.AddRange(new []{new File("New Text Document.txt", sub1), new File("New Text Document - Copy.txt", sub1)});
        var sub2 = new MockFolder("sub2");
        sub2.Files.AddRange(new []{new File("New Text Document.txt", sub2), new File("New Text Document - Copy.txt", sub2)});

        test1.SubFolders.AddRange(new []{sub1, sub2});

        var test2 = new MockFolder("test2");
        test2.Files.Add(new File("New Text Document.txt", test2));

        root.SubFolders.AddRange(new []{test1, test2});

        return root;
    }
}