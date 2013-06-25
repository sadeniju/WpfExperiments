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
using MvvmCollectionExample.Models;
using MvvmCollectionExample.ViewModels;
using SampleModels;

namespace MvvmCollectionExample {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        #region Properties
        public LibraryViewModel lib { get; private set; }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs the MainWindow.
        /// </summary>
        public MainWindow() {
            lib = new LibraryViewModel(new Library());
            this.DataContext = lib;
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
                lib.Books.Add(new BookViewModel(new Book(authorInput.Text, titleInput.Text)));
            }
        }
        #endregion
    }
}
