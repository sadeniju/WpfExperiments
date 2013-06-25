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

namespace ErrorDialogSample {
    /// <summary>
    /// Interaktionslogik für ErrorDialog.xaml
    /// </summary>
    public partial class ErrorDialog : Window {
        public ErrorDialog(ErrorMessage errorMessage) {
            this.DataContext = errorMessage;
            InitializeComponent();
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
