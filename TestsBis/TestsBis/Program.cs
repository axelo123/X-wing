using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyDbLib;

namespace Tests
{
    static class Program
    {
        static System.Globalization.CultureInfo CultureAnglaise = System.Globalization.CultureInfo.GetCultureInfo("EN-US");

        /// <summary>
        /// Point d'entrée de l'application
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (MyDB BD = new MyDB("u_oenologue", "mZTbURtCucb92Grf", "oenologie"))
            {
                BD.OnConnectionEstablished += delegate ()
                {
                    Console.WriteLine("\nCONNEXION ETABLIE\n");
                };
                BD.OnConnectionLost += delegate ()
                {
                    Console.WriteLine("\nPERTE DE CONNEXION\n");
                };
                BD.OnConnectionError += delegate (string Message)
                {
                    Console.WriteLine("\nERREUR DE CONNEXION : {0}\n", Message);
                };
                BD.OnQueryError += delegate (MyDB.QueryMethods Methode, string Requete, string Message)
                {
                    Console.WriteLine("\nERREUR DE REQUETE : {0}\nRequête SQL : {1}\nMéthode appelée : {2}\n", Message, Requete, Methode);
                };

                /*
                BD.BeforeQuery += delegate (MyDB.QueryMethods Methode, string Requete, ref bool Annulation)
                {
                    Console.WriteLine("\nAVANT {0} : {1}\nAccepter (O/N) ?", Methode, Requete);
                    while (true)
                    {
                        char Reponse = char.ToUpper(Console.ReadKey(true).KeyChar);
                        if (Reponse == 'N')
                        {
                            Console.WriteLine("Non\n");
                            Annulation = true;
                            break;
                        }
                        else if (Reponse == 'O')
                        {
                            Console.WriteLine("Oui\n");
                            break;
                        }
                    }
                };
                BD.AfterReadingQuery += delegate (MyDB.QueryMethods Methode, string Requete, MyDB.IReadResult Resutat)
                {
                    Console.WriteLine("\nAPRES {0} : {1}\nRésultat : {2}\n", Methode, Requete, Resutat);
                };
                BD.AfterUpdatingQuery += delegate (MyDB.QueryMethods Methode, string Requete, MyDB.IUpdateResult Resutat)
                {
                    Console.WriteLine("\nAPRES {0} : {1}\nRésultat : {2}\n", Methode, Requete, Resutat);
                };
                */

                BD.Connect();

                using (Form Formulaire = new Test(BD)) Application.Run(Formulaire);

                Console.BufferHeight = 5000;
                Test1(BD);
                Test2(BD);
                Test3(BD);
                FinDesTests();
            }
        }

        /// <summary>
        /// Première série de tests
        /// </summary>
        /// <remarks>Code non optimisé afin de montrer la possibilité d'imbrications des consultations</remarks>
        static void Test1(MyDB BD)
        {
            Console.Clear();
            Console.WriteLine("'Test1' démarre ...");
            if (BD.IsConnected)
            {
                Console.WriteLine("\nLa connexion au serveur MySql est établie.");
                foreach (MyDB.IRecord Enregistrement in BD.Read("SELECT * FROM oenologue"))
                {
                    Console.WriteLine("\nOenologue {0} :\n* id : {1}\n* nom : {2}\n* indice_confiance : {3}\n* cotation_minimale : {4}\n* cotation_maximale : {5}",
                        Enregistrement.Result.RecordCount, Enregistrement["id"], Enregistrement["nom"], Enregistrement["indice_confiance"], Enregistrement["cotation_minimale"], Enregistrement["cotation_maximale"]);
                    foreach (MyDB.IRecord Enregistrement2 in BD.Read("SELECT * FROM avis WHERE ref_oenologue = {0}", Enregistrement["id"]).Take(5))
                    {
                        if (Enregistrement2.Result.RecordCount == 1) Console.WriteLine("* premiers avis :");
                        Console.WriteLine("  - cote n° {0} :\n    - valeur : {1}\n    - attribuée à : {2}",
                            Enregistrement2.Result.RecordCount, Enregistrement2["cote"], BD.GetValue<string>("SELECT nom FROM vin WHERE id = {0}", Enregistrement2["ref_vin"]));
                    }
                }
                foreach (MyDB.IRecord Enregistrement in BD.Read("SELECT * FROM vin"))
                {
                    Console.WriteLine("\nVin {0} :\n* id : {1}\n* nom : {2}",
                        Enregistrement.Result.RecordCount, Enregistrement["id"], Enregistrement["nom"], Enregistrement["indice_confiance"], Enregistrement["cotation_minimale"], Enregistrement["cotation_maximale"]);
                }
            }
            Console.WriteLine("\nNombre d'objets de consultation (utilisés/existants) : {0} / {1}", MyDB.UsedReadersCount, MyDB.ReadersCount);
            Console.WriteLine("\n'Test1' est terminé.");

            Console.WriteLine("\nAppuyez sur ESCAPE pour continuer");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
        }

        /// <summary>
        /// Seconde série de tests
        /// </summary>
        /// <remarks>Utilisation du modèle d'entité Oenologue - version 1</remarks>
        static void Test2(MyDB BD)
        {
            Console.Clear();
            Console.WriteLine("'Test2' démarre ...");
            if (BD.IsConnected)
            {
                Console.WriteLine("\nLa connexion au serveur MySql est établie.");
                List<OenologueV1> Oenologues = new List<OenologueV1>(OenologueV1.Lister(BD, OenologueV1.Listing.Tous));
                foreach (OenologueV1 Oenologue in Oenologues)
                {
                    Console.WriteLine("\n{0}", Oenologue);
                }
            }
            Console.WriteLine("\n");
            Test2a(BD);
            Console.WriteLine("\nNombre d'objets de consultation (utilisés/existants) : {0} / {1}", MyDB.UsedReadersCount, MyDB.ReadersCount);
            Console.WriteLine("\n'Test2' est terminé.");

            Console.WriteLine("\nAppuyez sur ESCAPE pour continuer");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
        }

        static void Test2a(MyDB BD)
        {
            OenologueV1 NouvelOenologue = new OenologueV1(BD);
            do
            {
                Console.WriteLine("Nom :");
                while (true)
                {
                    string Valeur = Console.ReadLine();
                    object Resultat = NouvelOenologue.DefinirNom(Valeur);
                    if (Resultat is bool) break;
                    Console.WriteLine("{0}", Resultat);
                }

                Console.WriteLine("Indice de confiance :");
                while (true)
                {
                    double Valeur;
                    while (!double.TryParse(Console.ReadLine().Replace(',', '.'), System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign, CultureAnglaise, out Valeur)) ;
                    object Resultat = NouvelOenologue.DefinirIndiceConfiance(Valeur);
                    if (Resultat is bool) break;
                    Console.WriteLine("{0}", Resultat);
                }

                Console.WriteLine("Cotation minimale :");
                while (true)
                {
                    short Valeur;
                    while (!short.TryParse(Console.ReadLine(), out Valeur)) ;
                    object Resultat = NouvelOenologue.DefinirCotationMinimale(Valeur);
                    if (Resultat is bool) break;
                    Console.WriteLine("{0}", Resultat);
                }

                Console.WriteLine("Cotation maximale :");
                while (true)
                {
                    short Valeur;
                    while (!short.TryParse(Console.ReadLine(), out Valeur)) ;
                    object Resultat = NouvelOenologue.DefinirCotationMaximale(Valeur);
                    if (Resultat is bool) break;
                    Console.WriteLine("{0}", Resultat);
                }
            } while (!NouvelOenologue.Ajouter());
            Console.WriteLine("\nEnregistrement réussi de {0}", NouvelOenologue);
        }

        /// <summary>
        /// Troisième série de tests
        /// </summary>
        /// <remarks>Utilisation du modèle d'entité Oenologue - version 2</remarks>
        static void Test3(MyDB BD)
        {
            Console.Clear();
            Console.WriteLine("'Test3' démarre ...");
            if (BD.IsConnected)
            {
                Console.WriteLine("\nLa connexion au serveur MySql est établie.");
                List<OenologueV2> Oenologues = new List<OenologueV2>(EntiteSimple.Lister<OenologueV2>(BD));
                foreach (OenologueV2 Oenologue in Oenologues)
                {
                    Console.WriteLine("\n{0}", Oenologue);
                }
            }
            Console.WriteLine("\n");
            Test3a(BD);
            Console.WriteLine("\nNombre d'objets de consultation (utilisés/existants) : {0} / {1}", MyDB.UsedReadersCount, MyDB.ReadersCount);
            Console.WriteLine("\n'Test3' est terminé.");

            Console.WriteLine("\nAppuyez sur ESCAPE pour continuer");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
        }

        static void Test3a(MyDB BD)
        {
            OenologueV2 NouvelOenologue = new OenologueV2(BD);
            do
            {
                Console.WriteLine("Nom :");
                while (true)
                {
                    string Valeur = Console.ReadLine();
                    object Resultat = NouvelOenologue.DefinirNom(Valeur);
                    if (Resultat is bool) break;
                    Console.WriteLine("{0}", Resultat);
                }

                Console.WriteLine("Indice de confiance :");
                while (true)
                {
                    double Valeur;
                    while (!double.TryParse(Console.ReadLine().Replace(',', '.'), System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign, CultureAnglaise, out Valeur)) ;
                    object Resultat = NouvelOenologue.DefinirIndiceConfiance(Valeur);
                    if (Resultat is bool) break;
                    Console.WriteLine("{0}", Resultat);
                }

                Console.WriteLine("Cotation minimale :");
                while (true)
                {
                    short Valeur;
                    while (!short.TryParse(Console.ReadLine(), out Valeur)) ;
                    object Resultat = NouvelOenologue.DefinirCotationMinimale(Valeur);
                    if (Resultat is bool) break;
                    Console.WriteLine("{0}", Resultat);
                }

                Console.WriteLine("Cotation maximale :");
                while (true)
                {
                    short Valeur;
                    while (!short.TryParse(Console.ReadLine(), out Valeur)) ;
                    object Resultat = NouvelOenologue.DefinirCotationMaximale(Valeur);
                    if (Resultat is bool) break;
                    Console.WriteLine("{0}", Resultat);
                }
            } while (!NouvelOenologue.Ajouter());
            Console.WriteLine("\nEnregistrement réussi de {0}", NouvelOenologue);
        }

        /// <summary>
        /// Fin des tests
        /// </summary>
        static void FinDesTests()
        {
            Console.Clear();

            Console.WriteLine("\nLibération des objets de consultation inutilisés...");
            MyDB.FreeReaders();
            Console.WriteLine("\nNombre d'objets de consultation (utilisés/existants) : {0} / {1}", MyDB.UsedReadersCount, MyDB.ReadersCount);

            Console.WriteLine("\nAppuyez sur ESCAPE pour quitter");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
        }
    }
}
