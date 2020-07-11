using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SystemInfo;

namespace CMD
{
    public class ConsoleMenu
    {
        private SystemInformation info;
        public SystemInformation Info
        {
            get => info;
            set
            {
                info = value;
            }
        }
        private string input;
        private List<string> history;

        public ConsoleMenu()
        {
            input = string.Empty;
            history = new List<string>();
        }
        public string Start()
        {
            Console.CursorVisible = true;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();

            Console.WriteLine("(c) Корпорация norison (norison Corporation), 2019. Все права их нет.\n");

            while (true)
            {
                try
                {
                    Console.Write($"{info.SystemPath}>");
                    input = Console.ReadLine();
                    history.Add(input);

                    if (input == "help") HELP();
                    else if (input.IndexOf(':') == 1) ChangeDrive();
                    else if (input == "history") History();
                    else if (input == "dir") DIR();
                    else if (input == "cd..") CDBack();
                    else if (input.Contains("cd ")) CD();
                    else if (input.Contains("md ")) MD();
                    else if (input.Contains("mf ")) MF();
                    else if (input.Contains("delete ")) DELETE();
                    else if (input.Contains("move ")) MOVE();
                    else if (input.Contains("copy ")) COPY();
                    else if (input.Contains("rename ")) RENAME();
                    else if (input.Contains("read ")) ReadText();
                    else if (input.Contains("open ")) Open();
                    else if (input == "cls") Console.Clear();
                    else if (input == "exit") { Console.CursorVisible = false; return info.SystemPath; }
                    else if (input == "") continue;
                    else Console.WriteLine($"\"{input}\" не является внутренней или внешней\nкомандой, исполняемой программой или пакетным файлом.\n");
                }
                catch (Exception ex) { Console.WriteLine($"{ex.Message}\n"); }
            }
        }
        private string[] SplitInput()
        {
            List<string> str = new List<string>();
            string temp = string.Empty;
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"' && count == 0)
                {
                    count++;
                }
                else if (input[i] != '"' && count == 1)
                {
                    temp += input[i];
                }
                else if (input[i] == '"' && count == 1)
                    count--;
                else if (input[i] != ' ' && count != 1)
                    temp += input[i];
                else
                {
                    str.Add(temp);
                    temp = string.Empty;
                    count = 0;
                }
            }
            str.Add(temp);

            return str.ToArray();
        }
        private void HELP()
        {
            Console.WriteLine();
            Console.WriteLine("cls\tОчищает экран.");
            Console.WriteLine("dir\tВывод списка файлов и подпапок из указанной папки.");
            Console.WriteLine("md\tСоздает каталог.");
            Console.WriteLine("mf\tСоздает файл.");
            Console.WriteLine("delete\tУдаление одного или нескольких файлов.");
            Console.WriteLine("copy\tКопирование одного или нескольких файлов в другое место.");
            Console.WriteLine("move\tПеремещает один или несколько файлов из одного каталога в другой.");
            Console.WriteLine("rename\tПереименование одного или нескольких файлов.");
            Console.WriteLine("read\tВывод содержимого файла по указанному пути.");
            Console.WriteLine("open\tОткрывает папку или файл.");
            Console.WriteLine("history\t Вывод истории введенных команд.");
            Console.WriteLine("exit\tВыход из консоли");
            Console.WriteLine();
        }
        private void History()
        {
            Console.WriteLine();
            foreach (var i in history)
                Console.WriteLine(i);
            Console.WriteLine();
        }
        private void DIR()
        {
            info.ReadFileSystemEntries();
            Console.WriteLine($"\nСодержимое папки {info.SystemPath}\n");

            var directoryInfo = info.GetDirectoryInfo();

            foreach (var i in info.DirectoriesAndFiles)
            {
                var fileInfo = info.GetFileInfo(i);

                Console.WriteLine($"{fileInfo["Дата"]}\t{fileInfo["Байт"]}\t{fileInfo["Имя"]}");
            }

            Console.WriteLine($"\t\t{directoryInfo["Файлов"]} файлов\t{directoryInfo["Байт"]} байт");
            Console.WriteLine($"\t\t{directoryInfo["Папок"]} папок\t{directoryInfo["Байт свободно"]} байт свободно");
            Console.WriteLine();
        }
        public void CDBack()
        {
            info.SystemPath = info.GetParent();
            Console.WriteLine();
        }
        private void CD()
        {
            var splitInput = SplitInput();
            if (splitInput.Length == 2)
            {
                bool t = false;
                bool wrongPath = true;
                foreach (var i in SystemInformation.LogicalDrives)
                    if (info.SystemPath == i.ToString())
                    {
                        if (Directory.Exists(Path.Combine(info.SystemPath, splitInput[1])))
                        {
                            info.SystemPath = Path.Combine(info.SystemPath, splitInput[1]);
                            wrongPath = false;
                        }
                        t = true;
                    }
                if (!t)
                    if (Directory.Exists(Path.Combine(info.SystemPath, splitInput[1])))
                    {
                        info.SystemPath = Path.Combine(info.SystemPath, splitInput[1]);
                        wrongPath = false;
                    }
                if (wrongPath)
                    throw new Exception("Системе не удается найти указанный путь.");
            }
            Console.WriteLine();
        }
        private void MD()
        {
            var splitInput = SplitInput();
            if (splitInput.Length >= 2)
            {
                for (int i = 1; i < splitInput.Length; i++)
                    info.CreateDirectory(Path.Combine(info.SystemPath, splitInput[i]));
            }
            Console.WriteLine();
        }
        private void MF()
        {
            var splitInput = SplitInput();
            if (splitInput.Length >= 2)
            {
                for (int i = 1; i < splitInput.Length; i++)
                    info.CreateFile(Path.Combine(info.SystemPath, splitInput[i]));
            }
            Console.WriteLine();
        }
        private void DELETE()
        {
            var splitInput = SplitInput();
            if (splitInput.Length >= 2)
                for (int i = 1; i < splitInput.Length; i++)
                    info.Delete(Path.Combine(info.SystemPath, splitInput[i]));

            Console.WriteLine();
        }
        private void ChangeDrive()
        {
            foreach (var i in SystemInformation.LogicalDrives)
                if (i.ToString() == $"{input}\\")
                    info.SystemPath = $"{input}\\";
            Console.WriteLine();
        }
        private void MOVE()
        {
            var splitInput = SplitInput();

            if (splitInput.Length == 3)
                info.Move(Path.Combine(info.SystemPath, splitInput[1]), Path.Combine(splitInput[2], splitInput[1]));
            Console.WriteLine();
        }
        private void COPY()
        {
            var splitInput = SplitInput();

            if (splitInput.Length == 3)
                info.Copy(Path.Combine(info.SystemPath, splitInput[1]), Path.Combine(splitInput[2], splitInput[1]));
            Console.WriteLine();
        }
        private void RENAME()
        {
            var splitInput = SplitInput();

            if (splitInput.Length == 3)
                info.Move(Path.Combine(info.SystemPath, splitInput[1]), Path.Combine(info.SystemPath, splitInput[2]));
            Console.WriteLine();
        }
        private void ReadText()
        {
            var splitInput = SplitInput();
            if (splitInput.Length == 2)
            {
                string text = File.ReadAllText(Path.Combine(info.SystemPath, splitInput[1]));
                Console.WriteLine(text);
            }
            Console.WriteLine();
        }
        private void Open()
        {
            var splitInput = SplitInput();
            if (splitInput.Length == 2)
                Process.Start(Path.Combine(info.SystemPath, splitInput[1]));
        }
    }
}
