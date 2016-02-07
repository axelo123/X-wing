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
        #region Membres privés relatifs à la connexion
        private MySqlConnection m_Connection;
        private string m_UserName, m_Password, m_DataBaseName, m_ServerAddress;
        #endregion

        /// <summary>
        /// Constructeur par défaut permettant de créer un objet de la classe BD
        /// </summary>
        public MyDB()
        {
            m_Connection = new MySqlConnection();
            m_UserName = string.Empty;
            m_Password = string.Empty;
            m_DataBaseName = string.Empty;
            m_ServerAddress = string.Empty;
        }

        /// <summary>
        /// Constructeur fournissant les paramètres de connexion, et créant un objet de la classe BD
        /// </summary>
        /// <param name="UserName">Nom de l'utilisateur défini dans le serveur MySQL auquel on veut se connecter</param>
        /// <param name="Password">Mot de passe de l'utilisateur spécifié via le paramètre précédent</param>
        /// <param name="DataBaseName">Nom de la base de données</param>
        /// <param name="ServerAddress">Adresse du serveur MySQL (par défaut : localhost)</param>
        public MyDB(string UserName, string Password, string DataBaseName, string ServerAddress = "localhost")
        {
            m_Connection = new MySqlConnection();
            m_UserName = UserName;
            m_Password = Password;
            m_DataBaseName = DataBaseName;
            m_ServerAddress = ServerAddress;
        }

        /// <summary>
        /// Permet de se connecter au serveur MySQL en fonction des paramètres de connexion spécifiés
        /// </summary>
        /// <param name="UserName">Nom de l'utilisateur défini dans le serveur MySQL auquel on veut se connecter</param>
        /// <param name="Password">Mot de passe de l'utilisateur spécifié via le paramètre précédent</param>
        /// <param name="DataBaseName">Nom de la base de données</param>
        /// <param name="ServerAddress">Adresse du serveur MySQL (par défaut : localhost)</param>
        /// <returns>Vrai si la connexion a pu être établie avec le serveur MySQL, sinon faux</returns>
        public bool Connect(string UserName, string Password, string DataBaseName, string ServerAddress = "localhost")
        {
            m_UserName = UserName;
            m_Password = Password;
            m_DataBaseName = DataBaseName;
            m_ServerAddress = ServerAddress;
            return Connect();
        }

        /// <summary>
        /// Permet de se connecter au serveur MySQL en fonction des paramètres de connexion précédemment fourni
        /// </summary>
        /// <returns>Vrai si la connexion a pu être établie avec le serveur MySQL, sinon faux</returns>
        public bool Connect()
        {
            try
            {
                #region Tests de validité des informations nécessaires à la connexion et à l'utilisation d'une base de données MySQL
                if (string.IsNullOrWhiteSpace(m_ServerAddress)) throw new Exception("Adresse du serveur non définie !");
                if (string.IsNullOrWhiteSpace(m_UserName)) throw new Exception("Nom d'utilisateur non défini !");
                if (string.IsNullOrWhiteSpace(m_Password)) throw new Exception("Mot de passe non défini !");
                if (string.IsNullOrWhiteSpace(m_DataBaseName)) throw new Exception("Nom de base de données non défini !");
                if (m_ServerAddress.Contains('=') || m_ServerAddress.Contains(';')) throw new Exception("Adresse du serveur contenant un caractère illicite !");
                if (m_UserName.Contains('=') || m_UserName.Contains(';')) throw new Exception("Nom d'utilisateur contenant un caractère illicite !");
                if (m_Password.Contains('=') || m_Password.Contains(';')) throw new Exception("Mot de passe contenant un caractère illicite !");
                #endregion

                #region Création/mise à jour de la chaîne de connexion
                m_Connection.ConnectionString = string.Format("server={0};user={1};password={2}",
                    m_ServerAddress,
                    m_UserName,
                    m_Password);
                #endregion

                m_Connection.Open();

                #region Exécution de la requête permettant de définir le jeu de caractères à utiliser pour les noms d'entités (bases, tables, vues, procédures/fonctions, et champs)
                using (MySqlCommand Command = new MySqlCommand("SET NAMES `latin1`", m_Connection))
                {
                    Command.ExecuteNonQuery();
                }
                #endregion

                #region Exécution de la requête permettant de se "placer" dans la base de données spécifiée
                using (MySqlCommand Command = new MySqlCommand(string.Format("USE `{0}`", m_DataBaseName.Replace("`", "")), m_Connection))
                {
                    Command.ExecuteNonQuery();
                }
                #endregion

                return true;
            }
            catch (Exception Error)
            {
                #region Affichage dans la console de débuggage du message d'erreur
                Debug.WriteLine(new string('-', 200));
                Debug.WriteLine("Erreur de connexion au serveur MySQL");
                Debug.WriteLine(Error.Message);
                Debug.WriteLine(new string('-', 200));
                #endregion

                return false;
            }
        }

        /// <summary>
        /// Permet de se déconnecter du serveur MySQL
        /// </summary>
        /// <returns>Vrai si la déconnexion a pu se faire, sinon faux</returns>
        public bool Disconnect()
        {
            if ((m_Connection == null) || (m_Connection.State != System.Data.ConnectionState.Open)) return false;
            m_Connection.Dispose();
            return true;
        }
    }
}
