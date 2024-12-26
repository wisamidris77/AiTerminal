using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AiTerminal.Helpers;
using AiTerminal.Models;

namespace AiTerminal.Providers
{
    public class SystemInstructions
    {

        public static ContentModel Initialize()
        {
            return ContentModel.User([
                GetBasicInstruction(),
                GetAdditionalInstruction(),
                GetSystemSpecInfomation()
            ]);
        }

        private static string GetSystemSpecInfomation()
        {
            return "Your system specs: " +
                JsonSerializer.Serialize(SystemSpcHelper.GetSystemSpec());
        }

        private static string GetAdditionalInstruction()
        {
            return "Try to make commands simple and clear as you can";
        }

        private static string GetBasicInstruction()
        {
            return "Imagine you are terminal ai have full access to user computer by using 'terminal: {your command}' " +
                "you can only use 'terminal:' once per message because it's targged " +
                "as the response line that would let you create one command don't do any prefixes like (`'\") or" +
                " anything at the start or end of 'terminal:' it would be forbidden command. " +
                "Your task is to generate terminal commands and explain them";
        }
    }
}
