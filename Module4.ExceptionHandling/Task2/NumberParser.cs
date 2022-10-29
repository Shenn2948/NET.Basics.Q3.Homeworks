using System;
using System.Linq;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            stringValue = stringValue.Trim(' ');

            Validate(stringValue);

            bool isNegative = stringValue.StartsWith('-');
            stringValue = stringValue.Trim('-', '+');

            double result = 0;
            const int radix = 10;
            int power = stringValue.Length - 1;

            foreach (char digit in stringValue)
            {
                switch (digit)
                {
                    case '0':
                        result += 0;
                        break;
                    case '1':
                        result += 1 * Math.Pow(radix, power);
                        break;
                    case '2':
                        result += 2 * Math.Pow(radix, power);
                        break;
                    case '3':
                        result += 3 * Math.Pow(radix, power);
                        break;
                    case '4':
                        result += 4 * Math.Pow(radix, power);
                        break;
                    case '5':
                        result += 5 * Math.Pow(radix, power);
                        break;
                    case '6':
                        result += 6 * Math.Pow(radix, power);
                        break;
                    case '7':
                        result += 7 * Math.Pow(radix, power);
                        break;
                    case '8':
                        result += 8 * Math.Pow(radix, power);
                        break;
                    case '9':
                        result += 9 * Math.Pow(radix, power);
                        break;
                }

                power--;
            }

            result = isNegative ? -result : result;

            if (result < int.MinValue || result > int.MaxValue)
            {
                throw new OverflowException($"Parse number is out of int32 range ({int.MinValue} < x < {int.MaxValue})");
            }

            return (int)result;
        }

        private static void Validate(string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new FormatException("Source string can't be empty or consist exclusively of white-space characters");
            }

            char firstChar = stringValue[0];
            bool validFirstChar = firstChar == '-' || firstChar == '+' || char.IsDigit(firstChar);

            if (!validFirstChar || stringValue.Skip(1).Any(c => !char.IsDigit(c)))
            {
                throw new FormatException("Source string contains invalid symbols. " +
                                          "(only digits from 0 to 9 and signs at start '+' and '-' are allowed");
            }

            stringValue = stringValue.Trim('-', '+');
            if (stringValue.Length > 10)
            {
                throw new OverflowException($"Parse number is out of int32 range ({int.MinValue} < x < {int.MaxValue})");
            }
        }
    }
}