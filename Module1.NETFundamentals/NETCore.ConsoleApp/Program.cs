using System;
using NETStandard.Lib;

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

            Console.WriteLine(Utils.GetOutputMessage(args[0]));
        }
    }
}
