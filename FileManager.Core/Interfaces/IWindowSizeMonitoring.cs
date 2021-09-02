using System;

namespace FileManager.Core.Interfaces
{
    public interface IWindowSizeMonitoring : IDisposable
    {
        event Action WindowSizeChanged;
        void Start();
        void Stop();
    }
}