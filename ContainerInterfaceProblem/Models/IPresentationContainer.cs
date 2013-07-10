using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceSolution {
    interface IPresentationContainer {
        List<Content> Contents { get; }

        void AddContent(T content);
    }
}
