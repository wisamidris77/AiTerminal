using System;
using System.IO;
using System.Threading.Tasks;
using LlmTornado.Chat.Models;
using Newtonsoft.Json;

namespace AiTerminal.Providers
{
    public class ApiKeyProvider
    {
        public static ChatModel? SelectedModel { get; set; }
        public static string ApiKey { get; private set; }
        private static int SelectedModelInt { get; set; }

        public static async Task InitApiKeyAndModel()
        {
            string appDataPath, jsonFilePath;
            GetDataPath(out appDataPath, out jsonFilePath);

            if (File.Exists(jsonFilePath))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFilePath);
                var config = JsonConvert.DeserializeObject<ApiConfig>(jsonData);
                ApiKey = config.ApiKey;

                SelectedModel ??= GetModelByNumber(config.SelectedModel);
            }

            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                while (true)
                {
                    Console.Write("Enter your API key: ");
                    ApiKey = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(ApiKey))
                    {
                        Console.WriteLine("API key is required.");
                        continue;
                    }
                    if (SelectedModel == null)
                        SelectModel();

                    Directory.CreateDirectory(appDataPath);
                    var config = new ApiConfig
                    {
                        ApiKey = ApiKey,
                        SelectedModel = SelectedModelInt
                    };

                    var jsonData = JsonConvert.SerializeObject(config, Formatting.Indented);
                    await File.WriteAllTextAsync(jsonFilePath, jsonData);
                    break;
                }
            }
        }

        private static void GetDataPath(out string appDataPath, out string jsonFilePath)
        {
            appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AiTerminal");
            jsonFilePath = Path.Combine(appDataPath, "aiData.json");
        }

        private static void SelectModel()
        {
            while (true)
            {
                Console.WriteLine("Select a model:");
                Console.WriteLine("1. GPT-4o");
                Console.WriteLine("2. GPT-4o-mini");
                Console.WriteLine("3. Claude 3.5 Sonnet");
                Console.WriteLine("4. Claude 3.5 Haiku");
                Console.WriteLine("5. Cohere CommandR Plus");
                Console.WriteLine("6. Cohere CommandR Default");
                Console.WriteLine("7. Google Gemini 1.5 Flash");
                Console.WriteLine("8. Google Gemini 1.5 Pro");
                Console.WriteLine("9. Groq Meta Llama 370B");

                Console.Write("Enter the number of the model you want to use: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int inputInt))
                {
                    Console.WriteLine("Enter valid model.");
                    Console.WriteLine("-------------------");
                    continue;
                }
                SelectedModel = GetModelByNumber(inputInt);
                SelectedModelInt = inputInt;
                break;
            }
        }

        private static ChatModel GetModelByNumber(int modelNumber)
        {
            return modelNumber switch
            {
                1 => ChatModel.OpenAi.Gpt4.O,
                2 => ChatModel.OpenAi.Gpt4.OMini,
                3 => ChatModel.Anthropic.Claude35.Sonnet,
                4 => ChatModel.Anthropic.Claude35.Haiku,
                5 => ChatModel.Cohere.Command.RPlus,
                6 => ChatModel.Cohere.Command.Default,
                7 => ChatModel.Google.Gemini.Gemini15FlashLatest,
                8 => ChatModel.Google.Gemini.Gemini15ProLatest,
                9 => ChatModel.Groq.Meta.Llama370B,
                _ => "Unknown Model"
            };
        }

        public static void RemoveAiData()
        {
            string appDataPath, jsonFilePath;
            GetDataPath(out appDataPath, out jsonFilePath);
            SelectedModel = null!;
            SelectedModelInt = 0;
            ApiKey = null!;
            File.Delete(jsonFilePath);
        }
    }

    public class ApiConfig
    {
        public string ApiKey { get; set; }
        public int SelectedModel { get; set; }
    }
}
