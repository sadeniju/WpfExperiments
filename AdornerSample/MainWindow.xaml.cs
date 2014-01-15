using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SharedClasses.SampleViewModels;
using System.Windows.Documents;

namespace AdornerSample {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        #region Fields
        private Point startPoint;
        private InsertionLineAdordner insertionLine;
        private InsertionRectangleAdordner insertionSelection;
        #endregion

        #region Properties
        public PageViewModel Root { get; private set; }
        #endregion

        #region Construction
        /// <summary>
        /// Initializes the main window.
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            Root = new PageViewModel();

            // Construct a random tree of pages
            for(int i=0; i < 5; i++){
                PageViewModel viewModel = new PageViewModel();
                viewModel.RandomlyCreateChildren(true);
                Root.PageViewModels.Add(viewModel);
            }
            this.DataContext = Root.PageViewModels;
        }
        #endregion

        #region Drag Drop Handling
        /// <summary>
        /// Handles dropping of elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_Drop(object sender, DragEventArgs e) {
            TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);    // the drop target
            
            // Insert the dropped item according to its insertion mode (before, after or onto the drop target)
            if(treeViewItem != null && e.Data.GetDataPresent("pageFormat")) {
                InsertionMode mousePosition = GetRelativeMousePosition(e, treeViewItem);
                PageViewModel droppedPage = e.Data.GetData("pageFormat") as PageViewModel;
                PageViewModel targetPage = treeViewItem.DataContext as PageViewModel;
                PageViewModel previousParent = droppedPage.Parent;
                PageViewModel newParent = targetPage.Parent;

                if(droppedPage != targetPage) {
                    if(mousePosition.Equals(InsertionMode.Bottom) || mousePosition.Equals(InsertionMode.Top)) {
                        // Insert the dropped item between the drop target and its neighbor (above or below)
                        int insertionPosition = newParent.PageViewModels.IndexOf(targetPage);
                        int previousPosition = newParent.PageViewModels.IndexOf(droppedPage);

                        if(previousParent == newParent) {
                            // Move inside a collection
                            if(previousPosition > insertionPosition && mousePosition.Equals(InsertionMode.Bottom)) {
                                insertionPosition += 1;
                            }
                            else if(previousPosition < insertionPosition && mousePosition.Equals(InsertionMode.Top)) {
                                insertionPosition -= 1;
                            }
                            newParent.PageViewModels.Move(previousPosition, insertionPosition);
                        }
                        else {
                            // Move to another parent page
                            if(mousePosition.Equals(InsertionMode.Bottom)) {
                                insertionPosition += 1;
                            }

                            newParent.PageViewModels.Insert(insertionPosition, droppedPage);
                            previousParent.PageViewModels.Remove(droppedPage);
                        }
                        droppedPage.Parent = newParent; // isn't updated automatically on insert - set manually
                    }
                    else {
                        // Add the dropped item to the target item's parent's collection
                        previousParent.PageViewModels.Remove(droppedPage);
                        targetPage.PageViewModels.Add(droppedPage);
                    }
                }
            }

            // Remove insertion visualization
            RemoveInsertionLineAdorner();   
            RemoveElementSelectionAdorner();
        }

        /// <summary>
        /// Initiates Drag and Drop processes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_PreviewMouseMove(object sender, MouseEventArgs e) {
            // Get the current mouse position to distinguish between click and drag
            if(e.LeftButton == MouseButtonState.Pressed && MovedDragDropDistance(e)) {
                TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);    // Find the hit item

                // Initiate Drag and Drop
                if(e.LeftButton == MouseButtonState.Pressed && treeViewItem != null) {
                    DataObject dragData = new DataObject("pageFormat", treeViewItem.DataContext);
                    DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                    //Console.WriteLine("dragging "+treeViewItem.DataContext + ", child of " + (treeViewItem.DataContext as PageViewModel).Parent);

                    // @TODO maybe also remove adorners here? If they still exist due to some bug? - Doesn't seem necessary.
                }
            }
        }

        #region Visual feedback
        /// <summary>
        /// Handles drag enter events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_PreviewDragEnter(object sender, DragEventArgs e) {
            TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);    // the entered item

            if(treeViewItem != null && e.Data.GetDataPresent("pageFormat")) {
                // Add an insertion line adorner to show the user, between which items the dragged item will be inserted, if dropped now.
                InsertionMode mode = GetRelativeMousePosition(e, treeViewItem);
                PageViewModel draggedPage = e.Data.GetData("pageFormat") as PageViewModel;
                PageViewModel targetPage = treeViewItem.DataContext as PageViewModel;

                if(draggedPage != targetPage) { // Dragged item must not be the target
                    if(!mode.Equals(InsertionMode.Middle)) {
                        insertionLine = new InsertionLineAdordner(treeViewItem, mode == InsertionMode.Top ? true : false);
                    }
                    else {
                        insertionSelection = new InsertionRectangleAdordner(treeViewItem);
                    }
                }

                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
            else {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles drag leave events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_PreviewDragLeave(object sender, DragEventArgs e) {
            TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);
            if(treeViewItem != null && e.Data.GetDataPresent("pageFormat")) {
                // Remove the insertion line adorner
                RemoveInsertionLineAdorner();
                RemoveElementSelectionAdorner();
            }
        }

        /// <summary>
        /// Handles the mouse hovering above an element while dragging.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_DragOver(object sender, DragEventArgs e) {
            TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);    // target item

            if(treeViewItem != null && e.Data.GetDataPresent("pageFormat")) {
                // Drag is valid - add visual cues for the user
                PageViewModel draggedPage = e.Data.GetData("pageFormat") as PageViewModel;
                PageViewModel targetPage = treeViewItem.DataContext as PageViewModel;

                if(draggedPage != targetPage) { // Dragged item must not be the target
                    // Update the adorners by removing, instantiating or moving them.
                    InsertionMode mode = GetRelativeMousePosition(e, treeViewItem);
                    if(mode.Equals(InsertionMode.Middle)) {
                        // Select the target item
                        //treeViewItem.ExpandSubtree();
                        RemoveInsertionLineAdorner();
                        if(insertionSelection == null) {
                            insertionSelection = new InsertionRectangleAdordner(treeViewItem);
                        }
                    }
                    else {
                        // Add insertion line to the item
                        RemoveElementSelectionAdorner();
                        if(insertionLine != null) {
                            insertionLine.UpdatePosition(mode == InsertionMode.Top ? true : false);
                        }
                        else {
                            insertionLine = new InsertionLineAdordner(treeViewItem, mode == InsertionMode.Top ? true : false);
                        }
                    }
                }
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
            else {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }
        #endregion
        #endregion

        #region Helper functions
        /// <summary>
        /// Keep the initial mouse pressed position (used to check the drag distance).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            // Store the mouse position 
            startPoint = e.GetPosition(null);
        }

        private void RemoveInsertionLineAdorner() {
            if(insertionLine != null) {
                insertionLine.Detach();
                insertionLine = null;
            }
        }

        private void RemoveElementSelectionAdorner() {
            if(insertionSelection != null) {
                insertionSelection.Detach();
                insertionSelection = null;
            }
        }

        /// <summary>
        /// Determines whether the mouse has moved over the minimal drag drop distance.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool MovedDragDropDistance(MouseEventArgs e) {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if(Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                return true;
            return false;
        }

        /// <summary>
        /// Helper method to search up the visual tree. 
        /// <see>http://wpftutorial.net/DragAndDrop.html</see> 
        /// </summary>
        /// <typeparam name="T">Type of object to search for.</typeparam>
        /// <param name="current">Current tree item from which the search originates.</param>
        /// <returns></returns>
        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject {
            do {
                if(current is T) {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while(current != null);
            return null;
        }

        private InsertionMode GetRelativeMousePosition(DragEventArgs e, TreeViewItem treeViewItem) {
            double mousePositionY = e.GetPosition(treeViewItem).Y;
            if(mousePositionY < treeViewItem.ActualHeight / 4) {
                return InsertionMode.Top;
            }
            else if(mousePositionY > treeViewItem.ActualHeight / 4 * 3) {
                return InsertionMode.Bottom;
            }
            return InsertionMode.Middle;
        }
        #endregion
    }
}
