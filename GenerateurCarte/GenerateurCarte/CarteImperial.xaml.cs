using System;
using System.Collections.Generic;
using System.Data;
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
using GenerateurCarte;
using X_wing.Model;
using X_wing.Core;

namespace GenerateurCarte
{
    /// <summary>
    /// Interaction logic for CarteImperial.xaml
    /// </summary>
    public partial class CarteImperial : UserControl
    {
        #region Déclaration des variables
        
        int ligneCourante = 0;

        private List<string> m_actions = new List<string>();
        private List<string> m_ameliorations = new List<string>();

        // liste des actions de la carte
        public List<string> actions
        {
            get { return m_actions; }
            set { m_actions = value; }
        }
        //Liste des ameliorations de la carte
        public List<string> ameliorations
        {
            get { return m_ameliorations; }
            set { m_ameliorations = value; }
        }


        #endregion

        public CarteImperial(int num_carte)
        {
            InitializeComponent();
            FonctionRemplir(new Carte_vaisseau_pilote(num_carte, "Figurine", "NomCarte", "Caracteristique_vaisseau", "Faction","TypeAmelioration"));
            

        }

        public void FonctionRemplir(Carte_vaisseau_pilote Cvp)
        {
            //Carte_vaisseau_pilote Cvp = new Carte_vaisseau_pilote(10, "Figurine","NomCarte", "Caracteristique_vaisseau","Faction");
            Caracteristique_vaisseau caracVaisseau=new Caracteristique_vaisseau(Int32.Parse(Cvp["Caracteristique_vaisseau"].Attribut["id"].ToString()),"Action");

            //Chargement faction
            changerFaction(Cvp["Faction"].Attribut["nom"].ToString());

            this.NomPilote.Text = Cvp["Nom_carte"].Attribut["nom"].ToString();
            this.NomVaisseau.Text= Cvp["Figurine"].Attribut["nom"].ToString();
            this.description.Text = Cvp.Attribut["description"].ToString();
            this.cout.Text = Cvp.Attribut["cout"].ToString();

            this.initiative.Text = Cvp.Attribut["valeur_pilotage"].ToString();
            this.PointAttaque.Text = Cvp["Caracteristique_vaisseau"].Attribut["valeur_attaque"].ToString();
            this.agilite.Text = Cvp["Caracteristique_vaisseau"].Attribut["valeur_agilite"].ToString();
            this.structure.Text = Cvp["Caracteristique_vaisseau"].Attribut["valeur_structure"].ToString();
            this.bouclier.Text = Cvp["Caracteristique_vaisseau"].Attribut["valeur_bouclier"].ToString();
            this.energie.Text = Cvp["Caracteristique_vaisseau"].Attribut["valeur_energie"].ToString();

            //Chargement actions
            foreach (KeyValuePair<ModelCore, Dictionary<string, string>> action in caracVaisseau.BelongsToMany["Action"])
            {
                X_wing.Model.Action act = (X_wing.Model.Action)action.Key ;
                chargerImageAction(act.Attribut["nom"].ToString());
            }
            this.action_1.Source = actions.Count >= 1 ? new BitmapImage(new Uri(actions[0])) : null;
            this.action_2.Source = actions.Count >= 2 ? new BitmapImage(new Uri(actions[1])) : null;
            this.action_3.Source = actions.Count >= 3 ? new BitmapImage(new Uri(actions[2])) : null;
            this.action_4.Source = actions.Count >= 4 ? new BitmapImage(new Uri(actions[3])) : null;

            //Chargement ameliorations
            foreach (KeyValuePair<ModelCore, Dictionary<string,string>> amelioration in Cvp.BelongsToMany["Type_amelioration"])
            {
                
                X_wing.Model.Type_amelioration amelio = (X_wing.Model.Type_amelioration)amelioration.Key;
                int quantity = Int32.Parse(amelioration.Value["quantite"]);
                for(int i=0;i<quantity;i++)
                {
                    chargerTypeAmelioration(amelio.Attribut["nom"].ToString());
                }
                
            }
            this.amelioration_1.Source = ameliorations.Count >= 1 ? new BitmapImage(new Uri(ameliorations[0])) : null;
            this.amelioration_2.Source = ameliorations.Count >= 2 ? new BitmapImage(new Uri(ameliorations[1])) : null;
            this.amelioration_3.Source = ameliorations.Count >= 3 ? new BitmapImage(new Uri(ameliorations[2])) : null;
            this.amelioration_4.Source = ameliorations.Count >= 4 ? new BitmapImage(new Uri(ameliorations[3])) : null;
            this.amelioration_5.Source = ameliorations.Count >= 5 ? new BitmapImage(new Uri(ameliorations[4])) : null;
            this.amelioration_6.Source = ameliorations.Count >= 6 ? new BitmapImage(new Uri(ameliorations[5])) : null;
            this.amelioration_7.Source = ameliorations.Count >= 7 ? new BitmapImage(new Uri(ameliorations[6])) : null;
            this.amelioration_8.Source = ameliorations.Count >= 8 ? new BitmapImage(new Uri(ameliorations[7])) : null;
            this.amelioration_9.Source = ameliorations.Count >= 9 ? new BitmapImage(new Uri(ameliorations[8])) : null;
            this.amelioration_10.Source = ameliorations.Count >= 10 ? new BitmapImage(new Uri(ameliorations[9])) : null;
            this.amelioration_11.Source = ameliorations.Count >= 11 ? new BitmapImage(new Uri(ameliorations[10])) : null;
            this.amelioration_12.Source = ameliorations.Count >= 12 ? new BitmapImage(new Uri(ameliorations[11])) : null;
        }

        private void changerFaction(string faction)
        {
            //Fond Carte
            if (faction  == "Empire")
            {
                string strUri2 = String.Format(@"pack://application:,,,/images/{0}", "imperial.png");
                this.faction.Source = new BitmapImage(new Uri(strUri2));
            }
            else
            {
                string strUri2 = String.Format(@"pack://application:,,,/images/{0}", "rebel.png");
                this.faction.Source = new BitmapImage(new Uri(strUri2));
            }
        }

        private void chargerImageAction(string action)
        {
            switch (action)
            {
                case "Acquisition De Cible":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "Target Lock.png"));
                    break;
                case "Concentration":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "Focus.png"));
                    break;
                case "Accélération":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "Boost.png"));
                    break;
                case "Évasion":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "Evade.png"));
                    break;
                case "Tonneau":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "Barrel Roll.png"));
                    break;
                case "SLAM":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "slam.png"));
                    break;
                case "Brouillage":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "jam.png"));
                    break;
                case "Coordination":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "coordinate.png"));
                    break;
                case "Récupération":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "recover.png"));
                    break;
                case "Renforcement":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "renforcement.png"));
                    break;
                case "Occultation":
                    actions.Add(String.Format(@"pack://application:,,,/XWing/Action/{0}", "Cloack.png"));
                    break;
            }
        }
        private void chargerTypeAmelioration(string typeAmelioration)
        {
            switch (typeAmelioration)
            {
                case "Bombe":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Bomb.png"));
                    break;
                case "Canon":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "cannon.png"));
                    break;
                case "Droide":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Astromech.png"));
                    break;
                case "Equipage":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Crew.png"));
                    break;
                case "Equipe Technique":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Team.png"));
                    break;
                case "Matériel De Cargaison":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Cargo.png"));
                    break;
                case "Missile":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Missiles.png"));
                    break;
                case "Modification":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "modification.png"));
                    break;
                case "Senseur":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Sensor.png"));
                    break;
                case "Talent":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "talent.png"));
                    break;
                case "Titre":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Titres.png"));
                    break;
                case "Torpille":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "Torpedoes.png"));
                    break;
                case "Tourelle":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "turret.png"));
                    break;
                case "Tourelle D'artillerie":
                    ameliorations.Add(String.Format(@"pack://application:,,,/XWing/Amelioration/{0}", "hardpoint.png"));
                    break;
            }
        }     
    }
}
