using System.Windows;
using SmartHome.@enum;

namespace SmartHome.View
{
    /// <summary>
    /// Interaction logic for AddElementWindow.xaml
    /// </summary>
    public partial class AddElementWindow
    {
        private string _name;
        private string _ip;
        private IconTypes _type;

        public AddElementWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Name of the added device.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(new DependencyPropertyChangedEventArgs());
            }
        }

        /// <summary>
        /// The ip address of the device.
        /// </summary>
        public string Ip
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged(new DependencyPropertyChangedEventArgs());
            }
        }

        /// <summary>
        /// The type of the device.
        /// </summary>
        public IconTypes Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(new DependencyPropertyChangedEventArgs());
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
