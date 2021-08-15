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

        private readonly IDirectoryManager _leftDirectoryManager;
        private readonly IDirectoryManager _rightDirectoryManager;
        private readonly IPainter _leftWindowPainter;
        private readonly IPainter _rightWindowPainter;

        private WindowPart _selectedWindowPart;
        private IPainter _selectedPainter;
        private IDirectoryManager _selectedDirectoryManager;

        #endregion

        #region Constructor

        public FileManager(IDirectoryManager leftDirectoryManager, IDirectoryManager rightDirectoryManager, IPainter leftWindowPainter, IPainter rightWindowPainter)
        {
            _leftDirectoryManager = leftDirectoryManager;
            _rightDirectoryManager = rightDirectoryManager;
            _leftWindowPainter = leftWindowPainter;
            _rightWindowPainter = rightWindowPainter;

            _selectedWindowPart = WindowPart.Left;
            _selectedPainter = leftWindowPainter;
            _selectedDirectoryManager = leftDirectoryManager;
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            _leftWindowPainter.DrawWindow();
            _leftWindowPainter.DrawPath(_leftDirectoryManager.Path);

            _rightWindowPainter.DrawWindow();
            _rightWindowPainter.DrawPath(_rightDirectoryManager.Path);
        }

        #endregion

        #region Private Methods

        private void SwitchWindow()
        {
            if (_selectedWindowPart == WindowPart.Left)
            {
                _selectedWindowPart = WindowPart.Right;
                _selectedPainter = _rightWindowPainter;
                _selectedDirectoryManager = _leftDirectoryManager;
            }
            else
            {
                _selectedWindowPart = WindowPart.Left;
                _selectedPainter = _leftWindowPainter;
                _selectedDirectoryManager = _rightDirectoryManager;
            }
        }

        #endregion
    }
}