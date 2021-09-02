using FileManager.ConsoleUI.Constants;
using FileManager.SystemInformation;
using System;
using System.Collections.Generic;
using System.IO;
using FileManager.Core.Interfaces;
using FileManager.Models;

namespace FileManager.ConsoleUI
{
    public class ConsoleWindowPresenter : IWindowPresenter
    {
        #region Private Fields

        private readonly IWindowSettings _settings;

        #endregion

        #region Constructor

        public ConsoleWindowPresenter(IWindowSettings settings)
        {
            _settings = settings;
        }

        #endregion

        #region Public Methods

        public void ShowBorder()
        {
            SetSecondaryColor();

            DrawTopLines();
            DrawVerticalLines();
            DrawHorizontalLines();
            DrawBottomLines();
        }

        public void ShowPath(string path)
        {
            SetPrimaryColor();

            var pathWithSpaces = $" {path} ";
            var position = GetPathStartPosition(pathWithSpaces);
            var resizedPath = GetResizedPath(pathWithSpaces);

            Console.SetCursorPosition(position, Console.WindowTop);
            Console.Write(resizedPath);
        }

        public void ShowHeader()
        {
            SetHeaderColor();

            var value = "Name";
            var valueCenter = value.Length / 2;

            Console.SetCursorPosition(_settings.LeftHeaderPosition - valueCenter, 1);
            Console.Write(value);

            Console.SetCursorPosition(_settings.RightHeaderPosition - valueCenter, 1);
            Console.Write(value);
        }

        public void ShowSystemEntries(IList<EntryInfo> entryInfos)
        {
            SetPrimaryColor();

            for (var i = 0; i < entryInfos.Count; i++)
            {
                if (i >= _settings.MaxEntriesLength)
                {
                    break;
                }

                SetColorByEntryType(entryInfos[i]);
                PrintEntry(i, entryInfos[i]);
            }
        }

        public void ShowEntryInfo(EntryInfo entryInfo)
        {
            SetSecondaryColor();
            PrintEntryInfoName(entryInfo.Name);
            PrintEntryInfoSizeAndTime(entryInfo);
        }

        public void ClearEntryInfo()
        {
            SetSecondaryColor();
            ClearField(_settings.LeftEntriesStartPosition, _settings.EntryInfosHeight, _settings.EntryInfoLength);
        }

        public void ClearPath()
        {
            SetSecondaryColor();

            Console.SetCursorPosition(_settings.PathStartPosition, 0);

            for (int i = 0; i < _settings.PathMaxLength; i++)
            {
                Console.Write(BorderSymbols.HorizontalStraightLine);
            }
        }

        public void ClearSystemEntries()
        {
            SetPrimaryColor();

            for (int i = 0; i < _settings.MaxEntriesLength; i++)
            {
                var top = GetEntryTopPosition(i);
                var startPosition = GetEntryStartPosition(i);
                var length = GetEntryMaxLength(i);

                ClearField(startPosition, top, length);
            }
        }

        public void ClearWindow()
        {
            SetPrimaryColor();

            Console.SetCursorPosition(_settings.LeftBorderPosition, 0);

            for (var i = 0; i < _settings.WindowHeight; i++)
            {
                for (var j = _settings.LeftBorderPosition; j <= _settings.RightBorderPosition; j++)
                {
                    Console.Write(' ');
                }

                Console.CursorLeft = _settings.LeftBorderPosition;
                Console.CursorTop++;
            }

            Console.CursorVisible = false;
        }

        public void HighlightEntry(int index, IList<EntryInfo> entryInfos)
        {
            if (index >= _settings.MaxEntriesLength - 1)
            {
                ClearSystemEntries();

                SetPrimaryColor();

                for (var i = 0; i < _settings.MaxEntriesLength; i++)
                {
                    if (i == _settings.MaxEntriesLength)
                    {
                        break;
                    }

                    SetColorByEntryType(entryInfos[index - _settings.MaxEntriesLength + 1 + i]);
                    PrintEntry(i, entryInfos[index - _settings.MaxEntriesLength + 1 + i]);
                }

                SetShowedEntryColor();
                ClearEntry(_settings.MaxEntriesLength - 1);
                PrintEntry(_settings.MaxEntriesLength - 1, entryInfos[index]);
            }
            else
            {
                SetShowedEntryColor();
                ClearEntry(index);
                PrintEntry(index, entryInfos[index]);
            }
        }

        public void DehighlightEntry(int index, IList<EntryInfo> entryInfos)
        {
            var entryInfo = entryInfos[index];

            if (index >= _settings.MaxEntriesLength - 1)
            {
                SetColorByEntryType(entryInfo);
                ClearEntry(_settings.MaxEntriesLength - 1);
                PrintEntry(_settings.MaxEntriesLength - 1, entryInfo);
            }
            else
            {
                SetColorByEntryType(entryInfo);
                ClearEntry(index);
                PrintEntry(index, entryInfo);
            }
        }

        #endregion

        #region Private Methods

        #region Colors

        private void SetColorByEntryType(EntryInfo entryInfo)
        {
            var extension = entryInfo.Extenstion;
            var attributes = entryInfo.Attributes;

            Console.BackgroundColor = ConsoleColor.DarkBlue;

            if (extension == ".exe" && (attributes & FileAttributes.Directory) != FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (extension == ".cmd" && (attributes & FileAttributes.Directory) != FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (extension == ".pak" && (attributes & FileAttributes.Directory) != FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.Magenta;
            else if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                Console.ForegroundColor = ConsoleColor.Blue;
            else if ((attributes & FileAttributes.System) == FileAttributes.System)
                Console.ForegroundColor = ConsoleColor.Blue;
            else if ((attributes & FileAttributes.Normal) == FileAttributes.Normal)
                Console.ForegroundColor = ConsoleColor.Cyan;
            else if ((attributes & FileAttributes.Archive) == FileAttributes.Archive)
                Console.ForegroundColor = ConsoleColor.Cyan;
            else if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.White;
        }

        private void SetPrimaryColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void SetSecondaryColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        private void SetShowedEntryColor()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void SetHeaderColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        #endregion

        #region Border

        private void DrawTopLines()
        {
            Console.SetCursorPosition(_settings.LeftBorderPosition, 0);

            for (int i = _settings.LeftBorderPosition; i <= _settings.RightBorderPosition; i++)
            {
                if (i == _settings.LeftBorderPosition)
                {
                    Console.Write(BorderSymbols.LeftTopCorner);
                }
                else if (i == _settings.RightBorderPosition)
                {
                    Console.Write(BorderSymbols.RightTopCorner);
                }
                else
                {
                    Console.Write(BorderSymbols.HorizontalStraightLine);
                }
            }
        }

        private void DrawVerticalLines()
        {
            Console.CursorTop = 1;

            for (int i = 0; i < _settings.EntryInfosHeight; i++)
            {
                Console.CursorLeft = _settings.LeftBorderPosition;
                Console.Write(BorderSymbols.VerticalStraightLine);

                if (i < Console.WindowHeight - 4)
                {
                    Console.CursorLeft = _settings.CenterPosition;
                    Console.Write(BorderSymbols.VerticalStraightLine);
                }

                Console.CursorLeft = _settings.RightBorderPosition;
                Console.Write(BorderSymbols.VerticalStraightLine);

                Console.CursorTop++;
            }
        }

        private void DrawHorizontalLines()
        {
            Console.SetCursorPosition(_settings.LeftBorderPosition, _settings.EntryMaxHeight);

            for (int i = _settings.LeftBorderPosition; i <= _settings.RightBorderPosition; i++)
            {
                if (i == _settings.LeftBorderPosition)
                {
                    Console.Write(BorderSymbols.LeftVerticalCornerLine);
                }
                else if (i == _settings.CenterPosition)
                {
                    Console.Write(BorderSymbols.CenterBottomCorner);
                }
                else if (i == _settings.RightBorderPosition)
                {
                    Console.Write(BorderSymbols.RightVerticalCornerLine);
                }
                else
                {
                    Console.Write(BorderSymbols.HorizontalStraightLine);
                }
            }
        }

        private void DrawBottomLines()
        {
            Console.SetCursorPosition(_settings.LeftBorderPosition, _settings.BottomBorderHeight);

            for (int i = _settings.LeftBorderPosition; i <= _settings.RightBorderPosition; i++)
            {
                if (i == _settings.LeftBorderPosition)
                {
                    Console.Write(BorderSymbols.LeftBottomCorner);
                }
                else if (i == _settings.RightBorderPosition)
                {
                    Console.Write(BorderSymbols.RightBottomCorner);
                }
                else
                {
                    Console.Write(BorderSymbols.HorizontalStraightLine);
                }
            }
        }

        #endregion

        #region Path

        private int GetPathStartPosition(string path)
        {
            if (path.Length < _settings.PathMaxLength)
            {
                return _settings.CenterPosition - path.Length / 2;
            }

            return _settings.PathStartPosition;
        }

        private string GetResizedPath(string path)
        {
            if (path.Length < _settings.PathMaxLength)
            {
                return path;
            }

            var length = path.Length - _settings.WindowWidth + 5;
            return path.Replace(path.Substring(4, length), "...");
        }

        #endregion

        #region Entries

        private void PrintEntryInfoName(string entryName)
        {
            var resizedEntryName = GetResizedField(entryName, _settings.EntryInfoNameMaxLength);
            PrintField(_settings.LeftEntriesStartPosition, _settings.EntryInfosHeight, resizedEntryName);
        }

        private void PrintEntryInfoSizeAndTime(EntryInfo entryInfo)
        {
            var text = GetEntryInfoText(entryInfo);
            var resizedText = GetResizedField(text, _settings.RightEntryMaxLength);
            PrintField(_settings.RightBorderPosition - resizedText.Length, _settings.EntryInfosHeight, resizedText);
        }

        private void PrintEntry(int index, EntryInfo entryInfo)
        {
            var top = GetEntryTopPosition(index);
            var startPosition = GetEntryStartPosition(index);
            var maxLength = GetEntryMaxLength(index);

            var resizedEntryName = GetResizedField(entryInfo.Name, maxLength);

            PrintField(startPosition, top, resizedEntryName);
        }

        private void ClearEntry(int index)
        {
            var top = GetEntryTopPosition(index);
            var startPosition = GetEntryStartPosition(index);
            var maxLength = GetEntryMaxLength(index);

            ClearField(startPosition, top, maxLength);
        }

        private int GetEntryTopPosition(int index)
        {
            var height = index + _settings.EntryStartHeight;

            return height < _settings.EntryMaxHeight
                ? height
                : height - _settings.EntriesLength;
        }

        private int GetEntryStartPosition(int index)
        {
            var height = index + _settings.EntryStartHeight;

            return height < _settings.EntryMaxHeight
                ? _settings.LeftEntriesStartPosition
                : _settings.RightEntriesStartPosition;
        }

        private int GetEntryMaxLength(int index)
        {
            var height = index + _settings.EntryStartHeight;

            return height < _settings.EntryMaxHeight
                ? _settings.LeftEntryMaxLength
                : _settings.RightEntryMaxLength;
        }

        private string GetUserFriendlyBytes(long bytes)
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

        private string GetEntryInfoText(EntryInfo entryInfo)
        {
            var output = string.Empty;

            if (IsEntryDirectory(entryInfo))
            {
                output += "Folder";
            }
            else
            {
                output += GetUserFriendlyBytes(entryInfo.Bytes);
            }

            output += " ";
            output += entryInfo.CreationTime;

            return output;
        }

        private bool IsEntryDirectory(EntryInfo entryInfo)
        {
            return (entryInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }

        #endregion

        #region Common

        private string GetResizedField(string field, int maxLength)
        {
            if (field.Length > maxLength)
            {
                return field.Substring(0, maxLength) + "}";
            }

            return field;
        }

        private void ClearField(int startPosition, int top, int length)
        {
            Console.SetCursorPosition(startPosition, top);
            for (var i = 0; i <= length; i++)
            {
                Console.Write(' ');
            }
        }

        private void PrintField(int startPosition, int top, string text)
        {
            Console.SetCursorPosition(startPosition, top);
            Console.Write(text);
        }

        #endregion

        #endregion
    }
}