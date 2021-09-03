using System.IO;
using FileManager.Models;

namespace FileManager.ConsoleUI.Extensions
{
    public static class EntryInfoExtensions
    {
        public static string ConvertToUserFriendlyText(this EntryInfo entryInfo)
        {
            if (IsBack(entryInfo))
            {
                return "Up";
            }

            var output = string.Empty;

            if (IsDirectory(entryInfo))
            {
                output += "Folder";
            }
            else
            {
                output += ConvertBytesToString(entryInfo.Bytes);
            }

            output += " ";
            output += entryInfo.CreationTime;

            return output;
        }

        private static bool IsBack(EntryInfo entryInfo)
        {
            return entryInfo.Name == "..";
        }

        private static bool IsDirectory(EntryInfo entryInfo)
        {
            return (entryInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }

        private static string ConvertBytesToString(long bytes)
        {
            var bytesStr = bytes.ToString();

            return bytesStr.Length switch
            {
                <= 6 and >= 4 => $"{bytes / 1024}K",
                >= 7 and <= 9 => $"{bytes / 1024 / 1024}M",
                > 9 => $"{bytes / 1024 / 1024 / 1024}G",
                _ => $"{bytes}B"
            };
        }
    }
}
