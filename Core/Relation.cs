﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDB;

namespace X_wing.Core
{
    public class Relation
    {
        #region Members

        protected string NomTable = null;
        private List<ModelCore> Models;
        private List<MyDB.MyDB.IRecord> Attributs;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        public Relation()
        {



        }

        #endregion

        #region Methods

        public void WithPivot(string argument)
        {



        }

        public void WithPivot(List<string> arguments)
        {



        }

        #endregion
    }
}
