using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager.SystemInformation
{
    public class DirectoryManager : IDirectoryManager
    {
        #region Private Fields

        private DirectoryInfo _directoryInfo;
        private Dictionary<string, FileSystemInfo> _fileSystemInfos;

        #endregion

        #region Properties

        public string Path => _directoryInfo?.FullName;
        public bool IsRoot => _directoryInfo?.Parent != null;

        #endregion

        #region Constructor

        public DirectoryManager(string path)
        {
            Setup(path);
        }

        #endregion

        #region Public Methods

        public void ChangeDirectory(string path)
        {
            Setup(path);
        }

        public IList<EntryInfo> GetEntryInfos()
        {
            return _fileSystemInfos.Values
                .Select(info => new EntryInfo
                {
                    Name = info.Name,
                    FullPath = info.FullName,
                    Attributes = info.Attributes,
                    Extenstion = info.Extension
                })
                .ToList();
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
            _fileSystemInfos = _directoryInfo.GetFileSystemInfos().ToDictionary(info => info.FullName);
        }

        #endregion
    }
}