using SharedClasses.SampleModels;
using System.Collections.ObjectModel;
using System;
using System.Collections.Specialized;

namespace SharedClasses.SampleViewModels {
    public class PageViewModel {
        #region Fields
        Page page;
        static Random random = new Random();
        #endregion

        # region Properties
        public Page Page {
            get { return page; }
        }

        public int Number { get { return page.Number; } }

        /// <summary>
        /// Children.
        /// </summary>
        public ObservableCollection<PageViewModel> PageViewModels { get; private set; }

        public PageViewModel Parent { get; set; }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs an instance of the PageViewModel.
        /// </summary>
        public PageViewModel(): this(new Page()) {}

        /// <summary>
        /// Constructs a viewmodel to a page model.
        /// </summary>
        /// <param name="model"></param>
        public PageViewModel(Page model) {
            page = model;
            PageViewModels = new ObservableCollection<PageViewModel>();
            foreach(Page child in model.Pages) {
                PageViewModels.Add(new PageViewModel(child));
            }
            PageViewModels.CollectionChanged += new NotifyCollectionChangedEventHandler(PageViewModels_CollectionChanged);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Represents the page as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return page.ToString();
        }

        /// <summary>
        /// Randomly creates a tree of pages.
        /// </summary>
        /// <param name="randomlyCreateGrandChildren">Recursively creates children if set to true</param>
        /// <param name="maxNumberOfChildren">Maximum number of children per page</param>
        public void RandomlyCreateChildren(bool randomlyCreateGrandChildren, int maxNumberOfChildren = 5) {
            //Random random = new Random();
            for(int i = 0; i < random.Next(1, maxNumberOfChildren); i++) {
                PageViewModel child = new PageViewModel();
                PageViewModels.Add(child);
                if(randomlyCreateGrandChildren && random.NextDouble() > 0.8){
                    child.RandomlyCreateChildren(true);
                }
            }
        }

        /// <summary>
        /// Manipulates the models collection of pages on collectionchanges of its viewmodel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.Action == NotifyCollectionChangedAction.Add) {
                foreach(PageViewModel pageViewModel in e.NewItems) {
                    pageViewModel.Parent = this;
                    Page.Pages.Add(pageViewModel.Page);
                    pageViewModel.Page.Parent = Page;
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove) {
                foreach(PageViewModel pageViewModel in e.OldItems) {
                    pageViewModel.Parent = null;
                    Page.Pages.Remove(pageViewModel.Page);
                    pageViewModel.Page.Parent = null;
                }
            }
        }
        #endregion
    }
}
