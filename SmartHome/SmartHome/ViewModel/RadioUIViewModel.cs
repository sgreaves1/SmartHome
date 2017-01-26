using SmartHome.Model;

namespace SmartHome.ViewModel
{
    public class RadioUIViewModel : BaseViewModel
    {
        private RadioModel _radio;

        public RadioUIViewModel(RadioModel radio)
        {
            Radio = radio;
        }

        public RadioModel Radio
        {
            get { return _radio; }
            set
            {
                _radio = value;
                OnPropertyChanged();
            }
        }
    }
}
