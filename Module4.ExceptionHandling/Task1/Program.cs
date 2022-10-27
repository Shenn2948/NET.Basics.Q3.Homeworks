using System;

namespace Task1
{
    internal class Program
    {
        private static void Main()
        {
            string line;

            Console.WriteLine("---");
            Console.WriteLine("Enter a line of text (press CTRL+C to exit).");
            Console.WriteLine("---");

            do
            {
                Console.Write("> ");

                line = Console.ReadLine();

                try
                {
                    PrintFirstChar(line);
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }

            } while (line != null);
        }

        private static void PrintFirstChar(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("The input could not be an empty string.", nameof(input));
            }

            Console.WriteLine($"First character of the entered line: {input[0]}");
        }
    }
}