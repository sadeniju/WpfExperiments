using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplatePractice
{
    public class Site
    {
        public Content Content
        {
            set;
            get;
        }

        public string Name
        {
            set;
            get;
        }

        public Site(Content content)
        {
            Content = content;
        }

        public Site()
        {
        }
    }
}
