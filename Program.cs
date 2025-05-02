using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using OpenAI.Chat;

namespace Shawbot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }


        static async Task MainAsync()
        {
            //Initialize OpenAI Client
            AIService chatService = new AIService();

            //Initialize Discord Client
            DiscordClient discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = Environment.GetEnvironmentVariable("DISCORD_TOKEN", EnvironmentVariableTarget.Machine),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            //Register Command Modules
            var slash = discord.UseSlashCommands();
            slash.RegisterCommands<CoreCommandModule>(954563370294607914);

            //Activity
            DiscordActivity activity = new();
            activity.Name = "wrinkling my brain";
            activity.ActivityType = ActivityType.Custom;
            discord.Ready += async (client, readyEventArgs) =>
                await discord.UpdateStatusAsync(activity);




            //Dictionary<string, TaskCompletionSource<string>> _LLMTasks = new Dictionary<string, TaskCompletionSource<string>>();

            /*
            string userId = e.User.Id.ToString();
            if (!_LLMTasks.ContainsKey(userId))
            {
                _LLMTasks[userId] = new TaskCompletionSource<string>();
            }*/

            discord.MessageCreated += async (client, e) =>
            {
                if (e.Message.Content.Contains("shawbot", StringComparison.OrdinalIgnoreCase))
                {
                    string response = await chatService.GetAIResponse(e.Message.Content);

                    await e.Message.RespondAsync(response);
                }
            };


            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}