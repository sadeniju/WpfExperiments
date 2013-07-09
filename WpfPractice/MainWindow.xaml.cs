using System;
using System.Windows;
using SharedClasses.SampleViewModels;
using SharedClasses.SampleModels;

namespace CSharpPractice
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookViewModel myBook = new BookViewModel(new Book(new Author("Stephen Hawking"), "A Brief History Of Time", "03.01.1999"));   // default private

        private int NumberOfClicks
        {
            get;
            set;
        }

        private string Output
        {
            get;
            set;
        }

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.NumberOfClicks = 0;
            this.Output = "Ouch, you didn't have to click that hard";

            Console.WriteLine(myBook.AuthorViewModel + ": " + myBook.Title);
            this.DataContext = myBook;   // Objekt zur Verfuegung stellen, damit in XAML verwendbar (DataBinding)
        }

        /// <summary>
        ///     myButton-Event: Da die Methode im xaml-File referenziert wird, 
        ///     wird sie jedesmal aufgerufen, sobald das (Click-)Event ausgeloest wurde.
        /// </summary>
        /// <param name="sender">Absender</param>
        /// <param name="e">Daten des Events</param>
        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            NumberOfClicks++;
            messageText.Text = Output + "...";
            Output += (NumberOfClicks > 1) ? " and again" : " again";
        }
    }
}
