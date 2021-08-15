using System;

namespace FileManager.ConsoleUI.Interfaces
{
    public interface IWindowSizeMonitoring : IDisposable
    {
        event Action WindowSizeChanged;
        void Start();
        void Stop();
    }
}