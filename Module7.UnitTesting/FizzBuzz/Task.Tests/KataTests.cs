namespace Task.Tests;

public class KataTests
{
    [Fact]
    public void Returns_Numbers_From_1_to_100()
    {
        // Arrange
        string[] expected = Enumerable.Range(1, 100).Select(num => num.ToString()).ToArray();

        // Act
        string[] actual = Kata.FizzBuzz();

        // Assert
        Assert.Equal(expected, actual);
    }
}