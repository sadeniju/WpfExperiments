using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeViewExample {
    public class Catalogue {
        public List<Author> Authors { get; set; }

        public Catalogue(List<Author> authors) {
            Authors = authors;
        }

        public override string ToString() {
            string tmp = "Catalogue [";
            foreach (Author a in Authors) {
                tmp += a.ToString();
            }
            return tmp + "]";
        }
    }
}
