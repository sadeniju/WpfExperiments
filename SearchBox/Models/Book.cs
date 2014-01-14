using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchBox {
    public class Book {
        public string Author { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }

        public Book(string author, string title, string releaseDate) {
            Author = author;
            Title = title;
            ReleaseDate = releaseDate;
        }
    }
}
