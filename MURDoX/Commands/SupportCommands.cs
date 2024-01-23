using System.Drawing;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Commands;
using MURDoX.Utils;

namespace MURDoX.Commands;
public class SupportCommands : CommandModule
{
    [Command("support")]
    [Description("creates a support ticket")]
    public async Task Support(CommandEvent invokator, string subject)
    {
        var supportChannelId = new Guid("479011d1-5631-46ac-b5c4-a5269c186daa");
        var author = await invokator.ParentClient.GetMemberAsync((HashId)invokator.ServerId!, invokator.CreatedBy);
        var id = Guid.NewGuid();
        
        //var channel = await invokator.ParentClient.GetChannelAsync(supportChannelId);
        var embed = new Embed()
        {
            Color = EmbedColors.GetColor("teal", Color.Teal),
            Description = $"{author.Name} your support ticket with ID: {id} has been created, the staff will review your request\n\ryou will receive a DM with the results within 48 hours.",
            Footer = new EmbedFooter($"{invokator.ParentClient.Name} Watching Everything!"),
            Timestamp = DateTime.Now
        };
        await invokator.CreateMessageAsync(embed);
        await invokator.ParentClient.CreateMessageAsync(supportChannelId,
            $"{author.Name} has created support ticket with ID: {id}\rwith subject {subject}");
        
        //TODO: we need to create the xml for the support tickets.

    }

    [Command("suggest")]
    [Description("adds a suggestion for the server modes to consider.")]
    public async Task Suggest(CommandEvent invokator, string suggestion)
    {
        
    }
}