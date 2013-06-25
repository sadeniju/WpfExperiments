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
using System.ComponentModel;    // Propertychanged events
using System.Collections.ObjectModel;
using SampleModels;   // ObservableCollection

namespace DataBindingPracticeDos
{
    /// <summary>
    ///     Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        GameCatalogue catalogue;

        // Main Application
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<Game> games = new ObservableCollection<Game>() {new Game("Bioshock", "2K"), 
                                                                                new Game("Borderlands", "Gearbox Software"),
                                                                                new Game("Portal", "Valve"),
                                                                                new Game("Deadspace", "EA"),
                                                                                new Game("Alice: Madness Returns", "EA"),
                                                                                new Game("God Of War", "Spicy Horse") };

            catalogue = new GameCatalogue(new Author("Kevin", "Levine"), games);

            this.DataContext = catalogue;   // Binding of the catalogue object (is now known to the GUI)
            // INFO: Binding of multiple objects using an anonymous type: this.DataContext = new {catalogue, instance};
            // Due to the binding of multiple objects, the objects must be addressed explicitly in the xaml-file
            // e.g.: "..Binding Path=catalogue.Author.." instead of "..Binding Path=Author.."

            Console.WriteLine(catalogue.ToString());
            //Console.Read();

            // TASKS:
            // 1.   DONE - Klasse Game und GameCatalogue implementieren
            // 2.   DONE - Mit WPF einen GameCatalogue anzeigen
            // 2.1  DONE - DataBinding umsetzen (Author und Spiele des Katalogs anzeigen)
            // 2.2  DONE - Die Spieleliste des Katalogs _richtig_ anzeigen => Converter benutzen (von obj zu string)?
            // 2.3  DONE - Neues Spiel zum Katalog hinzufuegen => Bekommt View das mit (wird Liste aktualisiert)? Nope, Event Subscription implementieren?
            // 2.4  DONE - View aktualisieren => Event Subscription? INotify..?
            // 2.5  DONE - INotifyPropertyChanged-Interface benutzen => Wo macht das Sinn?
            // 3.   todo - PresentationBuilder erweitern
        }

        // Method which reacts to a click on the addGameButton.
        private void addGameButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Processing new game..");
            string tmpTitle = newGameTitle.Text;
            string tmpPublisher = newGamePublisher.Text;
            Game newGame = new Game();

            if (tmpTitle.Length > 0 && tmpPublisher.Length > 0)
            {
                newGame.Title = tmpTitle;
                newGame.Publisher = tmpPublisher;
                catalogue.Add(newGame);
                Console.WriteLine(newGame.ToString() + " successfully added to the catalogue.");
                Console.WriteLine(catalogue.ToString());
            }
            else
            {
                Console.WriteLine("Please provide both a title and a publisher.");
            }
            
        }

        // Method which is called when the author is edited.
        private void editAuthor_Click(object sender, RoutedEventArgs e)
        {
            string[] names = authorTextBox.Text.Split();
            if (names.Length == 2) {
                catalogue.Author = new Author(names[0], names[1]);
            }
        }
    }
}
