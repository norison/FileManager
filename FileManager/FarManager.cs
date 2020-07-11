using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SystemInfo;
using SadInterface;
using CMD;

namespace FileManager
{
    public class FarManager : VisualInterface
    {
        public void Start()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            AddBackToParent(leftMenu);
            AddBackToParent(rightMenu);

            DrawFullWindow();

            Thread thread = new Thread(WindowSize);
            thread.Start();

            while (true)
            {
                try
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            UpArrow();
                            break;
                        case ConsoleKey.DownArrow:
                            DownArrow();
                            break;
                        case ConsoleKey.Tab:
                            Tab();
                            break;
                        case ConsoleKey.Enter:
                            Enter();
                            break;
                        case ConsoleKey.F7:
                            ChangeDrive();
                            break;
                        case ConsoleKey.F1:
                            Create();
                            break;
                        case ConsoleKey.F2:
                            Delete();
                            break;
                        case ConsoleKey.F3:
                            Move();
                            break;
                        case ConsoleKey.F4:
                            Copy();
                            break;
                        case ConsoleKey.F5:
                            if (keyInfo.Modifiers == ConsoleModifiers.Control)
                                DrawFullWindow();
                            else
                                ReName();
                            break;
                        case ConsoleKey.F6:
                            thread.Abort();
                            StartConsole();
                            DrawFullWindow();
                            thread = new Thread(WindowSize);
                            thread.Start();
                            break;
                        case ConsoleKey.F8:
                            thread.Abort();
                            Console.Clear();
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    thread.Abort();
                    ErrorAlert(ex.Message);
                    thread = new Thread(WindowSize);
                    thread.Start();
                }
            }
        }
        private void StartPopUpMenu(Action frame, Action elements, Action enter, Action escape)
        {
            bool t = false;
            popUpMenu.isActive = true;

            frame();

            while (true)
            {
                if (popUpChoice == -1) popUpChoice = 0;
                else if (popUpChoice == popUpMenu.elements.Length) popUpChoice--;

                elements();

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow: popUpChoice--; break;
                    case ConsoleKey.DownArrow: popUpChoice++; break;
                    case ConsoleKey.Escape:
                        popUpMenu.isActive = false;
                        escape();
                        t = true;
                        break;
                    case ConsoleKey.Enter:
                        popUpMenu.isActive = false;
                        enter();
                        t = true;
                        break;
                }
                if (t)
                    break;
            }
        }
        private void WindowSize()
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;

            while (true)
            {
                try
                {
                    Console.Title = $"norison`s Far Manager Time: {DateTime.Now.ToShortTimeString()}";
                    if (height != Console.WindowHeight || width != Console.WindowWidth)
                    {
                        Console.SetBufferSize(Console.WindowWidth, 9000);
                        height = Console.WindowHeight;
                        width = Console.WindowWidth;
                        Console.Clear();
                        DrawFullWindow();
                    }
                    Thread.Sleep(1000);
                }
                catch { }
            }
        }
        private void UpArrow()
        {
            if (whichInfo != 0)
            {
                RightUpArrow();
                ClearRightFileInfo();
                DrawRightFileInfo();
            }
            else
            {
                LeftUpArrow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();
            }
        }
        private void DownArrow()
        {
            if (whichInfo != 0)
            {
                RightDownArrow();
                ClearRightFileInfo();
                DrawRightFileInfo();
            }
            else
            {
                LeftDownArrow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();
            }
        }
        private void Tab()
        {
            if (whichInfo == 0)
                TABFromLeft();
            else
                TABFromRight();

            DrawBothPath();
        }
        private void Enter()
        {
            if (whichInfo != 0)
            {
                ClearRightWindow();
                Enter(ref rightChoice, rightMenu);
            }
            else
            {
                ClearLeftWindow();
                Enter(ref leftChoice, leftMenu);
            }
        }
        private void Enter(ref int choice, SystemInformation other)
        {
            if (choice == 0 && other.DirectoriesAndFiles[0] == "..")
            {
                other.SystemPath = other.GetParent();
                AddBackToParent(other);
                if (whichInfo == 0)
                    ReDrawFullLeftWindow();
                else
                    ReDrawFullRightWindow();
            }
            else
            {
                if ((File.GetAttributes(other.DirectoriesAndFiles[choice]) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    try
                    {
                        other.SystemPath = other.DirectoriesAndFiles[choice];
                        choice = 0;
                        AddBackToParent(other);
                        if (whichInfo == 0)
                            ReDrawFullLeftWindow();
                        else
                            ReDrawFullRightWindow();
                    }
                    catch (Exception ex)
                    {
                        other.SystemPath = other.GetParent();
                        AddBackToParent(other);
                        ErrorAlert(ex.Message);
                    }
                }
                else
                {
                    Process.Start(other.DirectoriesAndFiles[choice]);
                    if (whichInfo == 0)
                        ReDrawFullLeftWindow();
                    else
                        ReDrawFullRightWindow();
                }
            }
        }
        private void AddBackToParent(SystemInformation other, bool t = false)
        {
            if (!t)
            {
                foreach (var i in SystemInformation.LogicalDrives)
                {
                    if (i.ToString() == other.SystemPath)
                        t = true;
                }
            }

            if (!t)
            {
                string[] temp = new string[other.DirectoriesAndFiles.Length + 1];
                temp[0] = "..";

                for (int i = 0; i < other.DirectoriesAndFiles.Length; i++)
                {
                    temp[i + 1] = other.DirectoriesAndFiles[i];
                }

                other.DirectoriesAndFiles = temp;
            }
        }
        private void StartConsole()
        {
            SystemInformation temp;

            if (whichInfo == 0) temp = leftMenu;
            else temp = rightMenu;

            ConsoleMenu console = new ConsoleMenu
            {
                Info = temp
            };
            temp.SystemPath = console.Start();

            AddBackToParent(temp);

            if (whichInfo != 0) rightChoice = 0;
            else leftChoice = 0;

            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        private void PopUpMenuInfo(string text, string[] elements, Action operation)
        {
            popUpMenu.text = text;
            popUpMenu.elements = elements;
            if (whichInfo == 0)
                StartPopUpMenu(LeftPopUpMenuFrame, LeftPopUpMenuElements, operation, ClearLeftPopUpMenu);
            else
                StartPopUpMenu(RightPopUpMenuFrame, RightPopUpMenuElements, operation, ClearRightPopUpMenu);

            popUpChoice = 0;
        }
        private void ChangeDrive()
        {
            popUpMenu.bottomText = "Enter,Esc";
            if (whichInfo == 0)
                PopUpMenuInfo("Диски", SystemInformation.LogicalDrives, () =>
                {
                    ClearLeftPopUpMenu();
                    ClearLeftWindow();
                    leftMenu.SystemPath = SystemInformation.LogicalDrives[popUpChoice];
                    leftChoice = 0;
                    ReDrawFullLeftWindow();
                });
            else
                PopUpMenuInfo("Диски", SystemInformation.LogicalDrives, () =>
                {
                    ClearRightPopUpMenu();
                    ClearRightWindow();
                    rightMenu.SystemPath = SystemInformation.LogicalDrives[popUpChoice];
                    rightChoice = 0;
                    ReDrawFullRightWindow();
                });
        }
        private void Delete()
        {
            popUpMenu.bottomText = "Enter,Esc";
            if (whichInfo == 0)
                PopUpMenuInfo("Удалить", new string[] { "Нет", "Да" }, DeleteFromLeft);
            else
                PopUpMenuInfo("Удалить", new string[] { "Нет", "Да" }, DeleteFromRight);
        }
        private void DeleteFromLeft()
        {
            if (popUpChoice == 1)
            {
                popUpChoice = 0;

                if (leftMenu.DirectoriesAndFiles[leftChoice] == "..")
                    throw new Exception("Недопустимая папка или файл.");

                ClearLeftPopUpMenu();
                ClearLeftWindow();
                leftMenu.Delete(leftMenu.DirectoriesAndFiles[leftChoice]);
                leftMenu.ReadFileSystemEntries();
                leftChoice = 0;
                AddBackToParent(leftMenu);
                DrawLeftWindow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();

                if (leftMenu.SystemPath == rightMenu.SystemPath)
                {
                    ClearRightWindow();
                    rightMenu.ReadFileSystemEntries();
                    rightChoice = 0;
                    AddBackToParent(rightMenu);
                    DrawRightWindow();
                    ClearRightFileInfo();
                    DrawRightFileInfo();
                }
            }
            else
                ClearLeftPopUpMenu();
        }
        private void DeleteFromRight()
        {
            if (popUpChoice == 1)
            {
                popUpChoice = 0;

                if (rightMenu.DirectoriesAndFiles[rightChoice] == "..")
                    throw new Exception("Недопустимая папки или файл.");

                ClearRightPopUpMenu();
                ClearRightWindow();
                rightMenu.Delete(rightMenu.DirectoriesAndFiles[rightChoice]);
                rightMenu.ReadFileSystemEntries();
                rightChoice = 0;
                AddBackToParent(rightMenu);
                DrawRightWindow();
                ClearRightFileInfo();
                DrawRightFileInfo();

                if (leftMenu.SystemPath == rightMenu.SystemPath)
                {
                    ClearLeftWindow();
                    leftMenu.ReadFileSystemEntries();
                    leftChoice = 0;
                    AddBackToParent(leftMenu);
                    DrawLeftWindow();
                    ClearLeftFileInfo();
                    DrawLeftFileInfo();
                }
            }
            else
                ClearRightPopUpMenu();
        }
        private void Move()
        {
            popUpMenu.bottomText = "Enter,Esc";
            if (whichInfo == 0)
                PopUpMenuInfo("Переместить", new string[] { "Нет", "Да" }, MoveFromLeft);
            else
                PopUpMenuInfo("Переместить", new string[] { "Нет", "Да" }, MoveFromRight);
        }
        private void MoveFromLeft()
        {
            if (popUpChoice == 1)
            {
                popUpChoice = 0;

                if (leftMenu.DirectoriesAndFiles[leftChoice] == "..")
                    throw new Exception("Недопустимая папка или файл.");

                var soursePath = leftMenu.DirectoriesAndFiles[leftChoice];
                var destPath = Path.Combine(rightMenu.SystemPath, Path.GetFileName(leftMenu.DirectoriesAndFiles[leftChoice]));

                ClearLeftPopUpMenu();
                ClearLeftWindow();
                leftMenu.Move(soursePath, destPath);
                leftMenu.ReadFileSystemEntries();
                leftChoice = 0;
                AddBackToParent(leftMenu);
                DrawLeftWindow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();

                ClearRightWindow();
                rightMenu.ReadFileSystemEntries();
                rightChoice = 0;
                AddBackToParent(rightMenu);
                DrawRightWindow();
                ClearRightFileInfo();
                DrawRightFileInfo();
            }
            else
                ClearLeftPopUpMenu();
        }
        private void MoveFromRight()
        {
            if (popUpChoice == 1)
            {
                popUpChoice = 0;

                if (rightMenu.DirectoriesAndFiles[rightChoice] == "..")
                    throw new Exception("Недопустимая папки или файл.");

                var soursePath = rightMenu.DirectoriesAndFiles[rightChoice];
                var destPath = Path.Combine(leftMenu.SystemPath, Path.GetFileName(rightMenu.DirectoriesAndFiles[rightChoice]));

                ClearRightPopUpMenu();
                ClearRightWindow();
                rightMenu.Move(soursePath, destPath);
                rightMenu.ReadFileSystemEntries();
                rightChoice = 0;
                AddBackToParent(rightMenu);
                DrawRightWindow();
                ClearRightFileInfo();
                DrawRightFileInfo();

                ClearLeftWindow();
                leftMenu.ReadFileSystemEntries();
                leftChoice = 0;
                AddBackToParent(leftMenu);
                DrawLeftWindow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();
            }
            else
                ClearRightPopUpMenu();
        }
        private void Copy()
        {
            popUpMenu.bottomText = "Enter,Esc";
            if (whichInfo == 0)
                PopUpMenuInfo("Копировать", new string[] { "Нет", "Да" }, CopyFromLeft);
            else
                PopUpMenuInfo("Копировать", new string[] { "Нет", "Да" }, CopyFromRight);
        }
        private void CopyFromLeft()
        {
            if (popUpChoice == 1)
            {
                popUpChoice = 0;

                if (leftMenu.DirectoriesAndFiles[leftChoice] == "..")
                    throw new Exception("Недопустимая папка или файл.");

                var soursePath = leftMenu.DirectoriesAndFiles[leftChoice];
                var destPath = Path.Combine(rightMenu.SystemPath, Path.GetFileName(leftMenu.DirectoriesAndFiles[leftChoice]));

                ClearLeftPopUpMenu();
                ClearLeftWindow();
                leftMenu.Copy(soursePath, destPath);
                leftChoice = 0;
                DrawLeftWindow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();

                ClearRightWindow();
                rightMenu.ReadFileSystemEntries();
                rightChoice = 0;
                AddBackToParent(rightMenu);
                DrawRightWindow();
                ClearRightFileInfo();
                DrawRightFileInfo();
            }
            else
                ClearLeftPopUpMenu();
        }
        private void CopyFromRight()
        {
            if (popUpChoice == 1)
            {
                popUpChoice = 0;

                if (rightMenu.DirectoriesAndFiles[rightChoice] == "..")
                    throw new Exception("Недопустимая папки или файл.");

                var soursePath = rightMenu.DirectoriesAndFiles[rightChoice];
                var destPath = Path.Combine(leftMenu.SystemPath, Path.GetFileName(rightMenu.DirectoriesAndFiles[rightChoice]));

                ClearRightPopUpMenu();
                ClearRightWindow();
                rightMenu.Copy(soursePath, destPath);
                rightChoice = 0;
                DrawRightWindow();
                ClearRightFileInfo();
                DrawRightFileInfo();

                ClearLeftWindow();
                leftMenu.ReadFileSystemEntries();
                leftChoice = 0;
                AddBackToParent(leftMenu);
                DrawLeftWindow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();
            }
            else
                ClearRightPopUpMenu();
        }
        private void Create()
        {
            popUpMenu.bottomText = "Enter,Esc";
            if (whichInfo == 0)
                PopUpMenuInfo("Создать", new string[] { "Папка", "Файл" }, CreateLeft);
            else
                PopUpMenuInfo("Создать", new string[] { "Папка", "Файл" }, CreateRight);
        }
        private void CreateLeft()
        {
            popUpMenu.input = new StringBuilder();
            Coordinate coordinate = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 3,
                leftCursor = 2
            };
            Action enter = () =>
            {
                leftMenu.CreateDirectory(Path.Combine(leftMenu.SystemPath, popUpMenu.input.ToString()));
                leftMenu.ReadFileSystemEntries();
                leftChoice = 0;
            };
            void reDraw()
            {
                ClearLeftPopUpMenu();
                ClearLeftWindow();
                AddBackToParent(leftMenu);
                DrawLeftWindow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();
                if (leftMenu.SystemPath == rightMenu.SystemPath)
                {
                    rightMenu.ReadFileSystemEntries();
                    ClearRightPopUpMenu();
                    ClearRightWindow();
                    AddBackToParent(rightMenu);
                    DrawRightWindow();
                    ClearRightFileInfo();
                    DrawRightFileInfo();
                }
            }
            if (popUpChoice == 0)
            {
                InputLogic(coordinate, LeftPopUpMenuFrameWithInput, enter, ClearLeftPopUpMenu, reDraw);
            }
            else if (popUpChoice == 1)
            {
                enter = () =>
                {
                    leftMenu.CreateFile(Path.Combine(leftMenu.SystemPath, popUpMenu.input.ToString()));
                    leftMenu.ReadFileSystemEntries();
                    leftChoice = 0;
                };
                InputLogic(coordinate, LeftPopUpMenuFrameWithInput, enter, ClearLeftPopUpMenu, reDraw);
            }
        }
        private void CreateRight()
        {
            popUpMenu.input = new StringBuilder();
            Coordinate coordinate = new Coordinate
            {
                maxRange = ((Console.WindowWidth - Console.WindowWidth / 2) / 2) - 4,
                leftCursor = Console.WindowWidth / 2 + 2
            };
            Action enter = () =>
            {
                rightMenu.CreateDirectory(Path.Combine(rightMenu.SystemPath, popUpMenu.input.ToString()));
                rightMenu.ReadFileSystemEntries();
                rightChoice = 0;
            };
            void reDraw()
            {
                ClearRightPopUpMenu();
                ClearRightWindow();
                AddBackToParent(rightMenu);
                DrawRightWindow();
                ClearRightFileInfo();
                DrawRightFileInfo();
                if (leftMenu.SystemPath == rightMenu.SystemPath)
                {
                    leftMenu.ReadFileSystemEntries();
                    ClearLeftPopUpMenu();
                    ClearLeftWindow();
                    AddBackToParent(leftMenu);
                    DrawLeftWindow();
                    ClearLeftFileInfo();
                    DrawLeftFileInfo();
                }
            }
            if (popUpChoice == 0)
            {
                InputLogic(coordinate, RightPopUpMenuFrameWithInput, enter, ClearRightPopUpMenu, reDraw);
            }
            else if (popUpChoice == 1)
            {
                enter = () =>
                {
                    rightMenu.CreateFile(Path.Combine(rightMenu.SystemPath, popUpMenu.input.ToString()));
                    rightMenu.ReadFileSystemEntries();
                    rightChoice = 0;
                };
                InputLogic(coordinate, RightPopUpMenuFrameWithInput, enter, ClearRightPopUpMenu, reDraw);
            }
        }
        private void ReName()
        {
            popUpMenu.bottomText = "Enter,Esc";
            popUpMenu.text = "Переименование";
            popUpMenu.elements = new string[2];
            if (whichInfo == 0)
                LeftReName();
            else
                RightReName();
        }
        private void LeftReName()
        {
            popUpMenu.input = new StringBuilder();
            Coordinate coordinate = new Coordinate
            {
                maxRange = Console.WindowWidth / 4 - 3,
                leftCursor = 2
            };
            void enter()
            {
                ClearLeftPopUpMenu();
                ClearLeftWindow();
                leftMenu.Rename(leftMenu.DirectoriesAndFiles[leftChoice], Path.Combine(leftMenu.SystemPath, popUpMenu.input.ToString()));
                leftMenu.ReadFileSystemEntries();
                leftChoice = 0;
            }
            void reDraw()
            {
                AddBackToParent(leftMenu);
                DrawLeftWindow();
                ClearLeftFileInfo();
                DrawLeftFileInfo();
                if (leftMenu.SystemPath == rightMenu.SystemPath)
                {
                    rightMenu.ReadFileSystemEntries();
                    ClearRightWindow();
                    AddBackToParent(rightMenu);
                    DrawRightWindow();
                    ClearRightFileInfo();
                    DrawRightFileInfo();
                }
            }

            InputLogic(coordinate, LeftPopUpMenuFrameWithInput, enter, ClearLeftPopUpMenu, reDraw);
        }
        private void RightReName()
        {
            popUpMenu.input = new StringBuilder();
            Coordinate coordinate = new Coordinate
            {
                maxRange = ((Console.WindowWidth - Console.WindowWidth / 2) / 2) - 4,
                leftCursor = Console.WindowWidth / 2 + 2
            };
            void enter()
            {
                ClearRightPopUpMenu();
                ClearRightWindow();
                rightMenu.Rename(rightMenu.DirectoriesAndFiles[rightChoice], Path.Combine(rightMenu.SystemPath, popUpMenu.input.ToString()));
                rightMenu.ReadFileSystemEntries();
                rightChoice = 0;
            }
            void reDraw()
            {
                AddBackToParent(rightMenu);
                DrawRightWindow();
                ClearRightFileInfo();
                DrawRightFileInfo();
                if (leftMenu.SystemPath == rightMenu.SystemPath)
                {
                    leftMenu.ReadFileSystemEntries();
                    ClearLeftWindow();
                    AddBackToParent(leftMenu);
                    DrawLeftWindow();
                    ClearLeftFileInfo();
                    DrawLeftFileInfo();
                }
            }

            InputLogic(coordinate, RightPopUpMenuFrameWithInput, enter, ClearRightPopUpMenu, reDraw);
        }
        private void InputLogic(Coordinate coordinate, Action frame, Action enter, Action escape, Action reDraw)
        {
            popUpMenu.isInputActive = true;
            frame();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(coordinate.leftCursor, 3);
            Console.Write("Введите имя:");
            Console.SetCursorPosition(coordinate.leftCursor, 4);
            Console.CursorVisible = true;
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Backspace)
                {
                    if (popUpMenu.input.Length != coordinate.maxRange)
                    {
                        bool invalidChar = key.KeyChar.ToString().Intersect(Path.GetInvalidFileNameChars()).Any();
                        if (!invalidChar)
                        {
                            popUpMenu.input.Append(key.KeyChar);
                            Console.Write(key.KeyChar);
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (Console.CursorLeft != coordinate.leftCursor)
                    {
                        popUpMenu.input.Remove(popUpMenu.input.Length - 1, 1);
                        Console.CursorLeft--;
                        Console.Write(' ');
                        Console.CursorLeft--;
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    popUpMenu.isInputActive = false;
                    Console.CursorVisible = false;
                    escape();
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    popUpMenu.isInputActive = false;
                    Console.CursorVisible = false;
                    enter();
                    reDraw();
                    break;
                }
            }
        }
    }
}
