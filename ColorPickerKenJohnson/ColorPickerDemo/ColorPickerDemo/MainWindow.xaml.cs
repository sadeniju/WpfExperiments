using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ColorPickerControls;
using ColorPickerControls.Dialogs;

namespace ColorPickerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
    
        }

        private void colorRect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dia = new ColorPickerFullDialog();
            dia.InitialColor =((SolidColorBrush ) colorRect.Fill).Color ; //set the initial color
            if (dia.ShowDialog() == true)
            {
                colorRect.Fill = new SolidColorBrush(dia.SelectedColor); //do something with the selected color
            }
        }

        private void alphaColorRect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dia = new ColorPickerFullWithAlphaDialog();
            dia.InitialColor = ((SolidColorBrush)alphaColorRect.Fill).Color; //set the initial color
            if (dia.ShowDialog() == true)
            {
                alphaColorRect.Fill = new SolidColorBrush(dia.SelectedColor); //do something with the selected color
            }

        }


        #region ForegroundBackGroundColorChip via events

        //One can Bind to the ForegroundBrushChanged or BackgroundBrushChanged events
        private void foregroundBackgroundChip_ForegroundBrushChanged(object sender, ColorPicker.EventArgs<SolidColorBrush> e)
        {
            if (rectFGEvent != null)
            {
                rectFGEvent.Stroke  = e.Value;
            }
        }

        //One can Bind to the ForegroundColorChanged or BackgroundColorChanged events
        private void foregroundBackgroundChip_BackgroundColorChanged(object sender, ColorPicker.EventArgs<Color> e)
        {
            if (rectFGEvent != null)
            {
                rectFGEvent.Fill =  new SolidColorBrush(e.Value);
            }

        }

       #endregion

        #region Standard
       

        private void exStandard_Expanded(object sender, RoutedEventArgs e)
        {
            cpStandard.InitialColor = cdStandard.Color;
            cpStandard.SelectedColor = cdStandard.Color;
        }

        private void btStandardAccept_Click(object sender, RoutedEventArgs e)
        {
            cdStandard.Color = cpStandard.SelectedColor;
            exStandard.IsExpanded = false;
        }

        private void btStandardCancel_Click(object sender, RoutedEventArgs e)
        {
            exStandard.IsExpanded = false;
        }

       #endregion

        #region StandardAlpha
       

        private void btStandardAlphaAccept_Click(object sender, RoutedEventArgs e)
        {
           cdStandardAlpha.Color  = cpStandardWithAlpha.SelectedColor;
            exStandardAlpha.IsExpanded = false;
        }

        private void btStandardAlphaCancel_Click(object sender, RoutedEventArgs e)
        {
            exStandardAlpha.IsExpanded = false;
        }

        private void exStandardAlpha_Expanded(object sender, RoutedEventArgs e)
        {
            cpStandardWithAlpha.InitialColor = cdStandardAlpha.Color;
            cpStandardWithAlpha.SelectedColor = cdStandardAlpha.Color;
        }
     #endregion

        #region Full
       

        private void exFull_Expanded(object sender, RoutedEventArgs e)
        {
            cpFull.InitialColor = cdFull.Color;
            cpFull.SelectedColor = cdFull.Color;
        }

        private void btFullAccept_Click(object sender, RoutedEventArgs e)
        {
           cdFull.Color =cpFull.SelectedColor ;
           exFull.IsExpanded = false;
        }

        private void btFullCancel_Click(object sender, RoutedEventArgs e)
        {
              exFull.IsExpanded = false;
        }
        #endregion

        #region Full Alpha
        

        private void exFullAlpha_Expanded(object sender, RoutedEventArgs e)
        {
            cpFullAlpha.InitialColor = cdFullAlpha.Color;
            cpFullAlpha.SelectedColor = cdFullAlpha.Color;
        }

        private void btFullAlphaAccept_Click(object sender, RoutedEventArgs e)
        {
            cdFullAlpha.Color=  cpFullAlpha.SelectedColor;
            exFullAlpha.IsExpanded = false;
        }

        private void btFullAlphaCancel_Click(object sender, RoutedEventArgs e)
        {
            exFullAlpha.IsExpanded = false;
        }
#endregion
    }   


}
