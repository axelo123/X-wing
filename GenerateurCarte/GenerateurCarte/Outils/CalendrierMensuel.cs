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
    /// <summary>
    /// Contrôle gérant une vue de calendrier mensuel
    /// </summary>
    public partial class CalendrierMensuel : UserControl
    {
        /// <summary>
        /// Interface requise pour un contrôle voulant fournir un initialiseur indépendant de la date
        /// </summary>
        public interface IInitialiseur
        {
            void Initialiser();
        }

        /// <summary>
        /// Interface requise pour un contrôle voulant fournir un initialiseur dépendant de la date
        /// </summary>
        public interface IInitialiseurAvecDate
        {
            void Initialiser(DateTime Date);
        }

        #region Membres privés
        private Func<DateTime, Control> m_ConstructeurElement;
        private Action<Control> m_InitialiseurElement1;
        private Action<Control, DateTime> m_InitialiseurElement2;
        private int m_Mois, m_Annee;
        private List<Control> m_Elements;
        #endregion

        /// <summary>
        /// Enumération des contrôles contenu dans ce calendrier
        /// </summary>
        public IEnumerable<Control> Elements
        {
            get
            {
                return m_Elements;
            }
        }

        /// <summary>
        /// Accesseur d'élément en fonction du numéro de jour dans le mois
        /// </summary>
        /// <param name="Jour">Numéro du jour dans le mois</param>
        /// <returns>Référence du contrôle correspondant à l'élément correspondant au numéro de jour spécifié, ou null si ce numéro (ou cet élément) est inexistant</returns>
        public Control this[int Jour]
        {
            get
            {
                return ((Jour >= 1) && (Jour <= m_Elements.Count)) ? m_Elements[Jour - 1] : null;
            }
        }

        /// <summary>
        /// Accesseur d'élément typé en fonction du numéro de jour dans le mois
        /// </summary>
        /// <typeparam name="T">Type du contrôle à récupérer (par cast de ce type)</typeparam>
        /// <param name="Jour">Numéro du jour dans le mois</param>
        /// <returns>Référence du contrôle correspondant à l'élément correspondant au numéro de jour spécifié, ou null si ce numéro (ou cet élément) est inexistant</returns>
        public T Element<T>(int Jour) where T : Control
        {
            return ((Jour >= 1) && (Jour <= m_Elements.Count) && (m_Elements[Jour - 1] is T)) ? m_Elements[Jour - 1] as T : null;
        }

        /// <summary>
        /// Constructeur d'élément, c'est à dire, la méthode permettant de générer un nouveau contrôle pour une date spécifiée
        /// </summary>
        public Func<DateTime, Control> ConstructeurElement
        {
            get
            {
                return m_ConstructeurElement;
            }
            set
            {
                foreach (Control Element in m_Elements) tlpCalendrier.Controls.Remove(Element);
                m_Elements.Clear();
                m_ConstructeurElement = value;
                ConstruireCalendrier();
            }
        }

        /// <summary>
        /// Permet de tenter de définir un initialiseur d'élément recevant uniquement en paramètre la référence du contrôle à initialiser (indépendamment de la date)
        /// </summary>
        public bool DefinirInitialiseurElement(Action<Control> Initialiseur)
        {
            if (Initialiseur == null) return false;
            m_InitialiseurElement1 = Initialiseur;
            m_InitialiseurElement2 = null;
            ConstruireCalendrier();
            return true;
        }

        /// <summary>
        /// Permet de tenter de définir un initialiseur d'élément recevant en paramètre la référence du contrôle à initialiser et la date servant à l'initialisation
        /// </summary>
        public bool DefinirInitialiseurElement(Action<Control, DateTime> Initialiseur)
        {
            if (Initialiseur == null) return false;
            m_InitialiseurElement2 = Initialiseur;
            m_InitialiseurElement1 = null;
            ConstruireCalendrier();
            return true;
        }

        /// <summary>
        /// Permet de supprimer l'initialiseur d'élément
        /// </summary>
        public void SupprimerInitialiseurElement()
        {
            m_InitialiseurElement1 = null;
            m_InitialiseurElement2 = null;
            ConstruireCalendrier();
        }

        /// <summary>
        /// Méthode privée permettant de simplifier l'appel de l'initialiseur d'élément
        /// </summary>
        /// <param name="Element">Element à initialiser</param>
        /// <param name="Date">Date associée à cet élément</param>
        private void InitialiserElement(Control Element, DateTime Date)
        {
            if (Element == null) return;
            if (m_InitialiseurElement1 != null)
                m_InitialiseurElement1(Element);
            else if (m_InitialiseurElement2 != null)
                m_InitialiseurElement2(Element, Date);
            else if (Element is IInitialiseurAvecDate)
                (Element as IInitialiseurAvecDate).Initialiser(Date);
            else if (Element is IInitialiseur)
                (Element as IInitialiseur).Initialiser();
        }

        /// <summary>
        /// Mois actuellement pris en compte
        /// </summary>
        public int Mois
        {
            get
            {
                return m_Mois;
            }
            set
            {
                if ((value >= 1) && (value <= 12) && (value != m_Mois))
                {
                    m_Mois = value;
                    ConstruireCalendrier();
                }
            }
        }

        /// <summary>
        /// Année actuellement prise en compte
        /// </summary>
        public int Annee
        {
            get
            {
                return m_Annee;
            }
            set
            {
                if ((value >= DateTime.MinValue.Year) && (value <= DateTime.MaxValue.Year) && (value != m_Annee))
                {
                    m_Annee = value;
                    ConstruireCalendrier();
                }
            }
        }

        /// <summary>
        /// Constructeur de ce contrôle utilisateur
        /// </summary>
        public CalendrierMensuel()
        {
            InitializeComponent();
            m_ConstructeurElement = null;
            m_Elements = new List<Control>();
            m_Mois = 0;
            m_Annee = 0;
        }

        /// <summary>
        /// Construit l'interface du calendrier
        /// </summary>
        private void ConstruireCalendrier()
        {
            if ((m_Mois == 0) || (m_Annee == 0)) return;
            int NombreJoursDansMois = DateTime.DaysInMonth(m_Annee, m_Mois);
            int JourSemainePremierDuMois = 1 + ((int)new DateTime(m_Annee, m_Mois, 1).DayOfWeek + 6) % 7;
            int JourSemaineDernierDuMois = 1 + ((int)new DateTime(m_Annee, m_Mois, NombreJoursDansMois).DayOfWeek + 6) % 7;
            int NombreJoursPremiereSemainePartielle = (JourSemainePremierDuMois > 1) ? 8 - JourSemainePremierDuMois : 0;
            int NombreSemainesDuMois = ((JourSemainePremierDuMois > 1) ? 1 : 0)
                                     + (NombreJoursDansMois - NombreJoursPremiereSemainePartielle) / 7
                                     + ((JourSemaineDernierDuMois < 7) ? 1 : 0);
            int NombreLignesDesirees = 1 + NombreSemainesDuMois; // une ligne d'entête plus autant de lignes que de semaines couvertes partiellement ou complètement par les jours du mois considéré
            if (tlpCalendrier.RowStyles.Count != NombreLignesDesirees)
            {
                while (tlpCalendrier.RowStyles.Count < NombreLignesDesirees) tlpCalendrier.RowStyles.Add(new RowStyle(SizeType.Percent, 0f));
                while (tlpCalendrier.RowStyles.Count > NombreLignesDesirees) tlpCalendrier.RowStyles.RemoveAt(tlpCalendrier.RowStyles.Count - 1);
                float Pourcentage = 1f / (float)NombreSemainesDuMois;
                for (int IndiceLigneDeDonnees = 1; IndiceLigneDeDonnees < NombreLignesDesirees; IndiceLigneDeDonnees++) tlpCalendrier.RowStyles[IndiceLigneDeDonnees].Height = Pourcentage;
                tlpCalendrier.RowCount = NombreLignesDesirees;
            }
            while (m_Elements.Count < NombreJoursDansMois)
            {
                Control NouvelElement = (m_ConstructeurElement != null) ? m_ConstructeurElement(new DateTime(m_Annee, m_Mois, m_Elements.Count + 1)) : new Panel() { BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle };
                NouvelElement.Visible = false;
                m_Elements.Add(NouvelElement);
                tlpCalendrier.Controls.Add(NouvelElement);
            }
            while (m_Elements.Count > NombreJoursDansMois)
            {
                tlpCalendrier.Controls.Remove(m_Elements[m_Elements.Count - 1]);
                m_Elements.RemoveAt(m_Elements.Count - 1);
            }
            int IndiceColonne = 1 + (JourSemainePremierDuMois - 1);
            int IndiceLigne = 1 + 0;
            int NumeroJour = 1;
            tlpCalendrier.SuspendLayout();
            foreach (Control Element in m_Elements)
            {
                tlpCalendrier.SetCellPosition(Element, new TableLayoutPanelCellPosition(IndiceColonne, IndiceLigne));
                IndiceColonne++;
                if (IndiceColonne >= tlpCalendrier.ColumnCount)
                {
                    IndiceColonne = 1 + 0;
                    IndiceLigne++;
                }
                InitialiserElement(Element, new DateTime(m_Annee, m_Mois, NumeroJour));
                Element.Visible = true;
                NumeroJour++;
            }
            tlpCalendrier.ResumeLayout(true);
        }
    }
}
