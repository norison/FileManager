using System.Collections.Generic;
using FileManager.SystemInformation;

namespace FileManager.ConsoleUI.Interfaces
{
    public interface IPainter
    {
        void DrawBorder();
        void DrawPath(string path);
        void DrawHeader();
        void DrawSystemEntries(IList<EntryInfo> entryInfos);
        void ClearPath();
        void ClearSystemEntries();
        void ClearWindow();
        void ShowEntry(int index, string entryName);
        void HideEntry(int index, string entryName);
    }
}