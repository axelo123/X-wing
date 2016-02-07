using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Contrôle gérant une vue sous forme d'une liste d'éléments ayant des valeurs à représenter dans diverses colonnes
    /// </summary>
    public partial class VueTable : UserControl
    {
        /// <summary>
        /// Modalités de remplissage souhaité
        /// </summary>
        public enum Remplissages
        {
            RafraichirLaTotalite,
            CompleterLaListe
        }

        /// <summary>
        /// Types d'ordonnancement
        /// </summary>
        public enum Ordonnancements
        {
            NonTrie,
            Croissant,
            Decroissant
        }

        /// <summary>
        /// Interface d'un objet décrivant une colonne
        /// </summary>
        public interface IDescriptionColonne
        {
            string Nom { get; }
            string Intitule { get; set; }
            HorizontalAlignment Alignement { get; set; }
            Func<object, string> Formateur { get; set; }
            bool SupporteOrdonnancement { get; set; }
            Ordonnancements Ordonnancement { get; }
            ulong PrioriteOrdonnancement { get; }
        }

        #region Classe privée de gestion d'un descriptif de colonne
        /// <summary>
        /// Description d'une colonne
        /// </summary>
        private class DescriptionColonne : IDescriptionColonne
        {
            #region Membres privés
            private static ulong s_DernierePrioriteOrdonnancement = 0;
            private CollectionColonnes m_Conteneur;
            private string m_Nom, m_Intitule;
            private HorizontalAlignment m_Alignement;
            private Func<object, string> m_Formateur;
            private bool m_SupporteOrdonnancement;
            private Ordonnancements m_Ordonnancement;
            private ulong m_PrioriteOrdonnancement;
            #endregion

            /// <summary>
            /// Nom (unique) de la colonne
            /// </summary>
            public string Nom { get { return m_Nom; } }

            /// <summary>
            /// Intitulé de la colonne
            /// </summary>
            public string Intitule
            {
                get
                {
                    return m_Intitule;
                }
                set
                {
                    if (!m_Intitule.Equals(value))
                    {
                        m_Intitule = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
                        if (m_Conteneur != null) m_Conteneur.Proprietaire.SignalerChangementIntituleColonne(m_Nom);
                    }
                }
            }

            /// <summary>
            /// Alignement de l'intitulé et des données de la colonne
            /// </summary>
            public HorizontalAlignment Alignement
            {
                get
                {
                    return m_Alignement;
                }
                set
                {
                    m_Alignement = (m_Conteneur != null) ? m_Conteneur.Proprietaire.ModifierAlignementColonne(m_Nom, value) : value;
                }
            }

            /// <summary>
            /// Méthode de formatage des valeurs de la colonne sous forme de texte à afficher
            /// </summary>
            public Func<object, string> Formateur
            {
                get
                {
                    return m_Formateur;
                }
                set
                {
                    m_Formateur = value;
                    if (m_Conteneur != null) m_Conteneur.Proprietaire.SignalerNecessiteRemplir(Remplissages.RafraichirLaTotalite);
                }
            }

            /// <summary>
            /// Indique si cette colonne supporte l'ordonnancement des valeurs en fonction des desiderata de l'utilisateur
            /// </summary>
            public bool SupporteOrdonnancement
            {
                get
                {
                    return m_SupporteOrdonnancement;
                }
                set
                {
                    m_SupporteOrdonnancement = value;
                }
            }

            /// <summary>
            /// Type d'ordonnancement des valeurs de cette colonne
            /// </summary>
            public Ordonnancements Ordonnancement
            {
                get
                {
                    return m_Ordonnancement;
                }
                set
                {
                    if (!m_Ordonnancement.Equals(value) && (value != Ordonnancements.NonTrie))
                    {
                        m_Ordonnancement = value;
                        m_PrioriteOrdonnancement = ++s_DernierePrioriteOrdonnancement;
                    }
                }
            }

            /// <summary>
            /// Priorité de cette colonne dans le cadre de l'ordonnancement
            /// </summary>
            public ulong PrioriteOrdonnancement
            {
                get
                {
                    return m_PrioriteOrdonnancement;
                }
            }

            /// <summary>
            /// Permet d'indiquer que cette colonne n'est pas ordonnancée
            /// </summary>
            public void ReinitialiserOrdonnancement()
            {
                m_Ordonnancement = Ordonnancements.NonTrie;
                m_PrioriteOrdonnancement = 0;
            }

            /// <summary>
            /// Constructeur
            /// </summary>
            /// <param name="Conteneur">Collection des colonnes contenant cette nouvelle colonne</param>
            /// <param name="Nom">Nom de cette colonne</param>
            public DescriptionColonne(CollectionColonnes Conteneur, string Nom)
            {
                m_Conteneur = Conteneur;
                m_Nom = Nom;
                m_Intitule = string.Empty;
                m_Alignement = HorizontalAlignment.Left;
                m_Formateur = null;
                m_SupporteOrdonnancement = false;
                ReinitialiserOrdonnancement();
            }

            /// <summary>
            /// Test d'égalité entre deux descriptifs de colonnes différenciables par leur nom de colonne
            /// </summary>
            /// <param name="obj">Autre objet auquel comparer le descriptif de colonne pour lequel cette méthode est appelée</param>
            /// <returns>Vrai en cas d'égalité de nom de colonne, sinon faux</returns>
            public override bool Equals(object obj)
            {
                return (obj is DescriptionColonne) ? string.Equals(m_Nom, (obj as DescriptionColonne).m_Nom) : false;
            }

            /// <summary>
            /// Valeur de hachage basée sur celle du nom de colonne
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return m_Nom.GetHashCode();
            }

            /// <summary>
            /// Affichage sous forme de texte du contenu de cet objet
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("Nom : {0} ; Intitule : {1} ; Alignement : {2} ; Formateur : {3}", m_Nom, m_Intitule, m_Alignement, (m_Formateur != null));
            }
        }
        #endregion

        /// <summary>
        /// Collection de descriptif de colonnes
        /// </summary>
        public class CollectionColonnes : IEnumerable<IDescriptionColonne>
        {
            #region Membres privés
            private VueTable m_Proprietaire;
            private List<DescriptionColonne> m_Descriptions;
            #endregion

            /// <summary>
            /// Objet propriétaire de cette collection (un objet de type VueTable)
            /// </summary>
            public VueTable Proprietaire { get { return m_Proprietaire; } }

            /// <summary>
            /// Nombre de colonnes
            /// </summary>
            public int Nombre { get { return m_Descriptions.Count; } }

            /// <summary>
            /// Accesseur de descriptif de colonne par l'indice spécifié
            /// </summary>
            /// <param name="Indice">Indice de l'élément désiré</param>
            /// <returns>Référence de la colonne désirée si l'indice est valide, sinon null</returns>
            public IDescriptionColonne this[int Indice] { get { return ((Indice >= 0) && (Indice < m_Descriptions.Count)) ? m_Descriptions[Indice] : null; } }

            /*
            /// <summary>
            /// Enumeration de toutes les colonnes
            /// </summary>
            public IEnumerable<IDescriptionColonne> Tous { get { return m_Descriptions; } }
            */

            /// <summary>
            /// Recherche l'indice d'une colonne en fonction du nom spécifié
            /// </summary>
            /// <param name="Nom">Nom de la colonne recherchée</param>
            /// <returns>Indice de la colonne si le nom spécifié existe dans la collection, sinon -1</returns>
            public int this[string Nom] { get { return m_Descriptions.IndexOf(new DescriptionColonne(null, Nom)); } }

            /// <summary>
            /// Constructeur
            /// </summary>
            /// <param name="Proprietaire">Objet de type VueTable propriétaire de cette collection</param>
            public CollectionColonnes(VueTable Proprietaire)
            {
                m_Proprietaire = Proprietaire;
                m_Descriptions = new List<DescriptionColonne>();
            }

            /// <summary>
            /// Permet de supprimer toutes les colonnes
            /// </summary>
            public void Vider()
            {
                m_Descriptions.Clear();
                m_Proprietaire.m_ListViewEnregistrements.Columns.Clear();
            }

            /// <summary>
            /// Permet de supprimer la colonne spécifiée par un indice
            /// </summary>
            /// <param name="Indice">Indice de la colonne à supprimer</param>
            /// <returns>Vrai si la colonne a pu être supprimée, sinon faux</returns>
            public bool Supprimer(int Indice)
            {
                if ((Indice >= 0) && (Indice < m_Descriptions.Count))
                {
                    m_Descriptions.RemoveAt(Indice);
                    m_Proprietaire.m_ListViewEnregistrements.Columns.RemoveAt(Indice);
                    if ((Indice == 0) && (m_Descriptions.Count > 0) && (m_Descriptions[0].Alignement != HorizontalAlignment.Left)) m_Descriptions[0].Alignement = HorizontalAlignment.Left;
                    return true;
                }
                else
                    return false;
            }

            /// <summary>
            /// Permet d'ajouter une nouvelle colonne dont l'intitulé est identique à son nom
            /// </summary>
            /// <param name="Nom">Nom et intitulé de la colonne</param>
            /// <param name="Alignement">Alignement de l'intitulé et des données de cette nouvelle colonne</param>
            /// <param name="SupporteOrdonnancement">Indique si cette colonne supporte l'ordonnancement selon les desiderata de l'utilisateur</param>
            /// <returns>Vrai si la colonne a pu être ajoutée, sinon faux</returns>
            public bool Ajouter(string Nom, HorizontalAlignment Alignement = HorizontalAlignment.Left, bool SupporteOrdonnancement = false)
            {
                return Ajouter(Nom, Nom);
            }

            /// <summary>
            /// Permet d'ajouter une nouvelle colonne en spécifiant un nom pouvant différer de l'intitulé
            /// </summary>
            /// <param name="Nom">Nom de la colonne</param>
            /// <param name="Intitule">Intitulé de la colonne</param>
            /// <param name="Alignement">Alignement de l'intitulé et des données de cette nouvelle colonne</param>
            /// <param name="SupporteOrdonnancement">Indique si cette colonne supporte l'ordonnancement selon les desiderata de l'utilisateur</param>
            /// <returns>Vrai si la colonne a pu être ajoutée, sinon faux</returns>
            public bool Ajouter(string Nom, string Intitule, HorizontalAlignment Alignement = HorizontalAlignment.Left, bool SupporteOrdonnancement = false)
            {
                DescriptionColonne NouvelleColonne = new DescriptionColonne(this, Nom);
                if (m_Descriptions.Contains(NouvelleColonne)) return false;
                m_Descriptions.Add(NouvelleColonne);
                ColumnHeader Colonne = m_Proprietaire.m_ListViewEnregistrements.Columns.Add(NouvelleColonne.Nom, NouvelleColonne.Intitule);
                NouvelleColonne.Intitule = Intitule;
                NouvelleColonne.Alignement = Alignement;
                NouvelleColonne.SupporteOrdonnancement = SupporteOrdonnancement;
                return true;
            }

            /// <summary>
            /// Permet de réinitialiser tous les indicateurs d'ordonnancement afin de revenir dans la situation où il n'y a aucun ordonnancement demandé par l'utilisateur
            /// </summary>
            public void SupprimerOrdonnancement()
            {
                foreach (DescriptionColonne Colonne in m_Descriptions) Colonne.ReinitialiserOrdonnancement();
                if (m_Proprietaire != null) m_Proprietaire.SignalerNecessiteRemplir(Remplissages.RafraichirLaTotalite);
            }

            /// <summary>
            /// Permet de décrire l'ordonnancement obtenu suite à l'action de "remplissage" de la liste
            /// </summary>
            /// <param name="Ordonnancement">Alternance de nom (ou indice) de colonne et d'indicateur de type d'ordonnancement ; ces clés de tri doivent être placées par ordre décroissant de priorité</param>
            public void DefinirOrdonnancement(params object[] Ordonnancement)
            {
                if ((Ordonnancement.Length % 2) == 1) return;
                SupprimerOrdonnancement();
                for (int Indice = Ordonnancement.Length - 2; Indice >= 0; Indice -= 2)
                {
                    int IndiceColonne = (Ordonnancement[Indice] is string) ? m_Descriptions.IndexOf(new DescriptionColonne(null, Ordonnancement[Indice].ToString())) : Ordonnancement[Indice].ToInt(-1);
                    int ValeurOrdonnancement = (Ordonnancement[Indice + 1] is Ordonnancements) ? (int)(Ordonnancements)Ordonnancement[Indice + 1] : Ordonnancement[Indice + 1].ToInt(-1);
                    if ((IndiceColonne >= 0) && (IndiceColonne < m_Descriptions.Count) && Enum.GetValues(typeof(Ordonnancements)).Cast<int>().Contains(ValeurOrdonnancement))
                    {
                        m_Descriptions[IndiceColonne].Ordonnancement = (Ordonnancements)ValeurOrdonnancement;
                    }
                }
                if (m_Proprietaire != null) m_Proprietaire.SignalerNecessiteRemplir(Remplissages.RafraichirLaTotalite);
            }

            #region Implémentation de l'interface IEnumerable<IDescriptionColonne>
            public IEnumerator<IDescriptionColonne> GetEnumerator()
            {
                return m_Descriptions.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return m_Descriptions.GetEnumerator();
            }
            #endregion
        }

        #region Membres privés
        private CollectionColonnes m_Colonnes;
        private ListViewScrollable m_ListViewEnregistrements;
        private bool m_IdentifiantEnregistrement;
        #endregion

        /// <summary>
        /// Description d'un changement d'intitulé de colonne
        /// </summary>
        public class ChangementIntituleColonneEventArgs : EventArgs
        {
            #region Membres privés
            private VueTable m_VueTable;
            private int m_IndiceColonne;
            #endregion

            public VueTable VueTable { get { return m_VueTable; } }

            public int IndiceColonne { get { return m_IndiceColonne; } }

            public ChangementIntituleColonneEventArgs(VueTable VueTable, int IndiceColonne)
            {
                m_VueTable = VueTable;
                m_IndiceColonne = IndiceColonne;
            }
        }

        /// <summary>
        /// Description d'un remplissage/complétude souhaité au sein de la liste des "enregistrements"
        /// </summary>
        public class NecessiteRemplirEventArgs : EventArgs
        {
            #region Membres privés
            private Remplissages m_Remplissage;
            #endregion

            public Remplissages Remplissage { get { return m_Remplissage; } }

            public NecessiteRemplirEventArgs(Remplissages Remplissage)
            {
                m_Remplissage = Remplissage;
            }
        }

        /// <summary>
        /// Description d'un changement de sélection "d'enregistrement"
        /// </summary>
        public class ChangementSelectionEventArgs : EventArgs
        {
            #region Membres privés
            private bool m_EstIdentifie;
            private object m_Valeur;
            #endregion

            public bool EstIdentifie { get { return m_EstIdentifie; } }

            public bool EstIndice { get { return !m_EstIdentifie; } }

            public object Identifiant { get { return m_Valeur; } }

            public int Indice { get { return (int)m_Valeur; } }

            public Action DoubleClick;

            public ChangementSelectionEventArgs(bool EstIdentifie, object Valeur, Action delegateDoubleClik=null )
            {
                m_EstIdentifie = EstIdentifie;
                m_Valeur = Valeur;
                this.DoubleClick = delegateDoubleClik;
            }

            public override string ToString()
            {
                return string.Format("{0} = {1}", (m_EstIdentifie) ? "identifiant" : "indice", m_Valeur);
            }
        }

        /// <summary>
        /// Se produit lors d'un changement d'intitulé de colonne
        /// </summary>
        public event EventHandler<ChangementIntituleColonneEventArgs> SurChangementIntituleColonne = null;

        /// <summary>
        /// Se produit lors qu'un rafaichissement ou une complétion du contenu de la liste des "enregistrements" serait opportun
        /// </summary>
        public event EventHandler<NecessiteRemplirEventArgs> SurNecessiteRemplir = null;

        /// <summary>
        /// Se produit lors d'un changement de sélection "d'enregistrement"
        /// </summary>
        public event EventHandler<ChangementSelectionEventArgs> SurChangementSelection = null;

        public event EventHandler<ChangementSelectionEventArgs> SurDoubleClickSelection = null;

        public event EventHandler<ChangementSelectionEventArgs> SurClickDroitSelection = null;
        /// <summary>
        /// Indique si les enregistrements disposent d'une valeur de champ les identifiant
        /// </summary>
        public bool IdentifiantEnregistrement { get { return m_IdentifiantEnregistrement; } }

        /// <summary>
        /// Collection du descriptif des colonnes
        /// </summary>
        public CollectionColonnes Colonnes { get { return m_Colonnes; } }

        /// <summary>
        /// Permet de supprimer tous les "enregistrements"
        /// </summary>
        public void Vider()
        {
            m_ListViewEnregistrements.Items.Clear();
        }

        /// <summary>
        /// Permet de remplir/reconstruire la liste avec les "enregistrements" énumérés
        /// </summary>
        /// <param name="Enregistrements">Enumération des "enregistrments"</param>
        public void Remplir(bool IdentifiantEnregistrement, IEnumerable<IEnumerable<object>> Enregistrements)
        {
            m_IdentifiantEnregistrement = IdentifiantEnregistrement;
            Vider();
            Completer(Enregistrements);
        }

        /// <summary>
        /// Permet de compléter la liste avec les "enregistrements" énumérés
        /// </summary>
        /// <param name="Enregistrements">Enumération des "enregistrments"</param>
        public void Completer(IEnumerable<IEnumerable<object>> Enregistrements)
        {
            foreach (IEnumerable<object> Enregistrement in Enregistrements)
            {
                ListViewItem NouvelleLigne = new ListViewItem();
                bool PremierChamp = true;
                int Indice = (m_IdentifiantEnregistrement) ? -1 : 0;
                object Identifiant = null;
                foreach (object ValeurChamp in Enregistrement)
                {
                    if (Indice == m_Colonnes.Nombre) break;
                    if (Indice == -1)
                    {
                        Identifiant = ValeurChamp; // la valeur du premier champ est l'identifiant de cet enregistrement
                    }
                    else
                    {
                        string Texte = (m_Colonnes[Indice].Formateur != null) ? m_Colonnes[Indice].Formateur(ValeurChamp) : ((ValeurChamp == null) ? string.Empty : ValeurChamp.ToString());
                        if (PremierChamp)
                        {
                            NouvelleLigne.Text = Texte;
                            PremierChamp = false;
                        }
                        else
                            NouvelleLigne.SubItems.Add(Texte);
                    }
                    Indice++;
                }
                NouvelleLigne.Tag = Identifiant;
                m_ListViewEnregistrements.Items.Add(NouvelleLigne);
            }
            m_ListViewEnregistrements.Columns.Add("");
            foreach (ColumnHeader Colonne in m_ListViewEnregistrements.Columns)
            {
                int Largeur0 = Colonne.Width;
                Colonne.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                int Largeur1 = Colonne.Width;
                Colonne.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int Largeur2 = Colonne.Width;
                Colonne.AutoResize(ColumnHeaderAutoResizeStyle.None);
                Colonne.Width = Math.Max(Largeur0, Math.Max(Largeur1, Largeur2));
            }
            m_ListViewEnregistrements.Columns.RemoveAt(m_ListViewEnregistrements.Columns.Count - 1);
        }

        /// <summary>
        /// Indique le nombre d'enregistrements présents dans la liste
        /// </summary>
        public int NombreEnregistrements { get { return m_ListViewEnregistrements.Items.Count; } }

        /// <summary>
        /// Constructeur
        /// </summary>
        public VueTable()
        {
            InitializeComponent();

            #region Création et caractérisation d'un objet de type ListViewScrollable
            m_ListViewEnregistrements = new ListViewScrollable()
            {
                Name = "listEnregistrements",
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                View = View.Details,
                LabelEdit = false,
                FullRowSelect = true,
                GridLines = true,
                AllowColumnReorder = false,
                MultiSelect = false
            };
            m_ListViewEnregistrements.SelectedIndexChanged += listEnregistrements_SelectedIndexChanged;
            m_ListViewEnregistrements.OnVerticalScroll += listEnregistrements_OnVerticalScroll;
            m_ListViewEnregistrements.ColumnClick += listEnregistrements_ColumnClick;
            m_ListViewEnregistrements.DoubleClick += listEnregistrements_DoubleClick;
            this.Controls.Add(m_ListViewEnregistrements);
            #endregion

            m_IdentifiantEnregistrement = false;
            m_Colonnes = new CollectionColonnes(this);
            this.SurChangementIntituleColonne += new EventHandler<ChangementIntituleColonneEventArgs>(VueTable_SurChangementIntituleColonne);
        }

        #region Méthodes privées relatives à la communication d'événement et d'ordre entre les éléments descrivant les colonnes et ce contrôle de vue d'une "table"
        /// <summary>
        /// Permet de modifier l'alignement d'une colonne nommée
        /// </summary>
        /// <param name="NomColonne">Nom de la colonne</param>
        /// <param name="NouvelAlignement">Nouvel alignement souhaité</param>
        /// <returns>Alignement à définir au sein du descriptif de colonne</returns>
        private HorizontalAlignment ModifierAlignementColonne(string NomColonne, HorizontalAlignment NouvelAlignement)
        {
            return ModifierAlignementColonne(m_Colonnes[NomColonne], NouvelAlignement);
        }

        /// <summary>
        /// Permet de modifier l'alignement d'une colonne spécifiée par un indice
        /// </summary>
        /// <param name="Indice">Indice de la colonne</param>
        /// <param name="NouvelAlignement">Nouvel alignement souhaité</param>
        /// <returns>Alignement à définir au sein du descriptif de colonne</returns>
        private HorizontalAlignment ModifierAlignementColonne(int Indice, HorizontalAlignment NouvelAlignement)
        {
            if (Indice == 0) return HorizontalAlignment.Left;
            if (!m_Colonnes[Indice].Alignement.Equals(NouvelAlignement)) m_ListViewEnregistrements.Columns[Indice].TextAlign = NouvelAlignement;
            return NouvelAlignement;
        }

        /// <summary>
        /// Permet de prendre en compte le changement d'intitulé d'une colonne nommée
        /// </summary>
        /// <param name="NomColonne">Nom de la colonne</param>
        private void SignalerChangementIntituleColonne(string NomColonne)
        {
            SignalerChangementIntituleColonne(m_Colonnes[NomColonne]);
        }

        /// <summary>
        /// Permet de prendre en compte le changement d'intitulé d'une colonne spécifiée par un indice
        /// </summary>
        /// <param name="Indice">Indice de la colonne</param>
        private void SignalerChangementIntituleColonne(int Indice)
        {
            if (SurChangementIntituleColonne != null) SurChangementIntituleColonne(this, new ChangementIntituleColonneEventArgs(this, Indice));
        }

        /// <summary>
        /// Permet de signaler qu'il serait opportun de réaliser un remplissage/complétude au sein de la liste des "enregistrements"
        /// </summary>
        /// <param name="Remplissage">Type de remplissage souhaité</param>
        private void SignalerNecessiteRemplir(Remplissages Remplissage)
        {
            if (SurNecessiteRemplir != null) SurNecessiteRemplir(this, new NecessiteRemplirEventArgs(Remplissage));
        }

        /// <summary>
        /// Se produit quand il y a un changement survenu au niveau d'un intitulé de colonne
        /// </summary>
        /// <param name="sender">Emetteur de l'événement</param>
        /// <param name="e">Description de l'événement</param>
        private void VueTable_SurChangementIntituleColonne(object sender, VueTable.ChangementIntituleColonneEventArgs e)
        {
            m_ListViewEnregistrements.Columns[e.IndiceColonne].Text = m_Colonnes[e.IndiceColonne].Intitule;
        }

        /// <summary>
        /// Se produit quand il y a un changement survenu au niveau de la barre de scrolling vertical de la liste
        /// </summary>
        /// <param name="sender">Emetteur de l'événement</param>
        /// <param name="e">Description de l'événement</param>
        private void listEnregistrements_OnVerticalScroll(object sender, ListViewScrollable.EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("{0} | {1} | {2} | {3} | {4}", e.Query, e.Position, e.Minimum, e.Maximum, e.PageSize));
            if (e.Query == ListViewScrollable.Queries.EndScroll && ((e.Position + e.PageSize) > e.Maximum))
            {
                SignalerNecessiteRemplir(Remplissages.CompleterLaListe);
            }
        }

        /// <summary>
        /// Se produit quand il y a un clic dans un entête de colonne de la liste
        /// </summary>
        /// <param name="sender">Emetteur de l'événement</param>
        /// <param name="e">Description de l'événement</param>
        private void listEnregistrements_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            DescriptionColonne Colonne = m_Colonnes[e.Column] as DescriptionColonne;
            if ((Colonne != null) && Colonne.SupporteOrdonnancement)
            {
                Colonne.Ordonnancement = (Colonne.Ordonnancement != Ordonnancements.Croissant) ? Ordonnancements.Croissant : Ordonnancements.Decroissant;
                SignalerNecessiteRemplir(Remplissages.RafraichirLaTotalite);
            }
        }

        private void listEnregistrements_DoubleClick(object sender, EventArgs e)
        {
            object IdentifiantSelectionne = (m_IdentifiantEnregistrement)
            ? ((m_ListViewEnregistrements.SelectedItems.Count == 1) ? m_ListViewEnregistrements.SelectedItems[0].Tag : null)
            : ((m_ListViewEnregistrements.SelectedItems.Count == 1) ? m_ListViewEnregistrements.SelectedItems[0].Index : -1);
            if (SurDoubleClickSelection != null) SurDoubleClickSelection(this, new ChangementSelectionEventArgs(m_IdentifiantEnregistrement, IdentifiantSelectionne));
        }

        /// <summary>
        /// Se produit quand il y a un changement de sélection au niveau de la liste
        /// </summary>
        /// <param name="sender">Emetteur de l'événement</param>
        /// <param name="e">Description de l'événement</param>
        private void listEnregistrements_SelectedIndexChanged(object sender, EventArgs e)
        {
            object IdentifiantSelectionne = (m_IdentifiantEnregistrement)
                ? ((m_ListViewEnregistrements.SelectedItems.Count == 1) ? m_ListViewEnregistrements.SelectedItems[0].Tag : null)
                : ((m_ListViewEnregistrements.SelectedItems.Count == 1) ? m_ListViewEnregistrements.SelectedItems[0].Index : -1);
            if (SurChangementSelection != null) SurChangementSelection(this, new ChangementSelectionEventArgs(m_IdentifiantEnregistrement, IdentifiantSelectionne));
        }
       
        #endregion
    }
}
