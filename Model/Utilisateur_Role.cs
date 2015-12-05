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

        protected string NomTable = "utilisateur_role";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        private Utilisateur_Role(string id) :base(id)
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
