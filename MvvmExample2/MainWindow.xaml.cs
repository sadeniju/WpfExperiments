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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharedClasses.SampleViewModels;

namespace MvvmExample2 {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        BookViewModel viewModel;

        public MainWindow() {
            viewModel = new BookViewModel();
            viewModel.Title = "Do Androids Dream of Electric Sheep?";
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void BookUpdateButton_Click(object sender, RoutedEventArgs e) {
            viewModel.Title = BookTitleTextBox.Text;
        }
    }
}
