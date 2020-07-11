using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace SystemInfo
{
    public class SystemInformation
    {
        private string systemPath;
        private static string[] logicalDrives;
        public string SystemPath
        {
            get => systemPath;
            set
            {
                systemPath = value;
                DirectoriesAndFiles = Directory.GetFileSystemEntries(SystemPath);
                DirectoriesAndFiles = SortDirectory();
            }
        }
        public static string[] LogicalDrives => logicalDrives;
        public string[] DirectoriesAndFiles { get; set; }

        public SystemInformation()
        {
            SystemPath = Directory.GetCurrentDirectory();
            logicalDrives = ReadReadyLogicalDrives();
            DirectoriesAndFiles = Directory.GetFileSystemEntries(SystemPath);
            DirectoriesAndFiles = SortDirectory();
        }

        private string[] ReadReadyLogicalDrives()
        {
            var result = from drive in DriveInfo.GetDrives()
                         where drive.IsReady
                         select drive.ToString();

            return result.ToArray();
        }
        private string[] SortDirectory()
        {
            var result = from path in DirectoriesAndFiles
                         orderby (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory descending
                         select path;

            return result.ToArray();
        }
        public void ReadFileSystemEntries()
        {
            DirectoriesAndFiles = Directory.GetFileSystemEntries(SystemPath);
            DirectoriesAndFiles = SortDirectory();
        }
        public string GetParent() => Directory.GetParent(SystemPath).ToString();
        public FileAttributes GetAttributes(string path) => File.GetAttributes(path);
        public Dictionary<string, string> GetDirectoryInfo()
        {
            Dictionary<string, string> directory = new Dictionary<string, string>();

            string directoriesLength = new DirectoryInfo(SystemPath).GetDirectories().Length.ToString();
            string rootFreeSpaceBytes = new DriveInfo(Directory.GetDirectoryRoot(SystemPath)).AvailableFreeSpace.ToString();
            string filesLenght = new DirectoryInfo(SystemPath).GetFiles().Length.ToString();
            long filesBytes = 0;
            foreach (var i in DirectoriesAndFiles)
            {
                if (!((File.GetAttributes(i) & FileAttributes.Directory) == FileAttributes.Directory))
                {
                    long bytes = 0;
                    filesBytes += bytes = new FileInfo(i).Length;
                }
            }
            directory.Add("Байт", filesBytes.ToString());
            directory.Add("Файлов", filesLenght);
            directory.Add("Папок", directoriesLength);
            directory.Add("Байт свободно", rootFreeSpaceBytes);

            return directory;
        }
        public Dictionary<string, string> GetFileInfo(string path)
        {
            Dictionary<string, string> directory = new Dictionary<string, string>();
            string fileName = Path.GetFileName(path);
            string creatorTime = Directory.GetCreationTime(path).ToString();
            string bytes;

            if (!((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory))
            {
                bytes = new FileInfo(path).Length.ToString();
            }
            else
                bytes = "Папка";

            directory.Add("Имя", fileName);
            directory.Add("Байт", bytes);
            directory.Add("Дата", creatorTime);

            return directory;
        }
        public void CreateDirectory(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
                directoryInfo.Create();
        }
        public void CreateFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.CreateNew);
            fs.Close();
        }
        public void Delete(string path)
        {
            var attributes = File.GetAttributes(path);
            if (!((attributes & FileAttributes.System) == FileAttributes.System))
            {
                if (!((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                        Directory.Delete(path, true);
                    else
                        File.Delete(path);
                }
                else { throw new Exception($"Файл является скрытым. {path}"); }
            }
            else { throw new Exception($"Файл является системным. {path}"); }

            Thread.Sleep(300);
        }
        public void Move(string sourcePath, string destPath)
        {
            var attributes = File.GetAttributes(sourcePath);
            if (!((attributes & FileAttributes.System) == FileAttributes.System))
            {
                if (!((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        if (!Directory.Exists(destPath))
                            Directory.Move(sourcePath, destPath);
                    }
                    else
                    {
                        if (!File.Exists(destPath))
                            File.Move(sourcePath, destPath);
                    }
                }
                else { throw new Exception($"Файл является скрытым. {sourcePath}"); }
            }
            else { throw new Exception($"Файл является системным. {sourcePath}"); }

            Thread.Sleep(300);
        }
        public void Rename(string oldName, string newName)
        {
            var attributes = File.GetAttributes(oldName);
            if (!((attributes & FileAttributes.System) == FileAttributes.System))
            {
                if (!((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        if (!Directory.Exists(newName))
                            Directory.Move(oldName, newName);
                    }
                    else
                    {
                        if (!File.Exists(newName))
                            File.Move(oldName, newName);
                    }
                }
                else { throw new Exception($"Файл является скрытым. {oldName}"); }
            }
            else { throw new Exception($"Файл является системным. {oldName}"); }

            Thread.Sleep(300);
        }
        public void Copy(string sourcePath, string destPath)
        {
            var attributes = File.GetAttributes(sourcePath);
            if (!((attributes & FileAttributes.System) == FileAttributes.System))
            {
                if (!((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        DirectoryInfo dir = new DirectoryInfo(sourcePath);

                        DirectoryInfo[] dirs = dir.GetDirectories();
                        if (!Directory.Exists(destPath))
                        {
                            Directory.CreateDirectory(destPath);
                        }

                        FileInfo[] files = dir.GetFiles();
                        foreach (var file in files)
                        {
                            string tempPath = Path.Combine(destPath, file.Name);
                            file.CopyTo(tempPath);
                        }

                        foreach (var subdir in dirs)
                        {
                            string tempPath = Path.Combine(destPath, subdir.Name);
                            Copy(subdir.FullName, tempPath);
                        }
                    }
                    else
                    {
                        File.Copy(sourcePath, destPath);
                    }
                }
                else { throw new Exception($"Файл является скрытым. {sourcePath}"); }
            }
            else { throw new Exception($"Файл является системным. {sourcePath}"); }

            Thread.Sleep(300);
        }
    }
}
