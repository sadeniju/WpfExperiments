using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using SharedClasses.SampleModels;
using SharedClasses.SampleViewModels;
using System.Windows.Controls.Primitives;

namespace DragnDropPractice {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        //Point startPoint = new Point();
        LibraryViewModel libraryViewModel;

        public MainWindow() {
            libraryViewModel = new LibraryViewModel();  
            libraryViewModel.CreateDummy(); // dummy viewmodel
            this.DataContext = libraryViewModel;
            InitializeComponent();
        }

        #region Drag Drop 
        /// <summary>
        /// Detects mouse movement (starts dragging if lmb is pressed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_MouseMove(object sender, MouseEventArgs e) {
            // Get the current mouse position
            //Point mousePos = e.GetPosition(null);
            //Vector diff = startPoint - mousePos;
 
            if (e.LeftButton == MouseButtonState.Pressed){   //@TODO define when dragging starts (mouse position)
                Console.WriteLine("Dragging");

                //// get dragged item
                ListView lb = sender as ListView; // view element from which the dragged item originated
                //ListViewItem lbi = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);    // dragged item
 
                //// get data behind dragged item
                //BookViewModel b = lb.ItemContainerGenerator.ItemFromContainer(lbi) as BookViewModel;

                BookViewModel b = lb.SelectedItem as BookViewModel; // can just use the selected item instead of searching the hit item!
                Console.WriteLine(b.Title);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", b );
                DragDrop.DoDragDrop(lb, dragData, DragDropEffects.Move);
            } 
        }

        /// <summary>
        /// Drop listener.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookListView_Drop(object sender, DragEventArgs e) {
            // Implement DragEnter event if item originates from another element

            if (e.Data.GetDataPresent("myFormat")){
                BookViewModel droppedBookViewModel = e.Data.GetData("myFormat") as BookViewModel;
                
                Console.WriteLine("Dropping " + droppedBookViewModel.Title);

                // Find out where to insert the dragged item
                int index = GetCurrentIndex(e.GetPosition, sender as ListView);

                // move dragged book to the chosen position
                // remove from previous position
                libraryViewModel.BookViewModels.RemoveAt(libraryViewModel.BookViewModels.IndexOf(droppedBookViewModel));
                // @TODO insert at new position
                libraryViewModel.BookViewModels.Insert(index, droppedBookViewModel);
            }
        }
        #endregion Drag Drop

        #region Cursor above item helper methods
        protected delegate Point GetPositionDelegate(IInputElement element);
        protected delegate TouchPoint GetTouchPositionDelegate(IInputElement element);

        /// <summary>
        /// Checks if a point is located on a target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="getPosition"></param>
        /// <returns></returns>
        protected bool IsOverTarget(Visual target, Point position) { 
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target); 
            return bounds.Contains(position);
        }

        protected ListViewItem GetListViewItem(int index, ListView list) { 
            if (list.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) 
                return null; 
            return list.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem; 
        } 

        #region Cursor above item
        /// <summary>
        /// Returns the index of the item, hit by the cursor.
        /// </summary>
        /// <param name="getPosition"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        protected int GetCurrentIndex(GetPositionDelegate getPosition, ListView list) { 
            int index = -1; 
            for (int i = 0; i < list.Items.Count; i++) { 
                ListViewItem item = GetListViewItem(i, list); 
                if (item == null) 
                    continue; 
                if (IsOverTarget(item, getPosition((IInputElement)item))) { 
                    index = i; 
                    break; 
                } 
            } 
            return index; 
        } 
        #endregion
        #endregion

        //#region Touch above item
        ///// <summary>
        ///// Return the index of the item, hit by the tap.
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="getTouchPosition"></param>
        ///// <returns></returns>
        //protected int GetCurrentTouchIndex(ListView list, GetTouchPositionDelegate getTouchPosition){
        //    int index = -1;
        //    for (int i = 0; i < list.Items.Count; i++) { 
        //        ListViewItem item = GetListViewItem(i, list); 
        //        if (item == null) 
        //            continue; 
        //        if (IsOverTarget(item, getTouchPosition((IInputElement)item).Position)) { 
        //            index = i; 
        //            break; 
        //        }
        //    }
        //    return index;
        //}
        //#endregion

        //// Helper to search up the VisualTree
        //private static T FindAnchestor<T>(DependencyObject current)
        //    where T : DependencyObject
        //{
        //    do
        //    {
        //        if( current is T )
        //        {
        //            return (T)current;
        //        }
        //        current = VisualTreeHelper.GetParent(current);
        //    }
        //    while (current != null);
        //    return null;
        //}

        ///// <summary>
        ///// Detects lmb pressed.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
        //    Console.WriteLine("Left button pressed.");
        //    startPoint = e.GetPosition(null);
        //}
    }
}