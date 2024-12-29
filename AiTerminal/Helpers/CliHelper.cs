using System;

namespace AiTerminal.Helpers
{
    public static class CliHelper
    {
        public static void WriteColored(string content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(content);
            Console.ResetColor();
        }
        public static void WriteLineColored(string content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(content);
            Console.ResetColor();
        }
    }
}
