using System.Windows;
using SmartHome.@enum;

namespace SmartHome.EventArgs
{
    public class NewDeviceAddedEventArgs : RoutedEventArgs
    {
        public string Name;
        public string Ip;
        public IconTypes Type;
    }
}
