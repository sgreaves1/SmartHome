using System;
using System.Collections.ObjectModel;
using SmartHome.Model;
using SmartHome.XMLReader;

namespace SmartHome.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<FloorModel> _floors;

        private void ModelReady(object sender, EventArgs eventArgs)
        {
            Floors.Add(((DataReaderEventArgs)eventArgs).Floor);
        }

        public MainWindowViewModel()
        {
            Floors = new ObservableCollection<FloorModel>();
            ReadXml();
        }

        void ReadXml()
        {
            DataReader reader = new DataReader();
            reader.ModelReady += ModelReady;

            reader.ReadXml();
        }

        public ObservableCollection<FloorModel> Floors
        {
            get { return _floors; }
            set
            {
                _floors = value;
                OnPropertyChanged();
            }
        } 
    }
}
