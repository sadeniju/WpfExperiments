using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SampleModels;

namespace MvvmExample2 {
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
