using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SharedClasses.SampleModels;

namespace LinqToXmlExample
{
    /// <summary>
    /// LINQ to XML Example.
    /// </summary>
    class Program
    {
        static XDocument file;
        static Book myBook;

        static void Main(string[] args){
            //CreateXmlManually();
            CreateXmlFromObjects();
            FindBookByTitle("REAMDE");

            Console.Read();
        }

        /// <summary>
        /// Search an XML file.
        /// </summary>
        /// <param name="title"></param>
        static void FindBookByTitle(string title){
            Console.WriteLine("Searching for a book named \""+title+"\"..");

            #region search using child element values
            // Element and Elements only search for the children (they don't go further) 
            var query = from book in file.Element("Books").Elements("Book") // Elements() returns all children
                        where book.Element("Title").Value == title
                        select book;   // NullReferenceException wenn es eines der Elemente nicht gibt => fangen! 
            #endregion search using child element values

            #region search using descendant axes
            // Descendants looks in the children, the children's children and so on (all elements with the specified name)
            query = from book in file.Descendants("Book")
                    where book.Element("Title").Value == title
                    select book;
            #endregion search using descendant axis

            #region search for parent of the title
            // Parent looks up one level 
            query = from bookTitle in file.Descendants("Title")
                    where bookTitle.Value.Equals(title)
                    select bookTitle.Parent;
            #endregion search for parent of the title

            #region more navigation options
            // Ancestors looks all the way up to the root
            // ElementsBeforeSelf and ElementsAfterSelf looks at the siblings
            #endregion more navigation options

            XElement oneBook = query.SingleOrDefault(); // returns the _single_ result, null if more than one result, exception if none

            if (oneBook != null)
            {
                // Book found:
                Console.WriteLine("Book found.");

                myBook = new Book(oneBook.Element("Author").Value,
                     oneBook.Element("Title").Value,
                     oneBook.Attribute("ReleaseDate").Value
                );

                Console.WriteLine(myBook.Author + " : " + myBook.Title + " (" + myBook.ReleaseDate + ")");
            }
            else{
                Console.WriteLine("Book not found.");
            }
        }

        /// <summary>
        /// Create an XML file manually.
        /// </summary>
        static void CreateXmlManually(){
            file = new XDocument(
                new XElement("Books",
                    new XElement("Book",
                        new XAttribute("ReleaseDate", "2007/08/01"),
                        new XElement("Author", "Elizabeth Corley"),
                        new XElement("Title", "Pretty Little Things")),
                    new XElement("Book",
                        new XAttribute("ReleaseDate", "2011/11/01"),
                        new XElement("Title", "A Brief History Of Time"),
                        new XElement("Author", "Stephen Hawking")),
                    new XElement("Book",
                        new XAttribute("ReleaseDate", "2001/03/20"),
                        new XElement("Title", "REAMDE"),
                        new XElement("Author", "Neal Stephenson"))
                ));
            file.Save("books.xml");
        }

        /// <summary>
        /// Create an XML file from existing objects.
        /// </summary>
        static void CreateXmlFromObjects(){
            List<Book> books = new List<Book> { 
                new Book { Author = new Author("Elizabeth Corley"), Title = "Pretty Little Things", ReleaseDate = "2007/08/01" },
                new Book("Stephen Hawking", "A Brief History Of Time", "2011/11/01"),
                new Book("Neal Stephenson", "REAMDE",  "2001/03/20" )
            };

            file = new XDocument(
                new XElement("Books",
                    from book in books select new XElement("Book",
                        new XAttribute("ReleaseDate", book.ReleaseDate),
                        new XElement("Author", book.Author),
                        new XElement("Title", book.Title))
            ));

            file.Save("books.xml");
        }
    }
}
