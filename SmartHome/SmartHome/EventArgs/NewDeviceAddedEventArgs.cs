using SmartHome.@enum;

namespace SmartHome.EventArgs
{
    public class NewDeviceAddedEventArgs : System.EventArgs
    {
        public string Name;
        public string Ip;
        public IconTypes Type;
    }
}
