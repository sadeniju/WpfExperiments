using System;
using System.Windows;
using System.Collections.ObjectModel;
using SharedClasses.SampleModels;
using SharedClasses.SampleViewModels;

namespace TreeViewExample {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public ObservableCollection<AuthorViewModel>Authors { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow() {
            Authors = new ObservableCollection<AuthorViewModel> 
            {
                new AuthorViewModel{FullName = "Elizabeth Corley", 
                            PublishedBooks = new ObservableCollection<BookViewModel>{
                                new BookViewModel{ Title = "Fatal Legacy"}, 
                                new BookViewModel{ Title = "Grave Doubts"}, 
                                new BookViewModel{ Title = "Innocent Blood"}}},
                new AuthorViewModel{FullName = "George Gamov", 
                            PublishedBooks = new ObservableCollection<BookViewModel>{
                                new BookViewModel{ Title = "One Two Three Infinity"}}},
                new AuthorViewModel{ FullName = "Stephen Hawking", 
                            PublishedBooks = new ObservableCollection<BookViewModel>{
                                new BookViewModel{Title = "The Grand Design"}, 
                                new BookViewModel{ Title = "A Brief History Of Time"}, 
                                new BookViewModel{ Title = "The Universe in a Nutshell"}}},
                new AuthorViewModel{FullName = "Phillip K. Dick", 
                            PublishedBooks = new ObservableCollection<BookViewModel>{
                                new BookViewModel{Title = "Do Androids Dream of Electric Sheep"}}}
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
                if (TreeView1.SelectedItem is AuthorViewModel) {
                    Console.WriteLine("author selected");
                    Authors[(Authors.IndexOf(TreeView1.SelectedItem as AuthorViewModel))].PublishedBooks.Add(new BookViewModel{Title = input});
                    Console.WriteLine("book added");
                }
            }
            else {
                Authors.Add(new AuthorViewModel { FullName = input });
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
                if (TreeView1.SelectedItem is AuthorViewModel) {
                    Authors.Remove(TreeView1.SelectedItem as AuthorViewModel);
                }
                else if (TreeView1.SelectedItem is BookViewModel) {
                    AuthorViewModel authorContainingBook = null;
                    BookViewModel bookToDelete = null;

                    // irgendwie vom buch auf parent kommen?
                    foreach (AuthorViewModel a in Authors) {
                        foreach (BookViewModel b in a.PublishedBooks) {
                            if (b.Equals(TreeView1.SelectedItem as BookViewModel)) {
                                authorContainingBook = a;
                                bookToDelete = b;
                                Console.WriteLine("found {0}[{1}]", b.Title, b.ReleaseDate);
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
