using System;

namespace FileManager.ConsoleUI.Settings
{
    public class LeftWindowConsoleSettings : IWindowConsoleSettings
    {
        public int LeftBorderPosition => 0;
        public int RightBorderPosition => Console.WindowWidth / 2 - 1;
        public int CenterPosition => Console.WindowWidth / 4;
        public int PathStartPosition => 1;
        public int PathMaxLength => Console.WindowWidth / 2 - 2;
    }
}