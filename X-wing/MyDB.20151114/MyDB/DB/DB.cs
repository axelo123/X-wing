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
    public partial class MyDB : IDisposable
    {
        /// <summary>
        /// Libération des ressources avec fermeture de la connexion au serveur MySQL
        /// </summary>
        public void Dispose()
        {
            Disconnect();
        }
    }
}
