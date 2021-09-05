using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Core.Interfaces
{
    public interface IWindowPresenter
    {
        void ClearWindow();
        void ShowBorder();
        void ShowHighlightedPath(string path);
        void ShowUnhighlightedPath(string path);
        void ShowHeader();
        void ShowSystemEntries(IList<EntryInfo> entryInfos);
        void ShowEntryInfo(EntryInfo entryInfo);
        void HighlightEntry(int index, IList<EntryInfo> entryInfos);
        void DehighlightEntry(int index, IList<EntryInfo> entryInfos);
        void ShowFolderInfo(string info);
    }
}