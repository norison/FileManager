using FileManager.ConsoleUI.Constants;
using FileManager.Core.Interfaces;
using FileManager.Models;
using System;
using System.Collections.Generic;
using FileManager.ConsoleUI.Color;
using FileManager.ConsoleUI.Extensions;

namespace FileManager.ConsoleUI
{
    public class ConsoleWindowPresenter : IWindowPresenter
    {
        #region Private Fields

        private readonly IWindowSettings _settings;
        private readonly IColorant _colorant;

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

        public void ShowHighlightedPath(string path)
        {
            ClearPath();
            _colorant.SetHighlightedPathColor();
            ShowPath(path);
        }

        public void ShowUnhighlightedPath(string path)
        {
            ClearPath();
            _colorant.SetPathColor();
            ShowPath(path);
        }

        private void ShowPath(string path)
        {
            var pathWithSpaces = $" {path} ";
            var position = GetPathStartPosition(pathWithSpaces);
            var resizedPath = GetResizedPath(pathWithSpaces);

            PrintField(position, Console.WindowTop, resizedPath);
        }

        private void ClearPath()
        {
            _colorant.SetBorderColor();

            var startPosition = _settings.PathStartPosition;
            var text = new string(BorderSymbols.HorizontalStraightLine, _settings.PathMaxLength);

            PrintField(startPosition, 0, text);
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

        #endregion

        #region Header

        public void ShowHeader()
        {
            _colorant.SetHeaderColor();

            PrintHeader(_settings.LeftHeaderPosition);
            PrintHeader(_settings.RightHeaderPosition);
        }

        private void PrintHeader(int position)
        {
            var centerPosition = position - _settings.HeaderValue.Length / 2;
            PrintField(centerPosition, 1, _settings.HeaderValue);
        }

        #endregion

        #region Entries

        public void ShowSystemEntries(IList<EntryInfo> entryInfos)
        {
            ClearSystemEntries();

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

        private void ClearSystemEntries()
        {
            _colorant.SetWindowColor();

            for (var i = 0; i < _settings.MaxEntriesLength; i++)
            {
                var top = GetEntryTopPosition(i);
                var startPosition = GetEntryStartPosition(i);
                var length = GetEntryMaxLength(i);

                var text = new string(' ', length);
                PrintField(startPosition, top, text);
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

        public void HighlightEntry(int index, IList<EntryInfo> entryInfos)
        {
            if (index >= _settings.MaxEntriesLength - 1)
            {
                ClearSystemEntries();

                for (var i = 0; i < _settings.MaxEntriesLength; i++)
                {
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

            var text = new string(' ', maxLength);
            PrintField(startPosition, top, text);
        }

        #endregion

        #region EntryInfo

        public void ShowEntryInfo(EntryInfo entryInfo)
        {
            ClearEntryInfo();
            _colorant.SetEntryInfoColor();
            PrintEntryInfoName(entryInfo.Name);
            PrintEntryInfoSizeAndTime(entryInfo);
        }

        private void ClearEntryInfo()
        {
            _colorant.SetWindowColor();
            var startPosition = _settings.LeftEntriesStartPosition;
            var top = _settings.EntryInfosHeight;
            var maxLength = _settings.EntryInfoLength;

            var text = new string(' ', maxLength);
            PrintField(startPosition, top, text);
        }

        private void PrintEntryInfoName(string entryName)
        {
            var resizedEntryName = GetResizedField(entryName, _settings.EntryInfoNameMaxLength);
            PrintField(_settings.LeftEntriesStartPosition, _settings.EntryInfosHeight, resizedEntryName);
        }

        private void PrintEntryInfoSizeAndTime(EntryInfo entryInfo)
        {
            var text = entryInfo.ConvertToUserFriendlyText();
            var resizedText = GetResizedField(text, _settings.RightEntryMaxLength);
            PrintField(_settings.RightBorderPosition - resizedText.Length, _settings.EntryInfosHeight, resizedText);
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
                return field.Substring(0, maxLength - 1) + "}";
            }

            return field;
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