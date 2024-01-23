using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guilded;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Client;
using Guilded.Content;
using MURDoX.Interfaces;

namespace MURDoX.Services
{
    public class GuildedMessageService : IGuildedMessageService
    {
        public async Task HandleMessageAsync(AbstractGuildedClient client, Message message, Guid channel)
        {
            //TODO: filter message for malicious links and embeds with malicious images
            if (client.Id.Equals(message.CreatedBy)) return; //MURDoX sent a message, ignore it.
            var author = await client.GetMemberAsync((HashId)message.ServerId!, message.CreatedBy);
            var embed = new Embed()
            {
                Description = $"<@{author.User.Id}> your message contained banned material and has been removed.",
                Footer = new EmbedFooter($"MURDoX watching everything"),
                Timestamp = DateTime.Now
            };
            //await message.ReplyAsync($"{author.Name}, your message contained banned material and has been removed!");
            await message.DeleteAsync();
            await client.CreateMessageAsync(channel, embed);
        }
    }
}
