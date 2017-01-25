using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MyLibrary.Command;

namespace FakePi.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private bool _isConnected;
        private ObservableCollection<string> _songs = new ObservableCollection<string>();
        private string _selectedSong;

        public MainWindowViewModel()
        {
            InitCommands();

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
                return;
            }

            int index = 0;

            foreach (string song in Songs)
            {
                if (song == SelectedSong)
                {
                    break;
                }

                index++;
            }

            if (index >= Songs.Count-1)
            {
                SelectedSong = Songs[0];
                return;
            }

            SelectedSong = Songs[index + 1];
        }
    }
}
