using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task.Tests;

public class OddEvenTests
{
    [Fact]
    public void Returns_Numbers_From_1_to_100()
    {
        // Arrange
        // Act
        string[] sut = OddEven.Generate();

        // Assert
        foreach (string item in sut)
        {
            bool isNumber = int.TryParse(item, out int actual);

            if (isNumber)
            {
                Assert.InRange(actual, 1, 100);
            }
        }
    }

    [Fact]
    public void Returns_Odd_InsteadOfNumber_Which_Is_Divisible_By_2()
    {
        // Arrange
        string expected = "Even";
        var indexesWithOdd = new[] { 3, 5 };

        // Act
        string[] actual = OddEven.Generate();

        // Assert
        foreach (int index in indexesWithOdd)
        {
            Assert.Equal(expected, actual[index]);
        }
    }

    [Fact]
    public void Returns_Even_InsteadOfNumber_Which_Is_Not_Divisible_By_2_And_Itself()
    {
        // Arrange
        string expected = "Odd";
        var indexesWithOdd = new[] { 8, 14 };

        // Act
        string[] actual = OddEven.Generate();

        // Assert
        foreach (int index in indexesWithOdd)
        {
            Assert.Equal(expected, actual[index]);
        }
    }

    [Fact]
    public void Returns_Number_When_Number_Is_Prime()
    {
        // Arrange
        var indexesWithPrime = new[] { 1, 2 };

        // Act
        string[] actual = OddEven.Generate();

        // Assert
        foreach (int index in indexesWithPrime)
        {
            var expected = (index + 1).ToString();
            Assert.Equal(expected, actual[index]);
        }
    }

    [Theory]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(10)]
    public void GenerateForNumber_Returns_Even_When_Number_Is_Divisible_By_2(int number)
    {
        // Arrange
        string expected = "Even";

        // Act
        string actual = OddEven.GenerateForNumber(number);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(9)]
    [InlineData(15)]
    [InlineData(21)]
    public void GenerateForNumber_Returns_Odd_When_Number_Is_Not_Divisible_By_2_And_Itself(int number)
    {
        // Arrange
        string expected = "Odd";

        // Act
        string actual = OddEven.GenerateForNumber(number);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(11)]
    [InlineData(13)]
    public void GenerateForNumber_Returns_Number_When_Number_Is_Prime(int number)
    {
        // Arrange
        string expected = number.ToString();

        // Act
        string actual = OddEven.GenerateForNumber(number);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void GenerateForNumber_Throws_ArgumentOutOfRangeException_When_Number_Is_Less_Than_1_And_Greater_Than_100(int number)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => OddEven.GenerateForNumber(number));
    }
}