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
    /// Logique d'interaction pour TabPanelXWing.xaml
    /// </summary>
    public partial class TabPanelXWing : UserControl
    {
        private Image[] m_DernierCtrlImages;

        public TabPanelXWing()
        {
            InitializeComponent();
            m_DernierCtrlImages = null;
            tabCVP.Children.Clear();
            tabCVP.Children.Add(new AffichageCarte());
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            TabItem CtrlTab = sender as TabItem;
            Image CtrlImageOff = WpfTools.FindChild<Image>(CtrlTab, "Off");
            Image CtrlImageOn = WpfTools.FindChild<Image>(CtrlTab, "On");
            if (m_DernierCtrlImages == null)
            {
                m_DernierCtrlImages = new Image[2];
            }
            else
            {
                m_DernierCtrlImages[0].Visibility = Visibility.Visible;
                m_DernierCtrlImages[1].Visibility = Visibility.Collapsed;
            }
            CtrlImageOff.Visibility = Visibility.Collapsed;
            CtrlImageOn.Visibility = Visibility.Visible;
            m_DernierCtrlImages[0] = CtrlImageOff;
            m_DernierCtrlImages[1] = CtrlImageOn;
        }
    }
}
