using System.Windows;

namespace FsRichTextBoxDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// 
    /// A Bindable WPF RichTextBox
    /// By David Veeneman
    /// http://www.codeproject.com/Articles/66054/A-Bindable-WPF-RichTextBox
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            var mainWindowViewModel = new MainWindowViewModel();
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
