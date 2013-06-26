using SharedClasses.SampleModels;
using System.Collections.ObjectModel;
using SharedClasses.Commands;
using System.Collections.Specialized;

namespace SharedClasses.SampleViewModels {
    /// <summary>
    /// Represents the ViewModel of a book.
    /// </summary>
    public class BookViewModel : BaseViewModel {
        private PageViewModel selectedPage;

        #region Properties
        public Book Model { get; private set; }

        public string Title { 
            get { return Model.Title; }
            set {
                Model.Title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string ReleaseDate {
            get { return Model.ReleaseDate; }
            set {
                Model.ReleaseDate = value;
                RaisePropertyChanged("ReleaseDate");
            }
        }

        public AuthorViewModel AuthorViewModel { get; private set; }

        public PageViewModel SelectedPage {
            get { return selectedPage; }
            set {
                selectedPage = value;
                RemovePageCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<PageViewModel> PageViewModels { get; private set; }

        public RemovePageCommand RemovePageCommand { get; private set; }
        public AddPageCommand AddPageCommand { get; private set; }
        #endregion

        #region Construction
        public BookViewModel(Book model) {
            Model = model;
            AuthorViewModel = new AuthorViewModel(model.Author);

            PageViewModels = new ObservableCollection<PageViewModel>();
            foreach (Page page in model.Pages) {
                PageViewModels.Add(new PageViewModel(page));
            }
            PageViewModels.CollectionChanged += new NotifyCollectionChangedEventHandler(PageViewModels_CollectionChanged);

            AddPageCommand = new AddPageCommand(this);
            RemovePageCommand = new RemovePageCommand(this);
        }

        public BookViewModel() : this(new Book()) { }
        #endregion

        private void PageViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add){
                foreach (PageViewModel pageViewModel in e.NewItems){
                    Model.Pages.Add(pageViewModel.Page);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove){
                foreach (PageViewModel pageViewModel in e.OldItems){
                    Model.Pages.Remove(pageViewModel.Page);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset){
                Model.Pages.Clear();
            }
            RemovePageCommand.RaiseCanExecuteChanged();
        }

        public override string ToString() {
            return Model.ToString();
        }
    }
}
