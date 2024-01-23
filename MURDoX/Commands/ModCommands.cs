using System.Drawing;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Commands;
using MURDoX.Utils;

namespace MURDoX.Commands
{
    public class ModCommands : CommandModule
    {
        [Command("warn")]
        [Description("warns a member with reason for warning")]
        public async Task Warn(CommandEvent invokator, HashId memberId, string reason)
        {

        }

        [Command("mute")]
        [Description("mutes member")]
        public async Task Mute(CommandEvent invokator, HashId memberId, string reason)
        {

        }

        [Command("ban")]
        [Description("bans member with reason for ban")]
        public async Task Ban(CommandEvent invokator, HashId memberId, string reason)
        {

        }

        [Command("kick")]
        [Description("kick a member from the server")]
        public async Task Kick(CommandEvent invokator, HashId memberId, string reason)
        {

        }

        [Command("promote")]
        [Description("promote a member to a higher role")]
        public async Task Promote(CommandEvent invokator, HashId memberId, string reason)
        {

        }

        [Command("demote")]
        [Description("demote a member to a lower role")]
        public async Task Demote(CommandEvent invokator, HashId memberId, string reason)
        {

        }

        [Command("purge")]
        [Description("removes a set amount of messages")]
        public async Task Purge(CommandEvent ctx, uint amount)
        {
            var authorId = ctx.Message.CreatedBy;
            var serverId = ctx.Message.ServerId;
            var author = ctx.ParentClient.GetMemberAsync((HashId)serverId!, authorId);
            var permissions = await author.Result.User.GetMemberPermissionsAsync((HashId)serverId);
            var test = "";
           
            
            var channelId = ctx.ChannelId;
            var messages = await ctx.ParentClient.GetMessagesAsync(channelId, false, amount);
            for (int i = 0; i < messages.Count; i++)
            {
                await messages[i].DeleteAsync();
                await Task.Delay(100);
            }

            var embed = new Embed()
            {
                Description = $"{amount} messages deleted",
                Color = EmbedColors.GetColor("orange", Color.Orange),
                Footer = new EmbedFooter($"{ctx.ParentClient.Name} watching everything."),
                Timestamp = DateTime.Now
            };
            var deleteMessage = await ctx.CreateMessageAsync(embed);

        }


        [Command("whoami")]
        [Description("who are you tho?")]
        public async Task WhoAmI(CommandEvent invokator)
        {
            var authorId = invokator.Message.CreatedBy;
            var serverID = invokator.Message.ServerId;
            var author = await invokator.ParentClient.GetMemberAsync((HashId)serverID!, authorId);
            var embed = new Embed();


            embed.AddField(new EmbedField("name", "user1", true));
            embed.Title = "yassss";
            embed.Description = $"<@{invokator.Message.CreatedBy}>";

            await invokator.ReplyAsync(embed);
        }

    }
}
