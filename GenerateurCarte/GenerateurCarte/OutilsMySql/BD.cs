using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Globalization;



namespace OutilsMySql
{
    /// <summary>
    /// Représente une connexion et la manipulation réalisable sur une base de données hébergée sur un serveur MySql
    /// </summary>
    public class BD : IDisposable
    {
        #region Jeux de caractères utilisables par la connexion
        /// <summary>
        /// Jeux de caractères utilisables par la connexion
        /// </summary>
        public enum JeuxDeCaracteres
        {
            /// <summary>
            /// Jeu de caractères par défaut (ANSI pour les données, UTF8 pour les noms des champs)
            /// </summary>
            ParDefaut,
            /// <summary>
            /// Caractère ANSI (latin1/swedish) pour les données et les noms des champs
            /// </summary>
            Ansi,
            /// <summary>
            /// Caractère UTF8 pour les données et les noms des champs
            /// </summary>
            Utf8
        }

        private static readonly string[] s_CodesJeuxDeCaracteres = new string[]
        {
            "",
            "latin1",
            "UTF8"
        };
        #endregion

        #region Gestion des informations culturelles utilisées par les conversions entre numérique et texte
        private static CultureInfo s_CultureAnglaise = CultureInfo.GetCultureInfo("EN-US");
        #endregion

        #region Fonctions de conversion de valeur de type C# en texte à injecter dans le code SQL
        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur textuelle
        /// </summary>
        /// <param name="Texte">Texte à injecter</param>
        /// <returns>Code SQL pour cette valeur textuelle</returns>
        public static string EnSql(string Texte)
        {
            return "\"" + Texte.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur décimale
        /// </summary>
        /// <param name="Valeur">Valeur décimale à injecter</param>
        /// <returns>Code SQL pour cette valeur décimale</returns>
        public static string EnSql(decimal Valeur)
        {
            return Valeur.ToString(s_CultureAnglaise);
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur réelle (type double)
        /// </summary>
        /// <param name="Valeur">Valeur réelle (type double) à injecter</param>
        /// <returns>Code SQL pour cette valeur réelle (type double)</returns>
        public static string EnSql(double Valeur)
        {
            return Valeur.ToString(s_CultureAnglaise);
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur réelle (type float)
        /// </summary>
        /// <param name="Valeur">Valeur réelle (type float) à injecter</param>
        /// <returns>Code SQL pour cette valeur réelle (type float)</returns>
        public static string EnSql(float Valeur)
        {
            return Valeur.ToString(s_CultureAnglaise);
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur entière (type byte)
        /// </summary>
        /// <param name="Valeur">Valeur entière (type byte) à injecter</param>
        /// <returns>Code SQL pour cette valeur entière (type byte)</returns>
        public static string EnSql(byte Valeur)
        {
            return Valeur.ToString();
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur entière (type sbyte)
        /// </summary>
        /// <param name="Valeur">Valeur entière (type sbyte) à injecter</param>
        /// <returns>Code SQL pour cette valeur entière (type sbyte)</returns>
        public static string EnSql(sbyte Valeur)
        {
            return Valeur.ToString();
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur entière (type short)
        /// </summary>
        /// <param name="Valeur">Valeur entière (type short) à injecter</param>
        /// <returns>Code SQL pour cette valeur entière (type short)</returns>
        public static string EnSql(short Valeur)
        {
            return Valeur.ToString();
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur entière (type ushort)
        /// </summary>
        /// <param name="Valeur">Valeur entière (type ushort) à injecter</param>
        /// <returns>Code SQL pour cette valeur entière (type ushort)</returns>
        public static string EnSql(ushort Valeur)
        {
            return Valeur.ToString();
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur entière (type int)
        /// </summary>
        /// <param name="Valeur">Valeur entière (type int) à injecter</param>
        /// <returns>Code SQL pour cette valeur entière (type int)</returns>
        public static string EnSql(int Valeur)
        {
            return Valeur.ToString();
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur entière (type uint)
        /// </summary>
        /// <param name="Valeur">Valeur entière (type uint) à injecter</param>
        /// <returns>Code SQL pour cette valeur entière (type uint)</returns>
        public static string EnSql(uint Valeur)
        {
            return Valeur.ToString();
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur entière (type long)
        /// </summary>
        /// <param name="Valeur">Valeur entière (type long) à injecter</param>
        /// <returns>Code SQL pour cette valeur entière (type long)</returns>
        public static string EnSql(long Valeur)
        {
            return Valeur.ToString();
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur entière (type ulong)
        /// </summary>
        /// <param name="Valeur">Valeur entière (type ulong) à injecter</param>
        /// <returns>Code SQL pour cette valeur entière (type ulong)</returns>
        public static string EnSql(ulong Valeur)
        {
            return Valeur.ToString();
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur booléenne
        /// </summary>
        /// <param name="Valeur">Valeur booléenne à injecter</param>
        /// <returns>Code SQL pour cette valeur booléenne</returns>
        public static string EnSql(bool Valeur)
        {
            return Valeur ? "TRUE" : "FALSE";
        }

        /// <summary>
        /// Types SQL relatifs aux données DateTime de C#
        /// </summary>
        public enum TypesSqlDateTime
        {
            /// <summary>
            /// Date et heure
            /// </summary>
            DateTime,
            /// <summary>
            /// Date uniquement
            /// </summary>
            Date,
            /// <summary>
            /// Partie horaire uniquement
            /// </summary>
            Time
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur de type DateTime
        /// </summary>
        /// <param name="Valeur">Valeur de type DateTime à injecter</param>
        /// <param name="TypeSqlDateTime">Indicateur de type de date/time à générer en SQL</param>
        /// <returns>Code SQL pour cette valeur de type DateTime</returns>
        public static string EnSql(DateTime Valeur, TypesSqlDateTime TypeSqlDateTime = TypesSqlDateTime.DateTime)
        {
            switch (TypeSqlDateTime)
            {
                case TypesSqlDateTime.DateTime:
                    return "\"" + Valeur.ToString("yyyy-MM-dd HH:mm:ss") + "\"";
                case TypesSqlDateTime.Date:
                    return "\"" + Valeur.ToString("yyyy-MM-dd") + "\"";
                case TypesSqlDateTime.Time:
                    return "\"" + Valeur.ToString("HH:mm:ss") + "\"";
                default:
                    throw new Exception("Type Sql DateTime non géré !");
            }
        }

        /// <summary>
        /// Permet d'encapsuler une chaîne contenant une partie de code SQL à injecter directement dans une requête SQL
        /// </summary>
        public class CodeSql
        {
            #region Membre privé
            private string m_Code;
            #endregion

            /// <summary>
            /// Code SQL à injecter
            /// </summary>
            public string Code { get { return m_Code; } }

            /// <summary>
            /// Constructeur
            /// </summary>
            /// <param name="Code">Code SQL à injecter</param>
            public CodeSql(string Code)
            {
                m_Code = Code;
            }

            /// <summary>
            /// Constructeur
            /// </summary>
            /// <param name="Code">Code SQL à injecter</param>
            public CodeSql(StringBuilder Code)
            {
                m_Code = Code.ToString();
            }
        }

        /// <summary>
        /// Créer une chaîne à intégrer dans une requête SQL et englobant une valeur d'un certain type
        /// </summary>
        /// <param name="Valeur">Valeur à injecter</param>
        /// <returns>Code SQL pour cette valeur</returns>
        public static string EnSql(object Valeur)
        {
            if (Valeur == null) return "NULL";
            if (Valeur is string) return EnSql(Valeur.ToString());
            if (Valeur is decimal) return EnSql((decimal)Valeur);
            if (Valeur is double) return EnSql((double)Valeur);
            if (Valeur is float) return EnSql((float)Valeur);
            if (Valeur is byte) return EnSql((byte)Valeur);
            if (Valeur is sbyte) return EnSql((sbyte)Valeur);
            if (Valeur is short) return EnSql((short)Valeur);
            if (Valeur is ushort) return EnSql((ushort)Valeur);
            if (Valeur is int) return EnSql((int)Valeur);
            if (Valeur is uint) return EnSql((uint)Valeur);
            if (Valeur is long) return EnSql((long)Valeur);
            if (Valeur is ulong) return EnSql((ulong)Valeur);
            if (Valeur is bool) return EnSql((bool)Valeur);
            if (Valeur is DateTime) return EnSql((DateTime)Valeur);
            if (Valeur is MySql.Data.Types.MySqlDateTime) return EnSql(((MySql.Data.Types.MySqlDateTime)Valeur).Value);
            if (Valeur is CodeSql) return (Valeur as CodeSql).Code;
            throw new Exception("Type de valeur non supporté en Sql !");
        }
        #endregion

        #region Membres privés de la classe ; ils sont contenus dans chaque instance (objet) de la classe
        private string m_NomUtilisateur, m_MotDePasse, m_BaseDeDonnees, m_AdresseServeur;
        private JeuxDeCaracteres m_JeuDeCaracteres;
        private MySqlConnection m_Connexion;
        #endregion

        #region Propriété publique en lecture/écriture
        /// <summary>
        /// Adresse du serveur
        /// </summary>
        public string AdresseServeur
        {
            get // Accesseur de la propriété AdresseServeur
            {
                return m_AdresseServeur;
            }
            set // Modificateur de la propriété AdresseServeur
            {
                if (!EstConnecte && !string.IsNullOrWhiteSpace(value)) m_AdresseServeur = value.Trim();
            }
        }
        #endregion

        #region Propriétés publiques en lecture seule
        /// <summary>
        /// Nom de la base de données
        /// </summary>
        public string NomBD
        {
            get
            {
                return m_BaseDeDonnees;
            }
        }

        /// <summary>
        /// Indique si la connexion est établie (détection passive en fonction du dernier état connu de la connexion)
        /// </summary>
        public bool EstConnecte
        {
            get
            {
                return (m_Connexion != null) && (m_Connexion.State != System.Data.ConnectionState.Closed) && (m_Connexion.State != System.Data.ConnectionState.Broken);
            }
        }
        #endregion

        #region Propriété publique de type évènement

                #region Changement état
                public enum ChangementsEtat
                {
                    ConnexionEtablie,
                    ConnexionFermee,
                    ConnexionPerdue
                }

                public class ChangementEtatEventArgs : EventArgs
                {
                    public static readonly ChangementEtatEventArgs ConnexionEtablie = new ChangementEtatEventArgs(ChangementsEtat.ConnexionEtablie);
                    public static readonly ChangementEtatEventArgs ConnexionFermee = new ChangementEtatEventArgs(ChangementsEtat.ConnexionFermee);
                    public static readonly ChangementEtatEventArgs ConnexionPerdue = new ChangementEtatEventArgs(ChangementsEtat.ConnexionPerdue);

                    private ChangementsEtat m_ChangementEtat;

                    public ChangementsEtat ChangementEtat { get { return m_ChangementEtat; } }

                    private ChangementEtatEventArgs(ChangementsEtat ChangementEtat)
                    {
                        m_ChangementEtat = ChangementEtat;
                    }
                }

                public event EventHandler<ChangementEtatEventArgs> SurChangementEtat = null;//liste de message bien spécifique qui s'appelle changemnt EvertArgs
                // message de l'évènement : event Args

                #endregion 

                #region Sur Consultation
                public class MessageSurConsultationEventArgs : EventArgs
                {
                    public string m_Requete;
                }
                public event EventHandler<MessageSurConsultationEventArgs> SurConsultation = null;

                #endregion 

                #region Evènement sur execution
                public event EventHandler<MessageSurExecution> SurExecution = null;
                public class MessageSurExecution : EventArgs
                {
                    public string m_Requete;
                }
                #endregion 

                #region Evènement sur recuperation
                public event EventHandler<MessageSurRecuperation> SurRecuperation = null;
                public class MessageSurRecuperation : EventArgs
                {
                    public string m_Requete;
                    public string Requete { get { return m_Requete; } }
                    
                }
                #endregion 

                #region Sur erreur
                public class ErreurSqlEventArgs : EventArgs
                {
                    private object m_Contexte;
                    private string m_Requete, m_Message;

                    public string Requete { get { return m_Requete; } }

                    public string Message { get { return m_Message; } }

                    public object Contexte { get { return m_Contexte; } }

                    public ErreurSqlEventArgs(string Requete, string Message, object Contexte = null)
                    {
                        m_Contexte = Contexte;
                        m_Requete = Requete;
                        m_Message = Message;
                    }
                }

                public event EventHandler<ErreurSqlEventArgs> SurErreur = null;

                private void SignalerErreur(string Requete, string Message, object Contexte = null)
                {
                    if (SurErreur != null) SurErreur(this, new ErreurSqlEventArgs(Requete, Message, Contexte));
                }
                #endregion

        #endregion 

        #region Méthodes relatives à la connexion
        /// <summary>
        /// Permet de tester si la connexion est encore en vie (détection active de l'état de connexion par un PING)
        /// </summary>
        /// <returns>Vrai si la connexion est encore en vie, sinon faux</returns>
        public bool TesterConnexion()
        {
            return (m_Connexion != null) && m_Connexion.Ping();
        }

        /// <summary>
        /// Crée et initialise une nouvelle instance de la classe BD en permettant de définir les paramètres de connexion au serveur et les modalités d'accès à la base (constructeur)
        /// </summary>
        /// <param name="NomUtilisateur">Nom de l'utilisateur MySql</param>
        /// <param name="MotDePasse">Mot de passe de cet utilisateur</param>
        /// <param name="BaseDeDonnees">Nom de la base de données [optionnel]</param>
        /// <param name="JeuDeCaracteres">Jeu de caractères utilisé pour le transfert des données du serveur au client (noms et valeurs de champs), et inversément (code SQL envoyé au serveur) [optionnel]</param>
        /// <param name="AdresseServeur">Adresse du serveur MySql [par défaut : localhost]</param>
        public BD(string NomUtilisateur, string MotDePasse, string BaseDeDonnees = null, JeuxDeCaracteres JeuDeCaracteres = JeuxDeCaracteres.ParDefaut, string AdresseServeur = "localhost")
        {
            m_NomUtilisateur = NomUtilisateur;
            m_MotDePasse = MotDePasse;
            m_BaseDeDonnees = BaseDeDonnees;
            m_JeuDeCaracteres = JeuDeCaracteres;
            m_AdresseServeur = AdresseServeur;
            m_Connexion = null;
        }

        /// <summary>
        /// Destructeur (appelé quand le Garbage Collector décide de détruire cet objet une fois qu'il n'est plus référencé)
        /// </summary>
        ~BD()
        {
            Deconnecter();
        }

        /// <summary>
        /// Implémente la méthode de libération des ressources requis suite à l'héritage pour cette classe de l'interface IDisposable ; ce qui permet de créer un objet de cette classe avec l'instruction using
        /// </summary>
        public void Dispose()
        {
            Deconnecter();
        }

        /// <summary>
        /// Tente de se connecter à un serveur MySql avec les données de connexion spécifiées par le constructeur
        /// </summary>
        /// <returns>Vrai si la connexion a pu être établie selon les paramètres spécifiés par le constructeur, sinon faux</returns>
        public bool Connecter()
        {
            string ChaineDeConnexion = string.Format("server={0};uid={1};pwd={2};Allow User Variables=true;Allow Zero Datetime=true;Convert Zero Datetime=true;", m_AdresseServeur, m_NomUtilisateur, m_MotDePasse);
            try
            {
                if ((m_Connexion != null) && m_Connexion.Ping()) return false;
                m_Connexion = new MySqlConnection(ChaineDeConnexion);
                m_Connexion.Open();
                if (!string.IsNullOrWhiteSpace(m_BaseDeDonnees) && !Executer(string.Format("USE `{0}`;", m_BaseDeDonnees)).Reussite) return false;
                if ((m_JeuDeCaracteres != JeuxDeCaracteres.ParDefaut) && !Executer(string.Format("SET NAMES \"{0}\";", s_CodesJeuxDeCaracteres[(int)m_JeuDeCaracteres])).Reussite) return false;
                if (SurChangementEtat != null) SurChangementEtat(this, ChangementEtatEventArgs.ConnexionEtablie);
                return true;
            }
            catch (Exception Erreur)
            {
                Debug.WriteLine(string.Format("\n{0}\n{1}\n{2}\n{3}\n{0}\n",
                    new string('-', 120),
                    OutilsCode.ObtenirInformationAppelants(1),
                    ChaineDeConnexion,
                    Erreur.Message));
                if (m_Connexion != null)
                {
                    m_Connexion.Dispose();
                    m_Connexion = null;
                }
                return false;
            }
        }

        /// <summary>
        /// Tente de se déconnecter d'un serveur MySql
        /// </summary>
        /// <returns>Vrai si la déconnexion a pu se faire, sinon faux</returns>
        public bool Deconnecter()
        {
            if ((m_Connexion == null) || !m_Connexion.Ping())
            {
                if (m_Connexion != null)
                {
                    m_Connexion.Dispose();
                    m_Connexion = null;
                }
                return false;
            }
            m_Connexion.Dispose();
            m_Connexion = null;
            if (SurChangementEtat != null) SurChangementEtat(this, ChangementEtatEventArgs.ConnexionFermee); // Déclenchement de l'évènement pour signaler une "connexion fermée"
            return true;
        }

        /// <summary>
        /// Permet de tester si la connexion est toujours en vie ;  si ce n'est pas le cas, il y a déclenchement de l'évènement de changement d'état vers une "Connexion Perdue"
        /// </summary>
        /// <returns>Vrai si la connexion est encore en vie, sinon faux</returns>
        private bool TesterValiditeConnexion()
        {
            bool ConnexionPrecedemmentEtablie = EstConnecte;
            if (!EstConnecte && Connecter()) return true;
            if (TesterConnexion()) return true;
            if (ConnexionPrecedemmentEtablie && (SurChangementEtat != null)) SurChangementEtat(this, ChangementEtatEventArgs.ConnexionPerdue);
            return false;
        }
        #endregion

        #region Fonctions relatives à l'exécution de requête SQL de type action
        public class ResultatExecution
        {

            public static readonly ResultatExecution Echec = new ResultatExecution(false, 0, 0);

            private object m_Contexte;
            private bool m_Reussite;
            private long m_IdGenere;
            private long m_NombreEnregistrementsAffectes;

            public bool Reussite { get { return m_Reussite; } }

            public long IdGenere { get { return m_IdGenere; } }

            public long NombreEnregistrementsAffectes { get { return m_NombreEnregistrementsAffectes; } }

            public object Contexte { get { return m_Contexte; } }

            public ResultatExecution(long IdGenere, long NombreEnregistrementsAffectes, object Contexte = null)
                : this(true, IdGenere, NombreEnregistrementsAffectes)
            {
            }

            private ResultatExecution(bool Reussite, long IdGenere, long NombreEnregistrementsAffectes, object Contexte = null)
            {
                m_Contexte = Contexte;
                m_Reussite = Reussite;
                m_IdGenere = IdGenere;
                m_NombreEnregistrementsAffectes = NombreEnregistrementsAffectes;
            }
        }

        /// <summary>
        /// Permet d'exécuter la requête sql spécifiée auprès du serveur connecté
        /// </summary>
        /// <param name="Sql">Requête SQL à exécuter</param>
        /// <param name="Arguments">Arguments permettant de définir les valeurs des éventuelles parties variables de la requête SQL</param>
        /// <returns>Objet décrivant le résultat de cette action SQL</returns>
        public ResultatExecution Executer(string Sql, params object[] Arguments)
        {
            return ExecuterAvecContexte(null, Sql, Arguments);
        }

        /// <summary>
        /// Permet d'exécuter la requête sql spécifiée auprès du serveur connecté
        /// </summary>
        /// <param name="Contexte">Contexte d'exécution de cette requête, il est récupérable en cas d'erreur (via la descrition de l'événement SurErreur) et en cas de réussite (via l'objet de résultat de cette action SQL)</param>
        /// <param name="Sql">Requête SQL à exécuter</param>
        /// <param name="Arguments">Arguments permettant de définir les valeurs des éventuelles parties variables de la requête SQL</param>
        /// <returns>Objet décrivant le résultat de cette action SQL</returns>
        public ResultatExecution ExecuterAvecContexte(object Contexte, string Sql, params object[] Arguments)
        {
            MessageSurExecution message = new MessageSurExecution();
            message.m_Requete = Sql;
            if (SurExecution != null) SurExecution(this, message);
            if (!TesterValiditeConnexion()) return ResultatExecution.Echec;
            try
            {
                Sql = ((Arguments.Length == 0) ? Sql : string.Format(Sql, Arguments.Select<object, string>(Valeur => EnSql(Valeur)).ToArray())).Trim();
                using (MySqlCommand Commande = new MySqlCommand(Sql, m_Connexion))
                {
                    long NombreEnregistrementsAffectes = Commande.ExecuteNonQuery();
                    long IdGenere = 0;
                    if (Sql.StartsWith("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Commande.CommandText = "SELECT LAST_INSERT_ID();";
                        using (MySqlDataReader CurseurLecture = Commande.ExecuteReader())
                        {
                            if (CurseurLecture.Read())
                            {
                                IdGenere = CurseurLecture.GetInt64(0);
                            }
                        }
                    }
                    return new ResultatExecution(IdGenere, NombreEnregistrementsAffectes, Contexte);
                }
            }
            catch (Exception Erreur)
            {
                SignalerErreur(Sql, Erreur.Message, Contexte);
                Debug.WriteLine(string.Format("\n{0}\n{1}\n{2}\n{3}\n{0}\n",
                    new string('-', 120),
                    OutilsCode.ObtenirInformationAppelants(1),
                    Sql,
                    Erreur.Message));
                return ResultatExecution.Echec;
            }
        }
        #endregion

        #region Fonctions relatives à l'exploitation des résultats de requêtes de type consultation
        /// <summary>
        /// Décrit un enregistrement résultant d'une consultation
        /// </summary>
        public class Enregistrement : IEnumerable<object>
        {
            #region Membres privés
            private List<string> m_Noms;
            protected object[] m_Valeurs;
            #endregion

            /// <summary>
            /// Nombre de champs
            /// </summary>
            public int NombreChamps { get { return m_Noms.Count; } }

            /// <summary>
            /// Indexeur de valeur de champ (par indice)
            /// </summary>
            /// <param name="Index">Indice du champ</param>
            /// <returns>Valeur du champ</returns>
            public object this[int Index] { get { return ((Index >= 0) && (Index < m_Valeurs.Length)) ? m_Valeurs[Index] : null; } }

            /// <summary>
            /// Indexeur de valeur de champ (par nom de champ)
            /// </summary>
            /// <param name="Nom">Nom du champ</param>
            /// <returns>Valeur du champ</returns>
            public object this[string Nom] { get { return this[m_Noms.IndexOf(Nom)]; } }

            public IEnumerator<object> GetEnumerator()
            {
                foreach (object Valeur in m_Valeurs) yield return Valeur;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return m_Valeurs.GetEnumerator();
            }

            protected Enregistrement(IEnumerable<string> NomsChamps)
            {
                m_Noms = new List<string>(NomsChamps);
                m_Valeurs = new object[m_Noms.Count];
            }

            protected object[] Valeurs { get { return m_Valeurs; } }
        }

        private class EnregistrementConsulte : Enregistrement
        {

            public EnregistrementConsulte(MySqlDataReader CurseurLecture)
                : base(EnumererNomsChamps(CurseurLecture))
            {
            }

            private static IEnumerable<string> EnumererNomsChamps(MySqlDataReader CurseurLecture)
            {
                for (int Index = 0; Index < CurseurLecture.FieldCount; Index++) yield return CurseurLecture.GetName(Index);
            }

            public bool DefinirValeurs(MySqlDataReader CurseurLecture)
            {
                try
                {
                    return (CurseurLecture.GetValues(base.Valeurs) == m_Valeurs.Length);
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Permet de récupérer les données des enregistrements résultant d'une requête de consultation auprès du serveur connecté
        /// </summary>
        /// <param name="Sql">Requête SQL à exécuter</param>
        /// <param name="Arguments">Arguments permettant de définir les valeurs des éventuelles parties variables de la requête SQL</param>
        /// <returns>Vrai si la requête a pu être exécutée, faux sinon</returns>
        public IEnumerable<Enregistrement> Consulter(string Sql, params object[] Arguments)
        {
            return ConsulterAvecContexte(null, Sql, Arguments);
        }

        /// <summary>
        /// Permet de récupérer les données des enregistrements résultant d'une requête de consultation auprès du serveur connecté
        /// </summary>
        /// <param name="Contexte">Contexte d'exécution de cette requête, il est récupérable uniquement en cas d'erreur (via la descrition de l'événement SurErreur)</param>
        /// <param name="Sql">Requête SQL à exécuter</param>
        /// <param name="Arguments">Arguments permettant de définir les valeurs des éventuelles parties variables de la requête SQL</param>
        /// <returns>Vrai si la requête a pu être exécutée, faux sinon</returns>
        public IEnumerable<Enregistrement> ConsulterAvecContexte(object Contexte, string Sql, params object[] Arguments)
        {
            MessageSurConsultationEventArgs messageConsultation = new MessageSurConsultationEventArgs();
            messageConsultation.m_Requete = Sql;
            if (SurConsultation != null) SurConsultation(this, messageConsultation);
            if (string.IsNullOrWhiteSpace(Sql) || (Arguments == null) || (Arguments.Length == 0) || !TesterValiditeConnexion()) yield break;
            if (!Sql.StartsWith("SELECT", StringComparison.CurrentCultureIgnoreCase) && !Sql.StartsWith("SHOW", StringComparison.CurrentCultureIgnoreCase)) yield break;
            MySqlCommand Commande = null;
            MySqlDataReader CurseurLecture = null;
            EnregistrementConsulte Enregistrement = null;
            while (true)
            {
                try
                {
                    if (Enregistrement == null)
                    {
                        Sql = ((Arguments.Length == 1) ? Sql : string.Format(Sql, Arguments.Select<object, string>(Valeur => EnSql(Valeur)).ToArray())).Trim();
                        Commande = new MySqlCommand(Sql, m_Connexion);
                        CurseurLecture = Commande.ExecuteReader();
                        if (!CurseurLecture.Read()) break;
                        Enregistrement = new EnregistrementConsulte(CurseurLecture);
                    }
                    else
                    {
                        if (!CurseurLecture.Read()) break;
                    }
                    Enregistrement.DefinirValeurs(CurseurLecture);
                }
                catch (Exception Erreur)
                {
                    SignalerErreur(Sql, Erreur.Message);
                    Debug.WriteLine(string.Format("\n{0}\n{1}\n{2}\n{3}\n{0}\n",
                        new string('-', 120),
                        OutilsCode.ObtenirInformationAppelants(1),
                        Sql,
                        Erreur.Message));
                    break;
                }
                yield return Enregistrement;
            }
            if (CurseurLecture!= null) CurseurLecture.Dispose();
            Commande.Dispose();
          
        }

        /// <summary>
        /// Permet de récupérer la valeur du premier champ du premier enregistrement résultant d'une requête de consultation auprès du serveur connecté
        /// </summary>
        /// <param name="Sql">Requête SQL à exécuter</param>
        /// <param name="Arguments">Arguments permettant de définir les valeurs des éventuelles parties variables de la requête SQL</param>
        /// <returns>Valeur du premier champ du premier enregistrement si la requête a fourni au moins un enregistrement, sinon null</returns>
        public object Recuperer(string Sql, params object[] Arguments)
        {
            return RecupererAvecContexte(null, Sql, Arguments);
        }

        /// <summary>
        /// Permet de récupérer la valeur du premier champ du premier enregistrement résultant d'une requête de consultation auprès du serveur connecté
        /// </summary>
        /// <param name="Contexte">Contexte d'exécution de cette requête, il est récupérable uniquement en cas d'erreur (via la descrition de l'événement SurErreur)</param>
        /// <param name="Sql">Requête SQL à exécuter</param>
        /// <param name="Arguments">Arguments permettant de définir les valeurs des éventuelles parties variables de la requête SQL</param>
        /// <returns>Valeur du premier champ du premier enregistrement si la requête a fourni au moins un enregistrement, sinon null</returns>
        public object RecupererAvecContexte(object Contexte, string Sql, params object[] Arguments)
        {

            MessageSurRecuperation message = new MessageSurRecuperation();
            message.m_Requete = Sql;
            if (SurRecuperation != null) SurRecuperation(this, message);
            if (string.IsNullOrWhiteSpace(Sql) || !TesterValiditeConnexion()) return false;
            Sql = ((Arguments.Length == 0) ? Sql : string.Format(Sql, Arguments.Select<object, string>(Valeur => EnSql(Valeur)).ToArray())).Trim();
            if (!Sql.StartsWith("SELECT", StringComparison.CurrentCultureIgnoreCase) && !Sql.StartsWith("SHOW", StringComparison.CurrentCultureIgnoreCase)) return false;
            try
            {
                using (MySqlCommand Commande = new MySqlCommand(Sql, m_Connexion))
                {
                    using (MySqlDataReader CurseurLecture = Commande.ExecuteReader())
                    {
                        if (CurseurLecture.Read())
                        {
                            return CurseurLecture.GetValue(0);
                        }
                    }
                }
            }
            catch (Exception Erreur)
            {
                SignalerErreur(Sql, Erreur.Message, Contexte);
                Debug.WriteLine(string.Format("\n{0}\n{1}\n{2}\n{3}\n{0}\n",
                    new string('-', 120),
                    OutilsCode.ObtenirInformationAppelants(1),
                    Sql,
                    Erreur.Message));
            }
            return null;
        }

        /// <summary>
        /// Permet de récupérer la valeur du premier champ du premier enregistrement résultant d'une requête de consultation auprès du serveur connecté
        /// </summary>
        /// <typeparam name="T">Type de valeur attendue : type utilisé pour caster le "résultat" de la requête</typeparam>
        /// <param name="Sql">Requête SQL à exécuter</param>
        /// <param name="Arguments">Arguments permettant de définir les valeurs des éventuelles parties variables de la requête SQL</param>
        /// <returns>Valeur du premier champ du premier enregistrement si la requête a fourni au moins un enregistrement, sinon null</returns>
        public T Recuperer<T>(string Sql, params object[] Arguments)
        {
            return RecupererAvecContexte<T>(null, Sql, Arguments);
        }

        /// <summary>
        /// Permet de récupérer la valeur du premier champ du premier enregistrement résultant d'une requête de consultation auprès du serveur connecté
        /// </summary>
        /// <typeparam name="T">Type de valeur attendue : type utilisé pour caster le "résultat" de la requête</typeparam>
        /// <param name="Contexte">Contexte d'exécution de cette requête, il est récupérable uniquement en cas d'erreur (via la descrition de l'événement SurErreur)</param>
        /// <param name="Sql">Requête SQL à exécuter</param>
        /// <param name="Arguments">Arguments permettant de définir les valeurs des éventuelles parties variables de la requête SQL</param>
        /// <returns>Valeur du premier champ du premier enregistrement si la requête a fourni au moins un enregistrement, sinon null</returns>
        public T RecupererAvecContexte<T>(object Contexte, string Sql, params object[] Arguments)
        {
            MessageSurRecuperation message = new MessageSurRecuperation();
            message.m_Requete = Sql;
            if (SurRecuperation != null) SurRecuperation(this, message);
            object Valeur = Recuperer(Sql, Arguments);
            try
            {
                return (T)Valeur;
            }
            catch (Exception Erreur)
            {
                if (Valeur != null)
                {
                    SignalerErreur(Sql, Erreur.Message, Contexte);
                    Debug.WriteLine(string.Format("\n{0}\n{1}\n{2}\n{3}\n{0}\n",
                        new string('-', 120),
                        OutilsCode.ObtenirInformationAppelants(1),
                        Sql,
                        Erreur.Message));
                }
                return default(T);
            }
        }
        #endregion

    }
}
