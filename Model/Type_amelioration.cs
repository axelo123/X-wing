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

        static string NomTable = "type_amelioration";
        static string id = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Type_amelioration(string id) :base(id,NomTable)
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
