using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    public class Figurine : ModelCore
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

        public void Carte_vaisseau_pilote()
        {
            this.AddHasMany<Carte_vaisseau_pilote>();
        }

        public void Utilisateur()
        {
            this.AddBelongsToMany<Utilisateur>("utilisateur-figurine","id_utilisateur","id_figurine");
        }

        #endregion
    }
}
