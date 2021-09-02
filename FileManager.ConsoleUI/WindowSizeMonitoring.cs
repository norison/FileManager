using System;
using System.Timers;
using FileManager.Core.Interfaces;

namespace FileManager.ConsoleUI
{
    public class WindowSizeMonitoring : IWindowSizeMonitoring
    {
        #region Private Fields

        private readonly Timer _timer;

        private int _width;
        private int _height;

        #endregion

        #region Constructor

        public WindowSizeMonitoring(double interval)
        {
            _width = Console.WindowWidth;
            _height = Console.WindowHeight;

            _timer = new Timer(interval);
            _timer.Elapsed += TimerOnElapsed;
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Elapsed -= TimerOnElapsed;
                _timer.Dispose();
            }
        }

        #endregion

        #region Events

        public event Action WindowSizeChanged = () => { };

        #endregion

        #region Event Handlers

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_width != Console.WindowWidth || _height != Console.WindowHeight)
            {
                _width = Console.WindowWidth;
                _height = Console.WindowHeight;

                WindowSizeChanged();
            }
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        #endregion
    }
}