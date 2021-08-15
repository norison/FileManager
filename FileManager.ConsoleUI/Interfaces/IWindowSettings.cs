namespace FileManager.ConsoleUI.Interfaces
{
    public interface IWindowSettings
    {
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
    }
}