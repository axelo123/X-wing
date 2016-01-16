using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Escadron_Carte_vaisseau_pilote : ModelCore
    {
        #region Members

        static new string NomTable = "escadron-carte_vaisseau_pilote";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Escadron_Carte_vaisseau_pilote(int id) : base(primaryKey, NomTable, id)
        {

        }

        #endregion

        #region Methods

        public void Amelioration()
        {
            this.AddBelongsToMany<Amelioration>("escadron-carte_vaisseau_pilote-amelioration","id_amelioration","id_carte_vaisseau_pilote");
        }

        #endregion
    }
}
