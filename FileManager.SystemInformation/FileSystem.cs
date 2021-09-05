using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManager.Core.Interfaces;
using FileManager.Models;

namespace FileManager.SystemInformation
{
    public class FileSystem : IFileSystem
    {
        #region Private Fields

        private DirectoryInfo _directoryInfo;

        #endregion

        #region Properties

        public string Path => _directoryInfo?.FullName;
        public bool IsRoot => _directoryInfo?.Parent == null;
        public int FilesCount { get; private set; }
        public int FoldersCount { get; private set; }
        public long Bytes { get; private set; }
        public IEnumerable<EntryInfo> EntryInfos { get; private set; }

        #endregion

        #region Constructor

        public FileSystem(string path)
        {
            Setup(path);
        }

        #endregion

        #region Public Methods

        public void GoToParent()
        {
            if (IsRoot) return;

            Setup(_directoryInfo.Parent?.FullName);
        }

        public void ChangeDirectory(string path)
        {
            Setup(path);
        }

        #endregion

        #region Private Methods

        private void Setup(string path)
        {
            InitializeDirectoryInfo(path);
            InitializeFileInfos();
        }

        private void InitializeDirectoryInfo(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"Directory not found. Path: {path}");
            }

            _directoryInfo = new DirectoryInfo(path);
        }

        private void InitializeFileInfos()
        {
            var directories = _directoryInfo.GetDirectories();
            var files = _directoryInfo.GetFiles();

            FoldersCount = directories.Length;
            FilesCount = files.Length;
            Bytes = files.Sum(fileInfo => fileInfo.Length);

            var systemInfos = new FileSystemInfo[directories.Length + files.Length];
            directories.CopyTo(systemInfos, 0);
            files.CopyTo(systemInfos, directories.Length);

            EntryInfos = GetEntryInfos(systemInfos);
        }

        private IEnumerable<EntryInfo> GetEntryInfos(IEnumerable<FileSystemInfo> systemInfos)
        {
            return systemInfos.Select(info => new EntryInfo
            {
                Name = info.Name,
                FullPath = info.FullName,
                Attributes = info.Attributes,
                Extenstion = info.Extension,
                CreationTime = info.CreationTime,
                Bytes = info.Attributes.HasFlag(FileAttributes.Directory)
                        ? 0
                        : new FileInfo(info.FullName).Length
            });
        }

        #endregion
    }
}