using System;
using System.Windows;
using System.Collections.ObjectModel;
using SampleModels;

namespace TreeViewComponentExample {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ObservableCollection<MenuEntry> entries;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow() {
            // Filling the collection for the example
            MenuEntry entryWithSubs = new MenuEntry { Title = "1.3" };
            entryWithSubs.Add(new MenuEntry { Title = "1.3.1" });
            entryWithSubs.Add(new MenuEntry { Title = "1.3.2" });

            MenuEntry root = new MenuEntry { Title = "root" };
            root.Add(new MenuEntry{Title = "1.1"});
            root.Add(new MenuEntry { Title = "1.2" });
            root.Add(entryWithSubs);

            entries = new ObservableCollection<MenuEntry>();
            entries.Add(root);
            entries.Add(new MenuEntry { Title = "main" });

            this.DataContext = entries;
            InitializeComponent();
        }

        /// <summary>
        /// Adds a child to the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewChild_Click(object sender, RoutedEventArgs e) {
            MenuEntry newEntry = new MenuEntry { Title = newChildTitleTextBox.Text };
            MenuEntry parent = ComponentTree.SelectedItem as MenuEntry;
            parent.Add(newEntry);   // add a child to the selected entry
            Console.WriteLine("Successfully added "+newEntry.Title+" to "+newEntry.Parent+".");
        }

        /// <summary>
        /// Removes the selected child.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteChild_Click(object sender, RoutedEventArgs e) {
            MenuEntry entryToDelete = (ComponentTree.SelectedItem as MenuEntry);
            if (entryToDelete.Parent != null) {
                Console.WriteLine("Removing " + entryToDelete.Title + " from " + entryToDelete.Parent.Title + ".");
                entryToDelete.Parent.Children.Remove(entryToDelete);    // remove selected entry from parent
            }
            else {
                // Root selected => delete all children
                entryToDelete.Children.Clear();
            }
        }
    }
}
