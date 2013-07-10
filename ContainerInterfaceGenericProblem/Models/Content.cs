using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceGenericProblem {
    abstract class Content: IPresentationModule {
        public IContainer Parent { get; set; }

        public Content(string title) {
            Title = title;
        }
    }
}
