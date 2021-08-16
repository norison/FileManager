using System.Collections.Generic;

namespace FileManager.SystemInformation
{
    public interface IDirectoryManager
    {
        string Path { get; }
        bool IsRoot { get; }
        IList<EntryInfo> GetEntryInfos();
        void GoToParent();
        void ChangeDirectory(string path);
    }
}