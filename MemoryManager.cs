using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using OpenAI.Chat;


namespace Shawbot
{
    public class MemoryManager
    {
        private static MemoryManager _memoryManager;
        private MemoryManager() { }

        private List<ChatMessage> _memory = new List<ChatMessage>();

        private ChatMessageRole role;
        private ChatMessage currentMessage;

        public static MemoryManager GetMMInstance()
        {
            if (_memoryManager == null)
            {
                _memoryManager = new MemoryManager();
            }
            return _memoryManager;
        }
        
        public void UpdateMemory(MessageCreateEventArgs messageEvent)
        {
            

            if (messageEvent.Author.Id == 133155376332931072)
            {
                currentMessage = new AssistantChatMessage(messageEvent.Message.Content);
            }
            else
            {
                currentMessage = new UserChatMessage(messageEvent.Message.Content);
            }
            
            _memory.Add(currentMessage);

            if (_memory.Count > 60) { _memory.RemoveAt(0); }
        }

        public List<ChatMessage> PullMemory()
        {
            return _memory;
        }
    }
}
