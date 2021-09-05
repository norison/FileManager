using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Core.Interfaces
{
    public interface IFileSystem
    {
        string Path { get; }
        bool IsRoot { get; }
        int FilesCount { get; }
        int FoldersCount { get; }
        long Bytes { get; }
        IEnumerable<EntryInfo> EntryInfos { get; }
        void GoToParent();
        void ChangeDirectory(string path);
    }
}