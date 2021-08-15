using FileManager.ConsoleUI.Interfaces;

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

        private readonly IWindowManager _leftWindowManager;
        private readonly IWindowManager _rightWindowManager;

        private WindowPart _selectedWindowPart;
        private IWindowManager _selectedWindowManager;

        #endregion

        #region Constructor

        public FileManager(IWindowManager leftWindowManager, IWindowManager rightWindowManager)
        {
            _leftWindowManager = leftWindowManager;
            _rightWindowManager = rightWindowManager;

            _selectedWindowPart = WindowPart.Left;
            _selectedWindowManager = _leftWindowManager;
        }

        public void Dispose()
        {
            _leftWindowManager?.Dispose();
            _rightWindowManager?.Dispose();
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            _leftWindowManager.DrawWindow();
            _rightWindowManager.DrawWindow();
        }

        #endregion
    }
}