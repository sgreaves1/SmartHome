using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MyLibrary.Command;

namespace FakePi.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private bool _isConnected;
        private ObservableCollection<string> _songs = new ObservableCollection<string>();
        private string _selectedSong;

        TcpClient clientSocket = new TcpClient();

        public MainWindowViewModel()
        {
            InitCommands();

            Task task = new Task(Heartbeat);
            task.Start();

            Songs.Add("Side To Side");
            Songs.Add("Trap Queen");
            Songs.Add("Lifestyle");
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
            try
            {
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes("192.168.0.15: <Song>" + SelectedSong + "$");
                Console.WriteLine("Song Changed: " + SelectedSong);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            catch (Exception)
            {
            }
        }

        async void Heartbeat()
        {
            Task task = HeartbeatAsync();
            await task;
        }

        async Task HeartbeatAsync()
        {
            while (true)
            {
                try
                {
                    
                    Console.WriteLine("Client Started");
                    clientSocket.Connect("127.0.0.1", 8989);
                    Console.WriteLine("Client Socket Program - Server Connected ...");
                    IsConnected = true;

                    while (true)
                    {
                        NetworkStream serverStream = clientSocket.GetStream();
                        byte[] outStream = Encoding.ASCII.GetBytes("192.168.0.15: HeartBeat" + "$");
                        Console.WriteLine("Heartbeat");
                        serverStream.Write(outStream, 0, outStream.Length);
                        serverStream.Flush();

                        Thread.Sleep(3000);
                    }

                    //byte[] inStream = new byte[10025];
                    //serverStream.Read(inStream, 0, (int) clientSocket.ReceiveBufferSize);
                    //string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                    //Console.WriteLine(returndata);
                    //Console.WriteLine("");
                }
                catch (Exception)
                {
                    IsConnected = false;
                }

                Thread.Sleep(5000);
            }
        }
    }
}
