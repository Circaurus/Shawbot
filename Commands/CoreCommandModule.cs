using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shawbot
{
    public class CoreCommandModule : ApplicationCommandModule
    {
        [SlashCommand("help", "Returns a list of implemented commands")]
        async Task HelpCommand(InteractionContext com)
        {
            await com.CreateResponseAsync("No Commands at this time!");
        }

        [SlashCommand("clearhistory", "Removes all message history")]
        async Task ClearHistoryCommand(InteractionContext com)
        {
            string userID = com.Interaction.User.Id.ToString();
           //TODO
        }
    }
}
