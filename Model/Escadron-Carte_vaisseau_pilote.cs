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

        protected string NomTable = "escadron-carte_vaisseau_pilote";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        private Escadron_Carte_vaisseau_pilote(string id) :base(id)
        {

        }

        #endregion

        #region Methods

        public int Amelioration(Amelioration amelioration, int id_ECVP, int id)
        {
            return 1;
        }

        #endregion
    }
}
