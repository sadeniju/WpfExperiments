using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceSolution {
    abstract class Content: IPresentationModule {
        public IPresentationContainer Parent { get; set; }

        public Content(string title) {
            Title = title;
        }
    }
}
