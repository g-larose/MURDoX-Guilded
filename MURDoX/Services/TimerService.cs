using System.Diagnostics;

namespace MURDoX.Services
{
    public class TimerService : Interfaces.ITimer, IDisposable
    {
        public Stopwatch Timer { get; set; } = new();
        public string StartDate { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;

        public TimerService()
        {
            Start();
        }

        public void Start()
        {
            if (Timer.IsRunning)
                return;
            Timer = new Stopwatch();
            Timer.Start();
            StartDate = DateTime.Now.ToShortDateString();
            StartTime = DateTime.Now.ToShortTimeString();
        }

        public void Stop()
        {
            if (!Timer.IsRunning)
                return;

            Timer.Stop();
            Reset();
        }

        public void Reset()
        {
            if (Timer.IsRunning)
            {
                Stop();
                Timer = null;
                StartTime = "";
                StartDate = "";
            }
            else
            {
                Timer = null;
                StartTime = "";
                StartDate = "";
            }
        }

        public string GetStartDate()
        {
            return StartDate!;
        }

        public string GetStartTime()
        {
            return StartTime!;
        }

        public void Dispose()
        {
            Stop();
            Reset();
            this.Dispose();
        }

    }
}
