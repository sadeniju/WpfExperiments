using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MvvmExample2 {
    public class NotifyableObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
 
        /// <summary>
        /// Raises a PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName){
            if (PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
