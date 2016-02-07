using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarteXWing
{
    /// <summary>
    /// Logique d'interaction pour ZoomCarte.xaml
    /// </summary>
    public partial class ZoomCarte : UserControl
    {
        public ZoomCarte()
        {
            InitializeComponent();
        }

        private void CarteXWingZoom_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CarteXWingZoom.Visibility = Visibility.Visible;
        }

        private void CarteXWingZoom_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            CarteXWingZoom.Visibility = Visibility.Hidden;
        }
    }
}
