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
        #region Membre statique privé
        private static readonly CultureInfo s_EnglishCulture = CultureInfo.GetCultureInfo("EN-US");
        #endregion

        /// <summary>
        /// Classe permettant d'encapsuler du code SQL pour pouvoir ensuite être injecté tel quel au sein d'une requête SQL
        /// </summary>
        public class SqlCode
        {
            #region Membre privé
            private string m_Code;
            #endregion

            /// <summary>
            /// Propriété (en lecture/écriture) définissant le code SQL
            /// </summary>
            public string Code
            {
                get
                {
                    return m_Code;
                }
                set
                {
                    m_Code = string.IsNullOrWhiteSpace(value) ? "" : value;
                }
            }

            /// <summary>
            /// Constructeur par défaut
            /// </summary>
            public SqlCode()
            {
                m_Code = string.Empty;
            }

            /// <summary>
            /// Constructeur spécifique permettant de définir le code SQL
            /// </summary>
            /// <param name="Format">Code SQL (pouvant contenir des parties variables)</param>
            /// <param name="Arguments">Arguments fournissant les valeurs des parties variables</param>
            public SqlCode(string Format, params object[] Arguments)
                : this()
            {
                if (Arguments.Length == 0)
                    this.Code = Format;
                else
                    this.Code = FormatQuery(Format, Arguments);
            }

            /// <summary>
            /// Retourne le texte du code SQL
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return m_Code;
            }
        }

        /// <summary>
        /// Permet de formater une requête SQL contenant des parties variables
        /// </summary>
        /// <param name="Query">Format de requête SQL (cf. chaîne de format de string.Format)</param>
        /// <param name="Arguments">Valeurs des parties variables de la requête</param>
        /// <returns>Chaîne résultant de l'injection des valeurs au sein du code de la requête SQL</returns>
        public static string FormatQuery(string Query, params object[] Arguments)
        {
            try
            {
                return string.Format(Query, Arguments.Select<object, string>(Valeur => FormatValue(Valeur)).ToArray());
            }
            catch (Exception Error)
            {
                #region Affichage dans la console de débuggage du message d'erreur
                Debug.WriteLine(new string('-', 200));
                Debug.WriteLine(string.Format("Erreur d'exécution du formatage d'une requête\n{0}", Query));
                Debug.WriteLine(Error.Message);
                Debug.WriteLine(new string('-', 200));
                #endregion

                return "";
            }
        }

        /// <summary>
        /// Permet de formater une valeur (de type quelconque) en chaîne en respectant le formatage requis par MySQL, avec prtection contre l'injection (par inadvertance) de code SQL
        /// </summary>
        /// <param name="Value">Valeur à formater</param>
        /// <returns>Texte représentatif de la valeur à placer dans une requête SQL</returns>
        /// <exception cref="ArgumentException">Déclenchée si la valeur est d'un type non reconnu</exception>
        public static string FormatValue(object Value)
        {
            if (Value == null)
            {
                return "NULL";
            }
            if (Value is SqlCode)
            {
                return Value.ToString();
            }
            if ((Value is string) || (Value is char) || (Value is StringBuilder))
            {
                return string.Format("\"{0}\"", Value.ToString().Replace("\"", "\\\""));
            }
            if ((Value is sbyte) || (Value is byte) || (Value is short) || (Value is ushort) || (Value is int) || (Value is uint) || (Value is long) || (Value is ulong))
            {
                return Value.ToString();
            }
            if (Value is float)
            {
                return ((float)Value).ToString(s_EnglishCulture);
            }
            if (Value is double)
            {
                return ((double)Value).ToString(s_EnglishCulture);
            }
            if (Value is decimal)
            {
                return ((decimal)Value).ToString(s_EnglishCulture);
            }
            if (Value is MySqlDecimal)
            {
                MySqlDecimal d = (MySqlDecimal)Value;
                if (d.IsNull) return "NULL";
                return d.ToString().Replace(',', '.');
            }
            if (Value is DateTime)
            {
                return string.Format("\"{0}\"", ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (Value is MySqlDateTime)
            {
                MySqlDateTime dt = (MySqlDateTime)Value;
                if (dt.IsNull) return "NULL";
                return string.Format("\"{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}\"", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            }
            if (Value is bool)
            {
                return ((bool)Value) ? "TRUE" : "FALSE";
            }
            throw new ArgumentException(string.Format("Type non pris en charge pour MySQL : {0}", Value.GetType().FullName));
        }
    }
}
