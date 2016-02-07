using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Amelioration : ModelCore
    {
        #region Members

        protected string NomTable = "amelioration";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        private Amelioration(string id) :base(id)
        {

        }

        #endregion

        #region Methods

        public int Faction(Faction faction, int id_amelioration, int id)
        {
            return 1;
        }

        #endregion
    }
}
