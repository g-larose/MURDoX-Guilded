using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guilded.Commands;

namespace MURDoX.Commands
{
    public class ReminderCommands : CommandModule
    {
        [Command("remind")]
        [Description("sets a reminder")]
        public async Task Remind(CommandEvent invokator, string[] reminder)
        {

        }
    }
}
