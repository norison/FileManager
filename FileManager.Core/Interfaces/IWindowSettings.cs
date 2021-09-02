namespace FileManager.Core.Interfaces
{
    public interface IWindowSettings
    {
        int WindowHeight { get; }
        int EntryInfosHeight { get; }
        int EntryMaxHeight { get; }
        int EntryStartHeight { get; }
        int EntriesLength { get; }
        int EntryInfoLength { get; }
        int BottomBorderHeight { get; }
        int MaxEntriesLength { get; }
        int WindowWidth { get; }
        int LeftBorderPosition { get; }
        int RightBorderPosition { get; }
        int CenterPosition { get; }
        int PathStartPosition { get; }
        int PathMaxLength { get; }
        int LeftHeaderPosition { get; }
        int RightHeaderPosition { get; }
        int LeftEntriesStartPosition { get; }
        int RightEntriesStartPosition { get; }
        int LeftEntryMaxLength { get; }
        int RightEntryMaxLength { get; }
        int EntryInfoNameMaxLength { get; }
    }
}