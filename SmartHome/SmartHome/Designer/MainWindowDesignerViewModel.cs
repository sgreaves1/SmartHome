using System.Collections.ObjectModel;
using SmartHome.Model;
using SmartHome.ViewModel;

namespace SmartHome.Designer
{
    public class MainWindowDesignerViewModel : BaseViewModel
    {
        public ObservableCollection<FloorModel> Floors => new ObservableCollection<FloorModel>
        {
            new FloorModel {Name = "Ground Floor"},
            new FloorModel {Name = "Second Floor"}
        };
    }
}
