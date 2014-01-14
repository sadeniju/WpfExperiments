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
        Point startPoint;

        public PageViewModel Root { get; private set; }

        public MainWindow() {
            InitializeComponent();
            Root = new PageViewModel();

            for(int i=0; i < 5; i++){
                PageViewModel viewModel = new PageViewModel();
                viewModel.RandomlyCreateChildren(true);
                Root.PageViewModels.Add(viewModel);
            }
            this.DataContext = Root.PageViewModels;
        }

        private void TreeView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            // Store the mouse position 
            startPoint = e.GetPosition(null);
        }

        /// <summary>
        /// Initiates Drag and Drop processes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_PreviewMouseMove(object sender, MouseEventArgs e) {
            // Get the current mouse position to distinguish between click and drag
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if(e.LeftButton == MouseButtonState.Pressed && Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance) {
                // Find the hit item
                TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);
                //treeViewItem.Background.
                // Initiate Drag and Drop
                if(e.LeftButton == MouseButtonState.Pressed && treeViewItem != null) {
                    DataObject dragData = new DataObject("pageFormat", treeViewItem.DataContext);
                    DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                    //Console.WriteLine("dragging "+treeViewItem.DataContext + ", child of " + (treeViewItem.DataContext as PageViewModel).Parent);
                }

                
            }

            // @TODO Update Adorners
        }

        private void TreeView_Drop(object sender, DragEventArgs e) {
            // Find the hit item
            TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);
            
            if(treeViewItem != null && e.Data.GetDataPresent("pageFormat")) {
                double clickPositionY = e.GetPosition(treeViewItem).Y;
                bool clickedInUpperQuarter = clickPositionY < treeViewItem.ActualHeight / 4;
                bool clickedInLowerQuarter = clickPositionY > treeViewItem.ActualHeight / 4 * 3;
                PageViewModel droppedPage = e.Data.GetData("pageFormat") as PageViewModel;
                PageViewModel targetPage = treeViewItem.DataContext as PageViewModel;
                PageViewModel previousParent = droppedPage.Parent;
                PageViewModel newParent = targetPage.Parent;
  
                if(clickedInUpperQuarter || clickedInLowerQuarter) {
                    // Insert the dropped item above or below the target item
                    int insertionPosition = newParent.PageViewModels.IndexOf(targetPage);
                    int previousPosition = newParent.PageViewModels.IndexOf(droppedPage);

                    if(previousParent == newParent) {
                        // Move inside a collection
                        if(previousPosition > insertionPosition && clickedInLowerQuarter) {
                            insertionPosition += 1;
                        }
                        else if(previousPosition < insertionPosition && clickedInUpperQuarter) {
                            insertionPosition -= 1;
                        }
                        newParent.PageViewModels.Move(previousPosition, insertionPosition);
                    }
                    else {
                        // Move to another parent page
                        if(clickPositionY > treeViewItem.ActualHeight / 4 * 3) {
                            insertionPosition += 1;
                        }

                        newParent.PageViewModels.Insert(insertionPosition, droppedPage);
                        previousParent.PageViewModels.Remove(droppedPage);
                    }
                    droppedPage.Parent = newParent; // isn't updated automatically on insert
                }
                else {
                    // Add the dropped item to the target item's parent's collection
                    previousParent.PageViewModels.Remove(droppedPage);
                    targetPage.PageViewModels.Add(droppedPage);
                }
            }
        }

        /// <summary>
        /// Helper method to search up the visual tree. 
        /// <see>http://wpftutorial.net/DragAndDrop.html</see> 
        /// </summary>
        /// <typeparam name="T">Type of object to search for.</typeparam>
        /// <param name="current">Current tree item from which the search originates.</param>
        /// <returns></returns>
        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject {
            do{
                if(current is T){
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while(current != null);
            return null;
        }

        private void TreeView_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e) {

        }

        AdornerLayer myAdornerLayer;
        InsertionLineAdordner insertionLine;
        private void TreeView_PreviewDragEnter(object sender, DragEventArgs e) {
            TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);
            if(treeViewItem != null) {
                double mousePositionY = e.GetPosition(treeViewItem).Y;
                bool mouseInUpperQuarter = mousePositionY < treeViewItem.ActualHeight / 4;
                bool mouseInLowerQuarter = mousePositionY > treeViewItem.ActualHeight / 4 * 3;
                myAdornerLayer = AdornerLayer.GetAdornerLayer(treeViewItem);
                if(mouseInUpperQuarter) {
                    insertionLine = new InsertionLineAdordner(treeViewItem, true);
                    myAdornerLayer.Add(insertionLine);
                }
                else if(mouseInLowerQuarter) {
                    insertionLine = new InsertionLineAdordner(treeViewItem, false);
                    myAdornerLayer.Add(insertionLine);
                }
                else {
                    myAdornerLayer.Remove(insertionLine);
                }
            }
        }

        private void TreeView_PreviewDragLeave(object sender, DragEventArgs e) {
            TreeViewItem treeViewItem = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);
            if(treeViewItem != null) {
                myAdornerLayer = AdornerLayer.GetAdornerLayer(treeViewItem);
                myAdornerLayer.Remove(insertionLine);
            }
        }
    }
}
