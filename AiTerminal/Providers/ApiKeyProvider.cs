using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiTerminal.Providers
{
    public class ApiKeyProvider
    {
        public static string ApiKey { get; private set; }

        public static async Task InitApiKey()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AiTerminal");
            string apiKeyFilePath = Path.Combine(appDataPath, "apikey.txt");

            if (File.Exists(apiKeyFilePath))
            {
                ApiKey = await File.ReadAllTextAsync(apiKeyFilePath);
            }

            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                Console.Write("Enter your Gemini API key: ");
                ApiKey = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ApiKey))
                {
                    Console.WriteLine("API key is required.");
                    return;
                }

                Directory.CreateDirectory(appDataPath);
                await File.WriteAllTextAsync(apiKeyFilePath, ApiKey);
            }

        }
    }
}
