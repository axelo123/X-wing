using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Carte_vaisseau_pilote : ModelCore
    {
        #region Members

        static string NomTable = "carte_vaisseau_pilote";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Carte_vaisseau_pilote(int id) : base(primaryKey, NomTable, id)
        {
            //Figurine figurine = Figurine();
            this.TypeAmelioration();
        }

        #endregion

        #region Methods



        public void TypeAmelioration()
        {
            this.AddBelongsToMany<Utilisateur>("carte_vaisseau_pilote-type_amelioration", "id_carte_vaisseau_pilote", "type_amelioration","id_type_amelioration");

        }

        public void Figurine()
        {
            this.AddHasOne<Figurine>();
        }

        public void Faction()
        {
            this.AddHasOne<Faction>();
        }

        public void Utilisateur()
        {

            this.AddBelongsToMany<Utilisateur>("utilisateur-carte_vaisseau_pilote", "id_carte_vaisseau_pilote","id_utilisateur","id_utilisateur");
        }

        public void Escadron()
        {
            //this.AddBelongsToMany<Escadron>();
        }

        public void Caracteristique_vaisseau()
        {
             this.AddHasOne<Caracteristique_vaisseau>();
        }
        public void Carte()
        {
             this.AddHasOne<Nom_carte>();
        }
        #endregion
    }
}
