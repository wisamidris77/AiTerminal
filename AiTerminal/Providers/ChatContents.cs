using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiTerminal.Models;

namespace AiTerminal.Providers
{
    public class ChatContents
    {
        private List<ContentModel> _contentModels = [];

        public IReadOnlyList<ContentModel> ContentModels => _contentModels.AsReadOnly();
        public void AddContent(ContentModel content)
        {
            _contentModels.Add(content);
        }
    }
}
