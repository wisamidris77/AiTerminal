using AiTerminal.Providers;
using AiTerminal;
using AiTerminal.Abstraction;
using AiTerminal.Clients;

public class Program
{

    public static async Task Main(string[] args)
    {
        await ApiKeyProvider.InitApiKey();
        IAiClient geminiClient = new GeminiClient(ApiKeyProvider.ApiKey);
        var chatApp = new ChatApplication(geminiClient);
        //if (args.Any())
        //{
        //    await chatApp.RunCommand(args[0]); //  Future plans
        //}
        //else
        //{
        await chatApp.Run();
        //}
    }
}
