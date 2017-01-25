namespace FakePi.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private bool _isConnected;

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }
    }
}
