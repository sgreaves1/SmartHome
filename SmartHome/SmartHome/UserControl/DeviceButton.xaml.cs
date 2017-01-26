using System;
using System.Windows;
using System.Windows.Input;
using SmartHome.Command;
using SmartHome.Model;

namespace SmartHome.UserControl
{
    /// <summary>
    /// Interaction logic for DeviceButton.xaml
    /// </summary>
    public partial class DeviceButton
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageNameProperty =
            DependencyProperty.Register("ImageName", 
                typeof(string), 
                typeof(DeviceButton), 
                new PropertyMetadata(""));

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeviceModelProperty =
            DependencyProperty.Register("DeviceModel", 
                typeof(IDevice), 
                typeof(DeviceButton), 
                new PropertyMetadata(null, DeviceModelChanged));

        private static void DeviceModelChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (((DeviceButton)dependencyObject).DeviceModel != null)
                ((DeviceButton) dependencyObject).ImageName = ((DeviceButton) dependencyObject).DeviceModel.GetImageName();
        }

        public DeviceButton()
        {
            ImageName = @"\Images\Radio.png";

            InitializeComponent();
        }
        
        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        public IDevice DeviceModel
        {
            get { return (IDevice)GetValue(DeviceModelProperty); }
            set { SetValue(DeviceModelProperty, value); }
        }

        private void DeviceButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeviceModel.Activate();
            ImageName = DeviceModel.GetImageName();
        }
    }
}
