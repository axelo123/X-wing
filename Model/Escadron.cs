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

        static new string NomTable = "escadron";
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

        public void Utilisateur_Role()
        {
            this.AddHasOne<Utilisateur_Role>();
        }

        public void Carte_vaisseau_pilote()
        {
            this.AddBelongsToMany<Carte_vaisseau_pilote>("escadron-carte_vaisseau_pilote","id_carte_vaisseau_pilote","id_escadron");
        }

        #endregion
    }
}
