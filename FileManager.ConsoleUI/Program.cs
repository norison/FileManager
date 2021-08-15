using System;

namespace FileManager.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var consolePainter = new ConsolePainter();
            consolePainter.DrawWindow();
            Console.ReadKey();
        }
    }
}
