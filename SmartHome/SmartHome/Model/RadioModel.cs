using System;

namespace SmartHome.Model
{
    public class RadioModel : DeviceBase
    {
        public override void Activate()
        {
            // do nothing
        }

        public override string GetImageName()
        {
            return @"\Images\Radio.png";
        }
    }
}
