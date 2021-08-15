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

            IDirectoryManager directoryManager = new DirectoryManager(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            IPainter leftWindowPainter = new ConsolePainter(new LeftWindowConsoleSettings());
            IPainter rightWindowPainter = new ConsolePainter(new RightWindowConsoleSettings());
            IFileManager fileManager = new FileManager(directoryManager, leftWindowPainter, rightWindowPainter);

            fileManager.Start();

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
