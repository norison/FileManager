using System;
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

        private readonly IWindowSizeMonitoring _windowSizeMonitoring;
        private readonly IDirectoryManager _leftDirectoryManager;
        private readonly IDirectoryManager _rightDirectoryManager;
        private readonly IPainter _leftWindowPainter;
        private readonly IPainter _rightWindowPainter;

        private WindowPart _selectedWindowPart;
        private IPainter _selectedPainter;
        private IDirectoryManager _selectedDirectoryManager;

        #endregion

        #region Constructor

        public FileManager(IWindowSizeMonitoring windowSizeMonitoring, IDirectoryManager leftDirectoryManager, IDirectoryManager rightDirectoryManager, IPainter leftWindowPainter, IPainter rightWindowPainter)
        {
            _windowSizeMonitoring = windowSizeMonitoring ?? throw new ArgumentNullException(nameof(windowSizeMonitoring));
            _leftDirectoryManager = leftDirectoryManager ?? throw new ArgumentNullException(nameof(leftDirectoryManager));
            _rightDirectoryManager = rightDirectoryManager ?? throw new ArgumentNullException(nameof(rightDirectoryManager));
            _leftWindowPainter = leftWindowPainter ?? throw new ArgumentNullException(nameof(leftWindowPainter));
            _rightWindowPainter = rightWindowPainter ?? throw new ArgumentNullException(nameof(rightWindowPainter));

            _selectedWindowPart = WindowPart.Left;
            _selectedPainter = leftWindowPainter;
            _selectedDirectoryManager = leftDirectoryManager;

            _windowSizeMonitoring.WindowSizeChanged += WindowSizeMonitoringOnWindowSizeChanged;
        }

        public void Dispose()
        {
            _windowSizeMonitoring.WindowSizeChanged -= WindowSizeMonitoringOnWindowSizeChanged;
            _windowSizeMonitoring.Dispose();
        }

        #endregion

        #region Event Handlers

        private void WindowSizeMonitoringOnWindowSizeChanged()
        {
            DrawWindow();
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            _windowSizeMonitoring.Start();

            DrawWindow();
        }

        #endregion

        #region Private Methods

        private void DrawWindow()
        {
            try
            {
                _leftWindowPainter.DrawWindow();
                _leftWindowPainter.DrawPath(_leftDirectoryManager.Path);
                _leftWindowPainter.DrawSystemEntries(_leftDirectoryManager.FileInfos, _leftDirectoryManager.IsRoot);

                _rightWindowPainter.DrawWindow();
                _rightWindowPainter.DrawPath(_rightDirectoryManager.Path);
                _rightWindowPainter.DrawSystemEntries(_rightDirectoryManager.FileInfos, _rightDirectoryManager.IsRoot);
            }
            catch
            {
               // do nothing
            }
        }

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