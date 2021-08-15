using System;
using FileManager.ConsoleUI.Settings;
using FileManager.Core;
using FileManager.SystemInformation;

namespace FileManager.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupConsole();

            IDirectoryManager leftDirectoryManager = new DirectoryManager("D:\\Development\\Work\\Gateways_Generic\\AbbasDominusMillennium\\Gateway\\Configuration");
            IDirectoryManager rightDirectoryManager = new DirectoryManager("D:\\Development\\Work\\Gateways_Generic\\AbbasDominusMillennium\\Gateway\\Configuration");
            IPainter leftWindowPainter = new ConsolePainter(new LeftWindowConsoleSettings());
            IPainter rightWindowPainter = new ConsolePainter(new RightWindowConsoleSettings());
            IFileManager fileManager = new FileManager(leftDirectoryManager, rightDirectoryManager, leftWindowPainter, rightWindowPainter);

            fileManager.Start();
            fileManager.Dispose();

            Console.ReadKey();
        }

        static void SetupConsole()
        {
            Console.CancelKeyPress += (sender, e) => { };
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
    }
}
