using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

public static class OutilsCode
{
    #region Fonctions relatives aux débuggages
    /// <summary>
    /// Permet de retourner une chaîne donnant une information sur l'appelant direct d'une méthode
    /// </summary>
    /// <param name="DecallageIndex">1 pour l'appelant de cette méthode, 2 pour l'appelant de l'appelant de cette méthode, ... et ainsi de suite</param>
    /// <returns>Description d'un appelant de méthode</returns>
    public static string ObtenirInformationAppelant(int DecallageIndex = 2)
    {
        StackTrace PileAppel = new StackTrace(true);
        for (int Index = 0; Index < PileAppel.FrameCount; Index++)
        {
            StackFrame UnAppel = PileAppel.GetFrame(Index);
            if (UnAppel.GetMethod().Name.Equals("ObtenirInformationAppelant"))
            {
                UnAppel = PileAppel.GetFrame(Math.Max(0, Math.Min(Index + DecallageIndex, PileAppel.FrameCount - 1)));
                return string.Format("Appelé dans la méthode {0}, à la ligne {1} dans le fichier {2}", UnAppel.GetMethod().Name, UnAppel.GetFileLineNumber(), UnAppel.GetFileName());
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// Permet de retourner une chaîne donnant une information sur tous les appelants d'une méthode
    /// </summary>
    /// <param name="DecallageIndex">1 pour l'appelant de cette méthode et les suivants, 2 pour l'appelant de l'appelant de cette méthode et les suivants, ... et ainsi de suite</param>
    /// <returns>Description de tous les appelants de méthode jusqu'au point d'entrée du programme</returns>
    public static string ObtenirInformationAppelants(int DecallageIndex = 2)
    {
        StackTrace PileAppel = new StackTrace(true);
        for (int Index = 0; Index < PileAppel.FrameCount; Index++)
        {
            StackFrame UnAppel = PileAppel.GetFrame(Index);
            if (UnAppel.GetMethod().Name.Equals("ObtenirInformationAppelants"))
            {
                StringBuilder Resultat = new StringBuilder();
                for (Index = Math.Max(0, Math.Min(Index + DecallageIndex, PileAppel.FrameCount - 1)); Index < PileAppel.FrameCount; Index++)
                {
                    UnAppel = PileAppel.GetFrame(Index);
                    if (UnAppel.GetFileLineNumber() == 0) break;
                    if (Resultat.Length > 0) Resultat.AppendLine();
                    Resultat.AppendFormat("Appelé dans la méthode {0}, à la ligne {1} dans le fichier {2}", UnAppel.GetMethod().Name, UnAppel.GetFileLineNumber(), UnAppel.GetFileName());
                }
                return Resultat.ToString();
            }
        }
        return string.Empty;
    }
    #endregion

    /// <summary>
    /// Vérifie si la méthode en cours d'exécution a déjà été appelée avant l'appel pour lequel on effectue ce test ; si c'est le cas, l'appel est récursif
    /// </summary>
    /// <returns>Vrai si l'appel de la méthode qui fait appel à ce test est récursif, sinon faux</returns>
    public static bool AppelRecursif()
    {
        MethodBase MethodeATester = null;
        StackTrace PileAppel = new StackTrace();
        for (int Index = 0; Index < PileAppel.FrameCount; Index++)
        {
            StackFrame UnAppel = PileAppel.GetFrame(Index);
            if (MethodeATester == null)
            {
                if (UnAppel.GetMethod().Name.Equals("AppelRecursif"))
                {
                    UnAppel = PileAppel.GetFrame(++Index);
                    MethodeATester = UnAppel.GetMethod();
                }
            }
            else if (UnAppel.GetMethod().Equals(MethodeATester))
                return true;
        }
        return false;
    }

    public static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    /// <summary>
    /// Permet de marquer les appels à la portion de code durant laquelle cet objet est "en vie"
    /// </summary>
    public class MarqueurAppel : IDisposable
    {
        #region Membres privés
        private static Dictionary<string, int> s_Marqueurs = new Dictionary<string, int>();
        private string m_Identifiant;
        private int m_Compteur;
        #endregion

        /// <summary>
        /// Compteur d'appel
        /// </summary>
        public int Compteur { get { return m_Compteur; } }

        /// <summary>
        /// Indique si ce marqueur signale un appel réentrant ou non
        /// </summary>
        public bool EstReentrant { get { return (m_Compteur > 1); } }

        /// <summary>
        /// Initialise le marqueur d'appel uniquement pour la méthode réalisant cette construction de marqueur
        /// </summary>
        public MarqueurAppel()
        {
            MethodBase MethodeAppelante = new StackTrace().GetFrame(StackTrace.METHODS_TO_SKIP + 1).GetMethod();
            m_Identifiant = string.Format("{0}|{1}|{2}", MethodeAppelante.Module.FullyQualifiedName, MethodeAppelante.DeclaringType.FullName, MethodeAppelante.Name);
            if (!s_Marqueurs.ContainsKey(m_Identifiant))
            {
                s_Marqueurs.Add(m_Identifiant, 1);
                m_Compteur = 1;
            }
            else
            {
                m_Compteur = s_Marqueurs[m_Identifiant] + 1;
                s_Marqueurs[m_Identifiant] = m_Compteur;
            }
        }

        /// <summary>
        /// Initialise le marqueur d'appel pour la méthode réalisant cette construction de marqueur et pour le contexte additionnel spécifié
        /// </summary>
        /// <param name="Contexte">Contexte additionnel sous forme d'une chaîne de caractères</param>
        public MarqueurAppel(string Contexte)
        {
            MethodBase MethodeAppelante = new StackTrace().GetFrame(StackTrace.METHODS_TO_SKIP + 1).GetMethod();
            m_Identifiant = string.Format("{0}|{1}|{2}|{3}", MethodeAppelante.Module.FullyQualifiedName, MethodeAppelante.DeclaringType.FullName, MethodeAppelante.Name, Contexte);
            if (!s_Marqueurs.ContainsKey(m_Identifiant))
            {
                s_Marqueurs.Add(m_Identifiant, 1);
                m_Compteur = 1;
            }
            else
            {
                m_Compteur = s_Marqueurs[m_Identifiant] + 1;
                s_Marqueurs[m_Identifiant] = m_Compteur;
            }
        }

        /// <summary>
        /// Implémentation de la méthode Dispose de l'interface IDispose : gère la décrémentation du compteur
        /// </summary>
        public void Dispose()
        {
            if (s_Marqueurs.ContainsKey(m_Identifiant))
            {
                int Compteur = s_Marqueurs[m_Identifiant];
                if (Compteur == 1)
                    s_Marqueurs.Remove(m_Identifiant);
                else
                    s_Marqueurs[m_Identifiant] = Compteur - 1;
            }
        }
    }

    #region Méthodes d'extension
    public static int ToInt(this object Valeur, int ValeurParDefaut)
    {
        if (Valeur is int)
            return (int)Valeur;
        else if (Valeur is uint)
            return (int)(uint)Valeur;
        else if (Valeur is short)
            return (short)Valeur;
        else if (Valeur is ushort)
            return (int)(ushort)Valeur;
        else if (Valeur is sbyte)
            return (int)(sbyte)Valeur;
        else if (Valeur is byte)
            return (int)(byte)Valeur;
        else if (Valeur is long)
            return (int)(long)Valeur;
        else if (Valeur is ulong)
            return (int)(ulong)Valeur;
        else if (Valeur is string)
            return Int32.Parse((string)Valeur);
        else
            return ValeurParDefaut;
    }
    #endregion

    #region Méthodes d'extension
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
    public static string ToString(this object Valeur, string ValeurParDefaut = "", TypesSqlDateTime TypeSqlDateTime = TypesSqlDateTime.Date)
    {
        if (Valeur is int)
            return Valeur.ToString();
        else if (Valeur is string)
            return (string)Valeur;
        else if (Valeur is double)
            return Valeur.ToString();
        else if (Valeur is float)
            return Valeur.ToString();
        else if (Valeur is uint)
            return Valeur.ToString();
        else if (Valeur is short)
            return Valeur.ToString();
        else if (Valeur is ushort)
            return Valeur.ToString();
        else if (Valeur is sbyte)
            return Valeur.ToString();
        else if (Valeur is byte)
            return Valeur.ToString();
        else if (Valeur is long)
            return Valeur.ToString();
        else if (Valeur is ulong)
            return Valeur.ToString();
        else if (Valeur is DateTime)
            switch (TypeSqlDateTime)
            {
                case TypesSqlDateTime.DateTime:
                    return Valeur.ToString("dd/MM/yyyy HHhmm:ss");
                case TypesSqlDateTime.Date:
                    return Valeur.ToString("dd/MM/yyyy");
                case TypesSqlDateTime.Time:
                    return Valeur.ToString("HHhmm:ss");
                default:
                    throw new Exception("Type Sql DateTime non géré !");
            }
        else if (Valeur is MySql.Data.Types.MySqlDateTime)
            switch (TypeSqlDateTime)
            {
                case TypesSqlDateTime.DateTime:
                    return Valeur.ToString();
                case TypesSqlDateTime.Date:
                    return Valeur.ToString();
                case TypesSqlDateTime.Time:
                    return Valeur.ToString();
                default:
                    throw new Exception("Type MySql DateTime non géré !");
            }
        else
            return ValeurParDefaut;
    }
    #endregion
}
