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

namespace ApplicationIconAndConfigExample {
    /// <summary>
    /// Interaction logic für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        // This sample application has: 
        // - An application icon (see Properties, Application, Symbol)
        // - A window icon (see window tag, icon attribute)
        // - A config file (see app.config and the settings entry in Properties, Settings).
        // - An assembly info file (in the Properties folder), containing application info like company name, product name, version, description etc
        //   (will be included in the .exe file, you can view the information by right clicking on a file, goto Properties, Details or hover above the file)
        public MainWindow() {
            InitializeComponent();
            string configEntry = Properties.Settings.Default.ConfigProperty; // this is how you get the value of a config file property
            this.DataContext = configEntry;
        }
    }
}
