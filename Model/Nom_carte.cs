using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Nom_carte : ModelCore
    {
        #region Members

        protected string NomTable = "nom_carte";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        private Nom_carte(string id) :base(id)
        {

        }

        #endregion

        #region Methods

        public int Amelioration(Amelioration amelioration, int id_NC, int id)
        {
            return 1;
        }

        public int Carte_vaisseau_pilote(Carte_vaisseau_pilote CVP, int id_NC, int id)
        {
            return 1;
        }

        #endregion
    }
}
