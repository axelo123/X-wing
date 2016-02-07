using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    public class Caracteristique_vaisseau : ModelCore
    {
        #region Members

        static new string NomTable = "caracteristique_vaisseau";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Caracteristique_vaisseau(int id, params string[] arguments) : base(primaryKey, NomTable, id,arguments)
        {

        }

        #endregion

        #region Methods

        public void CVP()
        {
            this.AddHasOne<Carte_vaisseau_pilote>();
        }

        public void Action()
        {
            this.AddBelongsToMany<Action>("action-caracteristique_vaisseau","id_action", "id_caracteristique_vaisseau");
        }
        public void Taille()
        {
            this.AddHasOne<Taille>("id_taille");
        }

        #endregion
    }
}
