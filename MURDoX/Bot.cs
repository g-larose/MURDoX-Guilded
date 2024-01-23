using System.Reactive.Linq;
using System.Text.Json;
using Guilded;
using Guilded.Base;
using Guilded.Commands;
using MURDoX.Commands;
using MURDoX.Games.Trivia;
using MURDoX.Services;
using MURDoX.Utils;
using Websocket.Client;
using static ChalkDotNET.Chalk;

namespace MURDoX
{
    public class Bot
    {
        private static string? json = File.ReadAllText("config.json");
        private static string? token = JsonSerializer.Deserialize<ConfigJson>(json!).Token!;
        private static string? prefix = JsonSerializer.Deserialize<ConfigJson>(json!).Prefix!;
        private const string LogChannelId = "261dc3c2-8db4-44e0-8af1-a9dc50a0d90c";
        private GuildedMessageService messageService = new();
        public async Task RunAsync()
        {
            await using var client = new GuildedBotClient(token!)
                .AddCommands(new BotCommands(), prefix!)
                .AddCommands(new TriviaGame(), prefix!)
                .AddCommands(new SupportCommands(), prefix!)
                .AddCommands(new TagCommands(), prefix!)
                .AddCommands(new ReminderCommands())
                .AddCommands(new ModCommands(), prefix!)
                .AddCommands(new WikiCommands(), prefix!)
                .AddCommands(new EconomyCommands(), prefix!);
                
            client
                .Prepared
                .Subscribe(async me =>
                {
                    var timeStamp = string.Join(" ", DateTime.Now.ToShortDateString(),
                        DateTime.Now.ToLongTimeString());
                    Console.ForegroundColor = ConsoleUtil.FromHex("#e59202");
                    Console.WriteLine($"{DarkGray($"[{timeStamp}]")} {DarkYellow("[INFO]")}    {DarkGray($"[{client.Me.Name}]")}  {DarkYellow("connected")}");
                    
                });

            client.Disconnected
                .Where(e => e.Type != DisconnectionType.NoMessageReceived)
                .Subscribe(me =>
                {
                    //var eventType = me.Type;
                    //Console.WriteLine($"disconnected type: {eventType}");
                    var timeStamp = string.Join(" ", DateTime.Now.ToShortDateString(),
                        DateTime.Now.ToLongTimeString());
                    Console.WriteLine($"{DarkGray($"[{timeStamp}]")} {DarkRed($"[WARNING]")} {DarkGray($"[{client.Me.Name}]")}  {DarkRed($"disconnected from gateway...reconnecting")}");
                });
            client.Reconnected
                .Where(x => x.Type != ReconnectionType.Initial)
                .Where(x => x.Type != ReconnectionType.NoMessageReceived)
                .Subscribe(me =>
                {
                    var timeStamp = string.Join(" ", DateTime.Now.ToShortDateString(),
                        DateTime.Now.ToLongTimeString());
                    Console.WriteLine($"{DarkGray($"[{timeStamp}]")} {DarkYellow($"[INFO]")}    {DarkGray($"[{client.Me.Name}]")}  {DarkYellow("talking with gateway")}");
                });

            client.MessageCreated
                .Subscribe(async msgCreated =>
                {
                    if (msgCreated.CreatedBy == client.Id) return;
                    
                    //var channelId = new Guid(LogChannelId);
                    var channelId = msgCreated.ChannelId;
                    //var channel = await client.GetChannelAsync(channelId);
                   
                    var author = await client.GetMemberAsync((HashId)msgCreated.ServerId!, msgCreated.CreatedBy);
                    var message = msgCreated.Message;
                    //await messageService.HandleMessageAsync(client, message, channelId);
                    //await client.CreateMessageAsync(channelId, $"[{author.Name}] created message \"{message}\"");
                });

            client.MemberJoined
                .Subscribe(async memberJoined =>
                {
                    var channelId = new Guid(LogChannelId);
                    await client.CreateMessageAsync(channelId, $"`{memberJoined.Name}` joined {DateTime.Now.ToShortDateString()}");
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
            var timeStamp = string.Join(" ", DateTime.Now.ToShortDateString(),
                    DateTime.Now.ToLongTimeString());
            BotTimerService bts = new BotTimerService();
            Console.WriteLine($"{DarkGray($"[{timeStamp}]")} {DarkYellow("[INFO]")}    {DarkGray($"[{client.Me.Name}]")}  {DarkYellow("loading command modules")}");
            Console.WriteLine($"{DarkGray($"[{timeStamp}]")} {DarkYellow("[INFO]")}    {DarkGray($"[{client.Me.Name}] ")} {DarkYellow("timer service started")}");
            
            await Task.Delay(-1);
        }

        
    }
}
