using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Commands;
using MURDoX.Services;
using MURDoX.Utils;

namespace MURDoX.Commands
{
    public class BotCommands : CommandModule
    {
        [Command("hello")]
        public async Task SayHello(CommandEvent invokation) =>
            await invokation.ReplyAsync($"Hello {invokation.CreatedBy}");

        [Command("rank")]
        public async Task Rank(CommandEvent invokation)
        {
            var msg = await invokation.Message.CreateMessageAsync("rank");
        }

        [Command("blackjack")]
        public async Task Blackjack(CommandEvent invokation)
        {
           
            Embed embed = new Embed
            {
                Title = "Blackjack V1",
                Color = Color.Crimson,
                Description = "Blackjack is a WIP, stay tuned for more updates.",
                Footer = new EmbedFooter($"{invokation.ParentClient.Name} watching everything."),
                Timestamp = DateTime.Now
            };
            await invokation.CreateMessageAsync(embed);
        }

        [Command("leaderboard")]
        [Description("displays the blackjack leaderboard")]
        public async Task Leaderboard(CommandEvent ctx)
        {
            var rngColor = EmbedColors.GenerateRandomEmbedColor();
            Embed embed = new Embed
            {
                Title = "Blackjack V1",
                Color = EmbedColors.GetColor("red", Color.Red),
                Description = "Blackjack is a WIP, no Leaderboard yet.",
                Footer = new EmbedFooter($"{ctx.ParentClient.Name} watching everything."),
                Timestamp = DateTime.Now
            };
            await ctx.CreateMessageAsync(embed);
        }

        [Command("purge")]
        [Description("removes a set amount of messages")]
        public async Task Purge(CommandEvent ctx, uint amount)
        {
            var channelId = ctx.ChannelId;
            var messages = await ctx.ParentClient.GetMessagesAsync(channelId, false, amount);
            for (int i = 0; i < messages.Count; i++)
            {
                await messages[i].DeleteAsync();
                await Task.Delay(200);
            }

            var embed = new Embed()
            {
                Description = $"{amount} messages deleted",
                Color = EmbedColors.GetColor("yellow", Color.Yellow),
                Footer = new EmbedFooter($"{ctx.ParentClient.Name} watching everything."),
                Timestamp = DateTime.Now
            };
            var deleteMessage = await ctx.CreateMessageAsync(embed);
            await Task.Delay(1000);
            await deleteMessage.DeleteAsync();

        }

        [Command("uptime")]
        [Description("get the bot's online time")]
        public async Task Uptime(CommandEvent ctx)
        {
            var uptime = BotTimerService.GetBotUptime();
            var embed = new Embed()
            {
                Title = $"{ctx.ParentClient.Name} has been online for {uptime}",
                Color = EmbedColors.GetColor("teal", Color.Teal),
                Footer = new EmbedFooter($"{ctx.ParentClient.Name} watching everything."),
                Timestamp = DateTime.Now
            };

            await ctx.CreateMessageAsync(embed);
        }

    }
}
