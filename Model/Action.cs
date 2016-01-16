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

        static new string NomTable = "action";
        static string primaryKey = "id";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Action(int id) :base(primaryKey, NomTable,id)
        {
            
        }

        #endregion

        #region Methods

        public void CaracteristiqueVaisseau()
        {
            this.AddBelongsToMany<Caracteristique_vaisseau>("action-caracteristique_vaisseau", "id_caracteristique_vaisseau","id-action");
        }

        #endregion
    }
}
