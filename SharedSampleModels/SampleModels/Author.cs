using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SharedClasses.SampleModels {
    /// <summary>
    /// Represents an author. (Used this to test the INotifyPropertyChanged-Interface..)
    /// </summary>
    public class Author{
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
                }
            }
        }
        
        public string Surname{
            get { return surname; }
            set{
                if (value != null){
                    surname = value;
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
                    if (names.Length > 1)
                        Surname = names[names.Length - 1];
                    else {
                        Surname = "";
                    }
                }
            }
        }
        #endregion

        #region Construction
        public Author(string firstName, string surname) {
            this.firstName = firstName;
            this.surname = surname;
        }

        public Author(string fullName) {
            FullName = fullName;
        }

        public Author() { }
        #endregion

        public override string ToString(){
            return FullName;
        }
    }
}
