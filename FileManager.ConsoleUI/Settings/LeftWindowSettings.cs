using System;
using FileManager.ConsoleUI.Interfaces;

namespace FileManager.ConsoleUI.Settings
{
    public class LeftWindowSettings : IWindowSettings
    {
        public int WindowWidth => Console.WindowWidth / 2;
        public int LeftBorderPosition => 0;
        public int RightBorderPosition => WindowWidth - 1;
        public int CenterPosition => WindowWidth / 2;
        public int PathStartPosition => 1;
        public int PathMaxLength => WindowWidth - 2;
        public int LeftHeaderPosition => WindowWidth / 4;
        public int RightHeaderPosition => WindowWidth - WindowWidth / 4;
        public int LeftEntriesStartPosition => 1;
        public int RightEntriesStartPosition => CenterPosition + 1;
        public int LeftEntryMaxLength => CenterPosition - 2;
        public int RightEntryMaxLength => WindowWidth - CenterPosition - 3;
    }
}