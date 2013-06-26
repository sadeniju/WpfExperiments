using System;
using System.Windows;
using System.Collections.ObjectModel;
using SharedClasses.SampleModels;

namespace TreeViewExample {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public ObservableCollection<Author>Authors { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow() {
            Authors = new ObservableCollection<Author> 
            {
                new Author("Elizabeth Corley", 
                            new ObservableCollection<Book>{new Book("Fatal Legacy"), new Book("Grave Doubts"), new Book("Innocent Blood")}),
                new Author("George Gamov", 
                            new ObservableCollection<Book>{new Book("One Two Three Infinity")}),
                new Author("Stephen Hawking", 
                            new ObservableCollection<Book>{new Book("The Grand Design"), new Book("A Brief History Of Time"), new Book("The Universe in a Nutshell")}),
                new Author("Phillip K. Dick", 
                            new ObservableCollection<Book>{new Book("Do Androids Dream of Electric Sheep")})
            };

            //FillTree();
            this.DataContext = Authors;
            InitializeComponent();
        }

        /// <summary>
        /// Adds an element to the TreeView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddChildButton_Click(object sender, RoutedEventArgs e) {
            string input = ChildName.Text;

            if (TreeView1.SelectedItem != null) {
                if (TreeView1.SelectedItem is Author) {
                    Console.WriteLine("author selected");
                    Authors[(Authors.IndexOf(TreeView1.SelectedItem as Author))].PublishedBooks.Add(new Book(input));
                    Console.WriteLine("book added");
                }
            }
            else {
                Authors.Add(new Author(input, null));
                Console.WriteLine("author added");
            }
        }

        /// <summary>
        /// Delete a TreeViewItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem_Click(object sender, RoutedEventArgs e) {
            // removing rootitems
            if (TreeView1.SelectedItem != null) {
                if (TreeView1.SelectedItem is Author) {
                    Authors.Remove(TreeView1.SelectedItem as Author);
                }
                else if (TreeView1.SelectedItem is Book) {
                    Author authorContainingBook = new Author();
                    Book bookToDelete = new Book();

                    // irgendwie vom buch auf parent kommen?
                    foreach (Author a in Authors) {
                        foreach (Book b in a.PublishedBooks) {
                            if (b.Equals(TreeView1.SelectedItem as Book)) {
                                authorContainingBook = a;
                                bookToDelete = b;
                                Console.WriteLine("found {0}[{1}]", b.Title, b.Id);
                            }
                        }
                        //if (a.PublishedBooks.Contains(TreeView1.SelectedItem as Book)) {
                        //    a.PublishedBooks.Remove(TreeView1.SelectedItem as Book);
                        //}
                    }
                    authorContainingBook.PublishedBooks.Remove(bookToDelete);
                }
            }
        }
    }
}
