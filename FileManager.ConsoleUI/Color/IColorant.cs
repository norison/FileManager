using FileManager.Models;

namespace FileManager.ConsoleUI.Color
{
    public interface IColorant
    {
        void SetBorderColor();
        void SetHighlightedPathColor();
        void SetPathColor();
        void SetHeaderColor();
        void SetEntryInfoColor();
        void SetWindowColor();
        void SetHighlightedEntryColor();
        void SetColorByEntryType(EntryInfo entryInfo);
    }
}