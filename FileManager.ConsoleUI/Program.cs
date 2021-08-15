using System;
using FileManager.ConsoleUI.Constants;

namespace FileManager.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupConsole();
            DrawWindow();
            Console.ReadKey();
        }

        static void SetupConsole()
        {
            Console.CancelKeyPress += (sender, e) => { };
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        static void DrawWindow()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DrawTopLines(ConsolePositions.LeftWindowLeftBorder, ConsolePositions.LeftWindowRightBorder);
            DrawTopLines(ConsolePositions.RightWindowLeftBorder, ConsolePositions.RightWindowRightBorder);
            DrawVerticalLines(ConsolePositions.LeftWindowLeftBorder, ConsolePositions.LeftWindowCenter, ConsolePositions.LeftWindowRightBorder);
            DrawVerticalLines(ConsolePositions.RightWindowLeftBorder, ConsolePositions.RightWindowCenter, ConsolePositions.RightWindowRightBorder);
            DrawHorizontalLines(ConsolePositions.LeftWindowLeftBorder, ConsolePositions.LeftWindowCenter, ConsolePositions.LeftWindowRightBorder);
            DrawHorizontalLines(ConsolePositions.RightWindowLeftBorder, ConsolePositions.RightWindowCenter, ConsolePositions.RightWindowRightBorder);
            DrawBottomLines(ConsolePositions.LeftWindowLeftBorder, ConsolePositions.LeftWindowRightBorder);
            DrawBottomLines(ConsolePositions.RightWindowLeftBorder, ConsolePositions.RightWindowRightBorder);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void DrawTopLines(int leftPosition, int rightPosition)
        {
            Console.SetCursorPosition(leftPosition, 0);

            for (int i = leftPosition; i <= rightPosition; i++)
            {
                if (i == leftPosition)
                {
                    Console.Write(BorderSymbols.LeftTopCorner);
                }
                else if (i == rightPosition)
                {
                    Console.Write(BorderSymbols.RightTopCorner);
                }
                else
                {
                    Console.Write(BorderSymbols.HorizontalStraightLine);
                }
            }
        }

        static void DrawVerticalLines(int leftPosition, int centerPosition, int rightPosition)
        {
            Console.CursorTop = 1;

            for (int i = 0; i < Console.WindowHeight - 2; i++)
            {
                Console.CursorLeft = leftPosition;
                Console.Write(BorderSymbols.VerticalStraightLine);

                if (i < Console.WindowHeight - 4)
                {
                    Console.CursorLeft = centerPosition;
                    Console.Write(BorderSymbols.VerticalStraightLine);
                }

                Console.CursorLeft = rightPosition;
                Console.Write(BorderSymbols.VerticalStraightLine);

                Console.CursorTop++;
            }
        }

        static void DrawHorizontalLines(int leftPosition, int centerPosition, int rightPosition)
        {
            Console.SetCursorPosition(leftPosition, Console.WindowHeight - 3);

            for (int i = leftPosition; i <= rightPosition; i++)
            {
                if (i == leftPosition)
                {
                    Console.Write(BorderSymbols.LeftVerticalCornerLine);
                }
                else if (i == centerPosition)
                {
                    Console.Write(BorderSymbols.CenterBottomCorner);
                }
                else if (i == rightPosition)
                {
                    Console.Write(BorderSymbols.RightVerticalCornerLine);
                }
                else
                {
                    Console.Write(BorderSymbols.HorizontalStraightLine);
                }
            }
        }

        static void DrawBottomLines(int leftPosition, int rightPosition)
        {
            Console.SetCursorPosition(leftPosition, Console.WindowHeight - 1);

            for (int i = leftPosition; i <= rightPosition; i++)
            {
                if (i == leftPosition)
                {
                    Console.Write(BorderSymbols.LeftBottomCorner);
                }
                else if (i == rightPosition)
                {
                    Console.Write(BorderSymbols.RightBottomCorner);
                }
                else
                {
                    Console.Write(BorderSymbols.HorizontalStraightLine);
                }
            }
        }
    }
}
