using System.Collections.Generic;
using FileManager.SystemInformation;

namespace FileManager.ConsoleUI.Interfaces
{
    public interface IPainter
    {
        void SetEntryItems(IList<EntryInfo> entryInfos);
        void DrawBorder();
        void DrawPath(string path);
        void DrawSystemEntries();
        void ClearWindow();
        void ShowItem(int index);
        void HideItem(int index);
    }
}