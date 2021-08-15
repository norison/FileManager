using FileManager.Core;
using FileManager.SystemInformation;

namespace FileManager.ConsoleUI
{
    public enum WindowPart
    {
        Left,
        Right
    }

    public class FileManager : IFileManager
    {
        #region Private Fields

        private readonly IDirectoryManager _directoryManager;
        private readonly IPainter _leftWindowPainter;
        private readonly IPainter _rightWindowPainter;

        private WindowPart _selectedWindowPart;
        private IPainter _selectedPainter;

        #endregion

        #region Constructor

        public FileManager(IDirectoryManager directoryManager, IPainter leftWindowPainter, IPainter rightWindowPainter)
        {
            _directoryManager = directoryManager;
            _leftWindowPainter = leftWindowPainter;
            _rightWindowPainter = rightWindowPainter;

            _selectedWindowPart = WindowPart.Left;
            _selectedPainter = leftWindowPainter;
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            _leftWindowPainter.DrawWindow();
            _rightWindowPainter.DrawWindow();
        }

        #endregion

        #region Private Methods

        private void SwitchWindow()
        {
            if (_selectedWindowPart == WindowPart.Left)
            {
                _selectedWindowPart = WindowPart.Right;
                _selectedPainter = _rightWindowPainter;
            }
            else
            {
                _selectedWindowPart = WindowPart.Left;
                _selectedPainter = _leftWindowPainter;
            }
        }

        #endregion
    }
}