using System;
using System.Windows.Input;
using SmartHome.Command;

namespace SmartHome.Model
{
    public class RadioModel : DeviceBase
    {
        private string _currentSong;

        public EventHandler Activated;

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
            Activated?.Invoke(this, EventArgs.Empty);
        }

        public override string GetImageName()
        {
            return @"\Images\Radio.png";
        }
    }
}
