using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Business
{
    public class BusinessMessage
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public BusinessMessage(string type, string text)
        {
            Type = type;
            Text = text;
        }
    }
}
