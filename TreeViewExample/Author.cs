using System.Collections.ObjectModel;
using SharedClasses.SampleModels;

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
