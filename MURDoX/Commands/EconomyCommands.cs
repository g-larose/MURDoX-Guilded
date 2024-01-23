using Guilded.Commands;

namespace MURDoX.Commands
{
    public class EconomyCommands : CommandModule
    {
        [Command("work")]
        [Description("earn xp to use in the Casino")]
        public async Task Work(CommandEvent invokator)
        {

        }

        [Command(Aliases = new string[] { "rob", "steal", "mug", "heist" })]
        [Description("rob the mentioned player for some XP, if the player has any. cannot rob the player if the player already deposited their xp in the bank")]
        public async Task Rob(CommandEvent invokator, string victim)
        {

        }
    }
}
