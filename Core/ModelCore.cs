using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDbLib;
using System.Reflection;

namespace X_wing.Core
{
    /// <summary>
    /// Classe Parent de chaque model 
    /// </summary>
    public abstract class ModelCore
    {
        #region Members

        protected string m_NomTable;
        protected Dictionary<string, object> m_Attributs;
        private string m_PrimaryKey;
        private MyDB m_BDD;
        protected Dictionary<string, List<ModelCore>> relations;
        protected List<ModelCore> m_HasOne;
        protected List<ModelCore> m_HasMany;
        protected Dictionary<string, List<ModelCore>> m_BelongsToMany;



        #endregion

        #region Ascesseurs
        /// <summary>
        /// Nom de la table
        /// </summary>
        public string NomTable
        {
            get { return this.m_NomTable; }
            set { this.m_NomTable = value; }

        }
        /// <summary>
        /// dictionnaire d'attributs
        /// </summary>
        public Dictionary<string, object> Attribut
        {
            get { return this.m_Attributs; }
            set { this.m_Attributs = value; }

        }
        /// <summary>
        /// nom de la cle primaire de la table
        /// </summary>
        public string PrimaryKey
        {
            get { return this.m_PrimaryKey; }
            set { this.m_PrimaryKey = value; }
        }
        /// <summary>
        /// reference de la DB
        /// </summary>
        public MyDB BDD
        {
            get { return this.m_BDD; }
            set { this.m_BDD = value; }
        }
        /// <summary>
        /// Liste des relations de la table
        /// </summary>
        public Dictionary<string, List<ModelCore>> Relation
        {
            get { return this.relations; }
            set { this.relations = value; }
        }
        /// <summary>
        /// liste des tables avec liaisons 1 a 1
        /// </summary>
        public List<ModelCore> HasOne
        {
            get { return this.m_HasOne; }
            set { this.m_HasOne = value; }
        }
        /// <summary>
        /// liste des tables avec liaisons 1 à n
        /// </summary>
        public List<ModelCore> HasMany
        {
            get { return this.m_HasMany; }
            set { this.m_HasMany = value; }
        }
        /// <summary>
        /// liste des tables avec liaisons n a n
        /// </summary>
        public Dictionary<string, List<ModelCore>> BelongsToMany
        {
            get { return this.m_BelongsToMany; }
            set { this.m_BelongsToMany = value; }
        }

        private int m_id;

        public int id
        {
            get { return m_id; }
            set { m_id = value; }
        }


        #endregion

        #region Constructor
        /// <summary>
        /// constructeur de Model
        /// </summary>
        /// <param name="primaryKey">nom de la cle primaire</param>
        /// <param name="nomTable">nom de la table a instancier</param>
        /// <param name="id">valeur de la cle primaire</param>
        /// <param name="hydrate">true si c'est le premier passage pour l'instance de l'objet</param>
        protected ModelCore(string primaryKey, string nomTable, int id, params string[] arguments)
        {
            m_PrimaryKey = primaryKey;
            m_NomTable = nomTable;
            m_BDD = App.BDD;
            m_Attributs = new Dictionary<string, object>();
            IEnumerable<MyDB.IRecord> enreg = App.recuperation(nomTable, primaryKey, id);
            m_id = id;
            foreach (MyDB.IRecord Element in enreg)
            {
                for (int i = 0; i < Element.FieldCount; i++)
                {
                    m_Attributs.Add(Element.FieldName(i), Element[i]);
                }
            }

            relations = new Dictionary<string, List<ModelCore>>();
            m_HasOne = new List<ModelCore>();
            m_HasMany = new List<ModelCore>();
            m_BelongsToMany = new Dictionary<string, List<ModelCore>>();

            foreach(string methName in arguments)
            {
                Type typeEnfant = this.GetType();
                MethodInfo theMethod = typeEnfant.GetMethod(methName);
                theMethod.Invoke(this, new object[] { });
            }
        }

        #endregion

        #region Methods

        // pour 1 à 1 
        /// <summary>
        /// methode pour ajouter une liaison dans la liste hasOne
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void AddHasOne<T>() where T : ModelCore
        {
            T classeLier = (T)Activator.CreateInstance(typeof(T));
            this.m_HasOne.Add(classeLier as ModelCore);
        }
        //pour 1 à n
        /// <summary>
        /// methode pour ajouter une liaison dans la liste hasMany
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void AddHasMany<T>() where T : ModelCore
        {
            T classeLier = (T)Activator.CreateInstance(typeof(T));
            this.m_HasMany.Add(classeLier as ModelCore);
        }
        // pour n à n 
        /// <summary>
        /// methode pour ajouter une liaison dans la liste belongsToMany
        /// </summary>
        /// <typeparam name="T"></typeparam>

        protected void AddBelongsToMany<T>(string tableRelationnelle, string nomIdModelBase, string nomIdForeign) where T : ModelCore
        {

            BelongsToMany BTM = new Core.BelongsToMany(this, typeof(T), tableRelationnelle, nomIdForeign, nomIdModelBase);
            this.Relation = BTM.results;
        }

        #endregion
    }
}