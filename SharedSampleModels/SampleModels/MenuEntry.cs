using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SharedClasses.SampleModels {
    /// <summary>
    /// Represents a menu entry
    /// </summary>
    public class MenuEntry {
        ObservableCollection<MenuEntry> children;

        #region Properties
        public string Title { set; get; }
        public MenuEntry Parent { get; set; }

        public ObservableCollection<MenuEntry> Children {
            get { return children; }
        }
        #endregion

        #region Construction
        public MenuEntry() {
            children = new ObservableCollection<MenuEntry>();
        }
        
        public MenuEntry(ObservableCollection<MenuEntry> children) : this() {
            this.children = children;
        }
        #endregion

        public void Add(MenuEntry entry) {
            if (entry != null) {
                children.Add(entry);
                entry.Parent = this;
            }
        }

        public void Remove(MenuEntry entry) {
            if (children.Contains(entry)) {
                children.Remove(entry);
                entry.Parent = null;
            }
        }

        public MenuEntry GetChild(int index) {
            return children[index];
        }
        
        public bool HasChildren() {
            if (children.Count() == 0) {
                return false;
            }
            return true;
        }
    }
}
