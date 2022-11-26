namespace Task.Tests;

public class FizzBuzzTests
{
    [Fact]
    public void Returns_Numbers_From_1_to_100()
    {
        // Arrange
        // Act
        string[] sut = FizzBuzz.Generate();

        // Assert
        foreach (string item in sut)
        {
            bool isNumber = int.TryParse(item, out int result);

            if (isNumber)
            {
                Assert.InRange(result, 1, 100);
            }
        }
    }

    [Fact]
    public void Returns_Fizz_InsteadOfNumber_Which_Is_Divisible_By_3()
    {
        // Arrange
        string fizz = "Fizz";
        var indexesWithFizz = new[] { 2, 5 };

        // Act
        string[] actual = FizzBuzz.Generate();

        // Assert
        foreach (int index in indexesWithFizz)
        {
            Assert.Equal(actual[index], fizz);
        }
    }

    [Fact]
    public void Returns_Buzz_InsteadOfNumber_Which_Is_Divisible_By_5()
    {
        // Arrange
        string fizz = "Buzz";
        var indexesWithBuzz = new[] { 4, 9 };

        // Act
        string[] actual = FizzBuzz.Generate();

        // Assert
        foreach (int index in indexesWithBuzz)
        {
            Assert.Equal(actual[index], fizz);
        }
    }

    [Fact]
    public void Returns_FizzBuzz_InsteadOfNumber_Which_Is_Divisible_By_Both_3_And_5()
    {
        // Arrange
        string fizzBuzz = "FizzBuzz";
        var indexesWithFizzBuzz = new[] { 14 };

        // Act
        string[] actual = FizzBuzz.Generate();

        // Assert
        foreach (int index in indexesWithFizzBuzz)
        {
            Assert.Equal(actual[index], fizzBuzz);
        }
    }

    [Theory]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(9)]
    [InlineData(18)]
    public void GenerateForNumber_Returns_Fizz_InsteadOfNumber_Which_Is_Divisible_By_3(int number)
    {
        // Arrange
        string expected = "Fizz";

        // Act
        string actual = FizzBuzz.GenearateForNumber(number);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(25)]
    public void GenerateForNumber_Returns_Buzz_InsteadOfNumber_Which_Is_Divisible_By_5(int number)
    {
        // Arrange
        string expected = "Buzz";

        // Act
        string actual = FizzBuzz.GenearateForNumber(number);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(15)]
    [InlineData(30)]
    [InlineData(45)]
    [InlineData(60)]
    public void GenerateForNumber_Returns_FizzBuzz_When_Divisible_By_Both_3_And_5(int number)
    {
        // Arrange
        string expected = "FizzBuzz";

        // Act
        string actual = FizzBuzz.GenearateForNumber(number);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(34)]
    public void GenerateForNumber_Returns_Number_When_Not_Divisible_By_3_And_5(int number)
    {
        // Arrange
        string expected = number.ToString();

        // Act
        string actual = FizzBuzz.GenearateForNumber(number);

        // Assert
        Assert.Equal(expected, actual);
    }
}