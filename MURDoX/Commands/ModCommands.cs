using System.Drawing;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Commands;
using Guilded.Permissions;
using Microsoft.VisualBasic;
using MURDoX.Models;
using MURDoX.Utils;
using static ChalkDotNET.Chalk;

namespace MURDoX.Commands
{
    public class ModCommands : CommandModule
    {
        [Command("warn")]
        [Description("warns a member with reason for warning")]
        public async Task Warn(CommandEvent invokator, string mentioned, string reason)
        {
            var serverId = invokator.Message.ServerId;
            var authorId = invokator.Message.CreatedBy;
            var userId = invokator.Mentions.Users.First().Id!;
            var perms = await invokator.ParentClient.GetMemberPermissionsAsync((HashId)serverId!, authorId);
            var author = await invokator.ParentClient.GetMemberAsync((HashId)serverId!, authorId);
            var timeStamp = string.Join(" ", DateTime.Now.ToShortDateString(),
    DateTime.Now.ToLongTimeString());
            try
            {
                if (perms.Contains(Permission.ManageChannels))
                {
                    var mentionedId = invokator.Mentions.Users.First().Id;
                    var embed = new Embed();
                    embed.SetDescription($"WARNING! <@{mentionedId}> this is a warning\r\n `[reason: {reason}]`");
                    embed.SetColor(EmbedColors.GetColor("purple", Color.Purple));
                    embed.SetFooter($"MURDoX ");
                    embed.SetTimestamp(DateTime.Now);
                    await invokator.ReplyAsync(null, false, false, embed);
                    Console.WriteLine(
                        $"{DarkGray($"[{timeStamp}]")} {DarkYellow("[INFO]")}    {DarkGray($"[MURDoX]")}  {DarkYellow($"{author.Name} warned {mentioned} reason: {DarkGray($"[{reason}]")}")}");
                }
                else
                {

                    var embed = new Embed();
                    embed.SetDescription(
                        $"<@{author.Id}> you do not have the required permissions to execute this command, command ignored!");
                    embed.SetColor(EmbedColors.GetColor("purple", Color.Purple));
                    embed.SetFooter($"MURDoX ");
                    embed.SetTimestamp(DateTime.Now);
                    await invokator.ReplyAsync(null, true, false, embed);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(
                    $"{DarkGray($"[{timeStamp}]")} {DarkRed("[ERROR]")}    {DarkGray($"[MURDoX]")}  {DarkRed($"{e.Message}")}");
            }

            
        }

        [Command("mute")]
        [Description("mutes member")]
        public async Task Mute(CommandEvent invokator, string mentioned, string reason)
        {

        }

        [Command("ban")]
        [Description("bans member with reason for ban")]
        public async Task Ban(CommandEvent invokator, string mentioned, string reason)
        {

        }

        [Command("kick")]
        [Description("kick a member from the server")]
        public async Task Kick(CommandEvent invokator, string mentioned, string reason)
        {

        }

        [Command("promote")]
        [Description("promote a member to a higher role")]
        public async Task Promote(CommandEvent invokator, string mentioned, string reason)
        {

        }

        [Command("demote")]
        [Description("demote a member to a lower role")]
        public async Task Demote(CommandEvent invokator, string mentioned, string reason)
        {

        }

        [Command("purge")]
        [Description("removes a set amount of messages")]
        public async Task Purge(CommandEvent ctx, uint amount)
        {
            var authorId = ctx.Message.CreatedBy;
            var serverId = ctx.Message.ServerId;
            var author = await ctx.ParentClient.GetMemberAsync((HashId)serverId!, authorId);
            var permissions = await ctx.ParentClient.GetMemberPermissionsAsync((HashId)serverId!, authorId);

            if (permissions.Contains(Permission.ManageMessages))
            {
                var channelId = ctx.ChannelId;
                var channel = await ctx.ParentClient.GetChannelAsync(channelId);
                var messages = await ctx.ParentClient.GetMessagesAsync(channelId, false, amount);
                for (int i = 0; i < messages.Count; i++)
                {
                    await messages[i].DeleteAsync();
                    await Task.Delay(100);
                }

                var timeStamp = string.Join(" ", DateTime.Now.ToShortDateString(),
                    DateTime.Now.ToLongTimeString());
                Console.WriteLine($"{DarkGray($"[{timeStamp}]")} {DarkYellow("[INFO]")}    {DarkGray($"[MURDoX]")}  {DarkYellow($"{author.Name} deleted {amount} messages from {DarkGray($"[{channel.Name}]")}")}");
                var embed = new Embed()
                {
                    Description = $"{amount} messages deleted",
                    Color = EmbedColors.GetColor("orange", Color.Orange),
                    Footer = new EmbedFooter($"{ctx.ParentClient.Name} watching everything."),
                    Timestamp = DateTime.Now
                };
                var deleteMessage = await ctx.CreateMessageAsync(embed);
            }
            else
            {
                await ctx.ReplyAsync($"`{author}` you do not have the permission to manage messages, command ignored!");
            }
           

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
