using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    public class Type_amelioration : ModelCore
    {
        #region Members

        static new string NomTable = "type_amelioration";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Type_amelioration(int id) : base(primaryKey, NomTable, id)
        {

        }

        #endregion

        #region Methods

        public void Carte_vaisseau_pilote()
        {
            this.AddBelongsToMany<Carte_vaisseau_pilote>("carte_vaisseau_pilote-type_amelioration","id_carte_vaisseau_pilote","id_type_amelioration");
        }

        public void Amelioration()
        {
            this.AddHasMany<Amelioration>();
        }
      

        #endregion
    }
}
