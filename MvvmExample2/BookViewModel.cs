using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using SampleModels;

namespace MvvmExample2 
{
    public class BookViewModel : NotifyableObject {
        #region Fields
        private Book book;
        private ObservableCollection<PageViewModel> pages = new ObservableCollection<PageViewModel>();
        #endregion

        #region Properties
        public PageViewModel SelectedPage { get; set; }
        public string TitleText { get; set; }
        public Book Book { get { return book; } }
        public string Title { get { return Book.Title; } set { Book.Title = value; RaisePropertyChanged("Title"); } }
        public ObservableCollection<PageViewModel> Pages { get { return pages; } }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs an instance of the BookViewModel.
        /// </summary>
        public BookViewModel() {
            book = new Book{Title="Do Androids Dream Of Electric Sheep?"};
        }
        #endregion

        #region Commands
        #region Remove page
        /// <summary>
        /// Removes a page from the book.
        /// </summary>
        private void RemovePageExecute() {
            Book.RemovePage(SelectedPage.Page); // removing the page from the datamodel
            pages.Remove(SelectedPage); // removing the viewmodelpage from the book viewmodel

            // test output
            Console.WriteLine("Datamodel pages: ");
            if (Book.Pages != null && Book.Pages.Count > 0) {
                foreach (SampleModels.Page p in Book.Pages) {
                    Console.WriteLine(p.Number);
                }
            }
        }

        /// <summary>
        /// Tells if a page can be removed.
        /// </summary>
        /// <returns></returns>
        private bool CanRemovePage() {
            if (pages.Count > 0 && SelectedPage != null) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Command to remove a page.
        /// </summary>
        public ICommand RemovePageCommand {
            get {
                return new RelayCommand(RemovePageExecute, CanRemovePage);
            }
        }
        #endregion

        #region Add page
        /// <summary>
        /// Adds a new page to the book.
        /// </summary>
        private void AddPageExecute() {
            PageViewModel newPageViewModel = new PageViewModel();
            Book.AddPage(newPageViewModel.Page);    // add a new page to the datamodel
            Pages.Add(newPageViewModel);    // add the page viewmodel to the book viewmodel

            // test output
            Console.WriteLine("Datamodel pages: ");
            if (Book.Pages != null && Book.Pages.Count > 0) {
                foreach (SampleModels.Page p in Book.Pages) {
                    Console.WriteLine(p.Number);
                }
            }
        }

        /// <summary>
        /// Tells if a new page can be added to the book.
        /// </summary>
        /// <returns></returns>
        private bool CanAddPage() {
            return true;
        }

        /// <summary>
        /// Command to add a page to the book.
        /// </summary>
        public ICommand AddPageCommand {
            get {
                return new RelayCommand(AddPageExecute, CanAddPage);
            }
        }
        #endregion

        #region Update title
        /// <summary>
        /// Renames the book.
        /// </summary>
        private void UpdateBookTitleExecute(){
            if (! string.IsNullOrEmpty(TitleText)) {
                Title = TitleText;
                Console.WriteLine("Datamodel title: " + Book.Title);
            }
        }

        /// <summary>
        /// Tells if the book can be renamed.
        /// </summary>
        /// <returns></returns>
        private bool CanUpdateBookTitleExecute(){
            return true;
        }

        /// <summary>
        /// Command to change a book's title.
        /// </summary>
        public ICommand UpdateBookTitleCommand {
            get {
                return new RelayCommand(UpdateBookTitleExecute, CanUpdateBookTitleExecute);
            }
        }
        #endregion  // Update title
        #endregion  // Commands
    }
}