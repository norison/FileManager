namespace FileManager.SystemInformation
{
    public interface IDirectoryManager
    {
        string FolderPath { get; }
        bool IsRoot { get; }
        void ChangeDirectory(string path);
    }
}