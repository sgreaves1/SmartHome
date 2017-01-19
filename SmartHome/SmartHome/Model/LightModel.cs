
namespace SmartHome.Model
{
    public class LightModel : BaseModel
    {
        private string _name;
        private int _x;
        private int _y;
        private bool _isOn;

        public LightModel()
        {
            IsOn = false;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
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
