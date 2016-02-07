using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Type_amelioration : ModelCore
    {
        #region Members

        protected string NomTable = "type_amelioration";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        private Type_amelioration(string id) :base(id)
        {

        }

        #endregion

        #region Methods

        public int Carte_vaisseau_pilote(Carte_vaisseau_pilote CVP, int id_TA, int id)
        {
            return 1;
        }

        public int Amelioration(Amelioration amelioration, int id_TA, int id)
        {
            return 1;
        }

        #endregion
    }
}
