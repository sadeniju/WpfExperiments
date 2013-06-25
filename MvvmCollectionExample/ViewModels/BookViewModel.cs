using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmCollectionExample.Models;
using SampleModels;

namespace MvvmCollectionExample.ViewModels {
    /// <summary>
    /// Represents the ViewModel of a book.
    /// </summary>
    public class BookViewModel : BaseViewModel {
        #region Properties
        public Book Model { get; private set; }
        public string Title { get { return Model.Title; } }
        public string Author { get { return Model.Author.FullName; } }
        #endregion

        #region Construction
        public BookViewModel(Book model) {
            Model = model;
        }
        #endregion

        public override string ToString() {
            return Model.ToString();
        }
    }
}
