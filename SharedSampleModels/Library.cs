using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleModels;

namespace MvvmCollectionExample.Models {
    /// <summary>
    /// Represents a library containing several books.
    /// </summary>
    public class Library {
        #region Properties
        /// <summary>
        /// The library's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The library's books.
        /// </summary>
        public List<Book> Books { get; private set; }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a new library with a name.
        /// </summary>
        /// <param name="name"></param>
        public Library(string name) : this(name, new List<Book>()) {}

        /// <summary>
        /// Constructs a library with a name and collection of books.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="books"></param>
        public Library(string name, List<Book> books)  {
            this.Name = name;
            this.Books = books;
        }

        /// <summary>
        /// Constructs a default library.
        /// </summary>
        /// <param name="empty"></param>
        public Library() {
            Name = "Library XY";
            Books = new List<Book>{
                        new Book("Jilliane Hoffman","Cupido"), 
                        new Book("Juli Zeh","Nullzeit"),
                        new Book("Unknown", "Information is Beautiful"),
                        new Book("Philip K. Dick","Do Androids Dream of Electric Sheep?")
            };
        }
        #endregion
    }
}
