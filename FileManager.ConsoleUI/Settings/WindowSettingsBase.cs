using System;
using FileManager.Core.Interfaces;

namespace FileManager.ConsoleUI.Settings
{
    public abstract class WindowSettingsBase : IWindowSettings
    {
        public string HeaderValue => "Name";
        public int WindowHeight => Console.WindowHeight;
        public int EntryInfosHeight => WindowHeight - 2;
        public int EntryMaxHeight => WindowHeight - 3;
        public int EntryStartHeight => 2;
        public int EntryInfoLength => RightBorderPosition - LeftBorderPosition - EntryStartHeight;
        public int BottomBorderHeight => WindowHeight - 1;
        public int EntriesLength => EntryMaxHeight - EntryStartHeight;

        public abstract int MaxEntriesLength { get; }
        public abstract int WindowWidth { get; }
        public abstract int LeftBorderPosition { get; }
        public abstract int RightBorderPosition { get; }
        public abstract int CenterPosition { get; }
        public abstract int PathStartPosition { get; }
        public abstract int PathMaxLength { get; }
        public abstract int LeftHeaderPosition { get; }
        public abstract int RightHeaderPosition { get; }
        public abstract int LeftEntriesStartPosition { get; }
        public abstract int RightEntriesStartPosition { get; }
        public abstract int LeftEntryMaxLength { get; }
        public abstract int RightEntryMaxLength { get; }
        public abstract int EntryInfoNameMaxLength { get; }
    }
}