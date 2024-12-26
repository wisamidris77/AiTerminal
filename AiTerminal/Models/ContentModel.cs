using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiTerminal.Models
{
    public class ContentModel
    {
        public static ContentModel User(List<string> parts)
        {
            return new ContentModel
            {
                Role = "user",
                Parts = parts.Select(e => new PartModel()
                {
                    Text = e
                }).ToList()
            };
        }
        public static ContentModel Model(List<string> parts)
        {
            return new ContentModel
            {
                Role = "model",
                Parts = parts.Select(e => new PartModel()
                {
                    Text = e
                }).ToList()
            };
        }
        public string? Role { get; set; }
        public List<PartModel> Parts { get; set; }
    }
    public class PartModel
    {
        public string Text { get; set; }
    }

    public class CandidateModel
    {
        public ContentModel Content { get; set; }
    }

}
