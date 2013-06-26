using System.Windows;
using SharedClasses.SampleViewModels;

namespace DataBindingPractice
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        BookViewModel myBookViewModel;

        public MainWindow(){
            InitializeComponent();
            myBookViewModel = new BookViewModel();
            myBookViewModel.AuthorViewModel.FullName = "Stephen Hawking";
            myBookViewModel.Title = "A Brief History Of Time";
            myBookViewModel.ReleaseDate = "01.01.1999";
            this.DataContext = myBookViewModel;
        }

        // Method which is called when the book's author is edited.
        private void editAuthorButton_Click(object sender, RoutedEventArgs e){
            myBookViewModel.AuthorViewModel.FirstName = authorFirstNameTextBox.Text; // Autor aendern (Inhalt des Textfelds)
            myBookViewModel.AuthorViewModel.Surname = authorSurnameTextBox.Text; // Autor aendern (Inhalt des Textfelds)

            // Der Autor aendert sich direkt, eigentlich bekommt die GUI das nicht mit?
            // => Loesung: Buch subscribed Author-Event, bekommt also mit wenn sich dort was aendert
            // sobald Autor sich aendert sendet jetzt auch das Buch ein PropertyChanged Event an View.
            // _Genau_ dafuer muss das Buch das INotify... Interface implementieren (damit View notified?).
        }
    }
}
