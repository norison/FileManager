using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Core.Interfaces
{
    public interface IWindowPresenter
    {
        void ShowBorder();
        void ShowPath(string path);
        void ShowHeader();
        void ShowSystemEntries(IList<EntryInfo> entryInfos);
        void ShowEntryInfo(EntryInfo entryInfo);
        void ClearEntryInfo();
        void ClearPath();
        void ClearSystemEntries();
        void ClearWindow();
        void HighlightEntry(int index, IList<EntryInfo> entryInfos);
        void DehighlightEntry(int index, IList<EntryInfo> entryInfos);
    }
}