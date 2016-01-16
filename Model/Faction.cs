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

        static new string NomTable = "faction";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Faction(int id) : base(primaryKey, NomTable, id)
        {

        }

        #endregion

        #region Methods

        public void Carte_vaisseau_pilote()
        {
            this.AddHasMany<Carte_vaisseau_pilote>();
        }

        public void Amelioration()
        {
            this.AddHasMany<Amelioration>();
        }

        #endregion
    }
}
