using System.Windows.Input;
using SmartHome.Command;
using SmartHome.Model;
using TcpServer;

namespace SmartHome.ViewModel
{
    public class RadioUIViewModel : BaseViewModel
    {
        private RadioModel _radio;
        private Server _server;

        public RadioUIViewModel(RadioModel radio, Server server)
        {
            Radio = radio;
            _server = server;

            InitCommands();
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

        public ICommand SkipCommand { get; set; }

        private void InitCommands()
        {
            SkipCommand = new RelayCommand(SkipCommandExecute);
        }

        private void SkipCommandExecute()
        {
            _server.SendMessage(Radio.Ip, "Skip");
        }
    }
}
