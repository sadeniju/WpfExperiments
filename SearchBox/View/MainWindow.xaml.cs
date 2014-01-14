using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace SearchBox {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public ObservableCollection<Book> Books { get; private set; }   // subcollection of the library's books (depending on the search string)
        private List<Book> library; // the lib contains book
        private string searchLabelContent = "Search...";

        /// <summary>
        /// Construction.
        /// </summary>
        public MainWindow() {
            // Create a dummy list of books
            library = new List<Book>{
                new Book("George Gamow", "One Two Three Infinity", "01/01/1975"),
                new Book("Max Brooks", "World War Z", "06/01/2013"),
                new Book("Gillian Flynn", "Dark Places", "13/09/2013"),
                new Book("Somebody Else", "The Mysterious Flame", "24/11/2013"),
                new Book("Some Author", "How To Kill A Mockingbird", "17/05/2013"),
                new Book("Karin Slaughter", "Triptych", "07/31/2013"),
                new Book("Some Knuth", "The Art of Computer Programming", "06/01/2013")
            };

            Books = new ObservableCollection<Book>();
            foreach (Book b in library) {
                Books.Add(b);
            }

            this.DataContext = Books; // set the collection of books as the windows datacontext (to bind to)

            InitializeComponent(); // default
        }

        /// <summary>
        /// Detects search string changes and updates the selection of books..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e) {
            string searchString = searchBox.Text;
            searchLabel.Content = "";
            Books.Clear();

            // Update the displayed list of books
            if (!string.IsNullOrWhiteSpace(searchString)) {
                searchLabelContent = searchString;
                searchString = searchString.ToUpperInvariant();

                // Update books
                List<Book> selectedBooks = (from book in library
                                            where book.Title.ToUpperInvariant().Contains(searchString) || book.Author.ToUpperInvariant().Contains(searchString)
                                            select book).ToList<Book>();
                Books.AddAll<Book>(selectedBooks);
            }
            else {
                // Reset label
                searchLabelContent = "Search...";
                searchLabel.Content = searchLabelContent;

                // Reset books
                Books.AddAll<Book>((from book in library select book).ToList<Book>());
            }
        }

        /// <summary>
        /// Resets the search field's label on mouse click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            searchLabel.Content = "";
        }

        /// <summary>
        /// Updates the search box's label to the default "Search..." whenever the search box is unfocused and empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
                searchLabel.Content = searchLabelContent;
        }

        /// <summary>
        /// Resets the displayed list of books.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e) {
            // Reset label
            searchBox.Text = "";
            searchLabelContent = "Search...";
            searchLabel.Content = searchLabelContent;

            // Reset books
            Books.Clear();
            Books.AddAll<Book>((from book in library select book).ToList<Book>());
        }
    }
}
