using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SampleModels;

namespace DragnDropPractice {
    class BookCollection {
        public ObservableCollection<Book> Books { get; set; }

        public BookCollection(ObservableCollection<Book> books) {
            Books = books;
        }
    }
}
