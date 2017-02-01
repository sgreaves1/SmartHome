using System;
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

        public ICommand DownCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand SkipCommand { get; set; }
        public ICommand UpCommand { get; set; }

        private void InitCommands()
        {
            DownCommand = new RelayCommand(DownCommandExecute);
            BackCommand = new RelayCommand(BackCommandExecute);
            PlayCommand = new RelayCommand(PlayCommandExecute);
            SkipCommand = new RelayCommand(SkipCommandExecute);
            UpCommand = new RelayCommand(UpCommandExecute);
        }

        private void DownCommandExecute()
        {
            _server.SendMessage(Radio.Ip, "Down");
        }

        private void BackCommandExecute()
        {
            _server.SendMessage(Radio.Ip, "Back");
        }

        private void PlayCommandExecute()
        {
            _server.SendMessage(Radio.Ip, "Play");
        }

        private void SkipCommandExecute()
        {
            _server.SendMessage(Radio.Ip, "Skip");
        }

        private void UpCommandExecute()
        {
            _server.SendMessage(Radio.Ip, "Up");
        }
    }
}
