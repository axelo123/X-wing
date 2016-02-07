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
        /// Interface publique de classe permettant d'informer de la réussite ou de l'échec d'une requête d'action, avec éventuellement le nombre d'enregistrements affectés et le dernier nouvel identifiant généré si il s'agissait d'une requête de création d'enregistrement
        /// </summary>
        public interface IUpdateResult
        {
            /// <summary>
            /// Indique si la requête d'action a réussi à être exécutée ou pas
            /// </summary>
            bool IsSuccessful { get; }

            /// <summary>
            /// Indique le nombre d'enregistrements affectés
            /// </summary>
            int RecordCount { get; }

            /// <summary>
            /// Indique le dernier identifiant nouvellement généré par une requête de création d'enregistrement
            /// </summary>
            long NewId { get; }
        }

        /// <summary>
        /// Classe privée implémentant l'interface IResultatAction permettant d'informer de la réussite ou de l'échec d'une requête d'action, avec éventuellement le nombre d'enregistrements affectés et le dernier nouvel identifiant généré si il s'agissait d'une requête de création d'enregistrement
        /// </summary>
        private class UpdateResult : IUpdateResult
        {
            #region Membre privé statique
            private static readonly UpdateResult s_Failure = new UpdateResult();
            #endregion

            /// <summary>
            /// Objet unique qui indique l'échec d'exécution d'une requête d'action
            /// </summary>
            public static UpdateResult Failure { get { return s_Failure; } }

            #region Membres privés
            private bool m_IsSuccessful;
            private int m_RecordCount;
            private long m_NewId;
            #endregion

            /// <summary>
            /// Indique si la requête d'action a réussi à être exécutée ou pas
            /// </summary>
            public bool IsSuccessful { get { return m_IsSuccessful; } }

            /// <summary>
            /// Indique le nombre d'enregistrements affectés
            /// </summary>
            public int RecordCount { get { return m_RecordCount; } }

            /// <summary>
            /// Indique le dernier identifiant nouvellement généré par une requête de création d'enregistrement
            /// </summary>
            public long NewId { get { return m_NewId; } }

            /// <summary>
            /// Constructeur privé permettant de décrire un échec d'exécution d'une requête d'action
            /// </summary>
            private UpdateResult()
            {
                m_IsSuccessful = false;
                m_RecordCount = 0;
                m_NewId = 0;
            }

            /// <summary>
            /// Constructeur publique permettant de décrire une réussite d'exécution d'une requête d'action
            /// </summary>
            /// <param name="RecordCount">Nombre d'enregistrements affectés</param>
            /// <param name="NewId">Dernier identifiant lors de la création d'un nouvel enregistrement</param>
            public UpdateResult(int RecordCount, long NewId = 0)
            {
                m_IsSuccessful = true;
                m_RecordCount = RecordCount;
                m_NewId = NewId;
            }
        }

        /// <summary>
        /// Permet d'exécuter une requête d'action
        /// </summary>
        /// <param name="Query">Requête SQL d'action, pouvant comporter des parties variables</param>
        /// <param name="Arguments">Valeurs des parties variables de la requête</param>
        /// <returns>Objet décrivant la réussite ou non, ainsi que donnant des informations sur les enregistrements affectés</returns>
        public IUpdateResult Execute(string Query, params object[] Arguments)
        {
            Query = FormatQuery(Query, Arguments);
            try
            {
                using (MySqlCommand Command = new MySqlCommand(Query, m_Connection))
                {
                    int RecordCount = Command.ExecuteNonQuery();
                    long NewId = Query.Trim().StartsWith("INSERT", StringComparison.InvariantCultureIgnoreCase) ? Command.LastInsertedId : 0;
                    return new UpdateResult(RecordCount, NewId);
                }
            }
            catch (Exception Error)
            {
                #region Affichage dans la console de débuggage du message d'erreur
                Debug.WriteLine(new string('-', 200));
                Debug.WriteLine(string.Format("Erreur d'exécution d'une requête d'action\n{0}", Query));
                Debug.WriteLine(Error.Message);
                Debug.WriteLine(new string('-', 200));
                #endregion
                return UpdateResult.Failure;
            }
        }
    }
}
