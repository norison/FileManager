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

            IDirectoryManager leftDirectoryManager = new DirectoryManager(@"C:\Program Files (x86)");
            IDirectoryManager rightDirectoryManager = new DirectoryManager(@"C:\Program Files (x86)");
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
        }
    }
}
