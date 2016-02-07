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
using X_wing.View;
using X_wing.Model;
using System.Diagnostics;

namespace X_wing
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test1 win2 = new Test1();

            //Core.App.ConnecterBD();


            //Carte_vaisseau_pilote Cvp = new Carte_vaisseau_pilote(1,"TypeAmelioration");
            


            win2.Show();
            this.Close();
        }
    }
}
