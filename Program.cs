using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
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

            //Get Memory Manager
            MemoryManager memoryManager = MemoryManager.GetMMInstance();
            //Initialize Discord Client
            DiscordClient discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = Environment.GetEnvironmentVariable("DISCORD_TOKEN", EnvironmentVariableTarget.Machine),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            //Register Command Modules
            SlashCommandsExtension slash = discord.UseSlashCommands();
            slash.RegisterCommands<CoreCommandModule>(133155376332931072);

            //Activity
            DiscordActivity activity = new();
            activity.Name = "wrinkling my brain";
            activity.ActivityType = ActivityType.Custom;
            discord.Ready += async (client, readyEventArgs) =>
                await discord.UpdateStatusAsync(activity);


            List<ChatMessage> memories;

            discord.MessageCreated += async (client, e) =>
            {
                if (e.Message.Content.Contains("shawbot", StringComparison.OrdinalIgnoreCase) && (e.Channel.Id == 1362185428584890469 || e.Channel.Id == 1367207986199662662) && e.Message.Author.Id != 1367155472720990319)
                {
                    Console.WriteLine("Pulling Memory");
                    memories = memoryManager.PullMemory();

                    Console.WriteLine("Generating Response");
                    string response = await chatService.GetAIResponse(e, memories);

                    await e.Message.RespondAsync(response);
                }

                //Update Short Term Memory
                memoryManager.UpdateMemory(e);
            };


            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}