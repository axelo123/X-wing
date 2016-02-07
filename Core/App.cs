using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDB;

namespace X_wing.Core
{
    class App
    {
        #region Members

        protected static MyDB.MyDB BDD;

        #endregion

        #region Properties



        #endregion

        #region Constructor



        #endregion

        #region Methods

        public static MyDB.MyDB ConnecterBD()
        {
            BDD = new MyDB.MyDB("stock", "b5NmmVrLM8VGL4dx", "stock", "localhost");
            return BDD; 
        }

        public static List<MyDB.MyDB.IRecord> recuperation(string nomTable)
        {

            List<MyDB.MyDB.IRecord> enreg = new List<MyDB.MyDB.IRecord>();
            string query = string.Format("SELECT * FROM {0} ", nomTable);

            foreach (MyDB.MyDB.IRecord elem in BDD.Read(query))
            {
                enreg.Add(elem);
            }
            return enreg;
        }
        // cvp loc, fig foreign, id_fig idfor, 5 id local
        public static List<MyDB.MyDB.IRecord> Liaison_1a1(string nomTableLocal, string nomTableForeign, string idKeyForeign, int idLocal)
        {
            List<MyDB.MyDB.IRecord> enreg = new List<MyDB.MyDB.IRecord>();
            string query = string.Format("SELECT * FROM {1} WHERE {1}.id IN (SELECT {0}.{2} FROM {0} WHERE {0}.id = {3})", nomTableLocal, nomTableForeign, idKeyForeign, idLocal);
            foreach (MyDB.MyDB.IRecord elem in BDD.Read(query))
            {
                enreg.Add(elem);
            }
            return enreg;
        } 
        public static List<MyDB.MyDB.IRecord> Liaison_1aN( string nomTableForeign, string idKeyForeign, int idLocal)
        {
            List<MyDB.MyDB.IRecord> enreg = new List<MyDB.MyDB.IRecord>();
            string query = string.Format("SELECT * FROM {0} WHERE {0}.{1} = {2}", nomTableForeign, idKeyForeign, idLocal);
            foreach (MyDB.MyDB.IRecord elem in BDD.Read(query))
            {
                enreg.Add(elem);
            }
            return enreg;
        }
        public static List<MyDB.MyDB.IRecord> Liaison_NaN(string nomTableRelation, int idLocal)
        {
            List<MyDB.MyDB.IRecord> enreg = new List<MyDB.MyDB.IRecord>();
            string query = string.Format("SELECT * FROM {1} WHERE {1}.{2} = {3}", nomTableRelation, idLocal);
            foreach (MyDB.MyDB.IRecord elem in BDD.Read(query))
            {
                enreg.Add(elem);
            }
            return enreg;
        }

        #endregion
    }
}
