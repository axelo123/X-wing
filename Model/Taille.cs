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
        static string NomTable = "taille";
        static string id = "id";

        public Taille(string id) :base(id, NomTable)
        {

        }

    }
}
