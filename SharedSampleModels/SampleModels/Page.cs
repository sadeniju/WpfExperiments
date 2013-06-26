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
        #endregion

        /// <summary>
        /// Construction.
        /// </summary>
        public Page() {
            number = idCounter++;
        }
        
        public override string ToString() {
            return "Page " + number;
        }
    }
}
