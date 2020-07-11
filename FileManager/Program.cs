using System;
using System.IO;

namespace FileManager
{
    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(100, 30);
            Console.SetBufferSize(Console.WindowWidth, 9000);
            FarManager farMenu = new FarManager();
            farMenu.Start();
        }
    }
}
