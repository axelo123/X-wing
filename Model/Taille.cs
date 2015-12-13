﻿using System;
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
        static string primaryKey = "id";

        public Taille(int id) : base(primaryKey, NomTable, id)
        {

        }

    }
}
