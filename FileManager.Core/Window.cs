using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using FileManager.Core.Interfaces;
using FileManager.Models;

namespace FileManager.Core
{
    public class Window : IWindow
    {
        #region Private Fields

        private readonly IWindowSizeMonitoring _windowSizeMonitoring;
        private readonly IFileSystem _fileSystem;
        private readonly IWindowPresenter _windowPresenter;

        private IList<EntryInfo> _entryInfos;
        private int _selectedItemIndex;

        #endregion

        #region Properties

        public bool AutoShowSelectedItem { get; set; }

        #endregion

        #region Constructor

        public Window(IWindowSizeMonitoring windowSizeMonitoring, IFileSystem fileSystem, IWindowPresenter windowPresenter)
        {
            _windowSizeMonitoring = windowSizeMonitoring;
            _fileSystem = fileSystem;
            _windowPresenter = windowPresenter;

            _windowSizeMonitoring.WindowSizeChanged += WindowSizeMonitoringOnWindowSizeChanged;

            _selectedItemIndex = 0;
            _entryInfos = GetEntryInfosAddingBackToParent();
        }

        public void Dispose()
        {
            _windowSizeMonitoring.WindowSizeChanged -= WindowSizeMonitoringOnWindowSizeChanged;
        }

        #endregion

        #region Event Handlers

        private void WindowSizeMonitoringOnWindowSizeChanged()
        {
            ShowWindow();
        }

        #endregion

        #region Public Methods

        public void ShowWindow()
        {
            try
            {
                _windowPresenter.ClearWindow();
                _windowPresenter.ShowBorder();
                _windowPresenter.ShowHeader();
                _windowPresenter.ShowPath(_fileSystem.Path);
                _windowPresenter.ShowSystemEntries(_entryInfos);

                if (AutoShowSelectedItem)
                {
                    ShowSelectedItem();
                }
            }
            catch
            {
                // do nothing
            }
        }

        public void MoveUp()
        {
            if (_selectedItemIndex == 0) return;

            HideSelectedItem();
            _selectedItemIndex--;
            ShowSelectedItem();
        }

        public void MoveDown()
        {
            if (_selectedItemIndex == _entryInfos.Count - 1) return;

            HideSelectedItem();
            _selectedItemIndex++;
            ShowSelectedItem();
        }

        public void Execute()
        {
            var info = _entryInfos[_selectedItemIndex];

            if (info.Name == "..")
            {
                _fileSystem.GoToParent();
            }
            else if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                _fileSystem.ChangeDirectory(info.FullPath);
            }
            else
            {
                Process.Start(new ProcessStartInfo { FileName = info.FullPath, UseShellExecute = true });
            }

            ClearWindowElements();
            UpdateWindowElements();
        }

        public void ShowSelectedItem()
        {
            _windowPresenter.HighlightEntry(_selectedItemIndex, _entryInfos);
            _windowPresenter.ClearEntryInfo();
            _windowPresenter.ShowEntryInfo(_entryInfos[_selectedItemIndex]);
        }

        public void HideSelectedItem()
        {
            _windowPresenter.DehighlightEntry(_selectedItemIndex, _entryInfos);
            _windowPresenter.ClearEntryInfo();
            _windowPresenter.ShowEntryInfo(_entryInfos[_selectedItemIndex]);
        }

        #endregion

        #region Private Methods

        private IList<EntryInfo> GetEntryInfosAddingBackToParent()
        {
            var infos = _fileSystem.GetEntryInfos();

            if (!_fileSystem.IsRoot)
            {
                infos.Insert(0, new EntryInfo { Name = "..", Attributes = FileAttributes.Directory });
            }

            return infos;
        }

        private void ClearWindowElements()
        {
            _windowPresenter.ClearPath();
            _windowPresenter.ClearSystemEntries();
            _windowPresenter.ClearEntryInfo();
        }

        private void UpdateWindowElements()
        {
            _entryInfos = GetEntryInfosAddingBackToParent();
            SetCorrectSelectedItemIndex();

            _windowPresenter.ShowPath(_fileSystem.Path);
            _windowPresenter.ShowSystemEntries(_entryInfos);

            if (AutoShowSelectedItem)
            {
                ShowSelectedItem();
            }
        }

        private void SetCorrectSelectedItemIndex()
        {
            if (_selectedItemIndex >= _entryInfos.Count)
            {
                _selectedItemIndex = _entryInfos.Count - 1;
            }
        }

        #endregion
    }
}