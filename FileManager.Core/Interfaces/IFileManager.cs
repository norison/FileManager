using System;

namespace FileManager.Core.Interfaces
{
    public interface IFileManager : IDisposable
    {
        void Run();
    }
}