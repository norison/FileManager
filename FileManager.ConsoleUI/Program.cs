using System;
using System.Collections.Generic;
using FileManager.ConsoleUI.Color;
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

            IColorant colorant = new Colorant();

            IWindowPresenter leftWindowPresenter = new ConsoleWindowPresenter(new LeftWindowSettings(), colorant);
            IWindowPresenter rightWindowPresenter = new ConsoleWindowPresenter(new RightWindowSettings(), colorant);

            IWindowSizeMonitoring windowSizeMonitoring = new WindowSizeMonitoring(1000);

            IWindow leftWindow = new Window(windowSizeMonitoring, leftFileSystem, leftWindowPresenter);
            IWindow rightWindow = new Window(windowSizeMonitoring, rightFileSystem, rightWindowPresenter);

            ICommandManager commandManager = new ConsoleCommandManager();

            IList<IWindow> windows = new List<IWindow> { leftWindow, rightWindow };

            IFileManager fileManager = new Core.FileManager(windows, commandManager);

            windowSizeMonitoring.Start();
            fileManager.Run();

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
