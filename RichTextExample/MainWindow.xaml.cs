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

namespace RichTextExample {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public FlowDocument Doc { get; set; }

        public MainWindow() {
            InitializeComponent();
            Doc = new FlowDocument();
            Paragraph para = new Paragraph();
            para.Inlines.Add("normal");
            Bold b = new Bold();
            b.Inlines.Add("Bold");
            para.Inlines.Add(b);
            Italic it = new Italic();
            it.Inlines.Add("italic");
            para.Inlines.Add(it);
            Doc.Blocks.Add(para);
            
            this.DataContext = Doc;
        }
    }
}
