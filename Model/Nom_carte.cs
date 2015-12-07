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

        static string NomTable = "nom_carte";
        static string id = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Nom_carte(string id) :base(id,NomTable)
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
