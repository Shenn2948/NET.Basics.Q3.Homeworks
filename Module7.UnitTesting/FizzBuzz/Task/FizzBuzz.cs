namespace Task;

public static class FizzBuzz
{
    public static string[] Generate()
    {
        var arr = new string[100];

        for (int i = 0; i < arr.Length; i++)
        {
            int number = i + 1;

            string result = number switch
            {
                int when number % 3 == 0 => "Fizz",
                int when number % 5 == 0 => "Buzz",
                _ => number.ToString(),
            };

            arr[i] = result;
        }

        return arr;
    }
}
