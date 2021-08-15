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
            IWindowSizeMonitoring windowSizeMonitoring = new ConsoleWindowSizeMonitoring(1000);
            IFileManager fileManager = new FileManager(windowSizeMonitoring, leftDirectoryManager, rightDirectoryManager, leftWindowPainter, rightWindowPainter);

            fileManager.Start();

            Console.ReadKey();
        }

        static void SetupConsole()
        {
            Console.CancelKeyPress += (sender, e) => { };
            Console.CursorVisible = false;
            Console.Title = "File Manager";
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.Clear();
        }
    }
}
