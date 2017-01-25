using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmartHome.Converter;
using SmartHome.Model;
using XamlIcons.Commands;

namespace SmartHome.UserControl
{
    /// <summary>
    /// Interaction logic for MapUserControl.xaml
    /// </summary>
    public partial class MapUserControl
    {
        private ResourceDictionary myResource;

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
            ((MapUserControl)dependencyObject).UpdateDevices();
        }

        public MapUserControl()
        {
            InitializeComponent();

            myResource = new ResourceDictionary
            {
                Source = new Uri(@"\Resources\ResourceDictionary.xaml", UriKind.Relative)
            };
        }

        public FloorModel FloorModel
        {
            get { return (FloorModel)GetValue(FloorModelProperty); }
            set
            {
                SetValue(FloorModelProperty, value);
               
            }
        }

        public void UpdateDevices()
        {
            if (FloorModel != null)
            {
                foreach (var deviceModel in FloorModel.Devices)
                {
                    Image img = new Image();

                    string uri = deviceModel.GetImageName();
                    
                    img.Source = new BitmapImage(new Uri(uri, UriKind.Relative));

                    StackPanel stackPnl = new StackPanel();
                    stackPnl.Orientation = Orientation.Horizontal;
                    stackPnl.Children.Add(img);

                    Button deviceButton = new Button();
                    FloorCanvas.Children.Add(deviceButton);
                    deviceButton.Visibility = Visibility.Visible;
                    deviceButton.Content = stackPnl;
                    deviceButton.Width = 50;
                    deviceButton.Height = 50;
                    deviceButton.ToolTip = deviceModel.Name;

                    if (deviceModel.IsOnline)
                    {
                        deviceButton.Background = Brushes.Red;
                    }
                    else
                    {
                        deviceButton.Background = Brushes.Green;
                    }
                    deviceButton.BorderBrush = Brushes.Black;
                    deviceButton.BorderThickness = new Thickness(1);
                    deviceButton.Command = new DelegateCommand(DeviceButtonClickExecuteCommand);
                    deviceButton.CommandParameter = deviceModel;

                    BoolToOnlineColourConverter converter = new BoolToOnlineColourConverter();
                    Binding myBinding = new Binding("IsOnline");
                    myBinding.Converter = converter;
                    myBinding.Source = deviceModel;
                    deviceButton.SetBinding(BackgroundProperty, myBinding);

                    if (deviceModel.GetType() == typeof(RadioModel))
                    {
                        Binding toolBind = new Binding("CurrentSong");
                        toolBind.Source = deviceModel;
                        deviceButton.SetBinding(ToolTipProperty, toolBind);
                    }

                    deviceButton.Style = myResource["RoundedButton"] as Style;

                    Canvas.SetLeft(deviceButton, deviceModel.X);
                    Canvas.SetTop(deviceButton, deviceModel.Y);
                }                
            }
        }

        private void DeviceButtonClickExecuteCommand(object parameter)
        {
            ((IDevice) parameter).Activate();
            UpdateDevices();
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
