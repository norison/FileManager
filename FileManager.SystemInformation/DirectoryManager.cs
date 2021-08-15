using System.IO;

namespace FileManager.SystemInformation
{
    public class DirectoryManager : IDirectoryManager
    {
        #region Private Fields

        private DirectoryInfo _directoryInfo;
        private FileSystemInfo[] _fileSystemInfos;

        #endregion

        #region Properties

        public string Path => _directoryInfo?.FullName;
        public bool IsRoot => _directoryInfo?.Parent != null;

        #endregion

        #region Constructor

        public DirectoryManager(string path)
        {
            Initialize(path);
        }

        #endregion

        #region Public Methods

        public void ChangeDirectory(string path)
        {
            Initialize(path);
        }

        #endregion

        #region Private Methods

        private void Initialize(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"Directory not found. Path: {path}");
            }

            _directoryInfo = new DirectoryInfo(path);
            _fileSystemInfos = _directoryInfo.GetFileSystemInfos();
        }

        #endregion
    }
}