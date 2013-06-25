using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SampleModels {
    /// <summary>
    /// Represents an author. (Used this to test the INotifyPropertyChanged-Interface..)
    /// </summary>
    public class Author : INotifyPropertyChanged{
        public event PropertyChangedEventHandler PropertyChanged;
        #region Private fields
        string firstName;
        string surname;
        #endregion

        #region Properties
        public string FirstName {
            get { return firstName; }
            set {
                if (value != null){
                    firstName = value;
                    Notify("FirstName");
                }
            }
        }
        
        public string Surname{
            get { return surname; }
            set{
                if (value != null){
                    surname = value;
                    Notify("Surname");
                }
            }
        }

        public string FullName { 
            get { return firstName + " " + surname; }
            set {
                string[] names = value.Split();
                if (names.Length >= 1) {
                    FirstName = names[0];
                    for (int i = 1; i < names.Length - 1; i++) {
                        FirstName += " "+names[i];
                    }
                    if(names.Length > 1)
                        Surname = names[names.Length - 1];
                }
            }
        }
        #endregion

        #region Construction
        public Author(string firstName, string surname) 
        {
            this.firstName = firstName;
            this.surname = surname;
        }

        public Author() : this("", "") { }
        #endregion

        private void Notify(string propertyName){
            if (this.PropertyChanged != null) {  // ist null wenn es keine Subscriber gibt
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));  // an alle Subscriber Nachricht mit Absender und Info schicken
            }
        }

        public override string ToString(){
            return FullName;
        }
    }
}
