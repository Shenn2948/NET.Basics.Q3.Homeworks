namespace Task;

public static class Kata
{
    public static string[] FizzBuzz()
    {
        var arr = new string[100];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = (i + 1).ToString();
        }

        return arr;
    }
}
