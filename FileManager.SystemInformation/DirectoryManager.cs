using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager.SystemInformation
{
    public class DirectoryManager : IDirectoryManager
    {
        #region Private Fields

        private DirectoryInfo _directoryInfo;

        #endregion

        #region Properties

        public string Path => _directoryInfo?.FullName;
        public bool IsRoot => _directoryInfo?.Parent != null;
        public IList<EntryInfo> FileInfos { get; private set; }

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
            FileInfos = _directoryInfo
                .GetFileSystemInfos()
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
    }
}