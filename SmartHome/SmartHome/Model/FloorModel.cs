using System.Collections.ObjectModel;

namespace SmartHome.Model
{
    public class FloorModel : BaseModel
    {
        private string _name;
        private string _imageName;

        private ObservableCollection<LightModel> _lights = new ObservableCollection<LightModel>(); 
        private ObservableCollection<RadioModel> _radios = new ObservableCollection<RadioModel>();

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string ImageName
        {
            get { return _imageName; }
            set
            {
                _imageName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LightModel> Lights
        {
            get { return _lights; }
            set
            {
                _lights = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RadioModel> Radios
        {
            get { return _radios; }
            set
            {
                _radios = value;
                OnPropertyChanged();
            }
        }
    }
}
