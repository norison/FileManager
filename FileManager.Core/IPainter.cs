using System.Collections.Generic;
using FileManager.SystemInformation;

namespace FileManager.Core
{
    public interface IPainter
    {
        void DrawWindow();
        void DrawPath(string path);
        void DrawSystemEntries(IList<EntryInfo> fileInfos, bool isRoot);
    }
}