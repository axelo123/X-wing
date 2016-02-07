using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Caracteristique_vaisseau : ModelCore
    {
        #region Members

        protected string NomTable = "caracteristique_vaisseau";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        private Caracteristique_vaisseau(string id) :base(id)
        {

        }

        #endregion

        #region Methods

        public int CVP(Carte_vaisseau_pilote cvp, int id_CV, int id)
        {
            return 1;
        }

        public int Action(Action action, int id_CV, int id)
        {
            return 1;
        }

        #endregion
    }
}
