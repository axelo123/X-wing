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
        }

        #endregion

        #region Methods



        //public Type_amelioration TypeAmelioration()
        //{
        //    return new Type_amelioration("type");
        //}

        //public Figurine Figurine()
        //{
        //    return this.hasOne<Figurine>("id_figurine", "id&quot");
        //}

        //public Faction Faction()
        //{
        //    return this.hasOne<Faction>("id_faction", "id&quot");
        //}

        public int Utilisateur(Utilisateur utilisateur, int id_CVP, int id)
        {
            return 1;
        }

        public int Escadron(Escadron escadron, int id_CVP, int id)
        {
            return 1;
        }

        //public Caracteristique_vaisseau Caracteristique_vaisseau()
        //{
        //    return this.hasOne<Caracteristique_vaisseau>("id_caracteristique_vaisseau", "id&quot");
        //}
        //public Nom_carte Carte()
        //{
        //    return this.hasOne<Nom_carte>("id_nom_carte", "id&quot");
        //}
        #endregion
    }
}
