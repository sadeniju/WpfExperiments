using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;    // Events

namespace SampleModels
{
    /// <summary>
    /// Class representing a game.
    /// </summary>
    public class Game /*: IEquatable<Game>*/ {
        #region Fields
        string title;
        string publisher;
        #endregion

        #region Properties
        public string Title{
            get { return title; }
            set{
                if (value != null && value.Length > 0){
                    this.title = value;
                }
            }
        }

        public string Publisher{
            get { return publisher; }
            set{
                if (value != null && value.Length > 0){
                    this.publisher = value;
                }
            }
        }
        #endregion

        #region Construction
        public Game(string title, string publisher){
            this.title = title;
            this.publisher = publisher;
        }

        // Default constructor
        public Game() : this("", ""){}
        #endregion

        public override string ToString(){
            return "\""+title+"\" published by "+publisher;
        }

        #region Equality check
        ///// <summary>
        ///// Compares an object to the calling instance. Returns true if they are equal.
        ///// </summary>
        ///// <param name="obj">Object which has to be compared to this instance</param>
        ///// <returns>bool</returns>
        //public override bool Equals(object obj){
        //    // Equal object identity means equal contents
        //    if (object.ReferenceEquals(this, obj))
        //        return true;

        //    // Compare type
        //    if (this.GetType() != obj.GetType())
        //        return false;

        //    // Compare content of the onject
        //    return this.Equals(obj as Game);
        //}

        
        //public bool Equals(Game other){
        //    // The game objects are equal if the publisher and title are equal
        //    if (other.Publisher.Equals(this.Publisher) && other.Title.Equals(this.Title)){
        //        return true;
        //    }
        //    return false;
        //}
        #endregion

        // Overwrite GetHashCode()? If object type can be used as key => optimize search
        // all ref types have a correct (but inefficient) GetHashCode 
        // Overwriting the method:
        // 1. Objects which are equal must return the same hash value (int) 
        // 2. GetHashCode() of an instance is invariant (meaning: no matter which method is being called, the hash code never changes)
        //    the hash code only changes if a property which affects the equality-check changes?
        // 3. hash function returns random values
    }
}
