using System.Windows;
using SmartHome.Model;

namespace SmartHome.UserControl
{
    /// <summary>
    /// Interaction logic for MapUserControl.xaml
    /// </summary>
    public partial class MapUserControl
    {
        /// <summary>
        /// Dependency Property that backs the FloorModel object
        /// </summary>
        public static readonly DependencyProperty FloorModelProperty =
            DependencyProperty.Register("FloorModel", 
                typeof(FloorModel), 
                typeof(MapUserControl), 
                new PropertyMetadata(null));

        public MapUserControl()
        {
            InitializeComponent();
        }

        public FloorModel FloorModel
        {
            get { return (FloorModel)GetValue(FloorModelProperty); }
            set { SetValue(FloorModelProperty, value); }
        }
    }
}
