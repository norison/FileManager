using System;
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

            var exitRequested = false;

            while (!exitRequested)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        _selectedWindowManager.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        _selectedWindowManager.MoveDown();
                        break;
                    case ConsoleKey.Enter:
                        _selectedWindowManager.Execute();
                        break;
                    case ConsoleKey.Tab:
                        SwitchWindow();
                        break;
                    case ConsoleKey.Escape:
                        exitRequested = true;
                        break;

                }
            }
        }

        #endregion

        #region Private Methods

        private void SwitchWindow()
        {
            _selectedWindowManager.HideSelectedItem();

            if (_selectedWindowPart == WindowPart.Left)
            {
                _selectedWindowPart = WindowPart.Right;
                _selectedWindowManager = _rightWindowManager;
            }
            else
            {
                _selectedWindowPart = WindowPart.Left;
                _selectedWindowManager = _leftWindowManager;
            }

            _selectedWindowManager.ShowSelectedItem();
        }

        #endregion
    }
}