using System;

namespace FileManager.ConsoleUI.Settings
{
    public class LeftWindowSettings : WindowSettingsBase
    {
        public override int WindowWidth => Console.WindowWidth / 2;
        public override int MaxEntriesLength => (Console.WindowHeight - 5) * 2;
        public override int LeftBorderPosition => 0;
        public override int RightBorderPosition => WindowWidth - 1;
        public override int CenterPosition => WindowWidth / 2;
        public override int PathStartPosition => 1;
        public override int PathMaxLength => WindowWidth - 2;
        public override int LeftHeaderPosition => WindowWidth / 4;
        public override int RightHeaderPosition => WindowWidth - WindowWidth / 4;
        public override int LeftEntriesStartPosition => 1;
        public override int RightEntriesStartPosition => CenterPosition + 1;
        public override int LeftEntryMaxLength => CenterPosition - 1;
        public override int RightEntryMaxLength => WindowWidth - CenterPosition - 2;
        public override int EntryInfoNameMaxLength => CenterPosition - LeftBorderPosition - 2;
        public override int FolderInfoStartPosition => PathStartPosition;
        public override int FolderInfoMaxLength => PathMaxLength;
    }
}