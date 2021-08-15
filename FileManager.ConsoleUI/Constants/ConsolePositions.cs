using System;

namespace FileManager.ConsoleUI.Constants
{
    public static class ConsolePositions
    {
        public static readonly int LeftWindowLeftBorder = 0;
        public static readonly int LeftWindowRightBorder = Console.WindowWidth / 2 - 1;
        public static readonly int LeftWindowCenter = Console.WindowWidth / 4;
        public static readonly int RightWindowLeftBorder = Console.WindowWidth / 2;
        public static readonly int RightWindowRightBorder = Console.WindowWidth - 1;
        public static readonly int RightWindowCenter = Console.WindowWidth - 1 - Console.WindowWidth / 4;
    }
}