using System;
using FileManager.ConsoleUI.Interfaces;
using FileManager.ConsoleUI.Settings;
using FileManager.SystemInformation;

namespace FileManager.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupConsole();

            IDirectoryManager leftDirectoryManager = new DirectoryManager(@"C:\Program Files (x86)");
            IDirectoryManager rightDirectoryManager = new DirectoryManager(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            IPainter leftWindowPainter = new Painter(new LeftWindowSettings());
            IPainter rightWindowPainter = new Painter(new RightWindowSettings());

            IWindowSizeMonitoring windowSizeMonitoring = new WindowSizeMonitoring(1000);

            IWindowManager leftWindowManager = new WindowManager(windowSizeMonitoring, leftDirectoryManager, leftWindowPainter);
            IWindowManager rightWindowManager = new WindowManager(windowSizeMonitoring, rightDirectoryManager, rightWindowPainter);

            IFileManager fileManager = new FileManager(leftWindowManager, rightWindowManager);

            windowSizeMonitoring.Start();
            fileManager.Start();

            fileManager.Dispose();
            windowSizeMonitoring.Dispose();
        }

        static void SetupConsole()
        {
            Console.CancelKeyPress += (sender, e) => { };
            Console.CursorVisible = false;
            Console.Title = "File Manager";
        }
    }
}
