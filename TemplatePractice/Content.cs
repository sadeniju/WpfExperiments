using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplatePractice
{
    public class Content
    {
        public string Name
        {
            get;
            set;
        }

        public Content(string name)
        {
            Name = name;
        }

        public Content() : this("") { }
    }
}
