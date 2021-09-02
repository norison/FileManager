using FileManager.ConsoleUI.Constants;
using FileManager.Core.Interfaces;
using FileManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using FileManager.ConsoleUI.Color;

namespace FileManager.ConsoleUI
{
    public class ConsoleWindowPresenter : IWindowPresenter
    {
        #region Private Fields

        private readonly IWindowSettings _settings;
        private readonly IColorant _colorant;
        private readonly string _headerValue = "Name";

        #endregion

        #region Constructor

        public ConsoleWindowPresenter(IWindowSettings settings, IColorant colorant)
        {
            _settings = settings;
            _colorant = colorant;
        }

        #endregion

        #region Methods

        #region Border

        public void ShowBorder()
        {
            _colorant.SetBorderColor();

            DrawTopLines();
            DrawVerticalLines();
            DrawHorizontalLines();
            DrawBottomLines();
        }

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

        public void ShowPath(string path)
        {
            _colorant.SetPathColor();

            var pathWithSpaces = $" {path} ";
            var position = GetPathStartPosition(pathWithSpaces);
            var resizedPath = GetResizedPath(pathWithSpaces);

            Console.SetCursorPosition(position, Console.WindowTop);
            Console.Write(resizedPath);
        }

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

        public void ClearPath()
        {
            _colorant.SetBorderColor();

            Console.SetCursorPosition(_settings.PathStartPosition, 0);

            for (int i = 0; i < _settings.PathMaxLength; i++)
            {
                Console.Write(BorderSymbols.HorizontalStraightLine);
            }
        }

        #endregion

        #region Header

        public void ShowHeader()
        {
            _colorant.SetHeaderColor();

            ShowHeader(_settings.LeftHeaderPosition);
            ShowHeader(_settings.RightHeaderPosition);
        }

        private void ShowHeader(int position)
        {
            Console.SetCursorPosition(position - _headerValue.Length / 2, 1);
            Console.Write(_headerValue);
        }

        public void ClearHeader()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Entries

        public void ShowSystemEntries(IList<EntryInfo> entryInfos)
        {
            for (var i = 0; i < entryInfos.Count; i++)
            {
                if (i >= _settings.MaxEntriesLength)
                {
                    break;
                }

                var entryInfo = entryInfos[i];
                _colorant.SetColorByEntryType(entryInfo);
                PrintEntry(i, entryInfo);
            }
        }

        private void PrintEntry(int index, EntryInfo entryInfo)
        {
            var top = GetEntryTopPosition(index);
            var startPosition = GetEntryStartPosition(index);
            var maxLength = GetEntryMaxLength(index);

            var resizedEntryName = GetResizedField(entryInfo.Name, maxLength);

            PrintField(startPosition, top, resizedEntryName);
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

        public void ClearSystemEntries()
        {
            _colorant.SetWindowColor();

            for (int i = 0; i < _settings.MaxEntriesLength; i++)
            {
                var top = GetEntryTopPosition(i);
                var startPosition = GetEntryStartPosition(i);
                var length = GetEntryMaxLength(i);

                ClearField(startPosition, top, length);
            }
        }

        public void HighlightEntry(int index, IList<EntryInfo> entryInfos)
        {
            if (index >= _settings.MaxEntriesLength - 1)
            {
                ClearSystemEntries();

                for (var i = 0; i < _settings.MaxEntriesLength; i++)
                {
                    if (i == _settings.MaxEntriesLength)
                    {
                        break;
                    }

                    var entryInfo = entryInfos[index - _settings.MaxEntriesLength + 1 + i];

                    if (i == _settings.MaxEntriesLength - 1)
                    {
                        _colorant.SetHighlightedEntryColor();
                    }
                    else
                    {
                        _colorant.SetColorByEntryType(entryInfo);
                    }

                    PrintEntry(i, entryInfo);
                }
            }
            else
            {
                _colorant.SetHighlightedEntryColor();

                ClearEntry(index);
                PrintEntry(index, entryInfos[index]);
            }
        }

        public void DehighlightEntry(int index, IList<EntryInfo> entryInfos)
        {
            var entryInfo = entryInfos[index];

            if (index >= _settings.MaxEntriesLength - 1)
            {
                _colorant.SetColorByEntryType(entryInfo);
                ClearEntry(_settings.MaxEntriesLength - 1);
                PrintEntry(_settings.MaxEntriesLength - 1, entryInfo);
            }
            else
            {
                _colorant.SetColorByEntryType(entryInfo);
                ClearEntry(index);
                PrintEntry(index, entryInfo);
            }
        }

        private void ClearEntry(int index)
        {
            var top = GetEntryTopPosition(index);
            var startPosition = GetEntryStartPosition(index);
            var maxLength = GetEntryMaxLength(index);

            ClearField(startPosition, top, maxLength);
        }

        #endregion

        #region EntryInfo

        public void ShowEntryInfo(EntryInfo entryInfo)
        {
            _colorant.SetEntryInfoColor();

            PrintEntryInfoName(entryInfo.Name);
            PrintEntryInfoSizeAndTime(entryInfo);
        }

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

        public void ClearEntryInfo()
        {
            _colorant.SetWindowColor();
            ClearField(_settings.LeftEntriesStartPosition, _settings.EntryInfosHeight, _settings.EntryInfoLength);
        }

        #endregion

        #region Common

        public void ClearWindow()
        {
            _colorant.SetWindowColor();

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

        private string GetResizedField(string field, int maxLength)
        {
            if (field.Length > maxLength)
            {
                return field.Substring(0, maxLength) + "}";
            }

            return field;
        }

        private void PrintField(int startPosition, int top, string text)
        {
            Console.SetCursorPosition(startPosition, top);
            Console.Write(text);
        }

        private void ClearField(int startPosition, int top, int length)
        {
            Console.SetCursorPosition(startPosition, top);
            for (var i = 0; i <= length; i++)
            {
                Console.Write(' ');
            }
        }

        #endregion

        #endregion
    }
}