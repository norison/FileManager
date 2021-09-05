using FileManager.Core.Interfaces;
using System.Collections.Generic;

namespace FileManager.Core
{
    public class FileManager : IFileManager
    {
        #region Private Fields

        private readonly IList<IWindow> _windows;
        private readonly ICommandManager _commandManager;

        private int _selectedWindowIndex;
        private IWindow _selectedWindow;

        #endregion

        #region Constructor

        public FileManager(IList<IWindow> windows, ICommandManager commandManager)
        {
            _windows = windows;
            _commandManager = commandManager;
        }

        public void Dispose()
        {
            foreach (var window in _windows)
            {
                window.Dispose();
            }
        }

        #endregion

        #region Methods

        public void Run()
        {
            ShowWindows();
            SelectFirstWindowAndActivate();

            var exitRequested = false;

            while (!exitRequested)
            {
                var command = _commandManager.RequestCommand();

                switch (command)
                {
                    case Command.Up:
                        _selectedWindow.MoveUp();
                        break;
                    case Command.Down:
                        _selectedWindow.MoveDown();
                        break;
                    case Command.Action:
                        _selectedWindow.Execute();
                        break;
                    case Command.Switch:
                        SwitchWindow();
                        break;
                    case Command.Exit:
                        exitRequested = true;
                        break;
                }
            }
        }

        private void ShowWindows()
        {
            foreach (var window in _windows)
            {
                window.ShowWindow();
            }
        }

        private void SelectFirstWindowAndActivate()
        {
            _selectedWindowIndex = 0;
            _selectedWindow = _windows[_selectedWindowIndex];
            _selectedWindow.IsActive = true;
        }

        private void SwitchWindow()
        {
            _selectedWindow.IsActive = false;
            _selectedWindowIndex = _selectedWindowIndex == _windows.Count - 1 ? 0 : _selectedWindowIndex + 1;
            _selectedWindow = _windows[_selectedWindowIndex];
            _selectedWindow.IsActive = true;
        }

        #endregion
    }
}