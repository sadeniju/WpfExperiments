using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedClasses.SampleModels;

namespace SharedClasses.SampleViewModels {
    public class GameViewModel: BaseViewModel {
        #region Properties
        public Game Model { get; private set; }

        public string Title {
            get { return Model.Title; }
            set { 
                Model.Title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Publisher {
            get { return Model.Publisher; }
            set { 
                Model.Publisher = value; 
                RaisePropertyChanged("Publisher");
            }
        }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs a game viewmodel.
        /// </summary>
        /// <param name="model"></param>
        public GameViewModel(Game model) {
            Model = model;
        }

        public GameViewModel(): this(new Game()) {}
        #endregion

        public override string ToString(){
            return "\""+Model.Title+"\" published by "+Model.Publisher;
        }
    }
}
