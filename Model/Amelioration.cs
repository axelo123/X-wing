﻿using System;
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
        static string id = "id";
        #endregion

        #region Properties



        #endregion

        #region Constructor

        public Amelioration(string id) :base(id,NomTable)
        {

        }

        #endregion

        #region Methods

        public int Faction(Faction faction, int id_amelioration, int id)
        {
            return 1;
        }

        #endregion
    }
}
