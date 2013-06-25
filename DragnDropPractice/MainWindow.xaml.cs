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
using SampleModels;

namespace DragnDropPractice {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Point startPoint = new Point();
        BookCollection bCollection;

        public MainWindow() {

            ObservableCollection<Book> books = new ObservableCollection<Book> { new Book("Stephen Hawking", "A Brief History Of Time"),
                                                                                new Book("Unknown", "Don't Try This At Home"),
                                                                                new Book("George Gamov","One Two Three Infinity"),
                                                                                new Book("Elizabeth Corley","Fatal Legacy"),
                                                                                new Book("Goethe","Faust"),
                                                                                new Book("Elizabeth George", "Believing The Lie"),
                                                                                new Book("Harper Lee", "To Kill a Mockingbird")};
            bCollection = new BookCollection(books);
            this.DataContext = bCollection;  // bCollection = DataContext as BookCollection;

            InitializeComponent();
        }

        /// <summary>
        /// Detects mouse movement (starts dragging if lmb is pressed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_MouseMove(object sender, MouseEventArgs e) {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
 
            if (e.LeftButton == MouseButtonState.Pressed)   //@TODO define when dragging starts (mouse position)
            {
                Console.WriteLine("Dragging");

                // get dragged item
                ListBox lb = sender as ListBox; // view element from which the dragged item originated
                ListBoxItem lbi = FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);    // dragged item
 
                // get data behind dragged item
                Book b = lb.ItemContainerGenerator.ItemFromContainer(lbi) as Book;
                Console.WriteLine(b.Title);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", b );
                DragDrop.DoDragDrop(lbi, dragData, DragDropEffects.Move);
            } 
        }

        /// <summary>
        /// Detects lmb pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Console.WriteLine("Left button pressed.");
            startPoint = e.GetPosition(null);
        }

        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if( current is T )
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void BookListBox_Drop(object sender, DragEventArgs e) {
            // Implement DragEnter event if item originates from another element

            if (e.Data.GetDataPresent("myFormat")){
                Book b = e.Data.GetData("myFormat") as Book;
                
                Console.WriteLine("Dropping " + b.Title);

                // @TODO find out where to insert the dragged item
                int index = 1;
                //ListBox lb = sender as ListBox;

                // move dragged book to the chosen position
                // remove from previous position
                bCollection.Books.RemoveAt(bCollection.Books.IndexOf(b));
                // @TODO insert at new position
                bCollection.Books.Insert(index, b);
            }
        }

        
    }
}
