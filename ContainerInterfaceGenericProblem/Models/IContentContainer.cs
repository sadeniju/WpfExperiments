using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceGenericProblem {
    interface IContentContainer<T> : IContainer where T: Content {
        List<T> Contents { get; }

        void AddContent(T content);
    }
}
