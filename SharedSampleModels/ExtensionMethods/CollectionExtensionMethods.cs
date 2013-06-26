using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SharedClasses.ExtensionMethods {
    public static class CollectionExtensionMethods {
        /// <summary>
        /// Adds multiple items to a collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="listToAdd"></param>
        public static void AddAll<T>(this ICollection<T> collection, ICollection<T> listToAdd){
            foreach (T item in listToAdd) {
                if (!collection.Contains(item)) {
                    collection.Add(item);
                }
            }
        }

        /// <summary>
        /// Removes multiple items to a ObservableCollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="listToRemove"></param>
        public static void RemoveAll<T>(this ICollection<T> collection, ICollection<T> listToRemove){
            foreach (T item in listToRemove) {
                if (collection.Contains(item)) {
                    collection.Remove(item);
                }
            }
        }

        // @TODO predicate? filter?
    }
}