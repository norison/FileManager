using FileManager.ConsoleUI.Constants;
using FileManager.SystemInformation;
using System;
using System.Collections.Generic;
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            DrawTopLines();
            DrawVerticalLines();
            DrawHorizontalLines();
            DrawBottomLines();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DrawPath(string path)
        {
            if (path.Length < _settings.PathMaxLength)
            {
                var position = _settings.CenterPosition - path.Length / 2;
                Console.SetCursorPosition(position, Console.WindowTop);
                Console.Write(path);
            }
            else
            {
                path = path.Replace(path.Substring(3, path.Length - _settings.WindowWidth + 5), "...");
                var position = _settings.PathStartPosition;
                Console.SetCursorPosition(position, Console.WindowTop);
                Console.Write(path);
            }
        }

        public void DrawHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var value = "Name";
            var valueCenter = value.Length / 2;

            Console.SetCursorPosition(_settings.LeftHeaderPosition - valueCenter, 1);
            Console.Write(value);

            Console.SetCursorPosition(_settings.RightHeaderPosition - valueCenter, 1);
            Console.Write(value);
        }

        public void DrawSystemEntries(IList<EntryInfo> entryInfos)
        {
            for (var i = 0; i < entryInfos.Count; i++)
            {
                if (i >= _settings.MaxEntriesLength)
                {
                    break;
                }

                PrintEntry(i, entryInfos[i].Name);
            }
        }

        public void ClearSystemEntries()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < _settings.MaxEntriesLength; i++)
            {
                var top = GetTopPosition(i);
                var startPosition = GetStartPosition(i);
                var length = GetMaxLength(i);

                ClearField(startPosition, top, length);
            }
        }

        public void ClearWindow()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
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

        public void ShowEntry(int index, string entryName)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            PrintEntry(index, entryName);
        }

        public void HideEntry(int index, string entryName)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            PrintEntry(index, entryName);
        }

        #endregion

        #region Private Methods

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

        private void PrintEntry(int index, string entryName)
        {
            var top = GetTopPosition(index);
            var startPosition = GetStartPosition(index);
            var maxLength = GetMaxLength(index);

            var resizedEntryName = GetResizedEntryName(entryName, maxLength);

            ClearField(startPosition, top, maxLength);
            PrintField(startPosition, top, resizedEntryName);
        }

        private string GetResizedEntryName(string name, int maxLength)
        {
            if (name.Length > _settings.LeftEntryMaxLength)
            {
                return name.Substring(0, maxLength) + "}";
            }

            return name;
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

        private int GetTopPosition(int index)
        {
            return index + 2 < Console.WindowHeight - 3
                ? index + 2
                : index - Console.WindowHeight + 7;
        }

        private int GetStartPosition(int index)
        {
            return index + 2 < Console.WindowHeight - 3
                ? _settings.LeftEntriesStartPosition
                : _settings.RightEntriesStartPosition;
        }

        private int GetMaxLength(int index)
        {
            return index + 2 < Console.WindowHeight - 3
                ? _settings.LeftEntryMaxLength
                : _settings.RightEntryMaxLength;
        }

        #endregion
    }
}