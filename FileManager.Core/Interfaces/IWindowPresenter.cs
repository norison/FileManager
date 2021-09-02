using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Core.Interfaces
{
    public interface IWindowPresenter
    {
        void ClearWindow();
        void ShowBorder();
        void ShowPath(string path);
        void ClearPath();
        void ShowHeader();
        void ClearHeader();
        void ShowSystemEntries(IList<EntryInfo> entryInfos);
        void ClearSystemEntries();
        void ShowEntryInfo(EntryInfo entryInfo);
        void ClearEntryInfo();
        void HighlightEntry(int index, IList<EntryInfo> entryInfos);
        void DehighlightEntry(int index, IList<EntryInfo> entryInfos);
    }
}