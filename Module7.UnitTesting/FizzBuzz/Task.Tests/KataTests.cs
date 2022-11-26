namespace Task.Tests;

public class KataTests
{
    [Fact]
    public void Returns_Numbers_From_1_to_100()
    {
        string[] actual = Kata.FizzBuzz();

        foreach (string item in actual)
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
        var expected = new string[] { "1", "2", "Fizz", "4", "5", "Fizz" };

        string[] actual = Kata.FizzBuzz()[0..6];

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Returns_Buzz_InsteadOfNumber_Which_Is_Divisible_By_5()
    {
    }

    [Fact]
    public void Returns_FizzBuzz_InsteadOfNumber_Which_Is_Divisible_By_Both_3_And_5()
    {
    }
}