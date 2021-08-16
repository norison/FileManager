using System;

namespace FileManager.ConsoleUI.Interfaces
{
    public interface IWindowManager : IDisposable
    {
        bool AutoShowSelectedItem { get; set; }
        void DrawWindow();
        void MoveUp();
        void MoveDown();
        void Execute();
        void ShowSelectedItem();
        void HideSelectedItem();
    }
}