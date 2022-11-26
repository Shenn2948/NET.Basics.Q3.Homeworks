namespace Task;

public static class OddEven
{
    public static string GenerateForNumber(int number) => number switch
    {
        < 0 or > 100 => throw new ArgumentOutOfRangeException(nameof(number)),
        int when IsPrime(number) => number.ToString(),
        int when number % 2 == 0 => "Even",
        _ => "Odd",
    };

    public static string[] Generate()
    {
        var arr = new string[100];

        for (int i = 0; i < arr.Length; i++)
        {
            int number = i + 1;
            arr[i] = GenerateForNumber(number);
        }

        return arr;
    }

    private static bool IsPrime(int num)
    {
        if (num < 0) return false;

        for (int i = 2, s = (int)Math.Sqrt(num); i <= s; i++)
        {
            if (num % i == 0) return false;
        }

        return num > 1;
    }
}
