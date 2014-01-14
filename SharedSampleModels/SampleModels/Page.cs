using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedClasses.SampleModels {
    public class Page {
        static int idCounter = 0;
        int number;

        #region Properties
        /// <summary>
        /// Page number.
        /// </summary>
        public int Number { get { return number; } }

        public List<Page> Pages { get; private set; }

        public Page Parent { get; set; }
        #endregion

        /// <summary>
        /// Construction.
        /// </summary>
        public Page() {
            number = idCounter++;
            Pages = new List<Page>();
        }
        
        public override string ToString() {
            return "Page " + number;
        }
    }
}
