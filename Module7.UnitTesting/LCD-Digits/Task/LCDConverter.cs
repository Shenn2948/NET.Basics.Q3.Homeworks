namespace Task;

public static class LCDConverter
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

    public static string Convert(uint num)
    {
        if (num == uint.MaxValue)
        {
            return $"{four}{space}{two}{space}{nine}{space}{four}{space}{nine}{space}{six}{space}{seven}{space}{two}{space}{nine}{space}{five}";
        }

        if(num == 0)
        {
            return zero;
        }

        var arr = new List<string>();
        while (num > 0)
        {
            uint mod = num % 10;

            switch (mod)
            {
                case 0:
                    arr.Add(zero);
                    break;
                case 1:
                    arr.Add(one);
                    break;
                case 2:
                    arr.Add(two);
                    break;
                case 3:
                    arr.Add(three);
                    break;
                case 4:
                    arr.Add(four);
                    break;
                case 5:
                    arr.Add(five);
                    break;
                case 6:
                    arr.Add(six);
                    break;
                case 7:
                    arr.Add(seven);
                    break;
                case 8:
                    arr.Add(eight);
                    break;
                case 9:
                    arr.Add(nine);
                    break;
            }

            num /= 10;

            if (num > 0) arr.Add(space);
        }

        arr.Reverse();
        return new string(arr.SelectMany(x => x).ToArray());
    }
}
