using System;
using System.IO;
using FileManager.Models;

namespace FileManager.ConsoleUI.Color
{
    public class Colorant : IColorant
    {
        public void SetBorderColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public void SetPathColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void SetHeaderColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public void SetEntryInfoColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public void SetWindowColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void SetHighlightedEntryColor()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void SetColorByEntryType(EntryInfo entryInfo)
        {
            var extension = entryInfo.Extenstion;
            var attributes = entryInfo.Attributes;

            Console.BackgroundColor = ConsoleColor.DarkBlue;

            if (extension == ".exe" && (attributes & FileAttributes.Directory) != FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (extension == ".cmd" && (attributes & FileAttributes.Directory) != FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (extension == ".pak" && (attributes & FileAttributes.Directory) != FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.Magenta;
            else if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                Console.ForegroundColor = ConsoleColor.Blue;
            else if ((attributes & FileAttributes.System) == FileAttributes.System)
                Console.ForegroundColor = ConsoleColor.Blue;
            else if ((attributes & FileAttributes.Normal) == FileAttributes.Normal)
                Console.ForegroundColor = ConsoleColor.Cyan;
            else if ((attributes & FileAttributes.Archive) == FileAttributes.Archive)
                Console.ForegroundColor = ConsoleColor.Cyan;
            else if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
