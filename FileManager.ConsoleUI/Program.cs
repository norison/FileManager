using System;
using FileManager.ConsoleUI.Settings;
using FileManager.Core;
using FileManager.Core.Interfaces;
using FileManager.SystemInformation;

namespace FileManager.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupConsole();

            IFileSystem leftFileSystem = new FileSystem(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            IFileSystem rightFileSystem = new FileSystem(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            IWindowPresenter leftWindowPresenter = new ConsoleWindowPresenter(new LeftWindowSettings());
            IWindowPresenter rightWindowPresenter = new ConsoleWindowPresenter(new RightWindowSettings());

            IWindowSizeMonitoring windowSizeMonitoring = new WindowSizeMonitoring(1000);

            IWindow leftWindow = new Window(windowSizeMonitoring, leftFileSystem, leftWindowPresenter);
            IWindow rightWindow = new Window(windowSizeMonitoring, rightFileSystem, rightWindowPresenter);

            ICommandManager commandManager = new ConsoleCommandManager();

            IFileManager fileManager = new Core.FileManager(leftWindow, rightWindow, commandManager);

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
