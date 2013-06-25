using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplatePractice
{
    class Text : Content
    {
        public string Txt
        {
            get;
            set;
        }

        public override string ToString()
        {
            return "Image";
        }

        public Text(string name, string txt)
            : base(name)
        {
            Txt = txt;
        }

        public Text(string name) 
            : base(name)
        {
            Txt = "Lorem ipsum dolor sit amet.";
        }
    }
}
