using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedClasses.SampleModels {
    public class Book {
        #region Fields
        private Random rand = new Random();
        private static int counter = 0;
        #endregion

        #region Properties
        public List<Page> Pages { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public int Edition { get; set; }
        public int Id { get; set; }
        public string ReleaseDate { get; set; }
        #endregion

        #region Construction
        #region Construction with author string
        public Book(string authorFullName, string title)
            : this(new Author(), title) {
            Author.FullName = authorFullName;
        }

        public Book(string authorFullName, string title, string releaseDate)
            : this(authorFullName, title) {
                ReleaseDate = releaseDate;
        }
        #endregion

        #region Construction with author object
        public Book(Author author, string title, string releaseDate)
            : this(title) {
                Author = author;
                ReleaseDate = releaseDate;
        }

        public Book(Author author, string title)
            : this(title) {
                Author = author;
        }
        #endregion

        public Book(string title): this() {
            Title = title;
        }

        public Book(){
            Author = new Author();
            Edition = rand.Next();  
            Pages = new List<Page>();
            Id = counter++;
        }
        #endregion

        /// <summary>
        /// Adds a page to the book.
        /// </summary>
        /// <param name="page"></param>
        public void AddPage(Page page) {
            Pages.Add(page);
        }

        /// <summary>
        /// Removes a page from the book.
        /// </summary>
        /// <param name="page"></param>
        public void RemovePage(Page page) {
            Pages.Remove(page);
        }

        public override string ToString() {
            return Author.FullName+": \""+Title+"\"";
        }
    }
}
