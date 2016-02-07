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
using AjoutCarte;

namespace Login
{
    /// <summary>
    /// Logique d'interaction pour menu1.xaml
    /// </summary>
    public partial class menu1 : UserControl
    {
        private AjouterLaCarte ajoutCarte = new AjouterLaCarte();
        private Action<object, RoutedEventArgs> func_menu;
        private Action<object, RoutedEventArgs> func_disconnect;
        private Action<object, RoutedEventArgs> func_carte;

        public menu1()
        {
            InitializeComponent();
            
        }

        private void CreationCarteCVP_Click(object sender, RoutedEventArgs e)
        {
            // Menu_CVP
            func_menu(sender,e);
        }

        public void Set_func_menu(Action<object, RoutedEventArgs> temp)
        {
            func_menu = temp;
        }
        public void Set_func_disconnect(Action<object,RoutedEventArgs> temp)
        {
            func_disconnect = temp;
        }

        private void DeconnexionBut_Click(object sender, RoutedEventArgs e)
        {
            func_disconnect(sender, e);
        }

        public void Set_func_carte(Action<object,RoutedEventArgs> temp)
        {
            func_carte = temp;
        }

        private void GererCarte_Click(object sender, RoutedEventArgs e)
        {
            func_carte(sender, e);
        }
    }
}
