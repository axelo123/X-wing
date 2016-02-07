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

        protected string NomTable = "carte_vaisseau_pilote";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        private Carte_vaisseau_pilote(string id) :base(id)
        {

        }

        #endregion

        #region Methods

        public int NomCarte(Nom_carte nomCarte, int id_CVP, int id)
        { 
            return 1;
        }

        public int TypeAmelioration(Type_amelioration TA, int id_CVP, int id)
        {
            return 1;
        }

        public int Figurine(Figurine figurine, int id_CVP, int id)
        {
            return 1;
        }

        public int Faction(Faction faction, int id_CVP, int id)
        {
            return 1;
        }

        public int Utilisateur(Utilisateur utilisateur, int id_CVP, int id)
        {
            return 1;
        }

        public int Escadron(Escadron escadron, int id_CVP, int id)
        {
            return 1;
        }

        public int Caracteristique_vaisseau(Caracteristique_vaisseau CV, int id_CVP, int id)
        {
            return 1;
        }

        #endregion
    }
}
