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
using X_wing.Core;
using X_wing.Model;

namespace GenerateurCarte
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            X_wing.Core.App.ConnecterBD();
            InitializeComponent();

            CarteImperial carte = new CarteImperial(10);
            field.Children.Clear();
            field.Children.Add(carte);
            
        }
    }
}
