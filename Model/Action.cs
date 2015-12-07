using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Action : ModelCore
    {
        #region Members

        static string NomTable = "action";
        static string id= "id";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Action() : base(id, NomTable)
        {
            
        }

        #endregion

        #region Methods

        public int Utilisateur(Utilisateur utilisateur, int id_role, int id)
        {
            return 1;
        }

        #endregion
    }
}
