using System;

namespace FileManager.Core.Interfaces
{
    public interface IWindow : IDisposable
    {
        bool AutoShowSelectedItem { get; set; }
        void ShowWindow();
        void MoveUp();
        void MoveDown();
        void Execute();
        void ShowSelectedItem();
        void HideSelectedItem();
    }
}