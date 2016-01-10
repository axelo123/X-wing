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

        private ModelCore ModelLier ;
        private List<ModelCore> ListModelRelation;
        private Dictionary<string, object> DictAttributPivot=new Dictionary<string, object>();
        private List<string> NomAttribut;
        private string nomTable;



        #endregion

        #region Properties

        #endregion

        #region Constructor

        public Relation(string TableRelationnelle, Tuple<string, int> id_local, Tuple<string, int> id_etranger, string nomIdForeign)
        {
            this.nomTable = TableRelationnelle;
            List<MyDB.MyDB.IRecord> enreg = App.recuperationRelation(id_etranger.Item1, TableRelationnelle, id_local.Item1,id_local.Item2, nomIdForeign);
            foreach (MyDB.MyDB.IRecord Element in enreg)
            {
                for (int i = 0; i < Element.FieldCount; i++)
                {
                    DictAttributPivot.Add(Element.FieldName(i), Element[i]);
                    
                }
            }
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
