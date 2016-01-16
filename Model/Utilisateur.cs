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

        static new string NomTable = "utilisateur";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Utilisateur(int id) : base(primaryKey, NomTable, id)
        {

        }

        #endregion

        #region Methods

        public void Role()
        {
            this.AddBelongsToMany<Role>("utilisateur_role","id_role","id_utilisateur");
        }

        public void Carte_vaisseau_pilote()
        {
            this.AddBelongsToMany<Carte_vaisseau_pilote>("utilisateur-carte_vaisseau_pilote","id_carte_vaisseau_pilote","id_utilisateur");
        }

        public void Figurine()
        {
            this.AddBelongsToMany<Figurine>("utilisateur-figurine","id_figurine","id_utilisateur");
        }

        public void Amelioration()
        {
            this.AddBelongsToMany<Amelioration>("utilisateur-ameliortion","id_amelioration","id_utilisateur");
        }

        #endregion
    }
}
