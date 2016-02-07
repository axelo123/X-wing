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
using System.Diagnostics;
using GenerateurCarte;

namespace CarteXWing
{
    /// <summary>
    /// Logique d'interaction pour AffichageCarte.xaml
    /// </summary>
    public partial class AffichageCarte : UserControl
    {
        public AffichageCarte()
        {
            InitializeComponent();
            CarteImperial carte;
            field.Children.Clear();
            int counter = 1;

            RowDefinition RowGrid1 = new RowDefinition();
            RowDefinition RowGrid2 = new RowDefinition();

            field.RowDefinitions.Add(RowGrid1);
            field.RowDefinitions.Add(RowGrid2);

            ColumnDefinition ColumnGrid1 = new ColumnDefinition();
            ColumnDefinition ColumnGrid2 = new ColumnDefinition();
            ColumnDefinition ColumnGrid3 = new ColumnDefinition();
            ColumnDefinition ColumnGrid4 = new ColumnDefinition();

            field.ColumnDefinitions.Add(ColumnGrid1);
            field.ColumnDefinitions.Add(ColumnGrid2);
            field.ColumnDefinitions.Add(ColumnGrid3);
            field.ColumnDefinitions.Add(ColumnGrid4);

            for (int i = 0; i < 2; i++)
            {
                for (int ii = 0; ii < 4; ii++)
                {
                     carte = new CarteImperial(counter);
                    counter++;
                    Grid.SetRow(carte, i);
                    Grid.SetColumn(carte, ii);

                    field.Children.Add(carte);
                }
            }
        }

        private void image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            imageZoom.Source = (sender as Image).Source;
        }

        private void image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            imageZoom.Source = null;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ConteneurZoom.Width = (this.Content as Grid).Width;
            ConteneurZoom.Height = (this.Content as Grid).Height;

            imageZoom.Width = ConteneurZoom.Width;
            imageZoom.Height = ConteneurZoom.Height;
        }
    }
}
