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

        static new string NomTable = "type_amelioration";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Type_amelioration(int id) : base(primaryKey, NomTable, id)
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
