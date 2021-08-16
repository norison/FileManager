using System;
using System.Collections.Generic;
using System.Linq;
using FileManager.ConsoleUI.Interfaces;
using FileManager.SystemInformation;

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

        #region Constructor

        public WindowManager(IWindowSizeMonitoring windowSizeMonitoring, IDirectoryManager directoryManager, IPainter painter)
        {
            _windowSizeMonitoring = windowSizeMonitoring;
            _directoryManager = directoryManager;
            _painter = painter;

            _windowSizeMonitoring.WindowSizeChanged += WindowSizeMonitoringOnWindowSizeChanged;

            _selectedItemIndex = 0;
            _entryInfos = GetEntryInfosAddingBackToRoot();

            _painter.SetEntryItems(_entryInfos);
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
                _painter.DrawPath(_directoryManager.Path);
                _painter.DrawSystemEntries();
            }
            catch
            {
                // do nothing
            }
        }

        public void MoveUp()
        {
            if(_selectedItemIndex == 0) return;

            _painter.HideItem(_selectedItemIndex);
            _selectedItemIndex--;
            _painter.ShowItem(_selectedItemIndex);
        }

        public void MoveDown()
        {
            if(_selectedItemIndex == _entryInfos.Count - 1) return;

            _painter.HideItem(_selectedItemIndex);
            _selectedItemIndex++;
            _painter.ShowItem(_selectedItemIndex);
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void ShowSelectedItem()
        {
            _painter.ShowItem(_selectedItemIndex);
        }

        public void HideSelectedItem()
        {
            _painter.HideItem(_selectedItemIndex);
        }

        #endregion

        #region Private Methods

        private IList<EntryInfo> GetEntryInfosAddingBackToRoot()
        {
            var infos = _directoryManager.GetEntryInfos();

            if (_directoryManager.IsRoot)
            {
                infos.Insert(0, new EntryInfo { Name = ".." });
            }

            return infos;
        }

        #endregion
    }
}