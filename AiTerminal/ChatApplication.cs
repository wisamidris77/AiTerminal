using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<bool> Run()
        {
            Console.WriteLine("Ai Terminal Assistant");
            Console.WriteLine("---------------------");

            while (true)
            {
                var prompt = GetUserInput();
                if (prompt == "clear_data")
                    return true;
                if (prompt == "exit")
                    return false;
                if (string.IsNullOrWhiteSpace(prompt)) continue;
                if (prompt == "exit") break;
                _chatContents.AddContent(ContentModel.User([prompt]));
                var content = await _aiClient.GenerateContent(_chatContents, _systemInstruction);
                _chatContents.AddContent(ContentModel.Model([content]));
                var (terminalCommands, explanation) = ParseContent(content);

                DisplayExplanation(explanation);

                if (terminalCommands?.Any() ?? false)
                {
                    foreach (var terminalCommand in terminalCommands)
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
            return false;
        }

        private string? GetUserInput()
        {
            CliHelper.WriteColored("? ", ConsoleColor.Cyan);
            return Console.ReadLine();
        }

        private (List<string>? terminalCommand, StringBuilder explanation) ParseContent(string content)
        {
            List<string>? terminalCommands = [];
            StringBuilder explanation = new StringBuilder();

            foreach (var item in content.Split('\n'))
            {
                string pattern = @"^([""'`])|([""'`])$";
                var parsedLine = Regex.Replace(item, pattern, "");
                if (parsedLine.StartsWith("Terminal:", StringComparison.OrdinalIgnoreCase))
                {
                    terminalCommands.Add(parsedLine.Substring("Terminal:".Length).Trim());
                }
                else
                {
                    explanation.AppendLine(item);
                }
            }

            return (terminalCommands, explanation);
        }

        private void DisplayExplanation(StringBuilder explanation)
        {
            Console.WriteLine(explanation.ToString());
        }

        private void DisplayCommand(string terminalCommand)
        {
            CliHelper.WriteColored("> ", ConsoleColor.Yellow);
            CliHelper.WriteLineColored(terminalCommand, ConsoleColor.White);
            CliHelper.WriteLineColored("Warning: Ai might make mistakes with commands. Review carefully.", ConsoleColor.DarkYellow);
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
