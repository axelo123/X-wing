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

namespace Login
{
    /// <summary>
    /// Logique d'interaction pour CtrlInscription.xaml
    /// </summary>
    public partial class CtrlInscription : UserControl
    {
        private Action<object, RoutedEventArgs> func_login;


        public CtrlInscription()
        {
            InitializeComponent();          
        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;

            textBox.SelectAll();
            e.Handled = false;
        }

        private void Validation_Click(object sender, RoutedEventArgs e)
        {
            if(loginBox.Text!="" && passwordBox.Password==ValidationBox.Password)
            {
                func_login(sender, e);
            }
            
        }
        public void Set_func_login(Action<object, RoutedEventArgs> temp)
        {
            func_login = temp;
        }
    }
}
