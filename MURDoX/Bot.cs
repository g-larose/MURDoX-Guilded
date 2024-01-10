using System.Reactive.Linq;
using System.Text.Json;
using Guilded;
using Guilded.Base;
using Guilded.Commands;
using Guilded.Events;
using MURDoX.Commands;
using MURDoX.Games.Trivia;
using MURDoX.Services;
using MURDoX.Utils;
using MURDoX.Commands;
using static ChalkDotNET.Chalk;

namespace MURDoX
{
    public class Bot
    {
        private static string? json = File.ReadAllText("config.json");
        private static string? token = JsonSerializer.Deserialize<ConfigJson>(json!).Token!;
        private static string? prefix = JsonSerializer.Deserialize<ConfigJson>(json!).Prefix!;
        private const string LogChannelId = "261dc3c2-8db4-44e0-8af1-a9dc50a0d90c";
        public Bot()
        {
            
        }

        public async Task RunAsync()
        {
            await using var client = new GuildedBotClient(token!)
                .AddCommands(new BotCommands(), prefix!)
                .AddCommands(new TriviaGame(), prefix!)
                .AddCommands(new SupportCommands(), prefix!);
                
            client
                .Prepared
                .Subscribe(me =>
                {
                    Console.ForegroundColor = ConsoleUtil.FromHex("#e59202");
                    Console.WriteLine($"[{DarkGreen(client.Me.Name)}] Connected\nLogged in as \"{DarkGreen(me.Name)}\" with ID \"{DarkYellow(me.Id.ToString())}\"");

                });

            client.MessageCreated
                .Subscribe(async msgCreated =>
                {
                    if (msgCreated.CreatedBy == client.Id) return;
                    var channelId = new Guid(LogChannelId);
                    var author = await client.GetMemberAsync((HashId)msgCreated.ServerId!, msgCreated.CreatedBy);
                    var message = msgCreated.Message.Content;
                    //await client.CreateMessageAsync(channelId, $"[{author.Name}] created message \"{message}\"");
                });

            client.MemberJoined
                .Subscribe(async memberJoined =>
                {
                    var channelId = new Guid(LogChannelId);
                    await client.CreateMessageAsync(channelId, $"Welcome {memberJoined.Name}: \n\rjoined on {DateTime.Now.ToShortDateString()}");
                });

            client.MemberBanned
                .Subscribe(async memBanned =>
                {
                    var channelId = new Guid(LogChannelId);
                    await client.CreateMessageAsync(channelId, $"Welcome {memBanned.Name}: \n\rwas banned: on {DateTime.Now.ToShortDateString()}");
                });

            client.MessageDeleted
                .Subscribe(async mesDeleted =>
                {
                    //log the message and author.
                });
           
            await client.ConnectAsync();
            await client.SetStatusAsync("Watching Everything", 90002579);
            BotTimerService bts = new BotTimerService();
            Console.WriteLine($"{DarkYellow("bot timer service started")}: {DarkCyan(DateTime.Now.ToShortDateString())}");
            await Task.Delay(-1);
        }
        
    }
}
