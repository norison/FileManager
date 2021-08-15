namespace FileManager.ConsoleUI.Settings
{
    public interface IWindowConsoleSettings
    {
        int LeftBorderPosition { get; }
        int RightBorderPosition { get; }
        int CenterPosition { get; }
        int PathStartPosition { get; }
        int PathMaxLength { get; }
    }
}