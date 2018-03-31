using System;

namespace AECBudgetKeygen
{
    internal static class ConsoleApp
    {

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "AEC Budget 3.0 Keygen";
            String title = @"
╦ ╦╦═╗╔═╗╔╦╗
║ ║╠╦╝║╣  ║ 
╚═╝╩╚═╚═╝ ╩ 
Keygenning Tutorial
by Vikram
--------------------
AEC Budget 3.0
";
            var kgn = new Keygen();
            Console.Write(title.PadLeft(10));
            Console.WriteLine("Key: {0}",kgn.getKey());
            Console.Write("Enter any key to exit.");
            Console.ReadKey();
        }
    }
}