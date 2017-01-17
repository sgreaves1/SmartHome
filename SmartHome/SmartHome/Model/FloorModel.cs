namespace SmartHome.Model
{
    public class FloorModel : BaseModel
    {
        private string _name;
        private string _imageName;

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
    }
}
