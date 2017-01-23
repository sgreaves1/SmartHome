using System.Collections.ObjectModel;

namespace SmartHome.Model
{
    public class FloorModel : BaseModel
    {
        private string _name;
        private string _imageName;

        private ObservableCollection<IDevice> _devices = new ObservableCollection<IDevice>();

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

        public ObservableCollection<IDevice> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }
    }
}
