using System;

namespace SmartHome.Model
{
    public abstract class DeviceBase : BaseModel, IDevice
    {
        private string _ip;
        private bool _isOnline;
        private string _name;
        private int _x;
        private int _y;

        public string Ip
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
                OnPropertyChanged();
            }
        }

        public bool IsOnline
        {
            get
            {
                return _isOnline;
            }
            set
            {
                _isOnline = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public abstract string GetImageName();
        public abstract void Activate();
    }
}
