using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Escadron : ModelCore
    {
        #region Members

        static string NomTable = "escadron";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Escadron(int id) : base(primaryKey, NomTable, id)
        {

        }

        #endregion

        #region Methods

        public int Utilisateur_Role(Utilisateur_Role UR, int id_escadron, int id)
        {
            return 1;
        }

        public int Carte_vaisseau_pilote(Carte_vaisseau_pilote CVP, int id_escadron, int id)
        {
            return 1;
        }

        #endregion
    }
}
