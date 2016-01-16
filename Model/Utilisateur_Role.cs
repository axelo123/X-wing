using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Utilisateur_Role : ModelCore
    {
        #region Members

        static new string NomTable = "utilisateur_role";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Utilisateur_Role(int id) : base(primaryKey, NomTable, id)
        {

        }

        #endregion

        #region Methods

        public int Escadron(Escadron escadron, int id_UR, int id)
        {
            return 1;
        }

        #endregion
    }
}
