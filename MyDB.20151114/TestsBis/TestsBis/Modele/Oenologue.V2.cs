using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDbLib;

namespace Tests
{
    public abstract class EntiteSimple
    {
        #region Membres privés
        private MyDB m_BD;
        #endregion

        public abstract int Id { get; }

        protected abstract void DefinirId(int Id);

        public MyDB BD { get { return m_BD; } }

        protected EntiteSimple()
        {
            m_BD = null;
        }

        public EntiteSimple(MyDB BD)
        {
            m_BD = BD;
        }

        #region Méthodes statiques permettant de récupérer des entités à partir de la base de données MySQL
        private static T Creer<T>(MyDB.IRecord Enregistrement) where T : EntiteSimple, new()
        {
            try
            {
                if (Enregistrement == null) throw new ArgumentNullException("L'enregistrement n'existe pas (référence null) !");
                if (!MyDB.Binding.IsEntity<T>()) throw new Exception(string.Format("Le type {0} n'est pas identifié comme une entité par un attribut MyDB.Binding.Entity !", typeof(T).FullName));
                T NewEntity = new T();
                NewEntity.m_BD = Enregistrement.Result.DB;
                MyDB.Binding.SetProperties(NewEntity, Enregistrement.Select(
                    Champ => new KeyValuePair<string, object>(MyDB.Binding.GetProperties<T>().First(Property => Property.FieldName.Equals(Champ.Key)).PropertyIdentifier, Champ.Value)));
                return NewEntity;
            }
            catch (Exception Erreur)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("\nErreur de création d'une entité '{0}' à partir d'un enregistrement : {1}\n{2}\n", typeof(T).FullName, Enregistrement, Erreur.Message));
                return null;
            }
        }

        private static string NomTable<T>() where T : EntiteSimple
        {
            return NomTable(typeof(T));
        }

        private static string NomTable(Type Type)
        {
            MyDB.Binding.EntityAttribute Entite = MyDB.Binding.GetEntity(Type);
            return (Entite != null) ? Entite.TableName : string.Empty;
        }

        private static string EnumerationNomsChamps<T>(params string[] NomsChampsAExclure) where T : EntiteSimple
        {
            return EnumerationNomsChamps(typeof(T), NomsChampsAExclure);
        }

        private static string EnumerationNomsChamps(Type Type, params string[] NomsChampsAExclure)
        {
            return string.Join(", ", MyDB.Binding.GetProperties(Type)
                .Select<MyDB.Binding.PropertyAttribute, string>(Propriete => Propriete.FieldName)
                .Where(Nom => !NomsChampsAExclure.Contains(Nom))
                );
        }

        private static string EnumerationAssignationsChamps(Type Type, params string[] NomsChampsAExclure)
        {
            return EnumerationAssignationsChamps(Type, 0, NomsChampsAExclure);
        }

        private static string EnumerationAssignationsChamps(Type Type, int IndexInitial, params string[] NomsChampsAExclure)
        {
            int Index = IndexInitial;
            return string.Join(", ", MyDB.Binding.GetProperties(Type)
                .Select<MyDB.Binding.PropertyAttribute, string>(Propriete => Propriete.FieldName)
                .Where(Nom => !NomsChampsAExclure.Contains(Nom))
                .Select(Nom => string.Format("{0} = {{{1}}}", Nom, Index++))
                );
        }

        protected IEnumerable<object> ValeursChamps(params string[] NomsChampsAExclure)
        {
            return MyDB.Binding.GetProperties(this)
                .Where(ProprieteValeur => !NomsChampsAExclure.Contains(ProprieteValeur.Key.FieldName))
                .Select<KeyValuePair<MyDB.Binding.PropertyAttribute, object>, object>(ProprieteValeur => ProprieteValeur.Value);
        }

        public static IEnumerable<T> Lister<T>(MyDB BD) where T : EntiteSimple, new()
        {
            return BD.Read("SELECT * FROM {0}", new MyDB.SqlCode(NomTable<T>())).Select<MyDB.IRecord, T>(Enregistrement => Creer<T>(Enregistrement));
        }

        public static T Selectionner<T>(MyDB BD, int Id) where T : EntiteSimple, new()
        {
            return BD.Read("SELECT * FROM {0} WHERE id = {1}", new MyDB.SqlCode(NomTable<T>()), Id).Select<MyDB.IRecord, T>(Enregistrement => Creer<T>(Enregistrement)).FirstOrDefault();
        }
        #endregion

        #region Méthodes publiques permettant de mettre à jour la base de données MySQL
        public bool Ajouter()
        {
            string NomTable = EntiteSimple.NomTable(this.GetType());
            if (string.IsNullOrEmpty(NomTable)) return false;
            MyDB.IUpdateResult Resultat = m_BD.Execute(
                string.Format("INSERT INTO {0} SET {1}", NomTable, EnumerationAssignationsChamps(this.GetType(), "id")),
                ValeursChamps("id").ToArray());
            if (Resultat.IsSuccessful) DefinirId((int)Resultat.NewId);
            return Resultat.IsSuccessful;
        }

        public bool Modifier()
        {
            string NomTable = EntiteSimple.NomTable(this.GetType());
            if (string.IsNullOrEmpty(NomTable)) return false;
            return m_BD.Execute(
                string.Format("UPDATE {0} SET {1} WHERE id = {2}", NomTable, EnumerationAssignationsChamps(this.GetType(), "id"), Id),
                ValeursChamps("id")).IsSuccessful;
        }

        public bool Supprimer()
        {
            return m_BD.Execute(
                "DELETE oenologue WHERE id = {0}",
                Id).RecordCount == 1;
        }
        #endregion
    }

    public interface IrOenologueV2
    {
        int Id { get; }

        string Nom { get; }

        double IndiceConfiance { get; }

        short CotationMinimale { get; }

        short CotationMaximale { get; }
    }

    [MyDB.Binding.Entity("Oenologue", "oenologue")]
    public class OenologueV2 : EntiteSimple, IrOenologueV2
    {
        #region Valeurs limites
        public const int LongueurMinimaleNom = 3;

        public const int LongueurMaximaleNom = 120;

        public const double IndiceConfianceMinimal = 0.0;

        public const double IndiceConfianceMaximal = 1.0;

        public const short MinimumCotation = 0;

        public const short MaximumCotation = 1000;
        #endregion

        #region Membres privés
        [MyDB.Binding.Property("Id", "id")]
        private int m_Id;

        [MyDB.Binding.Property("Nom", "nom")]
        private string m_Nom;

        [MyDB.Binding.Property("IndiceConfiance", "indice_confiance")]
        private double m_IndiceConfiance;

        [MyDB.Binding.Property("CotationMinimale", "cotation_minimale")]
        private short m_CotationMinimale;

        [MyDB.Binding.Property("CotationMaximale", "cotation_maximale")]
        private short m_CotationMaximale;
        #endregion


        #region Accesseurs publiques
        public override int Id { get { return m_Id; } }

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

        #region Modificateurs protégés et publiques
        protected override void DefinirId(int Id)
        {
            m_Id = Id;
        }

        public object DefinirNom(string NouvelleValeur, int IdAExclure = 0)
        {
            if (string.IsNullOrWhiteSpace(NouvelleValeur)) return "Le nom ne peut être vide !";
            NouvelleValeur = NouvelleValeur.Trim();
            if (NouvelleValeur.Length < LongueurMinimaleNom) return string.Format("Le nom doit contenir au moins {0} caractère{1} !", LongueurMinimaleNom, (LongueurMinimaleNom >= 2) ? "s" : "");
            if (NouvelleValeur.Length > LongueurMaximaleNom) return string.Format("Le nom ne peut contenir plus de {0} caractère{1} !", LongueurMaximaleNom, (LongueurMaximaleNom >= 2) ? "s" : "");
            if (BD.GetValue<long>("SELECT COUNT(id) FROM oenologue WHERE (id <> {0}) AND (nom = {1})", IdAExclure, NouvelleValeur) != 0) return "Ce nom d'oenologue existe déjà !";
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
        public OenologueV2()
            : this(null)
        {
        }

        public OenologueV2(MyDB BD)
            : base(BD)
        {
            m_Id = 0;
            m_Nom = string.Empty;
            m_IndiceConfiance = IndiceConfianceMinimal;
            m_CotationMinimale = MinimumCotation;
            m_CotationMaximale = MaximumCotation;
        }
        #endregion

        #region Méthodes utilitaires
        public override bool Equals(object obj)
        {
            return (obj is OenologueV2)
                && EstValide && (obj as OenologueV2).EstValide
                && (obj as OenologueV2).Nom.Equals(Nom, StringComparison.InvariantCultureIgnoreCase);
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
