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

        static new string NomTable = "figurine";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Figurine(int id) : base(primaryKey, NomTable, id)
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
