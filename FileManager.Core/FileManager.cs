using System;
using FileManager.Core.Interfaces;

namespace FileManager.Core
{
    public enum WindowPart
    {
        First,
        Second
    }

    public class FileManager : IFileManager
    {
        #region Private Fields

        private readonly IWindow _firstWindow;
        private readonly IWindow _secondWindow;
        private readonly ICommandManager _commandManager;

        private WindowPart _selectedWindowPart;
        private IWindow _selectedWindow;

        #endregion

        #region Constructor

        public FileManager(IWindow firstWindow, IWindow secondWindow, ICommandManager commandManager)
        {
            _firstWindow = firstWindow;
            _secondWindow = secondWindow;
            _commandManager = commandManager;

            _selectedWindowPart = WindowPart.First;
            _selectedWindow = _firstWindow;
            _selectedWindow.AutoShowSelectedItem = true;
        }

        public void Dispose()
        {
            _firstWindow?.Dispose();
            _secondWindow?.Dispose();
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            _firstWindow.ShowWindow();
            _secondWindow.ShowWindow();

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
                    case Command.Back:
                        exitRequested = true;
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        private void SwitchWindow()
        {
            _selectedWindow.AutoShowSelectedItem = false;
            _selectedWindow.HideSelectedItem();

            if (_selectedWindowPart == WindowPart.First)
            {
                _selectedWindowPart = WindowPart.Second;
                _selectedWindow = _secondWindow;
            }
            else
            {
                _selectedWindowPart = WindowPart.First;
                _selectedWindow = _firstWindow;
            }

            _selectedWindow.AutoShowSelectedItem = true;
            _selectedWindow.ShowSelectedItem();
        }

        #endregion
    }
}