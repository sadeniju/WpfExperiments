using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplatePractice
{
    class ContentImage : Content
    {
        public string ContentImg
        {
            set;
            get;
        }

        public override string ToString()
        {
            return "Image";
        }

        public ContentImage(string name, string contentImg) 
            : base(name)
        {
            ContentImg = contentImg;
        }

        public ContentImage(string name)
            : base(name)
        {
            ContentImg = "<:3 )~";
        }
    }
}
