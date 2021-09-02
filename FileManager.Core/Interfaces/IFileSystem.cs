using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Core.Interfaces
{
    public interface IFileSystem
    {
        string Path { get; }
        bool IsRoot { get; }
        IList<EntryInfo> GetEntryInfos();
        void GoToParent();
        void ChangeDirectory(string path);
    }
}