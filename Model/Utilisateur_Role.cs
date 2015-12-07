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

        static string NomTable = "utilisateur_role";
        static string id = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Utilisateur_Role(string id) :base(id,NomTable)
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
