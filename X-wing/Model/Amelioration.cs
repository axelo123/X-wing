using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    public class Amelioration : ModelCore
    {
        #region Members

        static new string NomTable = "amelioration";
        static string primaryKey = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor
        public Amelioration() : base(primaryKey, NomTable, 1)
        {

        }

        public Amelioration(int id) :base(primaryKey, NomTable, id)
        {
            this.Utilisateur();
        }

        #endregion

        #region Methods

        public void Faction(Faction faction, int id_amelioration, int id)
        {
            this.AddHasOne<Faction>("id_faction");
        }
        
        public void NomCarte()
        {
            this.AddHasOne<Nom_carte>("id_nom_carte");                       
        }
        public void TypeAmelioration()
        {
            this.AddHasOne<Type_amelioration>("id_type_amelioration");
        }
        public void EscadronCarteVaisseauPilote()
        {
            this.AddBelongsToMany<Escadron_Carte_vaisseau_pilote>("escadron-carte_vaisseau_pilote-amelioration", "id_escadron_carte_vaisseau_pilote","id_amelioration");
        }
        public void Utilisateur()
        {
            this.AddBelongsToMany<Utilisateur>("utilisateur-amelioration","id_utilisateur","id_amelioration");
        }
        public void TailleMin()
        {
            this.AddHasOne<Taille>("id_taille");
        }
        public void TailleMax()
        {
            this.AddHasOne<Taille>("id_taille");
        }

        #endregion
    }
}
