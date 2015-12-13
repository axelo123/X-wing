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

            Core.App.ConnecterBD();

            /*
            List<MyDB.MyDB.IRecord> enreg = new List<MyDB.MyDB.IRecord>();
            string query = string.Format("SELECT * FROM {0} WHERE {1} = {2}", "amelioration", "id", 1);

            foreach (MyDB.MyDB.IRecord elem in Core.App.BDD.Read(query))
            {
                enreg.Add(elem);
            }

            // Remplir win2 via l'enregistrement existant
            foreach (MyDB.MyDB.IRecord Element in enreg)
            {
                for (int i = 0; i < Element.FieldCount; i++)
                {
                    // Liste des valeurs des champs
                    //win2.listView.Items.Add(Element[i]);
                    // Liste des noms des champs
                    win2.listView.Items.Add(Element.FieldName(i));
                }
            }
            */

            Amelioration MyAmelioration = new Amelioration(1);

            


            win2.Show();
            this.Close();
        }
    }
}
