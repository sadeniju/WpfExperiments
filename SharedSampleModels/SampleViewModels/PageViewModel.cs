using SharedClasses.SampleModels;

namespace SharedClasses.SampleViewModels {
    public class PageViewModel {
        #region Fields
        Page page;
        #endregion

        # region Properties
        public Page Page {
            get { return page; }
        }
        public int Number { get { return page.Number; } }
        #endregion

        #region Construction
        /// <summary>
        /// Constructs an instance of the PageViewModel.
        /// </summary>
        public PageViewModel() {
            page = new Page();
        }

        public PageViewModel(Page model) {
            page = model;
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
        #endregion
    }
}
