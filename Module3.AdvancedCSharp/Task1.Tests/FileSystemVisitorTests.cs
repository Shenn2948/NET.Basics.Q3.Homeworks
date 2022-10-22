using FluentAssertions;
using Task1.Delegates;
using Task1.Interfaces;
using Task1.Tests.Models;
using File = Task1.Models.File;

namespace Task1.Tests;

public class FileSystemVisitorTests
{
    private readonly IFolder _folder = MockFolderFactory.GetTestFolder();

    [Fact]
    public void Returns_All_Files_And_Folders()
    {
        // Arrange
        var sut = new FileSystemVisitor();

        // Act
        var actual = sut.Traverse(_folder).ToList();

        // Assert
        Assert.True(actual.Count == 13);
    }

    [Fact]
    public void Returns_Filtered_Files()
    {
        // Arrange
        var sut = new FileSystemVisitor(filePredicate: file => file.Name != "New Text Document.txt");

        // Act
        var actual = sut.Traverse(_folder);

        // Assert
        actual.OfType<File>().Select(x => x.Name).Should().NotBeEquivalentTo("New Text Document.txt");
    }

    [Fact]
    public void Returns_Filtered_Directories()
    {
        // Arrange
        var sut = new FileSystemVisitor(folderPredicate: folder => folder.Name != "sub1");

        // Act
        var actual = sut.Traverse(_folder);

        // Assert
        actual.OfType<IFolder>().Select(x => x.Name).Should().NotBeEquivalentTo("sub1");
    }

    [Fact]
    public void SearchStarted_Fires_Before_Search()
    {
        // Arrange
        var sut = new FileSystemVisitor();

        // Act
        // Assert
        var evt = Assert.Raises<EventArgs>(h => sut.SearchStarted += h,
                                           h => sut.SearchStarted -= h,
                                           () => sut.Traverse(_folder).FirstOrDefault());

        Assert.NotNull(evt);
        Assert.Equal(sut, evt.Sender);
        Assert.Equal(EventArgs.Empty, evt.Arguments);
    }

    [Fact]
    public void SearchFinished_Fires_After_Search()
    {
        // Arrange
        var sut = new FileSystemVisitor();

        // Act

        // Assert
        var evt = Assert.Raises<EventArgs>(h => sut.SearchFinished += h,
                                           h => sut.SearchFinished -= h,
                                           () => sut.Traverse(_folder).LastOrDefault());

        Assert.NotNull(evt);
        Assert.Equal(sut, evt.Sender);
        Assert.Equal(EventArgs.Empty, evt.Arguments);
    }

    [Fact]
    public void FileFound_Fires_Before_Filtering()
    {
        // Arrange
        const string fileNameToFilter = "New Text Document.txt";
        var sut = new FileSystemVisitor(filePredicate: file => file.Name != fileNameToFilter);

        var wasEventRaised = false;
        var wasWithFilteredName = false;

        FileSystemItemFoundEventArgs OnFileFound(File file)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            if (!wasWithFilteredName)
            {
                wasWithFilteredName = file.Name == fileNameToFilter;
            }

            return FileSystemItemFoundEventArgs.Empty;
        }

        sut.FileFound += OnFileFound;

        // Act
        var actual = sut.Traverse(_folder).ToList();
        sut.FileFound -= OnFileFound;

        // Assert
        Assert.True(wasEventRaised && wasWithFilteredName);
    }

    [Fact]
    public void DirectoryFound_Fires_Before_Filtering()
    {
        // Arrange
        const string nameToFilter = "sub1";
        var sut = new FileSystemVisitor(folderPredicate: folder => folder.Name != "sub1");

        var wasEventRaised = false;
        var wasWithFilteredName = false;

        FileSystemItemFoundEventArgs OnDirectoryFound(IFolder folder)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            if (!wasWithFilteredName)
            {
                wasWithFilteredName = folder.Name == nameToFilter;
            }

            return FileSystemItemFoundEventArgs.Empty;
        }

        sut.DirectoryFound += OnDirectoryFound;

        // Act
        var actual = sut.Traverse(_folder).ToList();
        sut.DirectoryFound -= OnDirectoryFound;

        // Assert
        Assert.True(wasEventRaised && wasWithFilteredName);
    }

    [Fact]
    public void FilteredFileFound_Fires_After_Filtering()
    {
        // Arrange
        const string fileNameToFilter = "New Text Document.txt";
        var sut = new FileSystemVisitor(filePredicate: file => file.Name != fileNameToFilter);

        var wasEventRaised = false;
        var wasNotWithFilteredName = false;

        FileSystemItemFoundEventArgs OnFilteredFileFound(File file)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            if (!wasNotWithFilteredName)
            {
                wasNotWithFilteredName = file.Name != fileNameToFilter;
            }

            return FileSystemItemFoundEventArgs.Empty;
        }

        sut.FilteredFileFound += OnFilteredFileFound;

        // Act
        var actual = sut.Traverse(_folder).ToList();
        sut.FilteredFileFound -= OnFilteredFileFound;

        // Assert
        Assert.True(wasEventRaised && wasNotWithFilteredName);
    }

    [Fact]
    public void FilteredDirectoryFound_Fires_After_Filtering()
    {
        // Arrange
        const string nameToFilter = "sub1";
        var sut = new FileSystemVisitor(folderPredicate: folder => folder.Name != nameToFilter);

        var wasEventRaised = false;
        var wasNotWithFilteredName = false;

        FileSystemItemFoundEventArgs OnFilteredDirectoryFound(IFolder folder)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            if (!wasNotWithFilteredName)
            {
                wasNotWithFilteredName = folder.Name != nameToFilter;
            }

            return FileSystemItemFoundEventArgs.Empty;
        }

        sut.FilteredDirectoryFound += OnFilteredDirectoryFound;

        // Act
        var actual = sut.Traverse(_folder).ToList();
        sut.FilteredDirectoryFound -= OnFilteredDirectoryFound;

        // Assert
        Assert.True(wasEventRaised && wasNotWithFilteredName);
    }

    [Fact]
    public void FileFound_AbortsSearch()
    {
        // Arrange
        const string nameToFilter = "New Text Document.txt";
        var sut = new FileSystemVisitor();

        var wasEventRaised = false;

        FileSystemItemFoundEventArgs OnFileFound(File file)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            return file.Name == nameToFilter
                   ? new FileSystemItemFoundEventArgs { AbortSearch = true }
                   : FileSystemItemFoundEventArgs.Empty;
        }

        sut.FileFound += OnFileFound;

        // Act
        List<IFileSystemItem> actual = sut.Traverse(_folder).ToList();
        sut.FileFound -= OnFileFound;

        // Assert
        Assert.True(wasEventRaised);
        Assert.Single(actual, item => item.Name == nameToFilter);
    }

    [Fact]
    public void DirectoryFound_AbortsSearch()
    {
        // Arrange
        const string nameToFilter = "sub1";
        var sut = new FileSystemVisitor();

        var wasEventRaised = false;

        FileSystemItemFoundEventArgs OnDirectoryFound(IFolder folder)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            return folder.Name == nameToFilter
                   ? new FileSystemItemFoundEventArgs { AbortSearch = true }
                   : FileSystemItemFoundEventArgs.Empty;
        }

        sut.DirectoryFound += OnDirectoryFound;

        // Act
        List<IFileSystemItem> actual = sut.Traverse(_folder).ToList();
        sut.DirectoryFound -= OnDirectoryFound;

        // Assert
        Assert.True(wasEventRaised);
        Assert.Single(actual, item => item.Name == nameToFilter);
    }

    [Fact]
    public void FilteredFileFound_AbortsSearch()
    {
        // Arrange
        const string nameToFilter = "New Text Document.txt";
        var sut = new FileSystemVisitor(filePredicate: file => file.Name == nameToFilter);

        var wasEventRaised = false;

        FileSystemItemFoundEventArgs OnFilteredFileFound(File file)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            return file.Name == nameToFilter
                   ? new FileSystemItemFoundEventArgs { AbortSearch = true }
                   : FileSystemItemFoundEventArgs.Empty;
        }

        sut.FilteredFileFound += OnFilteredFileFound;

        // Act
        List<IFileSystemItem> actual = sut.Traverse(_folder).ToList();
        sut.FilteredFileFound -= OnFilteredFileFound;

        // Assert
        Assert.True(wasEventRaised);
        Assert.Single(actual, item => item.Name == nameToFilter);
    }

    [Fact]
    public void FilteredDirectoryFound_AbortsSearch()
    {
        // Arrange
        const string nameToFilter = "sub1";
        var sut = new FileSystemVisitor(folderPredicate: folder => folder.Name == nameToFilter);

        var wasEventRaised = false;

        FileSystemItemFoundEventArgs OnFilteredDirectoryFound(IFolder folder)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            return folder.Name == nameToFilter
                   ? new FileSystemItemFoundEventArgs { AbortSearch = true }
                   : FileSystemItemFoundEventArgs.Empty;
        }

        sut.FilteredDirectoryFound += OnFilteredDirectoryFound;

        // Act
        List<IFileSystemItem> actual = sut.Traverse(_folder).ToList();
        sut.FilteredDirectoryFound -= OnFilteredDirectoryFound;

        // Assert
        Assert.True(wasEventRaised);
        Assert.Single(actual, item => item.Name == nameToFilter);
    }

    [Fact]
    public void FileFound_ExcludesFromResult()
    {
        // Arrange
        const string nameToFilter = "New Text Document.txt";
        var sut = new FileSystemVisitor();

        var wasEventRaised = false;

        FileSystemItemFoundEventArgs OnFileFound(File file)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            return file.Name == nameToFilter
                   ? new FileSystemItemFoundEventArgs { ExcludeFromResult = true }
                   : FileSystemItemFoundEventArgs.Empty;
        }

        sut.FileFound += OnFileFound;

        // Act
        List<IFileSystemItem> actual = sut.Traverse(_folder).ToList();
        sut.FileFound -= OnFileFound;

        // Assert
        Assert.True(wasEventRaised);
        Assert.DoesNotContain(actual, item => item.Name == nameToFilter);
    }

    [Fact]
    public void DirectoryFound_ExcludesFromResult()
    {
        // Arrange
        const string nameToFilter = "sub1";
        var sut = new FileSystemVisitor();

        var wasEventRaised = false;

        FileSystemItemFoundEventArgs OnDirectoryFound(IFolder folder)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            return folder.Name == nameToFilter
                   ? new FileSystemItemFoundEventArgs { ExcludeFromResult = true }
                   : FileSystemItemFoundEventArgs.Empty;
        }

        sut.DirectoryFound += OnDirectoryFound;

        // Act
        List<IFileSystemItem> actual = sut.Traverse(_folder).ToList();
        sut.DirectoryFound -= OnDirectoryFound;

        // Assert
        Assert.True(wasEventRaised);
        Assert.DoesNotContain(actual, item => item.Name == nameToFilter);
    }

    [Fact]
    public void FilteredFileFound_ExcludesFromResult()
    {
        // Arrange
        const string nameToFilter = "New Text Document.txt";
        var sut = new FileSystemVisitor(filePredicate: file => file.Name == nameToFilter);

        var wasEventRaised = false;

        FileSystemItemFoundEventArgs OnFilteredFileFound(File file)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            return file.Name == nameToFilter
                   ? new FileSystemItemFoundEventArgs { ExcludeFromResult = true }
                   : FileSystemItemFoundEventArgs.Empty;
        }

        sut.FilteredFileFound += OnFilteredFileFound;

        // Act
        List<IFileSystemItem> actual = sut.Traverse(_folder).ToList();
        sut.FilteredFileFound -= OnFilteredFileFound;

        // Assert
        Assert.True(wasEventRaised);
        Assert.DoesNotContain(actual, item => item.Name == nameToFilter);
    }

    [Fact]
    public void FilteredDirectoryFound_ExcludesFromResult()
    {
        // Arrange
        const string nameToFilter = "sub1";
        var sut = new FileSystemVisitor(folderPredicate: folder => folder.Name == nameToFilter);

        var wasEventRaised = false;

        FileSystemItemFoundEventArgs OnFilteredDirectoryFound(IFolder folder)
        {
            if (!wasEventRaised)
            {
                wasEventRaised = true;
            }

            return folder.Name == nameToFilter
                   ? new FileSystemItemFoundEventArgs { ExcludeFromResult = true }
                   : FileSystemItemFoundEventArgs.Empty;
        }

        sut.FilteredDirectoryFound += OnFilteredDirectoryFound;

        // Act
        List<IFileSystemItem> actual = sut.Traverse(_folder).ToList();
        sut.FilteredDirectoryFound -= OnFilteredDirectoryFound;

        // Assert
        Assert.True(wasEventRaised);
        Assert.DoesNotContain(actual, item => item.Name == nameToFilter);
    }
}