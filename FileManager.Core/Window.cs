using System;
using FileManager.Core.Interfaces;
using FileManager.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FileManager.Core
{
    public class Window : IWindow
    {
        #region Private Fields

        private readonly IWindowSizeMonitoring _windowSizeMonitoring;
        private readonly IFileSystem _fileSystem;
        private readonly IWindowPresenter _windowPresenter;

        private bool _isActive;
        private IList<EntryInfo> _entryInfos;
        private int _selectedItemIndex;

        #endregion

        #region Properties

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;

                if (value)
                {
                    _windowPresenter.HighlightEntry(_selectedItemIndex, _entryInfos);
                    _windowPresenter.ShowHighlightedPath(_fileSystem.Path);
                }
                else
                {
                    _windowPresenter.DehighlightEntry(_selectedItemIndex, _entryInfos);
                    _windowPresenter.ShowUnhighlightedPath(_fileSystem.Path);
                }
            }
        }

        #endregion

        #region Constructor

        public Window(IWindowSizeMonitoring windowSizeMonitoring, IFileSystem fileSystem, IWindowPresenter windowPresenter)
        {
            _windowSizeMonitoring = windowSizeMonitoring;
            _fileSystem = fileSystem;
            _windowPresenter = windowPresenter;

            _windowSizeMonitoring.WindowSizeChanged += WindowSizeMonitoringOnWindowSizeChanged;

            _selectedItemIndex = 0;

            UpdateEntryInfos();
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

        #region Methods

        public void ShowWindow()
        {
            try
            {
                _windowPresenter.ClearWindow();
                _windowPresenter.ShowBorder();
                _windowPresenter.ShowHeader();

                UpdateWindowElements();
            }
            catch
            {
                // do nothing
            }
        }

        public void MoveUp()
        {
            if (_selectedItemIndex == 0) return;

            _windowPresenter.DehighlightEntry(_selectedItemIndex, _entryInfos);
            _selectedItemIndex--;
            _windowPresenter.HighlightEntry(_selectedItemIndex, _entryInfos);
            _windowPresenter.ShowEntryInfo(_entryInfos[_selectedItemIndex]);
        }

        public void MoveDown()
        {
            if (_selectedItemIndex == _entryInfos.Count - 1) return;

            _windowPresenter.DehighlightEntry(_selectedItemIndex, _entryInfos);
            _selectedItemIndex++;
            _windowPresenter.HighlightEntry(_selectedItemIndex, _entryInfos);
            _windowPresenter.ShowEntryInfo(_entryInfos[_selectedItemIndex]);
        }

        public void Execute()
        {
            try
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

                UpdateWindowElements();
            }
            catch(Exception exception)
            {
                // TODO: add error popup
            }
        }

        private void UpdateWindowElements()
        {
            UpdateEntryInfos();
            SetCorrectSelectedItemIndex();

            _windowPresenter.ShowSystemEntries(_entryInfos);
            _windowPresenter.ShowEntryInfo(_entryInfos[_selectedItemIndex]);
            _windowPresenter.ShowFolderInfo($"Bytes: {_fileSystem.Bytes}. Folders: {_fileSystem.FoldersCount}. Files: {_fileSystem.FilesCount}");

            if (IsActive)
            {
                _windowPresenter.ShowHighlightedPath(_fileSystem.Path);
                _windowPresenter.HighlightEntry(_selectedItemIndex, _entryInfos);
            }
            else
            {
                _windowPresenter.ShowUnhighlightedPath(_fileSystem.Path);
            }
        }

        private void UpdateEntryInfos()
        {
            _entryInfos = _fileSystem.EntryInfos.ToList();
            EntryInfosAddBackToParent();
        }

        private void EntryInfosAddBackToParent()
        {
            if (_fileSystem.IsRoot) return;

            var backEntryInfo = new EntryInfo
            {
                Name = "..",
                Attributes = FileAttributes.Directory
            };

            _entryInfos.Insert(0, backEntryInfo);
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