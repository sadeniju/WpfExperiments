using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SampleModels
{
    /// <summary>
    ///     Class representing a game catalogue which has an author and a list of games.
    /// </summary>
    public class GameCatalogue : INotifyPropertyChanged {
        #region Private fields
        Author author;
        ObservableCollection<Game> games;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        // Field and property of the catalogue's author
        public Author Author{
            get { return author; }
            set { 
                if (value != null) {
                    this.author = value;
                    OnPropertyChanged("Author");
                }
            }
        }

        public ObservableCollection<Game> Games{
            get { return games; }
            private set {
                games = value;
                //OnPropertyChanged("Games");   // Unnecessary => ObservableCollection already ensures view update!
            }
        }
        #endregion

        #region Construction
        public GameCatalogue(Author author, ObservableCollection<Game> games){
            this.author = author;
            this.games = games;
        }

        public GameCatalogue(Author author) : this(author, new ObservableCollection<Game>()) { }
        #endregion

        // Method for adding a single game.
        public void Add(Game game) {
            if (!games.Contains(game)){
                games.Add(game);
                //OnPropertyChanged("Games");   // Unnecessary => ObservableCollection already ensures view update!
            }
        }

        // Method for adding multiple games (method overloading).
        public void Add(List<Game> games){
            foreach(Game g in games){
                if (!this.games.Contains(g)){
                    this.games.Add(g);
                    //OnPropertyChanged("Games");   // Unnecessary => ObservableCollection already ensures view update!
                }
            }
        }

        // Implementation of the toString-method (!! methods => PascalCasing instead of camelCasing !!)
        public override string ToString(){
            string catRepresentation = "Game Catalogue by "+this.author+":\r\n\r\n";

            foreach(Game g in games)
                catRepresentation += g.ToString() + "\r\n";

            return catRepresentation;
        }

        // Method which is called when a property changes.
        private void OnPropertyChanged(string propertyName){
            if (PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));  // event notifies all subscribers
            }
        }
    }
}
