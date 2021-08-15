using System;

namespace FileManager.ConsoleUI
{
    public interface IWindowSizeMonitoring : IDisposable
    {
        event Action WindowSizeChanged;
        void Start();
        void Stop();
    }
}