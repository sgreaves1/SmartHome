using SmartHome.Model;
using SmartHome.ViewModel;

namespace SmartHome.View
{
    /// <summary>
    /// Interaction logic for RadioUI.xaml
    /// </summary>
    public partial class RadioUI
    {
        public RadioUI(RadioModel radio)
        {
            InitializeComponent();

            DataContext = new RadioUIViewModel(radio);
        }
    }
}
