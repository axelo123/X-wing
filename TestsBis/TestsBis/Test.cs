using MyDbLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tests
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private class Oenologue : FormTools.IInitializeFromRecord
        {
            public int Id { get; set; }
            public string Nom { get; set; }
            public double IndiceConfiance { get; set; }
            public short CotationMinimale { get; set; }
            public short CotationMaximale { get; set; }

            public Oenologue()
            {
            }

            public bool Initialize(MyDB.IRecord Record)
            {
                Id = (int)Record["id"];
                Nom = Record["nom"].ToString();
                IndiceConfiance = (double)Record["indice_confiance"];
                CotationMinimale = (short)Record["cotation_minimale"];
                CotationMaximale = (short)Record["cotation_maximale"];
                return true;
            }

            /*
            public Oenologue(MyDB.IRecord Enregistrement)
            {
               Id = (int)Enregistrement["id"];
               Nom = Enregistrement["nom"].ToString();
               IndiceConfiance = (double)Enregistrement["indice_confiance"];
               CotationMinimale = (short)Enregistrement["cotation_minimale"];
               CotationMaximale = (short)Enregistrement["cotation_maximale"];
            }
            */
        }

        public Test(MyDB BD)
        {
            InitializeComponent();
            /*
            OenologueV1[] Listing = OenologueV1.Lister(BD, OenologueV1.Listing.Tous).ToArray();

            dgvList.DataSource = Listing;
            */

            /*
            dgvList.Fill(
                BD.Read("SELECT * FROM oenologue").Select<MyDB.IRecord, Oenologue>(e => new Oenologue(e)),
                new string[] { "Identifiant", "Nom", "Indice de confiance", "Cotation minimale", "Cotation maximale" });
            */

            dgvList.Fill<Oenologue>(
                BD.Read("SELECT * FROM oenologue"),
                "Identifiant", "Nom", "Indice de confiance", "Cotation minimale", "Cotation maximale");
        }
    }
}
