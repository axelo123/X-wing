using System;
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

        private ModelCore ModelRelation ;
        private List<ModelCore> ListModelRelation;
        private List<Object> ListAttributPivot;
        private string NomTable;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        public Relation(ModelCore modelRelation, List<ModelCore> listModelRelation)
        {
            this.ModelRelation = modelRelation;
            this.ListModelRelation = listModelRelation;

        }

        public Relation (string nomTable, ModelCore modelRelation, List<ModelCore> listModelRelation)
        {
            this.NomTable = nomTable;
            this.ModelRelation = modelRelation;
            this.ListModelRelation = listModelRelation;
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
