using System;
using System.Windows;
using HTMLConverter;

namespace FsRichTextBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// A Bindable WPF RichTextBox
    /// By David Veeneman
    /// http://www.codeproject.com/Articles/66054/A-Bindable-WPF-RichTextBox
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event Handlers

        /// <summary>
        /// Forces an update of the FsRichTextBox.Document property.
        /// Replace with a command for proper MVVM.
        /// </summary>
        private void OnForceUpdateClick(object sender, RoutedEventArgs e)
        {
            this.EditBox.UpdateDocumentBindings();
        }

        #endregion
    }
}
