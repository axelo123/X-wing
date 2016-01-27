using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDbLib;

namespace Tests
{
    public interface IrOenologueV1
    {
        int Id { get; }

        string Nom { get; }

        double IndiceConfiance { get; }

        short CotationMinimale { get; }

        short CotationMaximale { get; }
    }

    public class OenologueV1 : IrOenologueV1
    {
        #region Membre et propriété statiques définissant une entité exposant les valeurs par défaut
        private static readonly OenologueV1 s_Defaut = new OenologueV1(null);

        public static IrOenologueV1 Defaut { get { return s_Defaut; } }
        #endregion

        #region Membres privés
        private MyDB m_BD;
        private int m_Id;
        private string m_Nom;
        private double m_IndiceConfiance;
        private short m_CotationMinimale, m_CotationMaximale;
        #endregion

        #region Valeurs limites
        public const int LongueurMinimaleNom = 3;

        public const int LongueurMaximaleNom = 120;

        public const double IndiceConfianceMinimal = 0.0;

        public const double IndiceConfianceMaximal = 1.0;

        public const short MinimumCotation = 0;

        public const short MaximumCotation = 1000;
        #endregion

        #region Accesseurs publiques
        public int Id { get { return m_Id; } }

        public string Nom { get { return m_Nom; } }

        public double IndiceConfiance { get { return m_IndiceConfiance; } }

        public short CotationMinimale { get { return m_CotationMinimale; } }

        public short CotationMaximale { get { return m_CotationMaximale; } }

        public bool EstValide
        {
            get
            {
                return (m_Nom.Length >= LongueurMinimaleNom) && (m_Nom.Length <= LongueurMaximaleNom)
                    && (m_IndiceConfiance >= IndiceConfianceMinimal) && (m_IndiceConfiance <= IndiceConfianceMaximal)
                    && (m_CotationMinimale >= MinimumCotation)
                    && (m_CotationMinimale <= m_CotationMaximale)
                    && (m_CotationMaximale <= MaximumCotation);
            }
        }
        #endregion

        #region Modificateurs publiques
        public object DefinirNom(string NouvelleValeur, int IdAExclure = 0)
        {
            if (string.IsNullOrWhiteSpace(NouvelleValeur)) return "Le nom ne peut être vide !";
            NouvelleValeur = NouvelleValeur.Trim();
            if (NouvelleValeur.Length < LongueurMinimaleNom) return string.Format("Le nom doit contenir au moins {0} caractère{1} !", LongueurMinimaleNom, (LongueurMinimaleNom >= 2) ? "s" : "");
            if (NouvelleValeur.Length > LongueurMaximaleNom) return string.Format("Le nom ne peut contenir plus de {0} caractère{1} !", LongueurMaximaleNom, (LongueurMaximaleNom >= 2) ? "s" : "");
            if (m_BD.GetValue<long>("SELECT COUNT(id) FROM oenologue WHERE (id <> {0}) AND (nom = {1})", IdAExclure, NouvelleValeur) != 0) return "Ce nom d'oenologue existe déjà !";
            m_Nom = NouvelleValeur;
            return true;
        }

        public object DefinirIndiceConfiance(double NouvelleValeur)
        {
            NouvelleValeur = NouvelleValeur / 100.0;
            if (NouvelleValeur < IndiceConfianceMinimal) return string.Format("L'indice de confiance doit être plus grand ou égal à {0} % !", IndiceConfianceMinimal * 100.0);
            if (NouvelleValeur > IndiceConfianceMaximal) return string.Format("L'indice de confiance doit être plus petit ou égal à {0} % !", IndiceConfianceMaximal * 100.0);
            m_IndiceConfiance = NouvelleValeur;
            return true;
        }

        public object DefinirCotationMinimale(short NouvelleValeur)
        {
            if (NouvelleValeur < MinimumCotation) return string.Format("La cotation minimale doit être plus grande ou égale à {0} !", MinimumCotation);
            if (NouvelleValeur > m_CotationMaximale) return string.Format("La cotation minimale doit être plus petite ou égale à {0} !", m_CotationMaximale);
            m_CotationMinimale = NouvelleValeur;
            return true;
        }

        public object DefinirCotationMaximale(short NouvelleValeur)
        {
            if (NouvelleValeur < m_CotationMinimale) return string.Format("La cotation minimale doit être plus grande ou égale à {0} !", m_CotationMinimale);
            if (NouvelleValeur > MaximumCotation) return string.Format("La cotation minimale doit être plus petite ou égale à {0} !", MaximumCotation);
            m_CotationMaximale = NouvelleValeur;
            return true;
        }
        #endregion

        #region Constructeurs
        public OenologueV1(MyDB BD)
        {
            m_BD = BD;
            m_Nom = string.Empty;
            m_IndiceConfiance = IndiceConfianceMinimal;
            m_CotationMinimale = MinimumCotation;
            m_CotationMaximale = MaximumCotation;
        }

        public OenologueV1(MyDB BD, string Nom, double IndiceConfiance, short CotationMinimale, short CotationMaximale)
            : this(BD, 0, Nom, IndiceConfiance, CotationMinimale, CotationMaximale)
        {
        }

        public OenologueV1(MyDB BD, int Id, string Nom, double IndiceConfiance, short CotationMinimale, short CotationMaximale)
        {
            m_BD = BD;
            m_Id = Id;
            m_Nom = string.IsNullOrWhiteSpace(Nom) ? string.Empty : Nom.Trim();
            m_IndiceConfiance = IndiceConfiance;
            m_CotationMinimale = CotationMinimale;
            m_CotationMaximale = CotationMaximale;
        }
        #endregion

        #region Méthodes statiques permettant de récupérer des entités à partir de la base de données MySQL
        private static OenologueV1 Creer(MyDB.IRecord Enregistrement)
        {
            try
            {
                return new OenologueV1(Enregistrement.Result.DB, (int)Enregistrement["id"], (string)Enregistrement["nom"], (double)Enregistrement["indice_confiance"], (short)Enregistrement["cotation_minimale"], (short)Enregistrement["cotation_maximale"]);
            }
            catch (Exception Erreur)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("\nErreur de création d'une entité oenologue à partir d'un enregistrement : {0}\n{1}\n", Enregistrement, Erreur.Message));
                return null;
            }
        }

        public enum Listing
        {
            Tous,
            UniquementNonReferences
        }

        public static IEnumerable<OenologueV1> Lister(MyDB BD, Listing Modalite)
        {
            string Requete = "SELECT * FROM oenologue";
            if (Modalite == Listing.UniquementNonReferences) Requete += " WHERE id NOT IN (SELECT DISTINCT ref_oenologue FROM avis)";
            return BD.Read(Requete).Select<MyDB.IRecord, OenologueV1>(Enregistrement => Creer(Enregistrement));
        }

        public static OenologueV1 Selectionner(MyDB BD, int Id)
        {
            return BD.Read("SELECT * FROM oenologue WHERE id = {0}", Id).Select<MyDB.IRecord, OenologueV1>(Enregistrement => Creer(Enregistrement)).FirstOrDefault();
        }
        #endregion

        #region Méthodes publiques permettant de mettre à jour la base de données MySQL
        public bool Ajouter()
        {
            MyDB.IUpdateResult Resultat = m_BD.Execute(
                "INSERT INTO oenologue SET nom = {0}, indice_confiance = {1}, cotation_minimale = {2}, cotation_maximale = {3}",
                m_Nom, m_IndiceConfiance, m_CotationMinimale, m_CotationMaximale);
            if (Resultat.IsSuccessful) m_Id = (int)Resultat.NewId;
            return Resultat.IsSuccessful;
        }

        public bool Modifier()
        {
            return m_BD.Execute(
                "UPDATE oenologue SET nom = {0}, indice_confiance = {1}, cotation_minimale = {2}, cotation_maximale = {3} WHERE id = {4}",
                m_Nom, m_IndiceConfiance, m_CotationMinimale, m_CotationMaximale, m_Id).RecordCount == 1;
        }

        public bool Supprimer()
        {
            return m_BD.Execute(
                "DELETE oenologue WHERE id = {0}",
                m_Id).RecordCount == 1;
        }
        #endregion

        #region Méthodes utilitaires
        public override bool Equals(object obj)
        {
            return (obj is OenologueV1)
                && EstValide && (obj as OenologueV1).EstValide
                && (obj as OenologueV1).Nom.Equals(Nom, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return m_Nom.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{{ Id = {0} ; Nom = \"{1}\" ; IndiceConfiance = {2:0.00} % ; CotationMinimale = {3} ; CotationMaximale = {4} ; EstValide = {5} }}",
                m_Id, m_Nom, m_IndiceConfiance * 100.0, m_CotationMinimale, m_CotationMaximale, EstValide);
        }
        #endregion
    }
}
