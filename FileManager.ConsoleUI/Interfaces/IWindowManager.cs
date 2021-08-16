using System;

namespace FileManager.ConsoleUI.Interfaces
{
    public interface IWindowManager : IDisposable
    {
        void DrawWindow();
        void MoveUp();
        void MoveDown();
        void Execute();
        void ShowSelectedItem();
        void HideSelectedItem();
    }
}