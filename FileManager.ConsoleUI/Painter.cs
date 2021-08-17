using FileManager.ConsoleUI.Constants;
using FileManager.SystemInformation;
using System;
using System.Collections.Generic;
using System.IO;
using FileManager.ConsoleUI.Interfaces;

namespace FileManager.ConsoleUI
{
    public class Painter : IPainter
    {
        #region Private Fields

        private readonly IWindowSettings _settings;

        #endregion

        #region Constructor

        public Painter(IWindowSettings settings)
        {
            _settings = settings;
        }

        #endregion

        #region Public Methods

        public void DrawBorder()
        {
            SetSecondaryColor();

            DrawTopLines();
            DrawVerticalLines();
            DrawHorizontalLines();
            DrawBottomLines();
        }

        public void DrawPath(string path)
        {
            SetPrimaryColor();

            var pathWithSpaces = $" {path} ";
            var position = GetPathStartPosition(pathWithSpaces);
            var resizedPath = GetResizedPath(pathWithSpaces);

            Console.SetCursorPosition(position, Console.WindowTop);
            Console.Write(resizedPath);
        }

        public void DrawHeader()
        {
            SetHeaderColor();

            var value = "Name";
            var valueCenter = value.Length / 2;

            Console.SetCursorPosition(_settings.LeftHeaderPosition - valueCenter, 1);
            Console.Write(value);

            Console.SetCursorPosition(_settings.RightHeaderPosition - valueCenter, 1);
            Console.Write(value);
        }

        public void DrawSystemEntries(IList<EntryInfo> entryInfos)
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

        public void DrawEntryInfo(EntryInfo entryInfo)
        {
            SetSecondaryColor();
            PrintEntryInfoName(entryInfo.Name);
            PrintEntryInfoSizeAndTime(entryInfo);
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

            for (var i = 0; i < Console.WindowHeight; i++)
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

        public void HighlightEntry(int index, EntryInfo entryInfo)
        {
            SetShowedEntryColor();
            PrintEntry(index, entryInfo);
        }

        public void DehighlightEntry(int index, EntryInfo entryInfo)
        {
            SetColorByEntryType(entryInfo);
            PrintEntry(index, entryInfo);
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

            for (int i = 0; i < Console.WindowHeight - 2; i++)
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
            Console.SetCursorPosition(_settings.LeftBorderPosition, Console.WindowHeight - 3);

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
            Console.SetCursorPosition(_settings.LeftBorderPosition, Console.WindowHeight - 1);

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

        #region Entry

        private void PrintEntryInfoName(string entryName)
        {
            var position = _settings.LeftEntriesStartPosition;
            var top = Console.WindowHeight - 2;
            var maxLength = _settings.EntryInfoNameMaxLength;

            var resizedEntryName = GetResizedField(entryName, maxLength);

            ClearField(position, top, maxLength);
            PrintField(position, top, resizedEntryName);
        }

        private void PrintEntryInfoSizeAndTime(EntryInfo entryInfo)
        {
            
        }

        private void PrintEntry(int index, EntryInfo entryInfo)
        {
            var top = GetEntryTopPosition(index);
            var startPosition = GetEntryStartPosition(index);
            var maxLength = GetEntryMaxLength(index);

            var resizedEntryName = GetResizedField(entryInfo.Name, maxLength);

            ClearField(startPosition, top, maxLength);
            PrintField(startPosition, top, resizedEntryName);
        }

        private int GetEntryTopPosition(int index)
        {
            return index + 2 < Console.WindowHeight - 3
                ? index + 2
                : index - Console.WindowHeight + 7;
        }

        private int GetEntryStartPosition(int index)
        {
            return index + 2 < Console.WindowHeight - 3
                ? _settings.LeftEntriesStartPosition
                : _settings.RightEntriesStartPosition;
        }

        private int GetEntryMaxLength(int index)
        {
            return index + 2 < Console.WindowHeight - 3
                ? _settings.LeftEntryMaxLength
                : _settings.RightEntryMaxLength;
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