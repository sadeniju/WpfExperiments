using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceGenericProblem {
    class Page: Content, IContentContainer<Content> {
        public List<Content> Contents { get; private set; }
        public string SomeContainerProperty { get; set; }

        public Page(string title): base(title) {
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
