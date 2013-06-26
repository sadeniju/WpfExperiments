using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SharedClasses.SampleModels;
using SharedClasses.ExtensionMethods;
using System.Collections.Generic;

namespace SharedClasses.SampleViewModels {
    /// <summary>
    /// Represents the ViewModel of a library.
    /// </summary>
    public class LibraryViewModel : BaseViewModel {
        #region Properties
        public Library Model { get; private set; }
        public string Name { 
            get { return Model.Name; }
            set { Model.Name = value; }
        }
        public ObservableCollection<BookViewModel> BookViewModels { get; set; }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs the view model of a library instance.
        /// </summary>
        /// <param name="model"></param>
        public LibraryViewModel(Library model) {
            Model = model;
            InstantiateBookViewModels(model.Books);
        }

        /// <summary>
        /// Creates a dummy library which is represented by the viewmodel.
        /// </summary>
        public LibraryViewModel(): this(new Library()) {}
        #endregion

        public void CreateDummy() {
            Model.CreateDummy();
            BookViewModels.CollectionChanged -= Books_CollectionChanged;
            InstantiateBookViewModels(Model.Books);
        }

        private void InstantiateBookViewModels(ICollection<Book> bookModels) {
            BookViewModels = new ObservableCollection<BookViewModel>();

            foreach (Book book in bookModels) {
                BookViewModels.Add(new BookViewModel(book));
            }

            BookViewModels.CollectionChanged += Books_CollectionChanged;
        }

        #region Book collection manipulation
        public void Add(BookViewModel bookViewModel) {
            if (!BookViewModels.Contains(bookViewModel)) {
                BookViewModels.Add(bookViewModel);
            }
        }

        public void Remove(BookViewModel bookViewModel) {
            if (BookViewModels.Contains(bookViewModel)) {
                BookViewModels.Remove(bookViewModel);
            }
        }

        /// <summary>
        /// Manipulates the list of books.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Books_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e){
            if (e.Action == NotifyCollectionChangedAction.Add){
                foreach (BookViewModel bookViewModel in e.NewItems){
                    Model.Books.Add(bookViewModel.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove){
                foreach (BookViewModel bookViewModel in e.OldItems){
                    Model.Books.Remove(bookViewModel.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset){
                Model.Books.Clear();
            }
        }
        #endregion
    }
}
