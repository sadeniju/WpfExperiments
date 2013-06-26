using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SharedClasses.SampleModels
{
    /// <summary>
    ///     Class representing a game catalogue which has an author and a list of games.
    /// </summary>
    public class GameCatalogue {
        private Author author;

        #region Properties
        // Field and property of the catalogue's author
        public Author Author{
            get { return author; }
            set { 
                if (value != null) {
                    this.author = value;
                }
            }
        }

        public List<Game> Games{ get; set; }

        public string Title { get; set; }
        #endregion

        #region Construction
        public GameCatalogue(Author author, string title, List<Game> games): this(author, title){
            Games = games;
        }

        public GameCatalogue(Author author, string title) : this(){
            Author = author;
            Title = title;
        }

        public GameCatalogue() {
            Author = new Author();
            Games = new List<Game>();
        }
        #endregion

        /// <summary>
        /// Adds default values to the catalogue.
        /// </summary>
        public void CreateDummy() {
            Games = new List<Game>() {
                new Game("Bioshock", "2K"), 
                new Game("Borderlands", "Gearbox Software"),
                new Game("Portal", "Valve"),
                new Game("Deadspace", "EA"),
                new Game("Alice: Madness Returns", "EA"),
                new Game("God Of War", "Spicy Horse") 
            };

            Author.FullName = "Ken Levine";
            Title = "The Ultimate Game Catalogue";
        }

        /// <summary>
        /// Adds a single game to the catalogue.
        /// </summary>
        /// <param name="game"></param>
        public void Add(Game game) {
            if (!Games.Contains(game)){
                Games.Add(game);
                //OnPropertyChanged("Games");   // Unnecessary => ObservableCollection implements the INotifyCollectionChanged interface!
            }
        }

        /// <summary>
        /// Adds multiple games to the catalogue.
        /// </summary>
        /// <param name="games"></param>
        public void Add(List<Game> games){
            foreach(Game g in games){
                if (!Games.Contains(g)){
                    Games.Add(g);
                    //OnPropertyChanged("Games");   // Unnecessary => ObservableCollection implements the INotifyCollectionChanged interface!
                }
            }
        }

        /// <summary>
        /// Implementation of the ToString-method (!! methods => PascalCasing instead of camelCasing !!)
        /// </summary>
        /// <returns></returns>
        public override string ToString(){
            string catRepresentation = "Game Catalogue by "+Author+":\r\n\r\n";

            foreach(Game g in Games)
                catRepresentation += g.ToString() + "\r\n";

            return catRepresentation;
        }
    }
}
