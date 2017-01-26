using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using SmartHome.Model;
using SmartHome.View;
using SmartHome.XMLReader;
using TcpServer;
using TcpServer.EventArguments;
using TextLoggingPackage;

namespace SmartHome.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<FloorModel> _floors;

        private Server _server;

        private void ModelReady(object sender, EventArgs eventArgs)
        {
            Logger.Log("Floor read from xml file by the name of " + ((DataReaderEventArgs)eventArgs).Floor.Name, "Smart Home", LoggingLevel.Trace);

            Floors.Add(((DataReaderEventArgs)eventArgs).Floor);

            foreach (var device in ((DataReaderEventArgs)eventArgs).Floor.Devices)
            {
                if (device.GetType() == typeof (RadioModel))
                {
                    ((RadioModel)device).Activated += RadioActivated;
                }
            }
        }

        private void RadioActivated(object sender, EventArgs eventArgs)
        {
            RadioUI window = new RadioUI((RadioModel)sender, _server);
            window.ShowDialog();
        }

        public MainWindowViewModel()
        {
            Logger.ApplicationLoggingLevel = (LoggingLevel)Enum.Parse(typeof(LoggingLevel), ConfigurationManager.AppSettings["LoggingLevel"]);

            Logger.Log("*********************************", "Smart Home", LoggingLevel.Critical);
            Logger.Log("*********************************", "Smart Home", LoggingLevel.Critical);
            Logger.Log("App Started.", "Smart Home", LoggingLevel.Trace);

            _server = new Server();
            _server.MessageReceived += MessageReceived;

            Floors = new ObservableCollection<FloorModel>();
            ReadXml();
        }

        private void MessageReceived(object sender, EventArgs eventArgs)
        {
            foreach (var floorModel in Floors)
            {
                foreach (var device in floorModel.Devices)
                {
                    if (device.Ip == ((MessageReceivedEventArgs) eventArgs).message.Ip)
                    {
                        string message = ((MessageReceivedEventArgs) eventArgs).message.Message;
                        if (message == " HeartBeat")
                        {
                            device.IsOnline = true;
                        }

                        if (message.Contains("<Song>"))
                        {
                            string[] words = message.Split(new string[] {"<Song>"}, StringSplitOptions.None);
                            ((RadioModel)device).CurrentSong = words[1];
                        }
                    }
                }
            }
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
