using System.Collections.Generic;

namespace FileManager.SystemInformation
{
    public interface IDirectoryManager
    {
        string Path { get; }
        bool IsRoot { get; }
        IList<EntryInfo> FileInfos { get; }
        void ChangeDirectory(string path);
    }
}