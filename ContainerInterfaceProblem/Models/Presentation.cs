using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceSolution {
    class Presentation: IPresentationModule, IPresentationContainer {
        public List<Content> Contents { get; private set; }

        public Presentation(string title) {
            Title = title;
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
