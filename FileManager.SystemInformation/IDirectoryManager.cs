namespace FileManager.SystemInformation
{
    public interface IDirectoryManager
    {
        string Path { get; }
        bool IsRoot { get; }
        void ChangeDirectory(string path);
    }
}