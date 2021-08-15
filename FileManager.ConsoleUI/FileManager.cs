using FileManager.SystemInformation;

namespace FileManager.ConsoleUI
{
    public class FileManager : IFileManager
    {
        #region Private Fields

        private readonly IDirectoryManager _directoryManager;
        private readonly IPainter _painter;

        #endregion

        #region Constructor

        public FileManager(IDirectoryManager directoryManager, IPainter painter)
        {
            _directoryManager = directoryManager;
            _painter = painter;
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            _painter.DrawWindow();
        }

        #endregion
    }
}