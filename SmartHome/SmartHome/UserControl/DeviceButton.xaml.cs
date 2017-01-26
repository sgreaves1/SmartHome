using System.Windows;
using SmartHome.Model;

namespace SmartHome.UserControl
{
    /// <summary>
    /// Interaction logic for DeviceButton.xaml
    /// </summary>
    public partial class DeviceButton
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeviceModelProperty =
            DependencyProperty.Register("DeviceModel", 
                typeof(IDevice), 
                typeof(DeviceButton), 
                new PropertyMetadata(null));
        
        public DeviceButton()
        {
            InitializeComponent();
        }

        public IDevice DeviceModel
        {
            get { return (IDevice)GetValue(DeviceModelProperty); }
            set { SetValue(DeviceModelProperty, value); }
        }
    }
}
