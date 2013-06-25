using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmCollectionExample.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SampleModels;

namespace MvvmCollectionExample.ViewModels {
    /// <summary>
    /// Represents the ViewModel of a library.
    /// </summary>
    public class LibraryViewModel : BaseViewModel {
        #region Properties
        public Library Model { get; private set; }
        public string Name { get { return Model.Name; } }
        public ObservableCollection<BookViewModel> Books { 
            get; 
            set; 
        }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs the view model of a library instance.
        /// </summary>
        /// <param name="lib"></param>
        public LibraryViewModel(Library lib) {
            Model = lib;
            Books = new ObservableCollection<BookViewModel>();
            foreach (Book book in lib.Books) {
                Books.Add(new BookViewModel(book));
            }
            Books.CollectionChanged += Books_CollectionChanged;
        }
        #endregion

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
    }
}
