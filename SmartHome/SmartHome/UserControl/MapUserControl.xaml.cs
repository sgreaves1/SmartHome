using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmartHome.Command;
using SmartHome.Model;
using XamlIcons.Commands;

namespace SmartHome.UserControl
{
    /// <summary>
    /// Interaction logic for MapUserControl.xaml
    /// </summary>
    public partial class MapUserControl
    {
        /// <summary>
        /// Dependency Property that backs the FloorModel object
        /// </summary>
        public static readonly DependencyProperty FloorModelProperty =
            DependencyProperty.Register("FloorModel", 
                typeof(FloorModel), 
                typeof(MapUserControl), 
                new PropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((MapUserControl)dependencyObject).RemoveDevices();
            ((MapUserControl)dependencyObject).UpdateLights();
            ((MapUserControl)dependencyObject).UpdateRadios();
        }

        public MapUserControl()
        {
            InitializeComponent();
        }

        public FloorModel FloorModel
        {
            get { return (FloorModel)GetValue(FloorModelProperty); }
            set
            {
                SetValue(FloorModelProperty, value);
               
            }
        }

        public void UpdateLights()
        {
            if (FloorModel != null)
            {
                foreach (var lightModel in FloorModel.Lights)
                {
                    Image img = new Image();

                    string uri = lightModel.IsOn ? @"\Images\LightOn.png" : @"\Images\LightOff.png";

                    img.Source = new BitmapImage(new Uri(uri, UriKind.Relative));

                    StackPanel stackPnl = new StackPanel();
                    stackPnl.Orientation = Orientation.Horizontal;
                    stackPnl.Children.Add(img);

                    Button lightButton = new Button();
                    FloorCanvas.Children.Add(lightButton);
                    lightButton.Visibility = Visibility.Visible;
                    lightButton.Content = stackPnl;
                    lightButton.Width = 50;
                    lightButton.Height = 50;
                    lightButton.ToolTip = lightModel.Name;
                    lightButton.Command = new DelegateCommand(LightButtonClickExecuteCommand);
                    lightButton.CommandParameter = lightModel;

                    Canvas.SetLeft(lightButton, lightModel.X);
                    Canvas.SetTop(lightButton, lightModel.Y);
                }
            }
        }

        public void UpdateRadios()
        {
            if (FloorModel != null)
            {
                foreach (var radioModel in FloorModel.Radios)
                {
                    Image img = new Image();
                    
                    string uri = @"\Images\Radio.png";

                    img.Source = new BitmapImage(new Uri(uri, UriKind.Relative));

                    img.Width = 50;
                    img.Height = 50;

                    StackPanel stackPnl = new StackPanel();
                    stackPnl.Orientation = Orientation.Horizontal;
                    stackPnl.Children.Add(img);

                    Button radioButton = new Button();
                    FloorCanvas.Children.Add(radioButton);
                    radioButton.Visibility = Visibility.Visible;
                    radioButton.Content = stackPnl;
                    radioButton.Width = 50;
                    radioButton.Height = 50;
                    radioButton.ToolTip = radioModel.Name;
                    radioButton.Background = Brushes.Red;
                    radioButton.BorderBrush = Brushes.Black;
                    radioButton.BorderThickness = new Thickness(1);
                    //radioButton.Command = new DelegateCommand(LightButtonClickExecuteCommand);
                    radioButton.CommandParameter = radioButton;

                    var myResource = new ResourceDictionary
                    {
                        Source = new Uri(@"\Resources\ResourceDictionary.xaml", UriKind.Relative)
                    };

                    radioButton.Style = myResource["RoundedButton"] as Style;

                    Canvas.SetLeft(radioButton, radioModel.X);
                    Canvas.SetTop(radioButton, radioModel.Y);
                }
            }
        }

        private void LightButtonClickExecuteCommand(object parameter)
        {
            ((LightModel) parameter).IsOn = !((LightModel) parameter).IsOn;
            UpdateLights();
        }

        public void RemoveDevices()
        {
            var buttons = FloorCanvas.Children.OfType<Button>().ToList();
            foreach (var button in buttons)
            {
                FloorCanvas.Children.Remove(button);
            }
        }
    }
}
