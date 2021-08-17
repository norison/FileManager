using FileManager.ConsoleUI.Interfaces;
using FileManager.SystemInformation;
using System.Collections.Generic;
using System.IO;

namespace FileManager.ConsoleUI
{
    public class WindowManager : IWindowManager
    {
        #region Private Fields

        private readonly IWindowSizeMonitoring _windowSizeMonitoring;
        private readonly IDirectoryManager _directoryManager;
        private readonly IPainter _painter;

        private IList<EntryInfo> _entryInfos;
        private int _selectedItemIndex;

        #endregion

        #region Properties

        public bool AutoShowSelectedItem { get; set; }

        #endregion

        #region Constructor

        public WindowManager(IWindowSizeMonitoring windowSizeMonitoring, IDirectoryManager directoryManager, IPainter painter)
        {
            _windowSizeMonitoring = windowSizeMonitoring;
            _directoryManager = directoryManager;
            _painter = painter;

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
            DrawWindow();
        }

        #endregion

        #region Public Methods

        public void DrawWindow()
        {
            try
            {
                _painter.ClearWindow();
                _painter.DrawBorder();
                _painter.DrawHeader();

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
                _directoryManager.GoToParent();
            }
            else if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                _directoryManager.ChangeDirectory(info.FullPath);
            }

            ClearWindowElements();
            UpdateWindowElements();
        }

        public void ShowSelectedItem()
        {
            var entryInfo = _entryInfos[_selectedItemIndex];
            _painter.HighlightEntry(_selectedItemIndex, entryInfo);
            _painter.ClearEntryInfo();
            _painter.DrawEntryInfo(entryInfo);
        }

        public void HideSelectedItem()
        {
            var entryInfo = _entryInfos[_selectedItemIndex];
            _painter.DehighlightEntry(_selectedItemIndex, entryInfo);
            _painter.ClearEntryInfo();
            _painter.DrawEntryInfo(entryInfo);
        }

        #endregion

        #region Private Methods

        private IList<EntryInfo> GetEntryInfosAddingBackToParent()
        {
            var infos = _directoryManager.GetEntryInfos();

            if (!_directoryManager.IsRoot)
            {
                infos.Insert(0, new EntryInfo { Name = "..", Attributes = FileAttributes.Directory });
            }

            return infos;
        }

        private void ClearWindowElements()
        {
            _painter.ClearPath();
            _painter.ClearSystemEntries();
            _painter.ClearEntryInfo();
        }

        private void UpdateWindowElements()
        {
            _selectedItemIndex = 0;
            _entryInfos = GetEntryInfosAddingBackToParent();

            _painter.DrawPath(_directoryManager.Path);
            _painter.DrawSystemEntries(_entryInfos);

            if (AutoShowSelectedItem)
            {
                ShowSelectedItem();
            }
        }

        #endregion
    }
}