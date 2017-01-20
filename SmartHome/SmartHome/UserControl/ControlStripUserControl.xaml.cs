using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SmartHome.Command;
using SmartHome.View;

namespace SmartHome.UserControl
{
    /// <summary>
    /// Interaction logic for ControlStripUserControl.xaml
    /// </summary>
    public partial class ControlStripUserControl 
    {
        public ControlStripUserControl()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AddElementWindow window = new AddElementWindow();
            window.ShowDialog();
        }
    }
}
