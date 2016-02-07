using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Faction : ModelCore
    {
        #region Members

        protected string NomTable = "faction";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        private Faction(string id) :base(id)
        {

        }

        #endregion

        #region Methods

        public int Carte_vaisseau_pilote(Carte_vaisseau_pilote CVP, int id_faction, int id)
        {
            return 1;
        }

        public int Amelioration(Amelioration amelioration, int id_escadron, int id)
        {
            return 1;
        }

        #endregion
    }
}
