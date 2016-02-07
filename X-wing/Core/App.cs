using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDbLib;
using System.Collections;


namespace X_wing.Core
{
    /// <summary>
    /// classe outils 
    /// </summary>
    public static class App
    {
        private static Config s_config;
        public static Config config { get { return s_config; } }

        #region Members
        private static MyDB  s_BDD;
        public static MyDB BDD { get { return s_BDD; } }

        #endregion

        #region Properties



        #endregion

        #region Constructor



        #endregion

        #region Methods
        /// <summary>
        /// appel de la methode Connect du projet MyDB
        /// </summary>
        public static void ConnecterBD()
        {
            s_config = new Config();
            s_BDD = new MyDB(config.username, config.password, config.bdd, config.server);
            s_BDD.Connect();
        }
        /// <summary>
        /// récuperation d'un enregistrement
        /// </summary>
        /// <param name="nomTable">Nom de la table</param>
        /// <param name="primaryKey">Nom de la cle primaire</param>
        /// <param name="id">Nom de l'id a recuperer</param>
        /// <returns>Liste d'enregistrement</returns>
        public static IEnumerable<MyDB.IRecord> recuperation(string nomTable,string primaryKey, int id)
        {
            string query = string.Format("SELECT * FROM `{0}` WHERE `{1}` = {2}", nomTable, primaryKey, id);   
            return BDD.Read(query);
        }
        public static List<MyDB.IRecord> recuperationRelation(string nomTableForeign, string nomTableRelation,string nomIdLocal, int idLocal, string nomIdForeign)
        {
            List<MyDB.IRecord> enreg = new List<MyDB.IRecord>();
            
            //enreg = recuperation(nomTableRelation, nomIdLocal, idLocal);
            
            string query = string.Format("SELECT {0} FROM `{1}` WHERE `{2}` = {3}", nomIdForeign, nomTableForeign, nomIdLocal, idLocal);
            foreach (MyDB.IRecord elem in BDD.Read(query))
            {
                enreg.Add(elem);
            }
            return enreg;
        }
            
        // cvp loc, fig foreign, id_fig idfor, 5 id local
        public static List<MyDB.IRecord> Liaison_1a1(string nomTableLocal, string nomTableForeign, string idKeyForeign, int idLocal)
        {
            List<MyDB.IRecord> enreg = new List<MyDB.IRecord>();
            string query = string.Format("SELECT * FROM {1} WHERE {1}.id IN (SELECT {0}.{2} FROM {0} WHERE {0}.id = {3})", nomTableLocal, nomTableForeign, idKeyForeign, idLocal);
            foreach (MyDB.IRecord elem in BDD.Read(query))
            {
                enreg.Add(elem);
            }
            return enreg;
        } 
        //cvp foreign, id_figurine idForeign, 2 id_local
        public static List<MyDB.IRecord> Liaison_1aN( string nomTableForeign, string idKeyForeign, int idLocal)
        {
            List<MyDB.IRecord> enreg = new List<MyDB.IRecord>();
            string query = string.Format("SELECT * FROM {0} WHERE {0}.{1} = {2}", nomTableForeign, idKeyForeign, idLocal);
            foreach (MyDB.IRecord elem in BDD.Read(query))
            {
                enreg.Add(elem);
            }
            return enreg;
        }
        // carte_vaisseau_pilote-type_amelioration rel, Type for, cvp loc , 2 idloc
        
        public static List<MyDB.IRecord> Liaison_NaN(string nomTableRelation,string nomTableForeign,string nomTableLocal,string idForeign,string name_idLocal, int idLocal)
        {
            List<MyDB.IRecord> enreg = new List<MyDB.IRecord>();
            string query = string.Format("SELECT * FROM `{1}` WHERE {1}.id IN (SELECT `{0}`.{2} FROM `{0}` WHERE `{0}`.{3} = {4})", nomTableRelation, nomTableForeign, nomTableLocal, idForeign, name_idLocal, idLocal);
            foreach (MyDB.IRecord elem in BDD.Read(query))
            {
                enreg.Add(elem);
            }
            return enreg;
        }
        

        #endregion
    }
}
