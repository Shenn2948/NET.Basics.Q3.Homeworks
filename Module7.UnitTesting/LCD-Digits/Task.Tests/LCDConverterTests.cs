using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task.Tests;

public class LCDConverterTests
{
    private const string space = @"   
                                      
                                      ";
    private const string zero = @"._.
                                  |.|
                                  |_|";
    private const string one = @"...
                                 ..|
                                 ..|";
    private const string two = @"._.
                                 ._|
                                 |_.";
    private const string three = @"._.
                                   ._|
                                   ._|";
    private const string four = @"...
                                  |_|
                                  ..|";
    private const string five = @"._.
                                  |_.
                                  ._|";
    private const string six = @"._.
                                 |_.
                                 |_|";
    private const string seven = @"._.
                                   ..|
                                   ..|";
    private const string eight = @"._.
                                   |_|
                                   |_|";
    private const string nine = @"._.
                                  |_|
                                  ._|";

    [Theory]
    [InlineData(zero, 0)]
    [InlineData(one, 1)]
    [InlineData(two, 2)]
    [InlineData(three, 3)]
    [InlineData(four, 4)]
    [InlineData(five, 5)]
    [InlineData(six, 6)]
    [InlineData(seven, 7)]
    [InlineData(eight, 8)]
    [InlineData(nine, 9)]
    [InlineData($"{zero}{space}{one}{space}{two}", 012)]
    [InlineData($"{one}{space}{two}", 12)]
    [InlineData($"{two}{space}{five}{space}{six}{space}{four}", 2564)]
    public void Convert_Returns_Correct_String_Result(string expected, int input)
    {
        string actual = LCDConverter.Convert(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-1)]
    public void Convert_Throws_ArgumentException_When_Input_Is_Less_Than_Zero(int input)
    {
        Assert.Throws<ArgumentException>(() => LCDConverter.Convert(input));
    }
}