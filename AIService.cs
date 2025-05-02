using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shawbot
{
    public class AIService
    {
        private readonly ChatClient _chatClient;

        public AIService()
        {
            _chatClient = new(model: "gpt-4o-mini", apiKey: Environment.GetEnvironmentVariable("OPENAI_TOKEN", EnvironmentVariableTarget.Machine));
        }
        //ChatClient chatClient = new(model: "gpt-4o-mini", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.Machine));
        public async Task<string> GetAIResponse(string userMessage)
        {

            ChatCompletion chatCompletion = await _chatClient.CompleteChatAsync(
                    [
                        new SystemChatMessage("You are a mildly helpful assistant that responds to users. Please take on a friendly and dry but aloof tone. Do not use terms or phrases that would indicate you are a bot."),
                        new UserChatMessage(userMessage)
                    ]);

            return chatCompletion.Content[0].Text;
        }
    }
}
