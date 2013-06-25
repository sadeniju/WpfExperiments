using System.Windows;
using SampleModels;

namespace DataBindingPractice
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Book myBook;
        Author myAuthor;

        public MainWindow()
        {
            InitializeComponent();
            myAuthor = new Author("Stephen", "Hawking");
            myBook = new Book(myAuthor, "A Brief History Of Time", "01.01.1999");
            this.DataContext = myBook;
        }

        // Method which is called when the book's author is edited.
        private void editAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            myAuthor.FirstName = authorFirstNameTextBox.Text; // Autor aendern (Inhalt des Textfelds)
            myAuthor.Surname = authorSurnameTextBox.Text; // Autor aendern (Inhalt des Textfelds)

            // Der Autor aendert sich direkt, eigentlich bekommt die GUI das nicht mit?
            // => Loesung: Buch subscribed Author-Event, bekommt also mit wenn sich dort was aendert
            // sobald Autor sich aendert sendet jetzt auch das Buch ein PropertyChanged Event an View.
            // _Genau_ dafuer muss das Buch das INotify... Interface implementieren (damit View notified?).
        }
    }
}
