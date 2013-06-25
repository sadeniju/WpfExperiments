using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FsWpfControls.FsRichTextBox {
    /// <summary>
    /// Interaktionslogik für LinkDialog.xaml
    /// </summary>
    public partial class LinkDialog : Window {
        public LinkDialog() {
            InitializeComponent();
        }

        /// <summary>
        /// Cancels the creation of a new Url.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.linkUrl.Text = null;
            this.linkName.Text = null;  // reset the input
            this.Close();
        }

        /// <summary>
        /// Creates a new Url.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
