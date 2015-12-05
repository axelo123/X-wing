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

        #endregion
    }
}
