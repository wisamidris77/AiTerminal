using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiTerminal.Models;
using AiTerminal.Providers;

namespace AiTerminal.Abstraction
{
    public interface IAiClient
    {
        Task<string> GenerateContent(ChatContents contents, ContentModel systemInstructions);
    }
}
