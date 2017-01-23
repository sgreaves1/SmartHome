namespace SmartHome.Model
{
    public interface IDevice
    {
        string Ip { get; set; }
        bool IsOnline { get; set; }
        string Name { get; set; }
        int X { get; set; }
        int Y { get; set; }

        string GetImageName();
    }
}
