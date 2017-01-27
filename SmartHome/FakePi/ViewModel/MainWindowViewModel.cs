using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FakePi.Server;
using MyLibrary.Command;

namespace FakePi.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private bool _isConnected;
        private ObservableCollection<string> _songs = new ObservableCollection<string>();
        private string _selectedSong;
        private Server.Server server;
        public MainWindowViewModel()
        {
            InitCommands();

            server = new Server.Server();
            server.Connected += Connected;
            server.Disconnected += Disconnected;
            server.DataRecieved += DataRecieved;

            Songs.Add("Side To Side");
            Songs.Add("Trap Queen");
            Songs.Add("Lifestyle");
        }

        private void DataRecieved(object sender, EventArgs eventArgs)
        {
            if (((DataRecievedEventArgs) eventArgs).Message == "Skip")
            {
                ExecuteSkipCommand();
            }
        }

        private void Disconnected(object sender, EventArgs eventArgs)
        {
            IsConnected = false;
        }

        private void Connected(object sender, EventArgs eventArgs)
        {
            IsConnected = true;
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Songs
        {
            get { return _songs; }
            set { _songs = value; }
        }

        public string SelectedSong
        {
            get { return _selectedSong; }
            set
            {
                _selectedSong = value;
                OnPropertyChanged();
            }
        }

        public ICommand SkipCommand { get; set; }
        private void InitCommands()
        {
            SkipCommand = new RelayCommand(ExecuteSkipCommand);
        }

        private void ExecuteSkipCommand()
        {
            if (SelectedSong == null)
            {
                SelectedSong = Songs[0];
                SongChanged();
                return;
            }

            int index = Songs.TakeWhile(song => song != SelectedSong).Count();

            if (index >= Songs.Count-1)
            {
                SelectedSong = Songs[0];
                SongChanged();
                return;
            }

            SelectedSong = Songs[index + 1];
            SongChanged();
        }

        private void SongChanged()
        {
            server.SendMessage("192.168.0.15: <Song>" + SelectedSong);
        }
    }
}
