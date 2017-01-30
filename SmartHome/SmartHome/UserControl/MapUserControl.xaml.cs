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
                    DeviceButton button = new DeviceButton() {DeviceModel = deviceModel, ImageName = deviceModel.GetImageName()};
                    
                    button.Visibility = Visibility.Visible;
                    button.Width = 50;
                    button.Height = 50;

                    FloorCanvas.Children.Add(button);

                    Canvas.SetLeft(button, deviceModel.X);
                    Canvas.SetTop(button, deviceModel.Y);
                }                
            }
        }

        public void RemoveDevices()
        {
            var buttons = FloorCanvas.Children.OfType<DeviceButton>().ToList();
            foreach (var button in buttons)
            {
                FloorCanvas.Children.Remove(button);
            }
        }
    }
}
