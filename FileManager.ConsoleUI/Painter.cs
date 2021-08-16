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

        private IList<EntryInfo> _entryInfos;

        #endregion

        #region Constructor

        public Painter(IWindowSettings settings)
        {
            _settings = settings;
        }

        #endregion

        #region Public Methods

        public void SetEntryItems(IList<EntryInfo> entryInfos)
        {
            _entryInfos = entryInfos;
        }

        public void DrawBorder()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DrawTopLines();
            DrawVerticalLines();
            DrawHorizontalLines();
            DrawBottomLines();
            Console.ForegroundColor = ConsoleColor.Yellow;
            DrawHeader();
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

        public void DrawSystemEntries()
        {
            Console.SetCursorPosition(_settings.LeftEntriesStartPosition, 2);

            var maxLength = _settings.LeftEntryMaxLength;

            for (int i = 0; i < _entryInfos.Count; i++)
            {
                Console.Write(GetResizedEntryName(_entryInfos[i].Name, maxLength));

                if (i == Console.WindowHeight - 6)
                {
                    maxLength = _settings.RightEntryMaxLength;
                    Console.CursorLeft = _settings.RightEntriesStartPosition;
                    Console.CursorTop = 2;
                }
                else if (i < Console.WindowHeight - 6)
                {
                    Console.CursorLeft = _settings.LeftEntriesStartPosition;
                    Console.CursorTop++;
                }
                else
                {
                    Console.CursorLeft = _settings.RightEntriesStartPosition;
                    Console.CursorTop++;
                }
            }
        }

        public void ClearWindow()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(_settings.LeftBorderPosition, 0);

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = _settings.LeftBorderPosition; j <= _settings.RightBorderPosition; j++)
                {
                    Console.Write(' ');
                }

                Console.CursorLeft = _settings.LeftBorderPosition;
                Console.CursorTop++;
            }

            Console.CursorVisible = false;
        }

        public void ShowItem(int index)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            PrintEntry(index);
        }

        public void HideItem(int index)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            PrintEntry(index);
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

        private void DrawHeader()
        {
            var value = "Name";
            var valueCenter = value.Length / 2;

            Console.SetCursorPosition(_settings.LeftHeaderPosition - valueCenter, 1);
            Console.Write(value);

            Console.SetCursorPosition(_settings.RightHeaderPosition - valueCenter, 1);
            Console.Write(value);
        }

        private string GetResizedEntryName(string name, int maxLength)
        {
            if (name.Length > _settings.LeftEntryMaxLength)
            {
                return name.Substring(0, maxLength) + "}";
            }

            return name;
        }

        private void ClearField(int startPosition, int top, int maxLength)
        {
            Console.SetCursorPosition(startPosition, top);
            for (var i = 0; i <= maxLength; i++)
            {
                Console.Write(' ');
            }
        }

        private void PrintEntry(int index)
        {
            var position = index + 2;

            int top;
            int startPosition;
            int maxLength;

            if (position < Console.WindowHeight - 3)
            {
                top = index + 2;
                startPosition = _settings.LeftEntriesStartPosition;
                maxLength = _settings.LeftEntryMaxLength;
            }
            else
            {
                top = index - Console.WindowHeight + 7;
                startPosition = _settings.RightEntriesStartPosition;
                maxLength = _settings.RightEntryMaxLength;
            }

            ClearField(startPosition, top, maxLength);

            Console.SetCursorPosition(startPosition, top);
            Console.Write(GetResizedEntryName(_entryInfos[index].Name, maxLength));
        }

        #endregion
    }
}