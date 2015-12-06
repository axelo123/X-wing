using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Globalization;

namespace MyDB
{
    /// <summary>
    /// Permet d'avoir un accès à une base de données MySQL
    /// </summary>
    public partial class MyDB
    {
        /// <summary>
        /// Interface publique de classe permettant d'informer de la réussite ou de l'échec d'une requête de consultation, avec le nombre d'enregistrements consultés et un indicateur précisant si tous les enregistrements ont pu être pris en compte par la boucle de lecture ou si l'appelant de la méthode de consultation y a mis fin prématurément
        /// </summary>
        public interface IReadResult
        {
            /// <summary>
            /// Indique si la requête de consultation a réussi à être exécutée ou pas
            /// </summary>
            bool IsSuccessful { get; }

            /// <summary>
            /// Indique si la boucle de lecture des enregistrements a pu se terminer normalement, ou au contraire a été stoppée prématurément par l'appelant de la méthode de consultation
            /// </summary>
            bool IsComplete { get; }

            /// <summary>
            /// Indique le nombre d'enregistrements consultés
            /// </summary>
            int RecordCount { get; }
        }

        /// <summary>
        /// Classe privée implémentant l'interface IResultatAction permettant d'informer de la réussite ou de l'échec d'une requête de consultation, avec le nombre d'enregistrements consultés et un indicateur précisant si tous les enregistrements ont pu être pris en compte par la boucle de lecture ou si l'appelant de cette méthode y a mis fin prématurément
        /// </summary>
        private class ReadResult : IReadResult
        {
            #region Membres privés
            private bool m_IsSuccessful;
            private bool m_IsComplete;
            private int m_RecordCount;
            #endregion

            /// <summary>
            /// Indique si la requête de consultation a réussi à être exécutée ou pas
            /// </summary>
            public bool IsSuccessful { get { return m_IsSuccessful; } }

            /// <summary>
            /// Indique si la boucle de lecture des enregistrements a pu se terminer normalement, ou au contraire a été stoppée prématurément par la méthode de traitement
            /// </summary>
            public bool IsComplete { get { return m_IsComplete; } }

            /// <summary>
            /// Indique le nombre d'enregistrements consultés
            /// </summary>
            public int RecordCount { get { return m_RecordCount; } }

            /// <summary>
            /// Constructeur publique permettant de décrire un résultat de consultation à priori réussi et incomplet, avec 0 enregistrement consulté
            /// </summary>
            public ReadResult()
            {
                m_IsSuccessful = true;
                m_IsComplete = false;
                m_RecordCount = 0;
            }

            /// <summary>
            /// Fait évoluer cet objet de résultat en signalant un échec
            /// </summary>
            public void ReportFailure()
            {
                m_IsSuccessful = false;
            }

            /// <summary>
            /// Fait évoluer cet objet de résultat en signalant un enregistrement supplémentaire consulté
            /// </summary>
            public void ReportRecordAdded()
            {
                m_RecordCount++;
            }

            /// <summary>
            /// Fait évoluer cet objet de résultat en signalant que la consultation est complètement terminée
            /// </summary>
            public void ReportCompletenessReached()
            {
                m_IsComplete = true;
            }
        }

        /// <summary>
        /// Interface publique de classe permettant de fournir les informations relatives à un enregistrement consulté
        /// </summary>
        public interface IRecord
        {
            /// <summary>
            /// Informations de résultat de la consultation ayant produit cet enregistrement
            /// </summary>
            IReadResult Result { get; }

            /// <summary>
            /// Nombre de champs
            /// </summary>
            int FieldCount { get; }

            /// <summary>
            /// Indexeur de valeur de champ par indice
            /// </summary>
            /// <param name="Index">Indice du champ</param>
            /// <returns>Valeur du champ si l'indice est valide, sinon null</returns>
            object this[int Index] { get; }

            /// <summary>
            /// Indexeur de valeur de champ par nom
            /// </summary>
            /// <param name="FieldName">Nom du champ</param>
            /// <returns>Valeur du champ si le nom est valide, sinon null</returns>
            object this[string FieldName] { get; }

            /// <summary>
            /// Indexeur de nom de champ par indice
            /// </summary>
            /// <param name="Index">Indice du champ</param>
            /// <returns>Nom du champ si l'indice est valide, sinon null</returns>
            string FieldName(int Index);
        }

        /// <summary>
        /// Classe privée implémentant l'interface IEnregistrementConsulte permettant de fournir les informations relatives à un enregistrement consulté
        /// </summary>
        private class Record : IRecord
        {
            #region Membres privés
            private ReadResult m_Result; // Permettant d'avoir un référence sur l'objet fournissant les informations de résultat de la consultation ayant généré cet élément
            private string[] m_FieldNames; // Permettant d'avoir un accès par indice aux noms de champs
            private object[] m_FieldValues; // Permettant d'avoir un accès par indice aux valeurs de champs
            private Dictionary<string, object> m_Fields; // Permettant d'avoir un accès par nom aux valeurs de champs
            #endregion

            /// <summary>
            /// Informations de résultat de la consultation ayant produit cet enregistrement
            /// </summary>
            public IReadResult Result
            {
                get
                {
                    return m_Result;
                }
            }

            /// <summary>
            /// Nombre de champs
            /// </summary>
            public int FieldCount
            {
                get
                {
                    return m_FieldValues.Length;
                }
            }

            /// <summary>
            /// Indexeur de valeur de champ par indice
            /// </summary>
            /// <param name="Index">Indice du champ</param>
            /// <returns>Valeur du champ si l'indice est valide, sinon null</returns>
            public object this[int Index]
            {
                get
                {
                    return ((Index >= 0) && (Index < m_FieldValues.Length)) ? m_FieldValues[Index] : null;
                }
            }

            /// <summary>
            /// Indexeur de valeur de champ par nom
            /// </summary>
            /// <param name="FieldName">Nom du champ</param>
            /// <returns>Valeur du champ si le nom est valide, sinon null</returns>
            public object this[string FieldName]
            {
                get
                {
                    object Valeur;
                    return (FieldName != null) && m_Fields.TryGetValue(FieldName, out Valeur) ? Valeur : null;
                }
            }

            /// <summary>
            /// Indexeur de nom de champ par indice
            /// </summary>
            /// <param name="Index">Indice du champ</param>
            /// <returns>Nom du champ si l'indice est valide, sinon null</returns>
            public string FieldName(int Index)
            {
                return ((Index >= 0) && (Index < m_FieldNames.Length)) ? m_FieldNames[Index] : null;
            }

            /// <summary>
            /// Constructeur par défaut
            /// </summary>
            /// <param name="Result">Référence de l'objet décrivant le résultat de consultation produisant cet enregistrement</param>
            public Record(ReadResult Result)
            {
                m_Result = Result;
                m_FieldNames = new string[0];
                m_FieldValues = new object[0];
                m_Fields = new Dictionary<string, object>();
            }

            /// <summary>
            /// Permet de remplir le contenu de cet objet décrivant un enregistrement consulté
            /// </summary>
            /// <param name="MySqlRecord">Objet de type MySqlDataReader fournissant noms et valeurs des champs</param>
            public void Set(MySqlDataReader MySqlRecord)
            {
                try
                {
                    if (m_FieldValues.Length == 0)
                    {
                        m_FieldNames = new string[MySqlRecord.FieldCount];
                        m_FieldValues = new object[MySqlRecord.FieldCount];
                        for (int Indice = 0; Indice < MySqlRecord.FieldCount; Indice++)
                        {
                            m_FieldNames[Indice] = MySqlRecord.GetName(Indice);
                            m_Fields.Add(m_FieldNames[Indice], null);
                        }
                    }
                    MySqlRecord.GetValues(m_FieldValues);
                    for (int Indice = 0; Indice < MySqlRecord.FieldCount; Indice++)
                    {
                        m_Fields[m_FieldNames[Indice]] = m_FieldValues[Indice];
                    }
                }
                catch
                {
                    for (int Indice = 0; Indice < m_FieldValues.Length; Indice++)
                    {
                        m_FieldValues[Indice] = null;
                        m_Fields[m_FieldNames[Indice]] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Type de méthode de traitement d'un enregistrement consulté
        /// </summary>
        /// <param name="Record">Description de l'enregistrement</param>
        /// <returns>Vrai si on désire continuer la consultation des prochains enregistrements, sinon faux</returns>
        public delegate bool RecordProcessMethod(IRecord Record);

        /// <summary>
        /// Permet de consulter les enregistrements d'une requête de consultation (SELECT/SHOW) et les exploiter via une méthode de traitement
        /// </summary>
        /// <param name="ProcessMethod">Méthode de traitement appelée pour chaque enregistrement consulté</param>
        /// <param name="Query">Requête SQL de consultation, pouvant comporter des parties variables</param>
        /// <param name="Arguments">Valeurs des parties variables de la requête</param>
        /// <returns>Objet décrivant la réussite ou non de la consultation, ainsi que donnant des informations sur les enregistrements consultés</returns>
        public IReadResult Read(RecordProcessMethod ProcessMethod, string Query, params object[] Arguments)
        {
            ReadResult Result = new ReadResult();
            Query = FormatQuery(Query, Arguments);
            try
            {
                using (MySqlCommand Command = new MySqlCommand(Query, m_Connection))
                {
                    using (MySqlDataReader MySqlRecord = Command.ExecuteReader())
                    {
                        Record Record = new Record(Result);
                        while (MySqlRecord.Read())
                        {
                            Result.ReportRecordAdded();
                            Record.Set(MySqlRecord);
                            if (!ProcessMethod(Record))
                            {
                                return Result;
                            }
                        }
                        Result.ReportCompletenessReached();
                        return Result;
                    }
                }
            }
            catch (Exception Error)
            {
                #region Affichage dans la console de débuggage du message d'erreur
                Debug.WriteLine(new string('-', 200));
                Debug.WriteLine(string.Format("Erreur d'exécution d'une requête de consultation\n{0}", Query));
                Debug.WriteLine(Error.Message);
                Debug.WriteLine(new string('-', 200));
                #endregion
                Result.ReportFailure();
                return Result;
            }
        }

        /// <summary>
        /// Permet de consulter les enregistrements d'une requête de consultation (SELECT/SHOW) et les exploiter via consommation de l'énumération retournée
        /// </summary>
        /// <param name="Query">Requête SQL de consultation, pouvant comporter des parties variables</param>
        /// <param name="Arguments">Valeurs des parties variables de la requête</param>
        /// <returns>Objet décrivant chaque enregistrement consulté</returns>
        public IEnumerable<IRecord> Read(string Query, params object[] Arguments)
        {
            ReadResult Result = new ReadResult();
            Query = FormatQuery(Query, Arguments);
            Record Record = null;
            MySqlCommand Command = null;
            MySqlDataReader MySqlRecord = null;
            while (true)
            {
                try
                {
                    if (Record == null)
                    {
                        Command = new MySqlCommand(Query, m_Connection);
                        MySqlRecord = Command.ExecuteReader();
                        Record = new Record(Result);
                    }
                    if (MySqlRecord.Read())
                    {
                        Result.ReportRecordAdded();
                        Record.Set(MySqlRecord);
                    }
                    else
                    {
                        Result.ReportCompletenessReached();
                        break;
                    }
                }
                catch (Exception Error)
                {
                    #region Affichage dans la console de débuggage du message d'erreur
                    Debug.WriteLine(new string('-', 200));
                    Debug.WriteLine(string.Format("Erreur d'exécution d'une requête de consultation\n{0}", Query));
                    Debug.WriteLine(Error.Message);
                    Debug.WriteLine(new string('-', 200));
                    #endregion
                    Result.ReportFailure();
                    break;
                }
                yield return Record;
            }
            if (MySqlRecord != null) MySqlRecord.Dispose();
            if (Command != null) Command.Dispose();
        }

        /// <summary>
        /// Permet d'obtenir la valeur du premier champ du premier enregistrement résultant de cette requête de consultation (SELECT/SHOW)
        /// </summary>
        /// <param name="Query">Requête SQL de consultation, pouvant comporter des parties variables</param>
        /// <param name="Arguments">Valeurs des parties variables de la requête</param>
        /// <returns>Valeur du premier champ du premier enregistrement, ou en cas d'erreur d'exécution ou d'absence d'enregistrement, la valeur null</returns>
        public object GetValue(string Query, params object[] Arguments)
        {
            return GetValue2(null, Query, Arguments);
        }

        /// <summary>
        /// Permet d'obtenir la valeur du premier champ du premier enregistrement résultant de cette requête de consultation (SELECT/SHOW)
        /// </summary>
        /// <param name="DefaultValue">Valeur par défaut en cas d'échec d'exécution de la requête ou d'absence d'enregistrement</param>
        /// <param name="Query">Requête SQL de consultation, pouvant comporter des parties variables</param>
        /// <param name="Arguments">Valeurs des parties variables de la requête</param>
        /// <returns>Valeur du premier champ du premier enregistrement, ou en cas d'erreur d'exécution ou d'absence d'enregistrement, la valeur par défaut spécifiée</returns>
        public object GetValue2(object DefaultValue, string Query, params object[] Arguments)
        {
            Query = FormatQuery(Query, Arguments);
            try
            {
                using (MySqlCommand Command = new MySqlCommand(Query, m_Connection))
                {
                    using (MySqlDataReader MySqlRecord = Command.ExecuteReader())
                    {
                        if (MySqlRecord.Read())
                        {
                            return MySqlRecord[0];
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                #region Affichage dans la console de débuggage du message d'erreur
                Debug.WriteLine(new string('-', 200));
                Debug.WriteLine(string.Format("Erreur d'exécution d'une requête de consultation pour obtenir une seule valeur\n{0}", Query));
                Debug.WriteLine(Error.Message);
                Debug.WriteLine(new string('-', 200));
                #endregion
            }
            return DefaultValue;
        }

        /// <summary>
        /// Permet d'obtenir la valeur du premier champ du premier enregistrement résultant de cette requête de consultation (SELECT/SHOW)
        /// </summary>
        /// <typeparam name="T">Type de la valeur attendue comme résultat</typeparam>
        /// <param name="Query">Requête SQL de consultation, pouvant comporter des parties variables</param>
        /// <param name="Arguments">Valeurs des parties variables de la requête</param>
        /// <returns>Valeur du premier champ du premier enregistrement, ou en cas d'erreur d'exécution ou d'absence d'enregistrement, la valeur par défaut en fonction du type de valeur attendue comme résultat</returns>
        public T GetValue<T>(string Query, params object[] Arguments)
        {
            object Result = GetValue2((object)default(T), Query, Arguments);
            if (Result is MySqlDateTime)
            {
                MySqlDateTime MSQDT = (MySqlDateTime)Result;
                if (MSQDT.IsValidDateTime) Result = MSQDT.GetDateTime();
                return (Result is T) ? (T)Result : default(T);
            }
            else if (Result is MySqlDecimal)
            {
                MySqlDecimal MSQDC = (MySqlDecimal)Result;
                if (!MSQDC.IsNull) Result = MSQDC.Value;
                return (Result is T) ? (T)Result : default(T);
            }
            else
            {
                return (Result is T) ? (T)Result : default(T);
            }
        }

        /// <summary>
        /// Permet d'obtenir la valeur du premier champ du premier enregistrement résultant de cette requête de consultation (SELECT/SHOW)
        /// </summary>
        /// <typeparam name="T">Type de la valeur attendue comme résultat</typeparam>
        /// <param name="DefaultValue">Valeur par défaut en cas d'échec d'exécution de la requête ou d'absence d'enregistrement</param>
        /// <param name="Query">Requête SQL de consultation, pouvant comporter des parties variables</param>
        /// <param name="Arguments">Valeurs des parties variables de la requête</param>
        /// <returns>Valeur du premier champ du premier enregistrement, ou en cas d'erreur d'exécution ou d'absence d'enregistrement, la valeur par défaut spécifiée</returns>
        public T GetValue2<T>(T DefaultValue, string Query, params object[] Arguments)
        {
            object Result = GetValue2(DefaultValue, Query, Arguments);
            return (Result is T) ? (T)Result : DefaultValue;
        }
    }
}
