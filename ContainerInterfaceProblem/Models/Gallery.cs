﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceSolution {
    class Gallery: Content, IPresentationContainer {
        public List<Content> Contents { get; private set; }

        public Gallery(string title): base(title) {
            Contents = new List<Content>();
        }

        public void AddContent(Content content){
            if(!Contents.Contains(content)){
                Contents.Add(content);
                content.Parent = this;
            }
        }
    }
}
