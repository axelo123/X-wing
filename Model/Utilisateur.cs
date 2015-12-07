using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Utilisateur : ModelCore
    {
        #region Members

        static string NomTable = "utilisateur";
        static string id = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Utilisateur(string id) :base(id,NomTable)
        {

        }

        #endregion

        #region Methods

        public int Role(Role role, int id_utilisateur, int id)
        {
            return 1;
        }

        public int Carte_vaisseau_pilote(Carte_vaisseau_pilote CVP, int id_utilisateur, int id)
        {
            return 1;
        }

        public int Figurine(Figurine figurine, int id_utilisateur, int id)
        {
            return 1;
        }

        public int Amelioration(Amelioration amelioration, int id_utilisateur, int id)
        {
            return 1;
        }

        #endregion
    }
}
