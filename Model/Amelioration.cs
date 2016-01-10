using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Amelioration : ModelCore
    {
        #region Members

        static string NomTable = "amelioration";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Amelioration(int id) :base(primaryKey, NomTable, id)
        {
            this.Utilisateur();
        }

        #endregion

        #region Methods

        public void Faction(Faction faction, int id_amelioration, int id)
        {
            //this.AddBelongsToMany<Faction>();
        }
        
        public void NomCarte()
        {
            this.AddHasOne<Nom_carte>();                       
        }
        public void TypeAmelioration()
        {
            this.AddHasOne<Type_amelioration>();
        }
        public void EscadronCarteVaisseauPilote()
        {
           // this.AddBelongsToMany<Escadron_Carte_vaisseau_pilote>();
        }
        public void Utilisateur()
        {
            this.AddBelongsToMany<Utilisateur>("utilisateur-amelioration", "id_amelioration", "id_utilisateur", "id_utilisateur");
        }
        public void Taille()
        {
            this.AddHasOne<Taille>();
        }

        #endregion
    }
}
