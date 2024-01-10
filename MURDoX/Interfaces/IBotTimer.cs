using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MURDoX.Interfaces
{
    public interface IBotTimer
    {
        string GetBotUptime();
        string GetStartDate();
        string GetStartTime();
    }
}
