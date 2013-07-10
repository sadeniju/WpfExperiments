using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceGenericProblem {
    class ImageContent: Content {
        public string FileName { get; set; }

        public ImageContent(string title) : base(title) { }
    }
}
