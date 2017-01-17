using SmartHome.XMLReader;

namespace SmartHome.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            ReadXML();
        }

        void ReadXML()
        {
            DataReader reader = new DataReader();
        }

    }
}
