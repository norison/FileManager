using FileManager.ConsoleUI.Constants;
using FileManager.ConsoleUI.Settings;
using FileManager.Core;
using System;

namespace FileManager.ConsoleUI
{
    public class ConsolePainter : IPainter
    {
        #region Private Fields

        private readonly IWindowConsoleSettings _settings;

        #endregion

        #region Constructor

        public ConsolePainter(IWindowConsoleSettings settings)
        {
            _settings = settings;
        }

        #endregion

        #region Public Methods

        public void DrawWindow()
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
                path = path.Replace(path.Substring(3, path.Length - Console.WindowWidth / 2 + 5), "...");
                var position = _settings.PathStartPosition;
                Console.SetCursorPosition(position, Console.WindowTop);
                Console.Write(path);
            }
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

        #endregion
    }
}