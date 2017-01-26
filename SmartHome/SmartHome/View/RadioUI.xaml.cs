using System.Windows;
using SmartHome.Model;
using SmartHome.ViewModel;
using TcpServer;

namespace SmartHome.View
{
    /// <summary>
    /// Interaction logic for RadioUI.xaml
    /// </summary>
    public partial class RadioUI
    {
        public RadioUI(RadioModel radio, Server server)
        {
            InitializeComponent();

            DataContext = new RadioUIViewModel(radio, server);
        }
    }
}
