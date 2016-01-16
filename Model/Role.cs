using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Role : ModelCore
    {
        #region Members

        static new string NomTable = "role";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Role(int id) : base(primaryKey, NomTable, id)
        {

        }

        #endregion

        #region Methods

        public void Utilisateur()
        {
            this.AddBelongsToMany<Utilisateur>("utilisateur-role","id_utilisateur","id_role");
        }

        #endregion
    }
}
