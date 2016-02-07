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

        private Inscription _inscription = new Inscription();
        public CtrlLoginForm()
        {
            InitializeComponent();
            ins.Visibility = Visibility.Collapsed;
            //grid1.Children.Add(_inscription);
        }

        public event EventHandler OnConnection;
        public event EventHandler OnInscription;

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OnConnection != null)
                this.OnConnection(this, e);
        }

        private void InscriptionBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;    
            ins.Visibility = Visibility.Visible;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
      
                textBox.SelectAll();
                e.Handled = false;
        }
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            T parent = VisualTreeHelper.GetParent(child) as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parent);
        }
    }
}
