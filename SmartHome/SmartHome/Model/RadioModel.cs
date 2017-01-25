using System;

namespace SmartHome.Model
{
    public class RadioModel : DeviceBase
    {
        private string _currentSong;

        public string CurrentSong
        {
            get { return _currentSong; }
            set
            {
                _currentSong = value;
                OnPropertyChanged();
            }
        }

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
