using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Outils
{
    public partial class SelecteurMoisAnnee : UserControl
    {
        #region Membres privés
        private int m_NombreAnneesDansLePasse;
        private int m_NombreAnneesDansLeFutur;
        private int m_AnneeInitiale, m_MoisActuel, m_AnneeActuelle;
        private int m_MoisPrecedent, m_AnneePrecedente;
        #endregion

        public class EventArgs : System.EventArgs
        {
            #region Membres privés
            private int m_MoisPrecedent, m_AnneePrecedente;
            private int m_MoisActuel, m_AnneeActuelle;
            #endregion

            public int MoisAvantChangement { get { return m_MoisPrecedent; } }

            public int MoisSelectionne { get { return m_MoisActuel; } }

            public int AnneeAvantChangement { get { return m_AnneePrecedente; } }

            public int AnneeSelectionnee { get { return m_AnneeActuelle; } }

            public bool MoisModifie { get { return m_MoisActuel != m_MoisPrecedent; } }

            public bool AnneeModifiee { get { return m_AnneeActuelle != m_AnneePrecedente; } }

            public EventArgs(int MoisPrecedent, int MoisActuel, int AnneePrecedente, int AnneeActuelle)
            {
                m_MoisPrecedent = MoisPrecedent;
                m_MoisActuel = MoisActuel;
                m_AnneePrecedente = AnneePrecedente;
                m_AnneeActuelle = AnneeActuelle;
            }
        }

        public event EventHandler<EventArgs> SurChangement = null;

        public int NombreAnneesDansLePasse
        {
            get
            {
                return m_NombreAnneesDansLePasse;
            }
            set
            {
                if ((value < 0) && (value == m_NombreAnneesDansLePasse)) return;
                m_NombreAnneesDansLePasse = value;
                AjusterAnneeEnFonctionDesBornes();
            }
        }

        public int NombreAnneesDansLeFutur
        {
            get
            {
                return m_NombreAnneesDansLeFutur;
            }
            set
            {
                if ((value < 0) && (value == m_NombreAnneesDansLeFutur)) return;
                m_NombreAnneesDansLeFutur = value;
                AjusterAnneeEnFonctionDesBornes();
            }
        }

        public int AnneeInitiale
        {
            get
            {
                return m_AnneeInitiale;
            }
            set
            {
                if ((value < 0) && (value == m_AnneeInitiale)) return;
                m_AnneeInitiale = value;
                AjusterAnneeEnFonctionDesBornes();
            }
        }

        public int MoisActuel
        {
            get
            {
                return m_MoisActuel;
            }
            set
            {
                if ((value < 1) || (value > 12) || (value == m_MoisActuel)) return;
                OutilsFormulaire.SelectionnerDansListe<int>(comboMois, m_MoisActuel);
                DeclencherChangement();
            }
        }

        public int AnneeActuelle
        {
            get
            {
                return m_AnneeActuelle;
            }
            set
            {
                if ((value < (m_AnneeInitiale - m_NombreAnneesDansLePasse)) || (value > (m_AnneeInitiale + m_NombreAnneesDansLeFutur)) || (value == AnneeActuelle)) return;
                m_AnneeActuelle = value;
                OutilsFormulaire.SelectionnerDansListe<int>(comboAnnee, m_AnneeActuelle);
                DeclencherChangement();
            }
        }

        public SelecteurMoisAnnee()
        {
            InitializeComponent();
            DateTime Maintenant = DateTime.Now;
            m_AnneeInitiale = Maintenant.Year;
            m_MoisActuel = Maintenant.Month;
            m_AnneeActuelle = m_AnneeInitiale;
            m_MoisPrecedent = m_MoisActuel;
            m_AnneePrecedente = m_AnneeActuelle;
            OutilsFormulaire.RemplirListe<int>(comboMois, OutilsEnumeration.Enumerer(1, 12, 1), m_MoisActuel);
            RemplirListeAnnees();
        }

        #region Méthodes privées de gestion des mises à jour de l'interface utilisateur et des données, ainsi que des événements à déclencher
        private void RemplirListeAnnees()
        {
            OutilsFormulaire.DeselectionnerDansListe<int>(comboAnnee);
            OutilsFormulaire.RemplirListe<int>(comboAnnee,
                OutilsEnumeration.Enumerer(m_AnneeInitiale - m_NombreAnneesDansLePasse, m_AnneeInitiale + m_NombreAnneesDansLeFutur),
                m_AnneeInitiale);
            OutilsFormulaire.SelectionnerDansListe<int>(comboAnnee, m_AnneeActuelle);
        }

        private void AjusterAnneeEnFonctionDesBornes()
        {
            int AnneeMinimale = m_AnneeInitiale - m_NombreAnneesDansLePasse;
            int AnneeMaximale = m_AnneeInitiale + m_NombreAnneesDansLeFutur;
            if (m_AnneeActuelle < AnneeMinimale)
                m_AnneeActuelle = AnneeMinimale;
            else if (m_AnneeActuelle > AnneeMaximale)
                m_AnneeActuelle = AnneeMaximale;
            RemplirListeAnnees();
        }

        private void DeclencherChangement()
        {
            if (SurChangement != null) SurChangement(this, new EventArgs(m_MoisPrecedent, m_MoisActuel, m_AnneePrecedente, m_AnneeActuelle));
            m_MoisPrecedent = m_MoisActuel;
            m_AnneePrecedente = m_AnneeActuelle;
        }

        /// <summary>
        /// Met à jour les éléments d'interface du sélecteur
        /// </summary>
        private void MettreAJourSelecteur()
        {
            bool MoisPrecedentDisponible = (comboMois.SelectedIndex > 0) || (comboAnnee.SelectedIndex > 0);
            bool MoisSuivantDisponible = (comboMois.SelectedIndex < (comboMois.Items.Count - 1)) || (comboAnnee.SelectedIndex < (comboAnnee.Items.Count - 1));
            buttonMoisPrecedent.Enabled = MoisPrecedentDisponible;
            buttonMoisSuivant.Enabled = MoisSuivantDisponible;
            tooltipAideContextuelle.SetToolTip(buttonMoisPrecedent, !MoisPrecedentDisponible ? "Limite atteinte dans le passé" : "Permet de passer au mois précédent");
            tooltipAideContextuelle.SetToolTip(buttonMoisSuivant, !MoisSuivantDisponible ? "Limite atteinte dans le futur" : "Permet de passer au mois suivant");
        }
        #endregion

        #region Méthodes de gestion des événements de l'interface utilisateur
        /// <summary>
        /// Se produit sur un changement de sélection dans la liste des mois
        /// </summary>
        /// <param name="sender">Emetteur de l'évènement</param>
        /// <param name="e">Description de l'évènement</param>
        private void comboMois_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int MoisSelectionne = (comboMois.SelectedItem == null) ? 0 : (int)comboMois.SelectedItem;
            if ((MoisSelectionne != 0) && (MoisSelectionne != m_MoisActuel))
            {
                m_MoisActuel = MoisSelectionne;
                MettreAJourSelecteur();
                DeclencherChangement();
            }
        }

        /// <summary>
        /// Se produit sur un changement de sélection dans la liste des années
        /// </summary>
        /// <param name="sender">Emetteur de l'évènement</param>
        /// <param name="e">Description de l'évènement</param>
        private void comboAnnee_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int AnneeSelectionnee = (comboAnnee.SelectedItem == null) ? 0 : (int)comboAnnee.SelectedItem;
            if ((AnneeSelectionnee != 0) && (AnneeSelectionnee != m_AnneeActuelle))
            {
                m_AnneeActuelle = AnneeSelectionnee;
                MettreAJourSelecteur();
                DeclencherChangement();
            }
        }

        /// <summary>
        /// Se produit lors de l'activation du bouton permettant de passer au mois précédent
        /// </summary>
        /// <param name="sender">Emetteur de l'évènement</param>
        /// <param name="e">Description de l'évènement</param>
        private void buttonMoisPrecedent_Click(object sender, System.EventArgs e)
        {
            if (m_MoisActuel > 1)
                OutilsFormulaire.SelectionnerDansListe<int>(comboMois, m_MoisActuel - 1);
            else
            {
                m_MoisActuel = 12;
                m_AnneeActuelle--;
                OutilsFormulaire.SelectionnerDansListe<int>(comboMois, m_MoisActuel);
                OutilsFormulaire.SelectionnerDansListe<int>(comboAnnee, m_AnneeActuelle);
                DeclencherChangement();
            }
        }

        /// <summary>
        /// Se produit lors de l'activation du bouton permettant de passer au mois suivant
        /// </summary>
        /// <param name="sender">Emetteur de l'évènement</param>
        /// <param name="e">Description de l'évènement</param>
        private void buttonMoisSuivant_Click(object sender, System.EventArgs e)
        {
            if (m_MoisActuel < 12)
                OutilsFormulaire.SelectionnerDansListe<int>(comboMois, m_MoisActuel + 1);
            else
            {
                m_MoisActuel = 1;
                m_AnneeActuelle++;
                OutilsFormulaire.SelectionnerDansListe<int>(comboMois, m_MoisActuel);
                OutilsFormulaire.SelectionnerDansListe<int>(comboAnnee, m_AnneeActuelle);
                DeclencherChangement();
            }
        }
        #endregion
    }
}
