
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
        
    }
}
