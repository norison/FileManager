using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SystemInfo;

namespace SadInterface
{
    public abstract class VisualInterface
    {
        protected SystemInformation leftMenu;
        protected SystemInformation rightMenu;
        protected PopUpMenu popUpMenu;
        protected int popUpChoice;
        protected int whichInfo;
        protected int leftChoice;
        protected int rightChoice;

        public VisualInterface()
        {
            leftMenu = new SystemInformation();
            rightMenu = new SystemInformation();
        }

        // Пиздец

        protected void DrawFullWindow()
        {
            Console.SetBufferSize(Console.WindowWidth, 9000);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            DrawFrame();
            DrawBothPath();
            DrawStringName();
            if (leftChoice >= (Console.WindowHeight - 6) * 2) LeftArrowMoveOutOfPlace();
            else DrawLeftWindow();
            if (rightChoice >= (Console.WindowHeight - 6) * 2) RightArrowMoveOutOfPlace();
            else DrawRightWindow();
            DrawLeftFileInfo();
            DrawRightFileInfo();
            DrawLeftDirectoryInfo();
            DrawRightDirectoryInfo();
            DrawHelpInfo();
            if (popUpMenu.isActive == true)
            {
                if (whichInfo == 0)
                {
                    LeftPopUpMenuFrame();
                    LeftPopUpMenuElements();
                }
                else
                {
                    RightPopUpMenuFrame();
                    RightPopUpMenuElements();
                }
            }
        }
        protected void TABFromLeft()
        {
            int maxRange;
            int leftCursor;
            int topCursor;
            if (leftChoice < Console.WindowHeight - 6)
            {
                maxRange = Console.WindowWidth / 4 - 1;
                leftCursor = 1;
                topCursor = leftChoice + 2;

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                ClearWindowElement(maxRange, leftCursor, topCursor);
                DrawWindowElement(maxRange, leftCursor, topCursor, leftMenu, leftChoice);

                if (rightChoice < Console.WindowHeight - 6)
                {
                    maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1;
                    leftCursor = Console.WindowWidth / 2 + 1;
                    topCursor = Console.WindowHeight - (Console.WindowHeight - rightChoice) + 2;
                }
                else if (rightChoice >= (Console.WindowHeight - 6) * 2)
                {
                    maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1;
                    leftCursor = Console.WindowWidth - Console.WindowWidth / 4;
                    topCursor = Console.WindowHeight - 5;
                }
                else
                {
                    maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1;
                    leftCursor = Console.WindowWidth - Console.WindowWidth / 4;
                    topCursor = rightChoice - Console.WindowHeight + 8;
                }
            }
            else if (leftChoice >= (Console.WindowHeight - 6) * 2)
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2;
                leftCursor = Console.WindowWidth / 4 + 1;
                topCursor = Console.WindowHeight - 5;

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                ClearWindowElement(maxRange, leftCursor, topCursor);
                DrawWindowElement(maxRange, leftCursor, topCursor, leftMenu, leftChoice);

                if (rightChoice < Console.WindowHeight - 6)
                {
                    maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1;
                    leftCursor = Console.WindowWidth / 2 + 1;
                    topCursor = Console.WindowHeight - (Console.WindowHeight - rightChoice) + 2;
                }
                else if (rightChoice >= (Console.WindowHeight - 6) * 2)
                {
                    maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1;
                    leftCursor = Console.WindowWidth - Console.WindowWidth / 4;
                    topCursor = Console.WindowHeight - 5;
                }
                else
                {
                    maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1;
                    leftCursor = Console.WindowWidth - Console.WindowWidth / 4;
                    topCursor = rightChoice - Console.WindowHeight + 8;
                }
            }
            else
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2;
                leftCursor = Console.WindowWidth / 4 + 1;
                topCursor = leftChoice - Console.WindowHeight + 8;

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                ClearWindowElement(maxRange, leftCursor, topCursor);
                DrawWindowElement(maxRange, leftCursor, topCursor, leftMenu, leftChoice);

                if (rightChoice < Console.WindowHeight - 6)
                {
                    maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1;
                    leftCursor = Console.WindowWidth / 2 + 1;
                    topCursor = Console.WindowHeight - (Console.WindowHeight - rightChoice) + 2;
                }
                else if (rightChoice >= (Console.WindowHeight - 6) * 2)
                {
                    maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1;
                    leftCursor = Console.WindowWidth - Console.WindowWidth / 4;
                    topCursor = Console.WindowHeight - 5;
                }
                else
                {
                    maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1;
                    leftCursor = Console.WindowWidth - Console.WindowWidth / 4;
                    topCursor = rightChoice - Console.WindowHeight + 8;
                }
            }

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            ClearWindowElement(maxRange, leftCursor, topCursor);
            DrawWindowElement(maxRange, leftCursor, topCursor, rightMenu, rightChoice);

            whichInfo++;
        }
        protected void TABFromRight()
        {
            int maxRange;
            int leftCursor;
            int topCursor;
            if (rightChoice < Console.WindowHeight - 6)
            {
                maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1;
                leftCursor = Console.WindowWidth / 2 + 1;
                topCursor = Console.WindowHeight - (Console.WindowHeight - rightChoice) + 2;

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                ClearWindowElement(maxRange, leftCursor, topCursor);
                DrawWindowElement(maxRange, leftCursor, topCursor, rightMenu, rightChoice);

                if (leftChoice < Console.WindowHeight - 6)
                {
                    maxRange = Console.WindowWidth / 4 - 1;
                    leftCursor = 1;
                    topCursor = leftChoice + 2;
                }
                else if (leftChoice >= (Console.WindowHeight - 6) * 2)
                {
                    maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2;
                    leftCursor = Console.WindowWidth / 4 + 1;
                    topCursor = Console.WindowHeight - 5;
                }
                else
                {
                    maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2;
                    leftCursor = Console.WindowWidth / 4 + 1;
                    topCursor = leftChoice - Console.WindowHeight + 8;
                }
            }
            else if (rightChoice >= (Console.WindowHeight - 6) * 2)
            {
                maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1;
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4;
                topCursor = Console.WindowHeight - 5;

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                ClearWindowElement(maxRange, leftCursor, topCursor);
                DrawWindowElement(maxRange, leftCursor, topCursor, rightMenu, rightChoice);

                if (leftChoice < Console.WindowHeight - 6)
                {
                    maxRange = Console.WindowWidth / 4 - 1;
                    leftCursor = 1;
                    topCursor = leftChoice + 2;
                }
                else if (leftChoice >= (Console.WindowHeight - 6) * 2)
                {
                    maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2;
                    leftCursor = Console.WindowWidth / 4 + 1;
                    topCursor = Console.WindowHeight - 5;
                }
                else
                {
                    maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2;
                    leftCursor = Console.WindowWidth / 4 + 1;
                    topCursor = leftChoice - Console.WindowHeight + 8;
                }
            }
            else
            {
                maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1;
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4;
                topCursor = rightChoice - Console.WindowHeight + 8;

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                ClearWindowElement(maxRange, leftCursor, topCursor);
                DrawWindowElement(maxRange, leftCursor, topCursor, rightMenu, rightChoice);

                if (leftChoice < Console.WindowHeight - 6)
                {
                    maxRange = Console.WindowWidth / 4 - 1;
                    leftCursor = 1;
                    topCursor = leftChoice + 2;
                }
                else if (leftChoice >= (Console.WindowHeight - 6) * 2)
                {
                    maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2;
                    leftCursor = Console.WindowWidth / 4 + 1;
                    topCursor = Console.WindowHeight - 5;
                }
                else
                {
                    maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2;
                    leftCursor = Console.WindowWidth / 4 + 1;
                    topCursor = leftChoice - Console.WindowHeight + 8;
                }
            }

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            ClearWindowElement(maxRange, leftCursor, topCursor);
            DrawWindowElement(maxRange, leftCursor, topCursor, leftMenu, leftChoice);

            whichInfo--;
        }
        private void DrawWindowElement(int maxRange, int leftCursor, int topCursor, SystemInformation other, int choice)
        {
            Console.SetCursorPosition(leftCursor, topCursor);

            string fileName = Path.GetFileName(other.DirectoriesAndFiles[choice]);
            string extension = Path.GetExtension(other.DirectoriesAndFiles[choice]);
            var attributes = File.GetAttributes(other.DirectoriesAndFiles[choice]);

            if (extension == ".exe" && attributes != FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (extension == ".cmd" && attributes != FileAttributes.Directory)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (extension == ".pak" && attributes != FileAttributes.Directory)
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

            for (int i = 0; i < fileName.Length; i++)
                if (i != maxRange)
                    Console.Write(fileName[i]);
                else
                {
                    Console.CursorLeft--;
                    Console.Write('}');
                    break;
                }
        }
        private void ClearWindowElement(int maxRange, int leftCursor, int topCursor)
        {
            Console.SetCursorPosition(leftCursor, topCursor);
            for (int i = 0; i != maxRange; i++)
                Console.Write(' ');
            Console.SetCursorPosition(leftCursor, Console.CursorTop--);
        }
        private void UpArrow(SystemInformation other, ref int choice, params Coordinate[] coordinates)
        {
            if (choice != 0)
            {
                Coordinate firstWindow = coordinates[0];
                Coordinate secondWindow = coordinates[1];
                if (choice < Console.WindowHeight - 6)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    ClearWindowElement(firstWindow.maxRange, firstWindow.leftCursor, firstWindow.topCursor);
                    DrawWindowElement(firstWindow.maxRange, firstWindow.leftCursor, firstWindow.topCursor, other, choice);

                    Console.CursorTop--;
                    choice--;

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    ClearWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.CursorTop);
                    DrawWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.CursorTop, other, choice);
                }
                else if (choice == Console.WindowHeight - 6)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    ClearWindowElement(secondWindow.maxRange, secondWindow.leftCursor, secondWindow.topCursor);
                    DrawWindowElement(secondWindow.maxRange, secondWindow.leftCursor, secondWindow.topCursor, other, choice);

                    choice--;

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    ClearWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.WindowHeight - 5);
                    DrawWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.WindowHeight - 5, other, choice);
                }
                else if (!(choice >= (Console.WindowHeight - 6) * 2))
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    ClearWindowElement(secondWindow.maxRange, secondWindow.leftCursor, choice - Console.WindowHeight + 8);
                    DrawWindowElement(secondWindow.maxRange, secondWindow.leftCursor, choice - Console.WindowHeight + 8, other, choice);

                    Console.CursorTop--;
                    choice--;

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    ClearWindowElement(secondWindow.maxRange, secondWindow.leftCursor, Console.CursorTop);
                    DrawWindowElement(secondWindow.maxRange, secondWindow.leftCursor, Console.CursorTop, other, choice);
                }
                else
                {
                    choice--;
                    if (whichInfo == 0)
                        LeftArrowMoveOutOfPlace();
                    else
                        RightArrowMoveOutOfPlace();
                }

                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
        }
        protected void LeftUpArrow()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 1,
                leftCursor = 1,
                topCursor = leftChoice + 2
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2,
                leftCursor = Console.WindowWidth / 4 + 1,
                topCursor = 2
            };
            UpArrow(leftMenu, ref leftChoice, firstWindow, secondWindow);
        }
        protected void RightUpArrow()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1,
                leftCursor = Console.WindowWidth / 2 + 1,
                topCursor = Console.WindowHeight - (Console.WindowHeight - rightChoice) + 2
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1,
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4,
                topCursor = rightChoice - Console.WindowHeight + 8
            };
            UpArrow(rightMenu, ref rightChoice, firstWindow, secondWindow);
        }
        private void DownArrow(SystemInformation other, ref int choice, params Coordinate[] coordinates)
        {
            if (other.DirectoriesAndFiles.Length - 1 != choice)
            {
                Coordinate firstWindow = coordinates[0];
                Coordinate secondWindow = coordinates[1];
                if (choice < Console.WindowHeight - 7)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    ClearWindowElement(firstWindow.maxRange, firstWindow.leftCursor, firstWindow.topCursor);
                    DrawWindowElement(firstWindow.maxRange, firstWindow.leftCursor, firstWindow.topCursor, other, choice);

                    Console.CursorTop++;
                    choice++;

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    ClearWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.CursorTop);
                    DrawWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.CursorTop, other, choice);
                }
                else if (choice == Console.WindowHeight - 7)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    ClearWindowElement(firstWindow.maxRange, firstWindow.leftCursor, firstWindow.topCursor);
                    DrawWindowElement(firstWindow.maxRange, firstWindow.leftCursor, firstWindow.topCursor, other, choice);

                    choice++;

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    ClearWindowElement(secondWindow.maxRange, secondWindow.leftCursor, 2);
                    DrawWindowElement(secondWindow.maxRange, secondWindow.leftCursor, 2, other, choice);
                }
                else if (!(choice >= ((Console.WindowHeight - 6) * 2) - 2))
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    ClearWindowElement(secondWindow.maxRange, secondWindow.leftCursor, choice - Console.WindowHeight + 8);
                    DrawWindowElement(secondWindow.maxRange, secondWindow.leftCursor, choice - Console.WindowHeight + 8, other, choice);

                    Console.CursorTop++;
                    choice++;

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    ClearWindowElement(secondWindow.maxRange, secondWindow.leftCursor, Console.CursorTop);
                    DrawWindowElement(secondWindow.maxRange, secondWindow.leftCursor, Console.CursorTop, other, choice);
                }
                else
                {
                    choice++;
                    if (whichInfo == 0)
                        LeftArrowMoveOutOfPlace();
                    else
                        RightArrowMoveOutOfPlace();
                }
            }

            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        protected void LeftDownArrow()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 1,
                leftCursor = 1,
                topCursor = leftChoice + 2
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2,
                leftCursor = Console.WindowWidth / 4 + 1,
                topCursor = 2
            };
            DownArrow(leftMenu, ref leftChoice, firstWindow, secondWindow);
        }
        protected void RightDownArrow()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1,
                leftCursor = Console.WindowWidth / 2 + 1,
                topCursor = rightChoice + 2
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1,
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4,
                topCursor = 2
            };
            DownArrow(rightMenu, ref rightChoice, firstWindow, secondWindow);
        }
        private void MoveArrowOutOfPlace(SystemInformation other, int choice, params Coordinate[] coordinates)
        {
            Coordinate firstWindow = coordinates[0];
            Coordinate secondWindow = coordinates[1];

            var maxElements = (Console.WindowHeight - 6) * 2;
            int i;
            Console.CursorTop = 2;

            for (i = choice - maxElements + 1; i < other.DirectoriesAndFiles.Length; i++)
            {
                ClearWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.CursorTop);
                DrawWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.CursorTop, other, i);

                Console.CursorTop++;

                if (Console.CursorTop == Console.WindowHeight - 4)
                    break;
            }

            i++;
            Console.CursorTop = 2;

            for (; i < other.DirectoriesAndFiles.Length; i++)
            {
                if (i == choice)
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                else
                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                ClearWindowElement(secondWindow.maxRange, secondWindow.leftCursor, Console.CursorTop);
                DrawWindowElement(secondWindow.maxRange, secondWindow.leftCursor, Console.CursorTop, other, i);

                Console.CursorTop++;

                if (Console.CursorTop == Console.WindowHeight - 4)
                    break;
            }

            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        protected void LeftArrowMoveOutOfPlace()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 1,
                leftCursor = 1,
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2,
                leftCursor = Console.WindowWidth / 4 + 1,
            };

            MoveArrowOutOfPlace(leftMenu, leftChoice, firstWindow, secondWindow);
        }
        protected void RightArrowMoveOutOfPlace()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1,
                leftCursor = Console.WindowWidth / 2 + 1,
                topCursor = 2
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1,
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4,
                topCursor = 2
            };

            MoveArrowOutOfPlace(rightMenu, rightChoice, firstWindow, secondWindow);
        }
        protected void DrawFrame()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight - 4);
            for (int i = 0; i < Console.WindowWidth / 2; i++)
                Console.Write('═');
            Console.SetCursorPosition(0, 0);
            Console.Write("╔");
            for (int i = 0; i < Console.WindowWidth / 2 - 2; i++)
                Console.Write('═');
            Console.Write('╗');
            Console.Write('╔');
            if (Console.WindowWidth % 2 != 0)
                for (int i = 0; i < Console.WindowWidth / 2 - 1; i++)
                    Console.Write('═');
            else
                for (int i = 0; i < Console.WindowWidth / 2 - 2; i++)
                    Console.Write('═');
            Console.Write('╗');
            for (int i = 0; i < Console.WindowHeight - 3; i++)
            {
                if (i != Console.WindowHeight - 5)
                    Console.Write('║');
                else
                    Console.Write('╠');
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                if (i != Console.WindowHeight - 4 && i != Console.WindowHeight - 5)
                    Console.Write('║');
                Console.SetCursorPosition(Console.WindowWidth / 2 - 1, Console.CursorTop);
                if (i != Console.WindowHeight - 5)
                    Console.Write('║');
                else
                    Console.Write('╣');
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.CursorTop);
                if (i != Console.WindowHeight - 5)
                    Console.Write('║');
                else
                    Console.Write('╠');
                Console.SetCursorPosition(Console.WindowWidth - 1 - Console.WindowWidth / 4, Console.CursorTop);
                if (i != Console.WindowHeight - 5 && i != Console.WindowHeight - 4)
                    Console.Write('║');
                else if (i == Console.WindowHeight - 5)
                    Console.Write('╩');
                Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);
                if (i != Console.WindowHeight - 5)
                    Console.Write('║');
                else
                    Console.Write('╣');
            }
            Console.Write('╚');
            for (int i = 0; i < Console.WindowWidth / 2 - 2; i++)
                Console.Write('═');
            Console.Write('╝');
            Console.Write('╚');
            if (Console.WindowWidth % 2 != 0)
                for (int i = 0; i < Console.WindowWidth / 2 - 1; i++)
                    Console.Write('═');
            else
                for (int i = 0; i < Console.WindowWidth / 2 - 2; i++)
                    Console.Write('═');
            Console.Write('╝');
            Console.SetCursorPosition(Console.WindowLeft + 1, Console.WindowHeight - 4);
            for (int i = 0; i < Console.WindowWidth / 2 - 2; i++)
            {
                if (i != Console.WindowWidth / 2 / 2 - 1)
                    Console.Write('═');
                else
                    Console.Write('╩');
            }
            Console.CursorVisible = false;
        }
        private void DrawPath(SystemInformation other, Coordinate path, int deleteCharCount, int leftCenterCursor)
        {
            int centerPath = other.SystemPath.Length / 2;

            if (path.maxRange < other.SystemPath.Length)
            {
                try
                {
                    StringBuilder temp = new StringBuilder();
                    StringBuilder systemPath = new StringBuilder(other.SystemPath);

                    for (int i = 0; i < deleteCharCount; i++)
                        temp.Append(other.SystemPath[i + 3]);

                    systemPath.Replace(temp.ToString(), "...", 3, temp.Length);

                    Console.SetCursorPosition(path.leftCursor, path.topCursor);
                    Console.Write($" {systemPath.ToString()} ");
                }
                catch { }
            }
            else
            {
                Console.SetCursorPosition(leftCenterCursor - centerPath, path.topCursor);
                Console.Write($" {other.SystemPath} ");
            }
        }
        protected void DrawLeftPath()
        {
            Coordinate leftPath = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - 4,
                leftCursor = 1,
                topCursor = Console.WindowTop
            };
            int deleteCharCount = leftMenu.SystemPath.Length - Console.WindowWidth / 2 + 7;
            int leftCenterCursor = Console.WindowWidth / 4 - 1;

            DrawPath(leftMenu, leftPath, deleteCharCount, leftCenterCursor);
        }
        protected void DrawRightPath()
        {
            Coordinate leftPath = new Coordinate
            {
                maxRange = Console.WindowWidth - Console.WindowWidth / 2 - 5,
                leftCursor = Console.WindowWidth / 2 + 1,
                topCursor = Console.WindowTop
            };
            int deleteCharCount = (rightMenu.SystemPath.Length - (Console.WindowWidth - (Console.WindowWidth / 2))) + 7;
            int leftCenterCursor = Console.WindowWidth - Console.WindowWidth / 4 - 2;

            DrawPath(rightMenu, leftPath, deleteCharCount, leftCenterCursor);
        }
        protected void DrawBothPath()
        {
            if (whichInfo == 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            DrawLeftPath();

            if (whichInfo != 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            DrawRightPath();

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        protected void ClearLeftPath()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(1, 0);

            for (int i = 0; i < Console.WindowWidth / 2 - 2; i++)
                Console.Write('═');

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        protected void ClearRightPath()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(Console.WindowWidth / 2 + 1, 0);

            if (Console.WindowWidth % 2 != 0)
                for (int i = 0; i < Console.WindowWidth / 2 - 1; i++)
                    Console.Write('═');
            else
                for (int i = 0; i < Console.WindowWidth / 2 - 2; i++)
                    Console.Write('═');

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        private void MakeFileInfo(ref Dictionary<string, string> fileInfo, ref string name, ref StringBuilder info)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            foreach (var i in fileInfo)
            {
                if (i.Key == "Имя")
                    name = i.Value;
                else if (i.Key == "Байт")
                {
                    if (fileInfo["Имя"] == "..")
                        info.Append("Up ");
                    else if (fileInfo["Байт"] == "Папка")
                        info.Append("Папка ");
                    else
                    {
                        long K, M, G;
                        if (i.Value.Length <= 6 && i.Value.Length >= 4)
                        {
                            K = long.Parse(i.Value) / 1024;
                            info.Append($"{K} K ");
                        }
                        else if (i.Value.Length >= 7 && i.Value.Length <= 9)
                        {
                            M = long.Parse(i.Value) / 1024 / 1024;
                            info.Append($"{M} M ");
                        }
                        else if (i.Value.Length > 9)
                        {
                            G = long.Parse(i.Value) / 1024 / 1024 / 1024;
                            info.Append($"{G} G ");
                        }
                        else
                            info.Append($"{i.Value} ");
                    }
                }
                else if (i.Key == "Дата")
                {
                    for (int j = 0; j < i.Value.Length; j++)
                    {
                        if (j != 6 && j != 7 && j != 16 && j != 17 && j != 18)
                            info.Append(i.Value[j]);
                    }
                }
            }
        }
        private void DrawFileInfo(SystemInformation other, int choice, params Coordinate[] coordinates)
        {
            try
            {
                Coordinate firstInfo = coordinates[0];
                Coordinate infoIf = coordinates[1];
                Coordinate infoElse = coordinates[2];
                Console.ForegroundColor = ConsoleColor.Cyan;

                var fileInfo = other.GetFileInfo(other.DirectoriesAndFiles[choice]);
                string name = string.Empty;
                StringBuilder info = new StringBuilder();

                MakeFileInfo(ref fileInfo, ref name, ref info);

                Console.SetCursorPosition(firstInfo.leftCursor, Console.WindowHeight - 3);
                for (int i = 0; i < name.Length; i++)
                    if (i != firstInfo.maxRange)
                        Console.Write(name[i]);
                    else
                        break;

                if (info.Length > infoIf.maxRange)
                {
                    Console.SetCursorPosition(infoIf.leftCursor, Console.WindowHeight - 3);
                    for (int i = 0; i < info.Length - (info.Length - infoIf.maxRange); i++)
                    {
                        Console.Write(info[i]);
                    }
                }
                else
                {
                    Console.SetCursorPosition(infoElse.leftCursor - info.Length, Console.WindowHeight - 3);
                    Console.Write(info);
                }
            }
            catch { }

            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        protected void DrawLeftFileInfo()
        {
            Coordinate firstInfo = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 2,
                leftCursor = 1
            };
            Coordinate infoIf = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2,
                leftCursor = Console.WindowWidth / 4 + 1,
            };
            Coordinate infoElse = new Coordinate
            {
                leftCursor = Console.WindowWidth / 2 - 1
            };

            DrawFileInfo(leftMenu, leftChoice, firstInfo, infoIf, infoElse);
        }
        protected void DrawRightFileInfo()
        {
            Coordinate firstInfo = new Coordinate
            {
                maxRange = Console.WindowWidth - Console.WindowWidth / 4 - Console.WindowWidth / 2 - 3,
                leftCursor = Console.WindowWidth / 2 + 1
            };
            Coordinate infoIf = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2,
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4,
            };
            Coordinate infoElse = new Coordinate
            {
                leftCursor = Console.WindowWidth - 1
            };

            DrawFileInfo(rightMenu, rightChoice, firstInfo, infoIf, infoElse);
        }
        protected void ClearLeftFileInfo()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(1, Console.WindowHeight - 3);

            for (int i = 0; i < Console.WindowWidth / 2 - 2; i++)
                Console.Write(' ');
        }
        protected void ClearRightFileInfo()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(Console.WindowWidth / 2 + 1, Console.WindowHeight - 3);

            for (int i = 0; i < Console.WindowWidth - Console.WindowWidth / 2 - 2; i++)
                Console.Write(' ');
        }
        private void MakeDirectoryInfo(ref Dictionary<string, string> directoryInfo, ref StringBuilder info)
        {
            foreach (var i in directoryInfo)
            {
                if (i.Key == "Байт")
                {
                    long K, M, G;
                    if (i.Value.Length <= 6 && i.Value.Length >= 4)
                    {
                        K = long.Parse(i.Value) / 1024;
                        info.Append($" {i.Key}: {K} K, ");
                    }
                    else if (i.Value.Length >= 7 && i.Value.Length <= 9)
                    {
                        M = long.Parse(i.Value) / 1024 / 1024;
                        info.Append($" {i.Key}: {M} M, ");
                    }
                    else if (i.Value.Length > 9)
                    {
                        G = long.Parse(i.Value) / 1024 / 1024 / 1024;
                        info.Append($" {i.Key}: {G} G, ");
                    }
                    else
                        info.Append($" {i.Key}: {i.Value}, ");
                }
                else if (i.Key == "Файлов")
                    info.Append($"{i.Key}: {i.Value}, ");
                else if (i.Key == "Папок")
                    info.Append($"{i.Key}: {i.Value} ");
            }
        }
        private void DrawDirectoryInfo(SystemInformation other, Coordinate infoIf, Coordinate infoElse)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            var directoryInfo = other.GetDirectoryInfo();
            StringBuilder info = new StringBuilder();

            MakeDirectoryInfo(ref directoryInfo, ref info);

            int center = info.Length / 2;

            if (infoIf.maxRange < info.Length)
            {
                Console.SetCursorPosition(infoIf.leftCursor, Console.WindowHeight - 2);
                for (int i = 0; i < info.Length; i++)
                {
                    if (i != infoIf.maxRange)
                        Console.Write(info[i]);
                    else
                    {
                        Console.Write("...");
                        break;
                    }
                }
            }
            else
            {
                Console.SetCursorPosition(infoElse.leftCursor - center, Console.WindowHeight - 2);
                Console.Write(info.ToString());
            }
        }
        protected void DrawLeftDirectoryInfo()
        {
            Coordinate infoIf = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - 5,
                leftCursor = 1
            };
            Coordinate infoElse = new Coordinate
            {
                leftCursor = Console.WindowWidth / 4 - 1
            };

            DrawDirectoryInfo(leftMenu, infoIf, infoElse);
        }
        protected void DrawRightDirectoryInfo()
        {
            Coordinate infoIf = new Coordinate
            {
                maxRange = Console.WindowWidth - Console.WindowWidth / 2 - 5,
                leftCursor = Console.WindowWidth / 2 + 1
            };
            Coordinate infoElse = new Coordinate
            {
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4
            };

            DrawDirectoryInfo(rightMenu, infoIf, infoElse);
        }
        private void ClearLeftDirectoryInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(1, Console.WindowHeight - 2);

            for (int i = 0; i < Console.WindowWidth / 2 - 5; i++)
                Console.Write('═');

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            DrawLeftDirectoryInfo();
        }
        private void ClearRightDirectoryInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(Console.WindowWidth / 2 + 1, Console.WindowHeight - 2);

            for (int i = 0; i < Console.WindowWidth - Console.WindowWidth / 2 - 5; i++)
                Console.Write('═');

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            DrawRightDirectoryInfo();
        }
        protected void ReDrawFullLeftWindow()
        {
            DrawLeftWindow();

            ClearLeftPath();
            DrawLeftPath();

            ClearLeftFileInfo();
            DrawLeftFileInfo();

            ClearLeftDirectoryInfo();
            DrawLeftDirectoryInfo();
        }
        protected void ReDrawFullRightWindow()
        {
            DrawRightWindow();

            ClearRightPath();
            DrawRightPath();

            ClearRightFileInfo();
            DrawRightFileInfo();

            ClearRightDirectoryInfo();
            DrawRightDirectoryInfo();
        }
        protected void DrawStringName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(Console.WindowWidth / 8 - 1, 1);
            Console.Write("Имя");
            Console.SetCursorPosition(Console.WindowWidth / 2 - Console.WindowWidth / 8 - 2, 1);
            Console.Write("Имя");
            Console.SetCursorPosition(Console.WindowWidth / 2 + Console.WindowWidth / 8 - 1, 1);
            Console.Write("Имя");
            Console.SetCursorPosition((Console.WindowWidth + Console.WindowWidth - Console.WindowWidth / 4) / 2 - 2, 1);
            Console.Write("Имя");

            Console.ForegroundColor = ConsoleColor.White;
        }
        private void DrawWindow(SystemInformation other, int choice, bool whichWindow, params Coordinate[] coordinates)
        {
            Coordinate firstWindow = coordinates[0];
            Coordinate secondWindow = coordinates[1];

            Console.CursorTop = 2;
            int i;
            for (i = 0; i < other.DirectoriesAndFiles.Length; i++)
            {
                if (i == choice && whichWindow == true)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    ClearWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.CursorTop);
                }
                else
                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                DrawWindowElement(firstWindow.maxRange, firstWindow.leftCursor, Console.CursorTop, other, i);
                Console.CursorTop++;

                if (Console.CursorTop == Console.WindowHeight - 4)
                    break;
            }
            i++;
            Console.CursorTop = 2;
            for (; i < other.DirectoriesAndFiles.Length; i++)
            {
                if (i == choice && whichWindow == true)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    ClearWindowElement(secondWindow.maxRange, secondWindow.leftCursor, Console.CursorTop);
                }
                else
                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                DrawWindowElement(secondWindow.maxRange, secondWindow.leftCursor, Console.CursorTop, other, i);

                Console.CursorTop++;

                if (Console.CursorTop == Console.WindowHeight - 4)
                    break;
            }
        }
        protected void DrawLeftWindow()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 1,
                leftCursor = 1
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2,
                leftCursor = Console.WindowWidth / 4 + 1
            };

            if (whichInfo == 0)
                DrawWindow(leftMenu, leftChoice, true, firstWindow, secondWindow);
            else
                DrawWindow(leftMenu, leftChoice, false, firstWindow, secondWindow);
        }
        protected void DrawRightWindow()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1,
                leftCursor = Console.WindowWidth / 2 + 1
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1,
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4
            };

            if (whichInfo != 0)
                DrawWindow(rightMenu, rightChoice, true, firstWindow, secondWindow);
            else
                DrawWindow(rightMenu, rightChoice, false, firstWindow, secondWindow);
        }
        private void ClearWindow(SystemInformation other, params Coordinate[] coordinates)
        {
            Coordinate firstWindow = coordinates[0];
            Coordinate secondWindow = coordinates[1];

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            int i;
            Console.CursorTop = 2;

            for (i = 0; i < other.DirectoriesAndFiles.Length; i++)
            {
                Console.SetCursorPosition(firstWindow.leftCursor, Console.CursorTop);

                for (int j = 0; j != firstWindow.maxRange; j++)
                    Console.Write(' ');

                Console.CursorTop++;

                if (Console.CursorTop == Console.WindowHeight - 4)
                    break;
            }

            Console.CursorTop = 2;

            for (; i < other.DirectoriesAndFiles.Length; i++)
            {
                Console.SetCursorPosition(secondWindow.leftCursor, Console.CursorTop);

                for (int j = 0; j != secondWindow.maxRange; j++)
                    Console.Write(' ');

                Console.CursorTop++;

                if (Console.CursorTop == Console.WindowHeight - 4)
                    break;
            }
        }
        protected void ClearLeftWindow()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 1,
                leftCursor = 1
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth / 2 - Console.WindowWidth / 4 - 2,
                leftCursor = Console.WindowWidth / 4 + 1
            };

            ClearWindow(leftMenu, firstWindow, secondWindow);
        }
        protected void ClearRightWindow()
        {
            Coordinate firstWindow = new Coordinate
            {
                maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1,
                leftCursor = Console.WindowWidth / 2 + 1
            };
            Coordinate secondWindow = new Coordinate
            {
                maxRange = Console.WindowWidth - (Console.WindowWidth - Console.WindowWidth / 4) - 1,
                leftCursor = Console.WindowWidth - Console.WindowWidth / 4
            };

            ClearWindow(rightMenu, firstWindow, secondWindow);
        }
        protected void DrawHelpInfo()
        {
            List<string> helpInfo = new List<string>
            {
                "F1",
                "Создать ",
                "F2",
                "Удалить ",
                "F3",
                "Переместить ",
                "F4",
                "Копировать ",
                "F5",
                "Переименовать ",
                "F6",
                "Консоль ",
                "F7",
                "СменитьДиск ",
                "F8",
                "Выход"
            };

            int lenght = 0;
            foreach (var i in helpInfo)
                lenght += i.Length;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            for (int i = 0; i < Console.WindowWidth - 1; i++)
                Console.Write(' ');

            int centerInfo = lenght / 2;
            int centerCursor = Console.WindowWidth / 2;

            if (lenght < Console.WindowWidth - 1)
                Console.SetCursorPosition(centerCursor - centerInfo, Console.WindowHeight - 1);
            else
                Console.SetCursorPosition(0, Console.WindowHeight - 1);

            int count = 0;
            for (int i = 0; i < helpInfo.Count; i++)
            {
                if (i % 2 == 0)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;
                for (int j = 0; j < helpInfo[i].Length; j++)
                {
                    if (count < Console.WindowWidth - 1)
                    {
                        Console.Write(helpInfo[i][j]);
                        count++;
                    }
                    else
                        break;
                }
            }
        }
        private void DrawPopUpMenuFrame(Coordinate frame)
        {
            Console.CursorTop = 2;
            for (int i = 0; i < popUpMenu.elements.Length + 2; i++)
            {
                Console.SetCursorPosition(frame.leftCursor, Console.CursorTop);
                for (int j = 0; j < frame.maxRange + 1; j++)
                {
                    Console.Write(' ');
                }
                Console.CursorTop++;
            }
            Console.SetCursorPosition(frame.leftCursor, 2);
            Console.Write('╔');
            for (int i = 0; i < frame.maxRange - 1; i++)
                Console.Write('═');
            Console.Write('╗');
            Console.CursorTop = 3;
            for (int i = 0; i < popUpMenu.elements.Length; i++)
            {
                Console.SetCursorPosition(frame.leftCursor, Console.CursorTop);
                Console.Write('║');
                Console.SetCursorPosition(frame.leftCursor + frame.maxRange, Console.CursorTop);
                Console.Write('║');

                Console.CursorTop++;
            }
            Console.SetCursorPosition(frame.leftCursor, Console.CursorTop);
            Console.Write('╚');
            for (int i = 0; i < frame.maxRange - 1; i++)
                Console.Write('═');
            Console.Write('╝');

            var text = popUpMenu.text;
            var center = text.Length / 2;
            if (text.Length > frame.maxRange - 1)
            {
                Console.SetCursorPosition(frame.leftCursor + 1, 2);
                for (int i = 0; i < text.Length; i++)
                    if (i < frame.maxRange - 1)
                        Console.Write(text[i]);
                    else
                        break;
            }
            else
            {
                Console.SetCursorPosition((frame.leftCursor + frame.maxRange / 2) - center, 2);
                Console.Write(text);
            }
            var bottomText = popUpMenu.bottomText;
            var bottomCenter = bottomText.Length / 2;
            if (bottomText.Length > frame.maxRange - 1)
            {
                Console.SetCursorPosition(frame.leftCursor + 1, popUpMenu.elements.Length + 3);
                for (int i = 0; i < bottomText.Length; i++)
                    if (i < frame.maxRange - 1)
                        Console.Write(bottomText[i]);
                    else
                        break;
            }
            else
            {
                Console.SetCursorPosition((frame.leftCursor + frame.maxRange / 2) - bottomCenter, popUpMenu.elements.Length + 3);
                Console.Write(bottomText);
            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        private void DrawPopUpMenuElements(Coordinate elements)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorTop = 3;

            for (int i = 0; i < popUpMenu.elements.Length; i++)
            {
                Console.SetCursorPosition(elements.leftCursor + 1, Console.CursorTop);
                for (int j = 0; j != elements.maxRange - 1; j++)
                {
                    if (i == popUpChoice)
                        Console.BackgroundColor = ConsoleColor.Black;
                    else
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write(' ');
                }
                if (popUpMenu.elements[i].Length > elements.maxRange - 1)
                {
                    Console.SetCursorPosition(elements.leftCursor + 1, Console.CursorTop);
                    for (int j = 0; j < popUpMenu.elements[i].Length; j++)
                    {
                        if (j != elements.maxRange - 1)
                            Console.Write(popUpMenu.elements[i][j]);
                        else
                            break;
                    }
                }
                else
                {
                    int center = popUpMenu.elements[i].Length / 2;
                    Console.SetCursorPosition((elements.leftCursor + elements.maxRange / 2) - center, Console.CursorTop);
                    Console.Write(popUpMenu.elements[i]);
                }

                Console.CursorTop++;
            }
        }
        protected void LeftPopUpMenuFrame()
        {
            Coordinate frame = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 / 2,
                leftCursor = 1
            };
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            DrawPopUpMenuFrame(frame);
        }
        protected void LeftPopUpMenuElements()
        {
            Coordinate elements = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 / 2,
                leftCursor = 1
            };
            DrawPopUpMenuElements(elements);
        }
        protected void RightPopUpMenuFrame()
        {
            Coordinate frame = new Coordinate
            {
                maxRange = ((Console.WindowWidth - Console.WindowWidth / 2) / 2) / 2,
                leftCursor = Console.WindowWidth / 2 + 1
            };
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            DrawPopUpMenuFrame(frame);
        }
        protected void RightPopUpMenuElements()
        {
            Coordinate elements = new Coordinate
            {
                maxRange = ((Console.WindowWidth - Console.WindowWidth / 2) / 2) / 2,
                leftCursor = Console.WindowWidth / 2 + 1
            };
            DrawPopUpMenuElements(elements);
        }
        private void ClearPopUpMenu(Coordinate coordinate)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.CursorTop = 2;

            for (int i = 0; i < popUpMenu.elements.Length + 2; i++)
            {
                Console.SetCursorPosition(coordinate.leftCursor, Console.CursorTop);

                for (int j = 0; j != coordinate.maxRange; j++)
                    Console.Write(' ');

                Console.CursorTop++;
            }
            if (whichInfo == 0)
            {
                if (leftChoice >= (Console.WindowHeight - 6) * 2)
                    LeftArrowMoveOutOfPlace();
                else
                    DrawLeftWindow();
            }
            else
            {
                if (rightChoice >= (Console.WindowHeight - 6) * 2)
                    RightArrowMoveOutOfPlace();
                else
                    DrawRightWindow();
            }
        }
        protected void ClearLeftPopUpMenu()
        {
            Coordinate coordinate = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 1,
                leftCursor = 1
            };
            ClearPopUpMenu(coordinate);
        }
        protected void ClearRightPopUpMenu()
        {
            Coordinate coordinate = new Coordinate
            {
                maxRange = (Console.WindowWidth - 1 - Console.WindowWidth / 4) - Console.WindowWidth / 2 - 1,
                leftCursor = Console.WindowWidth / 2 + 1
            };
            ClearPopUpMenu(coordinate);
        }
        protected void LeftPopUpMenuFrameWithInput()
        {
            Coordinate frame = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 2,
                leftCursor = 1
            };
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            DrawPopUpMenuFrame(frame);
        }
        protected void RightPopUpMenuFrameWithInput()
        {
            Coordinate frame = new Coordinate
            {
                maxRange = ((Console.WindowWidth - Console.WindowWidth / 2) / 2) - 3,
                leftCursor = Console.WindowWidth / 2 + 1
            };
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            DrawPopUpMenuFrame(frame);
        }
        protected void ErrorAlert(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Coordinate frame;
            if (whichInfo == 0)
            {
                frame = new Coordinate
                {
                    maxRange = Console.WindowWidth / 2 - 3,
                    leftCursor = 1
                };
            }
            else
            {
                frame = new Coordinate
                {
                    maxRange = Console.WindowWidth - Console.WindowWidth / 2 - 3,
                    leftCursor = Console.WindowWidth / 2 + 1
                };
            }

            popUpMenu.text = "Ошибка";
            popUpMenu.bottomText = "Enter";

            int j = 0;
            int count = 1;
            StringBuilder errorText = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                if (j != frame.maxRange - 1)
                {
                    errorText.Append(message[i]);
                    j++;
                }
                else
                {
                    errorText.Append('\n');
                    i--;
                    j = 0;
                    count++;
                }
            }
            popUpMenu.elements = new string[count];
            DrawPopUpMenuFrame(frame);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(frame.leftCursor + 1, 3);
            for (int i = 0; i < errorText.Length; i++)
            {
                if (errorText[i] != '\n')
                    Console.Write(errorText[i]);
                else
                    Console.SetCursorPosition(frame.leftCursor + 1, ++Console.CursorTop);
            }
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    break;
            }
            DrawFullWindow();
        }
    }
}
