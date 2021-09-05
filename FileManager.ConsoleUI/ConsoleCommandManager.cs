using FileManager.Core;
using FileManager.Core.Interfaces;
using System;

namespace FileManager.ConsoleUI
{
    public class ConsoleCommandManager : ICommandManager
    {
        public Command RequestCommand()
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    return Command.Up;
                case ConsoleKey.DownArrow:
                    return Command.Down;
                case ConsoleKey.Enter:
                    return Command.Action;
                case ConsoleKey.Tab:
                    return Command.Switch;
                case ConsoleKey.Escape:
                    return Command.Exit;
                default:
                    return Command.Unknown;
            }
        }
    }
}
