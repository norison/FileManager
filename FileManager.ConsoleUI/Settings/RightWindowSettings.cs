using System;
using FileManager.ConsoleUI.Interfaces;

namespace FileManager.ConsoleUI.Settings
{
    public class RightWindowSettings : IWindowSettings
    {
        public int MaxEntriesLength => (Console.WindowHeight - 5) * 2;
        public int WindowWidth => Console.WindowWidth - Console.WindowWidth / 2;
        public int LeftBorderPosition => Console.WindowWidth / 2;
        public int RightBorderPosition => Console.WindowWidth - 1;
        public int CenterPosition => Console.WindowWidth - 1 - Console.WindowWidth / 4;
        public int PathStartPosition => Console.WindowWidth / 2 + 1;
        public int PathMaxLength => Console.WindowWidth - Console.WindowWidth / 2 - 2;
        public int LeftHeaderPosition => Console.WindowWidth / 2 + Console.WindowWidth / 8;
        public int RightHeaderPosition => (Console.WindowWidth + Console.WindowWidth - Console.WindowWidth / 4) / 2;
        public int LeftEntriesStartPosition => LeftBorderPosition + 1;
        public int RightEntriesStartPosition => CenterPosition + 1;
        public int LeftEntryMaxLength => CenterPosition - LeftBorderPosition - 2;
        public int RightEntryMaxLength => Console.WindowWidth - CenterPosition - 3;
    }
}