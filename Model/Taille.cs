using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_wing.Core;

namespace X_wing.Model
{
    class Taille : ModelCore
    {
        static new string NomTable = "taille";
        static string primaryKey = "id";

        public Taille(int id) : base(primaryKey, NomTable, id)
        {

        }

        public void Caracteristique_vaisseau ()
        {
            this.AddHasMany<Caracteristique_vaisseau>();
        }

        public void Amelioration()
        {
            this.AddHasMany<Amelioration>();
        }

    }
}
