using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiTerminal.Abstraction;
using AiTerminal.Models;
using AiTerminal.Providers;
using LlmTornado;
using LlmTornado.Chat.Models;
using LlmTornado.Models;
namespace AiTerminal.Clients
{
    public class LlmTornadoClient(TornadoApi tornadoApi, ChatModel chatModel) : IAiClient
    {
        public async Task<string?> GenerateContent(ChatContents contents, ContentModel systemInstructions)
        {
            var conversion = tornadoApi.Chat.CreateConversation(chatModel);
            foreach (var systemInstructionPart in systemInstructions.Parts)
            {
                conversion.AppendSystemMessage(systemInstructionPart.Text);
            }
            if (conversion != null)
            {
                foreach (var contentModel in contents.ContentModels)
                {
                    foreach (var part in contentModel.Parts)
                    {
                        if (contentModel.Role == "user")
                            conversion.AppendUserInput(part.Text);
                        else
                            conversion.AppendExampleChatbotOutput(part.Text);
                    }
                }
            }
            return await (conversion?.GetResponse() ?? Task.FromResult(default(string)));
        }
    }
}
