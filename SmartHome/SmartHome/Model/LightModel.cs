
using System;

namespace SmartHome.Model
{
    public class LightModel : DeviceBase
    {
        private bool _isOn;

        public LightModel()
        {
            IsOn = false;
        }
        
        public bool IsOn
        {
            get { return _isOn; }
            set
            {
                _isOn = value;
                OnPropertyChanged();
            }
        }

        public override string GetImageName()
        {
            return IsOn ? @"\Images\LightOn.png" : @"\Images\LightOff.png";
        }
    }
}
