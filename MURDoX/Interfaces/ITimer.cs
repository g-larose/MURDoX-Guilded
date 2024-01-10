using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MURDoX.Interfaces
{
    public interface ITimer
    {
        Stopwatch Timer { get; set; }
        string StartDate { get; set; }
        string StartTime { get; set; }
        public string GetStartDate();
        public string GetStartTime();
        void Start();
        void Stop();
        void Reset();

    }
}
