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

        static string NomTable = "caracteristique_vaisseau";
        static string id = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Caracteristique_vaisseau(string id) :base(id, NomTable)
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
        public Taille Taille()
        {
            return this.hasOne<Taille>("id_taille", "id&quot");
        }

        #endregion
    }
}
