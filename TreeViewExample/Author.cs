using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SampleModels;

namespace TreeViewExample {
    public class Author {
        public ObservableCollection<Book> PublishedBooks { get; set; }
        public string Name{ get; set; }

        public Author(string name, ObservableCollection<Book> books) {
            Name = name;
            PublishedBooks = books;
        }

        public Author() : this("", null) { }

        public override string ToString() {
            return Name;
        }
    }
}
