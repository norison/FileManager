namespace FileManager.ConsoleUI.Settings
{
    public interface IWindowConsoleSettings
    {
        int WindowWidth { get; }
        int WindowHeight { get; }
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