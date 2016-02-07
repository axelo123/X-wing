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
using System.Windows.Media.Animation;

namespace Login
{
    /// <summary>
    /// Logique d'interaction pour CtrlLogin.xaml
    /// </summary>
    public partial class CtrlLogin : UserControl
    {

        public CtrlLogin()
        {
            InitializeComponent();
        }

        public event EventHandler OnConnection;

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {  
            if(loginBox.Text=="a"&& passwordBox.Password=="a")
            {
                if (this.OnConnection != null)
                    this.OnConnection(this, e);
            }
            else
            {

            }           
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
      
                textBox.SelectAll();
                e.Handled = false;
        }
        public void setEvent(EventHandler event_main_window)
        {
            this.OnConnection += event_main_window;
        }
    }
}
