using System.Windows;
using SmartHome.@enum;

namespace SmartHome.EventArguments
{
    public class NewDeviceAddedEventArgs : RoutedEventArgs
    {
        public string Name;
        public string Ip;
        public IconTypes Type;
    }
}
