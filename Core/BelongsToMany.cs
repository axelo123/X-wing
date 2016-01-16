using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using MyDbLib;

namespace X_wing.Core
{
    public class BelongsToMany
    {
        #region Properties & Accesors
        /// <summary>
        /// Table intermédiaire pour la relation
        /// </summary>
        private string m_table;

        public string table
        {
            get { return m_table; }
            set { m_table = value; }
        }

        /// <summary>
        /// Clé étrangère du modelèe parent
        /// </summary>
        private string m_foreignKey;

        public string foreignKey
        {
            get { return m_foreignKey; }
            set { m_foreignKey = value; }
        }


        /// <summary>
        /// La clé du modele à lier
        /// </summary>
        private string m_otherKey;
        public string otherKey
        {
            get { return m_otherKey; }
            set { m_otherKey = value; }
        }

        private Dictionary<string,List<ModelCore>> m_results = new Dictionary<string, List<ModelCore>>();

        public Dictionary<string,List<ModelCore>> results
        {
            get { return m_results; }
            set { m_results = value; }
        }


        /// <summary>
        /// Le contenu de la table relationnelle. Chaque enregistrement aura une liste de Pivot associés
        /// </summary>
        private Dictionary<string, string> m_pivot;

        public Dictionary<string, string> pivot
        {
            get { return m_pivot; }
            set { m_pivot = value; }
        }

        #endregion

        #region Methods
        public BelongsToMany(ModelCore ModelBase,Type TypeModelALier, string table, string foreignKey, string otherKey, string relationName = "")
        {
            //On définit les propriétés
            m_table = table;
            m_foreignKey = foreignKey;
            m_otherKey = otherKey;

            //recuperation des id des modèles liés au model qui a appellé cette methode
         
            //On liste les résults
            List<ModelCore> resultats = new List<ModelCore>();
            //Pour chaque résultats
            foreach (MyDB.IRecord record in App.recuperation(table, foreignKey, ModelBase.id))
            {
                //On créée l'instance du modèle que l'on a passé en paramètre
                var ModelALier = Activator.CreateInstance(TypeModelALier, new object[] { Int32.Parse(record[otherKey].ToString()) });
                //On l'ajoute à la liste des résultats
                resultats.Add((ModelCore)ModelALier);
            }
            //On met à jour les résultats généraux de la relation
            results.Add(TypeModelALier.Name.ToString(), resultats);
        }
        #endregion
    }
}
