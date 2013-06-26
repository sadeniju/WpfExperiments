using System;
using System.Windows;
using SharedClasses.SampleViewModels;

namespace DataBindingPracticeDos{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        //public event PropertyChangedEventHandler PropertyChanged;
        GameCatalogueViewModel catalogueViewModel;

        // Main Application
        public MainWindow(){
            InitializeComponent();
            catalogueViewModel = new GameCatalogueViewModel();   // dummy
            catalogueViewModel.CreateDummy();

            this.DataContext = catalogueViewModel;   // Binding of the catalogue object (is now known to the GUI)
            // INFO: Binding of multiple objects using an anonymous type: this.DataContext = new {catalogue, instance};
            // Due to the binding of multiple objects, the objects must be addressed explicitly in the xaml-file
            // e.g.: "..Binding Path=catalogue.Author.." instead of "..Binding Path=Author.."

            Console.WriteLine(catalogueViewModel.ToString());
            //Console.Read();

            // TASKS:
            // 1.   DONE - Implement the classes Game and GameCatalogue
            // 2.   DONE - Display a GameCatalogue (WPF)
            // 2.1  DONE - Implement DataBinding (display the author and games of a catalogue)
            // 2.2  DONE - Display the list of games _correctly_ => maybe use a converter to convert the object to strings? => unnecessary! Just implement ToString()
            // 2.3  DONE - Add a new game to the catalogue => Will the view be updated? Nope, implement event subscription?
            // 2.4  DONE - Update the view => Event Subscription? INotify..?
            // 2.5  DONE - Use the INotifyPropertyChanged-Interface => Where does this make sense?
            // 3.   todo - Extend the PresentationBuilder
        }

        /// <summary>
        /// Method which reacts to a click on the addGameButton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addGameButton_Click(object sender, RoutedEventArgs e){
            Console.WriteLine("Processing new game..");
            string tmpTitle = newGameTitle.Text;
            string tmpPublisher = newGamePublisher.Text;
            GameViewModel newGameViewModel = new GameViewModel();

            if (tmpTitle.Length > 0 && tmpPublisher.Length > 0){
                newGameViewModel.Title = tmpTitle;
                newGameViewModel.Publisher = tmpPublisher;
                catalogueViewModel.Add(newGameViewModel);
                Console.WriteLine(newGameViewModel.ToString() + " successfully added to the catalogue.");
                Console.WriteLine(catalogueViewModel.ToString());
            }
            else{
                Console.WriteLine("Please provide both a title and a publisher.");
            }
            
        }

        /// <summary>
        /// Method which is called when the author is being edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editAuthor_Click(object sender, RoutedEventArgs e){
            catalogueViewModel.AuthorViewModel.FullName = authorTextBox.Text;
        }
    }
}
