using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;

namespace ErrorDialogSample {
    /// <summary>
    /// The base of all ViewModels which implements the INotifyProperty Interface and methods for raising change events.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged {
        #region Notifications
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises a PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">Name of the changed property</param>
        protected void RaisePropertyChanged(string propertyName){
            if (PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
