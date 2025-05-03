using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Shawbot
{
    public class AIService
    {
        private readonly ChatClient _chatClient;
        private List<ChatMessage> promptMessages = new List<ChatMessage>()
        {
            new SystemChatMessage("You are Shawbot. Please take on a friendly and dry tone. Use casual language and format all messages dryly, using shitpost language. Keep messages short and dry, stick to dry humor without overly explaining any topics or going into detail about anything.")
        };

        public AIService()
        {
            _chatClient = new(model: "gpt-4o-mini", apiKey: Environment.GetEnvironmentVariable("OPENAI_TOKEN", EnvironmentVariableTarget.Machine));
        }
        public async Task<string> GetAIResponse(MessageCreateEventArgs messageEvent, List<ChatMessage> memories)
        {
            promptMessages.AddRange(memories);
            promptMessages.Add(new UserChatMessage(messageEvent.Message.Content));
            
            ChatCompletion chatCompletion = await _chatClient.CompleteChatAsync(promptMessages.ToArray());

            return chatCompletion.Content[0].Text;
        }
    }
}
