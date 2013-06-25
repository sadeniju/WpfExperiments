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
using System.Collections.ObjectModel;

namespace TemplatePractice
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Site> mySiteList = new ObservableCollection<Site>();
        public ObservableCollection<Site> MySiteList
        {
            get;
            set;
        }

        Site mySite;
        public Site MySite
        {
            get;
            set;
        }

        Site yourSite;
        public Site YourSite
        {
            get;
            set;
        }

        public MainWindow()
        {
            mySite = new Site(new ContentImage("La pagina de la imagen"));
            yourSite = new Site(new Text("La pagina del texto"));
            mySiteList.Add(yourSite);
            mySiteList.Add(mySite);

            InitializeComponent();
            this.DataContext = mySiteList;
        }
    }
}
