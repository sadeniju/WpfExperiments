using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SearchBox {
    public static class ObservableCollectionExtensionMethods {
        public static void AddAll<T>(this ObservableCollection<T> collection, List<T> listToAdd){
            foreach (T item in listToAdd)
                collection.Add(item);
        }
    }
}
