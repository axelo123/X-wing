using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    public class Carte_vaisseau_pilote : ModelCore
    {
        #region Members

        static new string NomTable = "carte_vaisseau_pilote";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Carte_vaisseau_pilote(int id, params string[] arguments) : base(primaryKey, NomTable, id, arguments)
        {
  
        }

        #endregion

        #region Methods



        public void TypeAmelioration()
        {
            this.AddBelongsToMany<Type_amelioration>("carte_vaisseau_pilote-type_amelioration", "id_type_amelioration", "id_carte_vaisseau_pilote" );
        }

        public void Figurine()
        {
            this.AddHasOne<Figurine>("id_figurine");
        }

        public void Faction()
        {
            this.AddHasOne<Faction>("id_faction");
        }

        public void Utilisateur()
        {

            this.AddBelongsToMany<Utilisateur>("utilisateur-carte_vaisseau_pilote","id_utilisateur","id_carte_vaisseau_pilote");
        }

        public void Escadron()
        {
            //this.AddBelongsToMany<Escadron>();
        }

        public void Caracteristique_vaisseau()
        {
             this.AddHasMany<Caracteristique_vaisseau>("id_caracteristique_vaisseau");
        }
        public void NomCarte()
        {
             this.AddHasOne<Nom_carte>("id_nom_carte");
        }
        #endregion
    }
}
