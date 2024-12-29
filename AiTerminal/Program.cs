using AiTerminal.Providers;
using AiTerminal;
using AiTerminal.Abstraction;
using AiTerminal.Clients;
using LlmTornado.Chat.Models;
using LlmTornado.Code;
using LlmTornado;

public class Program
{
    public static async Task Main(string[] args)
    {
        //ApiKeyProvider.SelectedModel = ChatModel.Groq.Meta.Llama38B; // Custom models
        
        while (true)
        {
            await ApiKeyProvider.InitApiKeyAndModel();
            TornadoApi api = new TornadoApi(new List<ProviderAuthentication>
            {
                new ProviderAuthentication(LLmProviders.OpenAi, ApiKeyProvider.ApiKey),
                new ProviderAuthentication(LLmProviders.Anthropic, ApiKeyProvider.ApiKey),
                new ProviderAuthentication(LLmProviders.Cohere, ApiKeyProvider.ApiKey),
                new ProviderAuthentication(LLmProviders.Google, ApiKeyProvider.ApiKey),
                new ProviderAuthentication(LLmProviders.Groq, ApiKeyProvider.ApiKey)
            });


            IAiClient aiClient = new LlmTornadoClient(api, ApiKeyProvider.SelectedModel);
            var chatApp = new ChatApplication(aiClient);
            //if (args.Any())
            //{
            //    await chatApp.RunCommand(args[0]); //  Future plans
            //}
            //else
            //{
            var response = await chatApp.Run();
            if (response)
            {
                ApiKeyProvider.RemoveAiData();
                continue;
            }
            break;
            //}
        }
    }
}
