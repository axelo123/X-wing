using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;


namespace X_wing.Core
{
    public class Config
    {
        #region private Variables

        private string m_username;
        public string username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        private string m_password;
        public string password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        private string m_bdd;
        public string bdd
        {
            get { return m_bdd; }
            set { m_bdd = value; }
        }

        private string m_server;

        public string server
        {
            get { return m_server; }
            set { m_server = value; }
        }

        public string acc_type = "DEMO";
        public string acc_number = "ABC123";
        private string path = @"../../ENV.ini";
        #endregion

        public Config()
        {
            this.chargerFicher();
        }

        private void chargerFicher()
        {
            //Ouverture et Lecture du fichier
            if (File.Exists(this.path))
            {
                string[] lines = File.ReadAllLines(this.path);
                foreach (string line in lines)
                {
                    string[] varValue = line.Split('\t');
                    switch (varValue[0])
                    {
                        case "username": this.username = varValue[1]; break;
                        case "password": this.password = varValue[1]; break;
                        case "bdd": this.bdd = varValue[1]; break;
                        case "server": this.server = varValue[1]; break;
                    }
                    //Debug.WriteLine(line);
                }
            }
        }
        public bool isOk()
        {
            if (!File.Exists(this.path))
                return false;
            if (String.IsNullOrEmpty(username))
                return false;
            if (String.IsNullOrEmpty(password))
                return false;
            if (String.IsNullOrEmpty(bdd))
                return false;
            if (String.IsNullOrEmpty(server))
                return false;

            return true;
        }
    }
}

