using System;
using System.Windows;
using SmartHome.EventArgs;
using SmartHome.View;

namespace SmartHome.UserControl
{
    /// <summary>
    /// Interaction logic for ControlStripUserControl.xaml
    /// </summary>
    public partial class ControlStripUserControl
    {
        public EventHandler NewDeviceAdded;

        public ControlStripUserControl()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AddElementWindow window = new AddElementWindow();
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                NewDeviceAdded?.Invoke(this, new NewDeviceAddedEventArgs() {Name = window.Name, Ip = window.Ip, Type = window.Type});
            }
        }
    }
}
