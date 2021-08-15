using System;

namespace FileManager.Core
{
    public interface IFileManager : IDisposable
    {
        void Start();
    }
}