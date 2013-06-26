using System.Windows;
using SharedClasses.SampleModels;
using SharedClasses.SampleViewModels;
using System;

namespace MvvmCollectionExample {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        #region Properties
        public LibraryViewModel LibraryViewModel { get; private set; }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs the MainWindow.
        /// </summary>
        public MainWindow() {
            LibraryViewModel = new LibraryViewModel();
            LibraryViewModel.CreateDummy();
            this.DataContext = LibraryViewModel;
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Adds a new book to the library.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBook_Click(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrEmpty(authorInput.Text) && !string.IsNullOrEmpty(titleInput.Text)) {
                BookViewModel newBookViewModel = new BookViewModel();
                newBookViewModel.AuthorViewModel.FullName = authorInput.Text;
                newBookViewModel.Title = titleInput.Text;
                LibraryViewModel.BookViewModels.Add(newBookViewModel);
            }

            Console.WriteLine("- MODELS -");
            foreach (Book b in LibraryViewModel.Model.Books) {
                Console.WriteLine(b.ToString());
            }
        }
        #endregion
    }
}
