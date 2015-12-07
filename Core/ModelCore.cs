using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_wing.Core
{
    public abstract class ModelCore
    {
        #region Members

        protected string m_NomTable;
        protected List<MyDB.MyDB.IRecord> m_Attributs;
        private string m_PrimaryKey;
        private MyDB.MyDB m_BDD;
        protected List<Relation> relations;
        protected List<ModelCore> m_M_HasOne;
        protected List<ModelCore> m_M_HasMany;
        protected List<ModelCore> m_M_BelongsToMany;
        protected bool m_Connected;



        #endregion

        #region Ascesseurs

        public string NomTable
        {
            get { return this.m_NomTable; }
            set { this.m_NomTable = value; }

        }

        public List<MyDB.MyDB.IRecord> Attribut
        {
            get { return this.m_Attributs; }
            set { this.m_Attributs = value; }

        }

        public string PrimaryKey
        {
            get { return this.m_PrimaryKey; }
            set { this.m_PrimaryKey = value; }
        }

        public MyDB.MyDB BDD
        {
            get { return this.m_BDD; }
            set { this.m_BDD = value; }
        }

        public List<Relation> M_Relation
        {
            get { return this.relations; }
            set { this.relations = value; }
        }

        public List<ModelCore> M_HasOne
        {
            get { return this.m_M_HasOne; }
            set { this.m_M_HasOne = value; }
        }

        public List<ModelCore> M_HasMany
        {
            get { return this.m_M_HasMany; }
            set { this.m_M_HasMany = value; }
        }

        public List<ModelCore> M_BelongsToMany
        {
            get { return this.m_M_BelongsToMany; }
            set { this.m_M_BelongsToMany = value; }
        }

        public bool Connected
        {
            get { return this.m_Connected; }
            set { this.m_Connected = value; }
        }

        #endregion

        #region Constructor

        protected ModelCore(string id, string nomTable, bool hydrate = true)
        {
            m_PrimaryKey = id;
            m_NomTable = nomTable;
            m_Attributs = new List<MyDB.MyDB.IRecord>();
            m_BDD = App.ConnecterBD();
            m_Connected = m_BDD.Connect();
            relations = new List<Relation>();
            m_M_HasOne = new List<ModelCore>();
            m_M_HasMany = new List<ModelCore>();
            m_M_BelongsToMany = new List<ModelCore>();

        }

        #endregion

        #region Methods

        // pour 1 à 1 
        protected T hasOne<T>(string idLocal, string idForeign)
        {
            T classeLiée = (T)Activator.CreateInstance(typeof(T));
            return (T)Convert.ChangeType(classeLiée, typeof(T));
        }
        //pour 1 à n
        protected T hasMany<T>(string idLocal, string idForeign)
        {
            T classeLiée = (T)Activator.CreateInstance(typeof(T));
            return (T)Convert.ChangeType(classeLiée, typeof(T));
        }
        // pour n à n 
        // carte_vaisseau_pilote-type_amelioration rel, Type for, cvp loc , 2 idloc
        public  BelongsToMany(string nomTableRelation, string nomTableForeign, string idForeign, string name_idLocal, int idLocal)
        {
            
        }

        // pour table relation
        public List<Relation> Relations(string nomTableRelation)
        {
            return this.relations;
        }


        #endregion
    }
}
