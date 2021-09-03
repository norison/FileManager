using System;

namespace FileManager.Core.Interfaces
{
    public interface IWindow : IDisposable
    {
        bool IsActive { get; set; }
        void ShowWindow();
        void MoveUp();
        void MoveDown();
        void Execute();
    }
}