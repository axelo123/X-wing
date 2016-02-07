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
    /// Logique d'interaction pour CtrlLoginForm.xaml
    /// </summary>
    public partial class CtrlLoginForm : UserControl
    {

        public CtrlLoginForm()
        {
            InitializeComponent();
            ins.Visibility = Visibility.Collapsed;
            
            //log.OnConnection += new EventHandler(GoToMainMenu);
        }

        public event EventHandler OnConnection;

        private void InscriptionBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Visibility = Visibility.Collapsed;
            but.Visibility = Visibility.Collapsed; 
            ins.Visibility = Visibility.Visible;
            ins.Set_func_login(validation);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
      
                textBox.SelectAll();
                e.Handled = false;
        }

        public void setEvent(EventHandler event_main_window)
        {
            log.setEvent(event_main_window);
        }

        public void validation(object sender, RoutedEventArgs e)
        {
            ins.Visibility = Visibility.Collapsed;
            log.Visibility = Visibility.Visible;
        }
    }
}
