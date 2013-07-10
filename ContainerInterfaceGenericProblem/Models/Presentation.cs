using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceGenericProblem {
    class Presentation: IPresentationModule, IContentContainer<Page> {
        public List<Page> Contents { get; private set; }
        public string SomeContainerProperty { get; set; }

        public Presentation(string title) {
            Title = title;
            Contents = new List<Page>();
        }

        public void AddContent(Page page){
            if(!Contents.Contains(page)){
                Contents.Add(page);
                //page.Parent = this;
            }
        }

        
    }
}
