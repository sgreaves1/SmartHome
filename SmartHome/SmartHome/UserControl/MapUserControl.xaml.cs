using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
                new PropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((MapUserControl)dependencyObject).UpdateLights();
        }

        public MapUserControl()
        {
            InitializeComponent();
        }

        public FloorModel FloorModel
        {
            get { return (FloorModel)GetValue(FloorModelProperty); }
            set
            {
                SetValue(FloorModelProperty, value);
               
            }
        }

        public void UpdateLights()
        {
            //RemoveLights();
            if (FloorModel != null)
            {
                foreach (var lightModel in FloorModel.Lights)
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(@"\Images\LightOff.png", UriKind.Relative));

                    StackPanel stackPnl = new StackPanel();
                    stackPnl.Orientation = Orientation.Horizontal;
                    stackPnl.Children.Add(img);

                    Button b = new Button();
                    FloorCanvas.Children.Add(b);
                    b.Visibility = Visibility.Visible;
                    b.Content = stackPnl;
                    b.Width = 50;
                    b.Height = 50;
                    b.ToolTip = lightModel.Name;
                    Canvas.SetLeft(b, lightModel.X);
                    Canvas.SetTop(b, lightModel.Y);
                }
            }
        }
    }
}
