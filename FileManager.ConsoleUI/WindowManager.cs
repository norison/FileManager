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
                _painter.DrawSystemEntries(GetEntryInfosAddingBackToRoot());
            }
            catch
            {
                // do nothing
            }
        }

        public void MoveUp()
        {
            throw new NotImplementedException();
        }

        public void MoveDown()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void ShowSelectedItem()
        {

        }

        public void HideSelectedItem()
        {

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