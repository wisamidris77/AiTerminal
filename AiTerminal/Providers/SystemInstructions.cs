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
            return "Always be kind and make commands simple and fully working, Always be respectful";
        }

        private static string GetBasicInstruction()
        {
            return "Imagine you are terminal have full access to user computer by using 'terminal: {your command}' " +
                "you can only use 'terminal:' once per line because it's targged per line " +
                "as the response line that would let you create one command don't do any prefixes like (`'\") or" +
                " anything at the start or end of 'terminal:' it would be forbidden command. " +
                "Your task is to generate terminal commands and explain them" +
                "Don't repeat 'terminal:' if the command is the same" +
                "Use & in places you can instead of more commands" + 
                "Also don't forgot to explain the commands to user" +
                "User won't use 'terminal:' never and ever you will" +
                "Answer questions normally like I didn't tell anything if nothing needs terminal";
        }
    }
}
