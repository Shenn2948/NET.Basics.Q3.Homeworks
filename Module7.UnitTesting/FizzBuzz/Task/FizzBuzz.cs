namespace Task;

public static class FizzBuzz
{
    public static string GenearateForNumber(int number) => number switch
    {
        int x when (x % 3 == 0 && x % 5 == 0) => "FizzBuzz",
        int when number % 3 == 0 => "Fizz",
        int when number % 5 == 0 => "Buzz",
        _ => number.ToString(),
    };

    public static string[] Generate()
    {
        var arr = new string[100];

        for (int i = 0; i < arr.Length; i++)
        {
            int number = i + 1;
            arr[i] = GenearateForNumber(number);
        }

        return arr;
    }
}
