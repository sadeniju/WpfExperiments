using System;
using System.Collections.Generic;
using SharedClasses.SampleModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SharedClasses.SampleViewModels {
    public class GameCatalogueViewModel: BaseViewModel {
        #region Properties
        public GameCatalogue Model { get; private set; }

        public AuthorViewModel AuthorViewModel { get; private set; }

        public string Title {
            get { return Model.Title; }
            set { 
                Model.Title = value; 
                RaisePropertyChanged("Publisher");
            }
        }

        public ObservableCollection<GameViewModel> GameViewModels { get; private set; }
        #endregion

        #region Construction
        public GameCatalogueViewModel(GameCatalogue model) {
            Model = model;
            AuthorViewModel = new AuthorViewModel(model.Author);
            InstantiateGameViewModels(Model.Games);
        }

        /// <summary>
        /// Creates a game catalogue viewmodel with a dummy model.
        /// </summary>
        public GameCatalogueViewModel(): this(new GameCatalogue()) {}
        #endregion

        public void CreateDummy() {
            Model.CreateDummy();
            AuthorViewModel.FullName = Model.Author.FullName;
            GameViewModels.CollectionChanged -= new NotifyCollectionChangedEventHandler(GameViewModels_CollectionChanged);
            InstantiateGameViewModels(Model.Games);
        }

        private void InstantiateGameViewModels(ICollection<Game> gameModels) {
            GameViewModels = new ObservableCollection<GameViewModel>();

            foreach (Game game in gameModels) {
                GameViewModels.Add(new GameViewModel(game));
            }

            GameViewModels.CollectionChanged += new NotifyCollectionChangedEventHandler(GameViewModels_CollectionChanged);
        }

        #region Game list manipulation
        public void Add(GameViewModel gameViewModel) {
            if (!GameViewModels.Contains(gameViewModel)) {
                GameViewModels.Add(gameViewModel);
            }
        }

        public void Remove(GameViewModel gameViewModel) {
            if (GameViewModels.Contains(gameViewModel)) {
                GameViewModels.Remove(gameViewModel);
            }
        }

        private void GameViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add){
                foreach (GameViewModel gameViewModel in e.NewItems){
                    Model.Games.Add(gameViewModel.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove){
                foreach (GameViewModel gameViewModel in e.OldItems){
                    Model.Games.Remove(gameViewModel.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset){
                Model.Games.Clear();
            }
        }
        #endregion
    }
}
