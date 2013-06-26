using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedClasses.SampleModels {
    /// <summary>
    /// Represents a library containing several books.
    /// </summary>
    public class Library {
        #region Properties
        /// <summary>
        /// The library's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The library's books.
        /// </summary>
        public List<Book> Books { get; set; }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a default library with a collection of books.
        /// </summary>
        /// <param name="empty"></param>
        public Library() {
            Books = new List<Book>();
        }

        /// <summary>
        /// Constructs a new library with a name.
        /// </summary>
        /// <param name="name"></param>
        public Library(string name) : this() {
            Name = name;
        }

        /// <summary>
        /// Constructs a library with a name and collection of books.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="books"></param>
        public Library(string name, List<Book> books): this(name) {
            Books = books;
        }
        #endregion

        public void CreateDummy() {
            Name = "Library";
            Books = new List<Book>{
                        new Book("George Gamov","One Two Three Infinity"),
                        new Book("Jilliane Hoffman","Cupido"), 
                        new Book("Juli Zeh","Nullzeit"),
                        new Book("Unknown", "Information is Beautiful"),
                        new Book("Philip K. Dick","Do Androids Dream of Electric Sheep?"),
                        new Book("Stephen Hawking", "A Brief History Of Time"),
                        new Book("Unknown", "Don't Try This At Home"),
                        new Book("Elizabeth Corley","Fatal Legacy"),
                        new Book("Goethe","Faust"),
                        new Book("Elizabeth George", "Believing The Lie"),
                        new Book("Harper Lee", "To Kill a Mockingbird")
            };
        }
    }
}
