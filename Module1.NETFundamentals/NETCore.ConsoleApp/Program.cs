using System;

namespace NETCore.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("please, provide a username");
                return;
            }

            Console.WriteLine(GetOutputMessage(args[0]));
        }

        private static string GetOutputMessage(string userName)
        {
            return $"Hello, {userName}";
        }
    }
}
