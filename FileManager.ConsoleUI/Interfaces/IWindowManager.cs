using System;

namespace FileManager.ConsoleUI.Interfaces
{
    public interface IWindowManager : IDisposable
    {
        void DrawWindow();
        void Up();
        void Down();
        void Enter();
    }
}