using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiTerminal.Abstraction;
using AiTerminal.Helpers;
using AiTerminal.Models;
using AiTerminal.Providers;

namespace AiTerminal
{
    public class ChatApplication
    {
        private readonly IAiClient _aiClient;
        private readonly ChatContents _chatContents;
        private readonly ContentModel _systemInstruction;
        public ChatApplication(IAiClient aiClient)
        {
            _aiClient = aiClient;
            _chatContents = new ChatContents();
            _systemInstruction = SystemInstructions.Initialize();
        }

        public async Task Run()
        {
            Console.WriteLine("Gemini Terminal Assistant");
            Console.WriteLine("---------------------");

            while (true)
            {
                var prompt = GetUserInput();
                if (string.IsNullOrWhiteSpace(prompt)) continue;
                if (prompt == "exit") break;
                _chatContents.AddContent(ContentModel.User([prompt]));
                var content = await _aiClient.GenerateContent(_chatContents, _systemInstruction);
                _chatContents.AddContent(ContentModel.Model([content]));
                var (terminalCommand, explanation) = ParseContent(content);

                DisplayExplanation(explanation);

                if (terminalCommand != null)
                {
                    DisplayCommand(terminalCommand);
                    if (GetUserConfirmation())
                    {
                        TerminalCommandRunner.ExecuteCommand(terminalCommand);
                    }
                    else
                    {
                        Console.WriteLine("Command execution skipped.");
                    }
                }
            }
        }

        private string? GetUserInput()
        {
            CliHelper.WriteColored("? ", ConsoleColor.Cyan);
            return Console.ReadLine();
        }

        private (string? terminalCommand, StringBuilder explanation) ParseContent(string content)
        {
            string? terminalCommand = null;
            StringBuilder explanation = new StringBuilder();

            foreach (var item in content.Split('\n'))
            {
                if (terminalCommand == null && item.StartsWith("Terminal:", StringComparison.OrdinalIgnoreCase))
                {
                    terminalCommand = item.Substring("Terminal:".Length).Trim();
                }
                else if (terminalCommand == null)
                {
                    explanation.AppendLine(item);
                }
            }

            return (terminalCommand, explanation);
        }

        private void DisplayExplanation(StringBuilder explanation)
        {
            Console.WriteLine(explanation.ToString());
        }

        private void DisplayCommand(string terminalCommand)
        {
            CliHelper.WriteColored("> ", ConsoleColor.Yellow);
            CliHelper.WriteLineColored(terminalCommand, ConsoleColor.White);
            CliHelper.WriteLineColored("Warning: Gemini might make mistakes with commands. Review carefully.", ConsoleColor.DarkYellow);
        }

        private bool GetUserConfirmation()
        {
            CliHelper.WriteColored("Do you want to run this command? (", ConsoleColor.White);
            CliHelper.WriteColored("yes", ConsoleColor.Green);
            CliHelper.WriteColored("/", ConsoleColor.White);
            CliHelper.WriteColored("no", ConsoleColor.Red);
            CliHelper.WriteColored("): ", ConsoleColor.White);
            var confirmation = Console.ReadLine()?.ToLower();
            return confirmation == "yes";
        }
    }
}
