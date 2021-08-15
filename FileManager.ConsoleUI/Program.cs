using System;
using FileManager.SystemInformation;

namespace FileManager.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IDirectoryManager directoryManager = new DirectoryManager(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            IPainter painter = new ConsolePainter();
            IFileManager fileManager = new FileManager(directoryManager, painter);

            fileManager.Start();

            Console.ReadKey();
        }
    }
}
