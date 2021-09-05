using System;

namespace FileManager.ConsoleUI.Settings
{
    public class RightWindowSettings : WindowSettingsBase
    {
        public override int MaxEntriesLength => (Console.WindowHeight - 5) * 2;
        public override int WindowWidth => Console.WindowWidth - Console.WindowWidth / 2;
        public override int LeftBorderPosition => Console.WindowWidth / 2;
        public override int RightBorderPosition => Console.WindowWidth - 1;
        public override int CenterPosition => Console.WindowWidth - 1 - Console.WindowWidth / 4;
        public override int PathStartPosition => Console.WindowWidth / 2 + 1;
        public override int PathMaxLength => Console.WindowWidth - Console.WindowWidth / 2 - 2;
        public override int LeftHeaderPosition => Console.WindowWidth / 2 + Console.WindowWidth / 8;
        public override int RightHeaderPosition => (Console.WindowWidth + Console.WindowWidth - Console.WindowWidth / 4) / 2;
        public override int LeftEntriesStartPosition => LeftBorderPosition + 1;
        public override int RightEntriesStartPosition => CenterPosition + 1;
        public override int LeftEntryMaxLength => CenterPosition - LeftBorderPosition - 1;
        public override int RightEntryMaxLength => Console.WindowWidth - CenterPosition - 2;
        public override int EntryInfoNameMaxLength => CenterPosition - LeftBorderPosition - 2;
        public override int FolderInfoStartPosition => PathStartPosition;
        public override int FolderInfoMaxLength => PathMaxLength;
    }
}