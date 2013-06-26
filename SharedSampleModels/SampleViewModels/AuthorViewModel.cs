using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedClasses.SampleModels;

namespace SharedClasses.SampleViewModels {
    public class AuthorViewModel: BaseViewModel {
        #region Properties
        public Author Model { get; private set; }

        public string FirstName {
            get { return Model.FirstName; }
            set {
                Model.FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string Surname {
            get { return Model.Surname; }
            set {
                Model.Surname = value;
                RaisePropertyChanged("Surname");
            }
        }

        public string FullName {
            get { return Model.FullName; }
            set {
                Model.FullName = value;
                RaisePropertyChanged("FullName");
            }
        }
        #endregion

        #region Construction
        public AuthorViewModel(Author model) {
            Model = model;
        }

        public AuthorViewModel() : this(new Author()) { }
        #endregion

        public override string ToString() {
            return Model.ToString();
        }
    }
}
