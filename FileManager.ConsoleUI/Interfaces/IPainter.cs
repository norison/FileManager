using System.Collections.Generic;
using FileManager.SystemInformation;

namespace FileManager.ConsoleUI.Interfaces
{
    public interface IPainter
    {
        void DrawBorder();
        void DrawPath(string path);
        void DrawSystemEntries(IList<EntryInfo> entryInfos);
        void ClearWindow();
    }
}