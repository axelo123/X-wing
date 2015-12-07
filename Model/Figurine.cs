using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Figurine : ModelCore
    {
        #region Members

        static string NomTable = "figurine";
        static string id = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Figurine(string id) :base(id,NomTable)
        {

        }

        #endregion

        #region Methods

        public int Carte_vaisseau_pilote(Carte_vaisseau_pilote CVP, int id_figurine, int id)
        {
            return 1;
        }

        public int Utilisateur(Utilisateur utilisateur, int id_figurine, int id)
        {
            return 1;
        }

        #endregion
    }
}
