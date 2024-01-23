using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guilded.Base.Embeds;
using Guilded.Commands;
using MURDoX.Extensions;

namespace MURDoX.Commands
{
    public class TagCommands : CommandModule
    {
        //TODO
        [Command("tag")]
        [Description("MURDoX tag command - mod role or higher needed to use this command!")]
        public async Task Tag(CommandEvent invokator, string command, string[] args)
        {
            await HandleTagAsync(invokator, command, args);

        }

        private async Task HandleTagAsync(CommandEvent invokator, string command, string[] args)
        {
            switch (command)
            {
                case "add":
                    await invokator.ReplyAsync($"**[{invokator.Message.CreatedBy}]** you said [\"{command}\"] with the arguments of \"{string.Join(' ', args)}\"");
                    break;
                case "remove":
                    
                    break;
                case "update":
                    
                    break;
                case "view":
                    
                    break;
                default:
                    await invokator.ReplyAsync("unrecognized command: command aborted");
                    break;
            }
        }

        [Command(Aliases = new string[] { "itdepends" })]
        [Description("humor")]
        public async Task ItDepends(CommandEvent invokator)
        {
            var embed = new Embed()
            {
                Image = new EmbedMedia("https://i.imgur.com/uPkTGJM.png"),
                Footer = new EmbedFooter($"{invokator.ParentClient.Name}"),
                Timestamp = new DateTime().Date
            };
            await invokator.Message.ReplyAsync(embed);
            //await invokator.ReplyAsync(embed);
        }
    }
}
