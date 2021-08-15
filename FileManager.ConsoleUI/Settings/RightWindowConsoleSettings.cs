﻿using System;

namespace FileManager.ConsoleUI.Settings
{
    public class RightWindowConsoleSettings : IWindowConsoleSettings
    {
        public int LeftBorderPosition => Console.WindowWidth / 2;
        public int RightBorderPosition => Console.WindowWidth - 1;
        public int CenterPosition => Console.WindowWidth - 1 - Console.WindowWidth / 4;
        public int PathStartPosition => Console.WindowWidth / 2 + 1;
        public int PathMaxLength => Console.WindowWidth - Console.WindowWidth / 2 - 2;
    }
}