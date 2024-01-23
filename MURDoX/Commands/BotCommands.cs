using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Commands;
using Guilded.Users;
using MURDoX.Games._8Ball;
using MURDoX.Helpers;
using MURDoX.Models;
using MURDoX.Services;
using MURDoX.Utils;
using User = Guilded.Users.User;

namespace MURDoX.Commands
{
    public class BotCommands : CommandModule
    {
        private string token = "";
        private GuildedDataService _dataService = new();
        private CommandHelper cmdHelper = new();

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

     

        [Command(Aliases = new string[] { "uptime", "alive", "online" })]
        [Description("get the bots online time since last disconnect")]
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

        [Command(Aliases = new string[] { "info", "i" })]
        [Description("bot information")]
        public async Task Info(CommandEvent invokator)
        {
            var embed = new Embed();
            var lastReboot = $"{BotTimerService.GetStartDate()} at: {BotTimerService.GetStartTime()}";
            var serverId = invokator.ServerId;
            var server = await invokator.ParentClient.GetServerAsync((HashId)serverId!);
            var botName = invokator.ParentClient.Name;
            var ownerId = server.OwnerId;
            var owner = await invokator.ParentClient.GetMemberAsync((HashId)serverId, ownerId);
            var mems = await _dataService.GetServerMembersAsync((HashId)serverId);
            var memCount = mems.Members?.Where(x => x.User.Type != "bot");
            //var mem = mems.Members.Where(x => x.User.Id.Equals(invokator.Message.CreatedBy)).FirstOrDefault();
            var memXp = await invokator.ParentClient.AddXpAsync((HashId)serverId, invokator.Message.CreatedBy, 0);
            var bots = mems.Members?.Where(x => x.User.Type == "bot");
            var commands = new string[] { "blackjack", "rank", "purge", "uptime", "leaderboard" };

            embed.AddField(new EmbedField("Creator", "Async<Task<Guilded>>"));
            embed.AddField(new EmbedField("Server", server.Name, true));
            embed.AddField(new EmbedField("Owner", owner.Name, true));
            embed.AddField(new EmbedField("Members", memCount!.Count(), true));
            embed.AddField(new EmbedField("Bots", bots!.Count(), true));
            embed.AddField(new EmbedField("Commands", string.Join(',', commands), true));
            embed.AddField(new EmbedField("Last Reboot", lastReboot));
            embed.SetTitle($"{botName} Information");
            embed.AddField("", "[info](https://www.google.com)", true);
            embed.AddField("", "[test](https://www.google.com)", true);
            embed.SetThumbnail(new Uri("https://i.imgur.com/tbrKXgH.png"));
            embed.SetFooter(
                new EmbedFooter($"MURDoX: Watching Everything {string.Format(DateTime.Now.ToString("hh:mm tt"))}"));

            await invokator.ReplyAsync(embed);
        }

        [Command(Aliases = new string[] { "8ball", "8b", "ask" })]
        [Description("a basic 8ball, ask me a question and I will answer with some random responses")]
        public async Task Ask(CommandEvent invokator, string[] question)
        {
            var authorId = invokator.Message.CreatedBy;
            var serverId = invokator.Message.ServerId;
            var author = await invokator.ParentClient.GetMemberAsync((HashId)serverId!, authorId);
            var responsePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Games", "8Ball",
                "eight_ball_response.txt");
            try
            {

                var queryStr = string.Join("+", question);
                var query = $"https://eightballapi.com/api?question={queryStr}?&lucky=false";
                using var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(query);
                var result = JsonSerializer.Deserialize<Result>(json);
                var response = result!.Reading;
                await invokator.ReplyAsync($"`{author.Name}` {response}", true);
            }
            catch (Exception e)
            {
                await invokator.ReplyAsync(
                    $"`{author.Name}` I'm sorry but, I didn't understand your question, try again later!\rhere is your question {string.Join(" ", question)}");
            }


        }

        [Command(Aliases = new string[] { "profile" })]
        [Description("returns the given user's profile")]
        public async Task Profile(CommandEvent invokator, string mentioned = "")
        {
            if (mentioned is null || string.IsNullOrWhiteSpace(mentioned.ToString()))
            {
                await invokator.ReplyAsync(
                    "I did't recognize the user id, please mention the user to fetch the user profile!");
                return;
            }

            try
            {
                var serverId = invokator.Message.ServerId;
                var userId = invokator.Mentions.Users.First().Id;
                //var userId = invokator.Mentions.Users.First().Id;
                var member = await invokator.ParentClient.GetMemberAsync((HashId)serverId, userId);
                var memXp = await invokator.ParentClient.AddXpAsync((HashId)serverId, userId, 0);
                var user = invokator.ParentClient.GetUserAsync((HashId)member.User.Id);
                var joined = member.JoinedAt.ToShortDateString();
                //var roles = invokator.ParentClient.GetRolesAsync((HashId)serverId);
                //var servers = await _dataService.GetUserServersAsync(userId.Id.ToString());
                //var roleParsed = new List<string>();
                //foreach (var role in roles.Result)
                //{

                //    roleParsed.Add(role.ToString());
                //}

                var status = member.User!.Status!.Content;
                var embed = new Embed();
                embed.AddField(new EmbedField("Name", $"<@{member.Id}>", true));
                embed.AddField(new EmbedField("Joined", joined, true));
                embed.AddField(new EmbedField("XP", memXp, true));
                // embed.AddField(new EmbedField("Roles", roleParsed, true));
                //embed.AddField(new EmbedField("Servers", string.Join(",", servers), true));
                embed.AddField(new EmbedField("Status", status, true));
                embed.SetColor(Color.DarkCyan);
                await invokator.CreateMessageAsync(embed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [Command("shout")]
        [Description("makes a shout")]
        public async Task ShoutOut(CommandEvent invokator, [CommandParam] string shoutTitle,
            [CommandParam] string shoutText)
        {
            var authorId = invokator.Message.CreatedBy;
            var serverID = invokator.Message.ServerId;
            var author = await invokator.ParentClient.GetMemberAsync((HashId)serverID!, authorId);
            var embed = new Embed();
            embed.SetTitle("Shout Command");
            embed.AddField(new EmbedField(shoutTitle, shoutText, false));
            embed.SetFooter(new EmbedFooter($"Shout from {author.Name}"));
            

            var server = await invokator.ParentClient.GetServerAsync((HashId)serverID!);
            var ownerId = server.OwnerId;

            try
            {
                if (authorId == ownerId)
                {
                    //await invokator.CreateMessageAsync(embed);
                    await invokator.ReplyAsync("you do not have permission to use this command", true);
                }
                else
                {
                    embed = new Embed();
                    embed.Title = "Test Title";
                    embed.Description = "You do not have premission to run this command!";
                    embed.AddField("arg1", shoutTitle, true);
                    embed.AddField("arg2", shoutText, true);
                    await invokator.ReplyAsync(null, true, false, embed);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           

        }

        [Command("help")]
        [Description("shows all the commands for MURDoX")]
        public async Task Help(CommandEvent invokator, string commandQuery = "")
        {
            if (commandQuery == "")
            {
                var embed = new Embed();
                embed.SetTitle(":BlobHelpSign: Help");
                embed.SetDescription("-Member Commands-\r\n`ask - MURDoX responds with a random answer`\r\n" +
                                     "`profile - fetches the mentioned user profile`\r\n" +
                                     "`suggest - adds server suggestion`\r\n" +
                                     "`info - get server info`\r\n" +
                                     "`uptime - how long MURDoX has been online`\r\n\r\n" +
                                     "`rob - rob xp from the mentioned player`\r\n" +
                                     "`work - earn xp to play in the casino`" +
                                     "-Mod Commands-\r\n`purge -deletes channel messages`\r\n" +
                                     "`warn - warn a member`\r\n" +
                                     "`mute - mute a member`\r\n" +
                                     "`ban - ban a member`\r\n" +
                                     "`kick - kick member from server`\r\n" +
                                     "`promote - promote member role`\r\n" +
                                     "`demote - demote member role`\r\n");
                embed.SetColor(EmbedColors.GetColor("gray", Color.DarkGray));
                embed.SetTimestamp(DateTime.Now);
                embed.SetFooter(new EmbedFooter("MURDoX watching everything..."));
                await invokator.ReplyAsync(null, false, false, embed);

            }
            else
            {
                await cmdHelper.HandleCommand(invokator, commandQuery);
            }
            
        }
    }
}
