using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MURDoX.Extensions
{
    public static class TimerExtensions
    {
        public static double ToSeconds(this double value)
        {
             return Math.Round(value / 1000);
        }

        public static double MillisecondsToMinutes(this double value)
        {
            return Math.Round(value / 60000);
        }

        public static double SecondsToMinutes(this double value)
        {
            return Math.Round(value / 60);
        }

        public static double ToHours(this double value)
        {
            return Math.Round(value / (1000 * 60 * 60));
        }


           
    }
}
