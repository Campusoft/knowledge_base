
using Microsoft.SharePoint.Client;
using System;
using System.Net;
using System.Security;

namespace Sharepoint.PnP.Auth
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio");
            Autorize1();
            Autorize();
            Console.WriteLine("Fin");
            Console.ReadKey();
        }

        public static ClientContext Autorize()
        {
            ClientContext context = null;
            //string siteUrl = "https://utpl.sharepoint.com/sites/dev_CarpetaEstudiante/Becas";
            //string userName = "jcsaavedra1@utpl.edu.ec";
            //SecureString password = new NetworkCredential(userName, "Jc@Loja.2019").SecurePassword;
            //OfficeDevPnP.Core.AuthenticationManager authManager = new OfficeDevPnP.Core.AuthenticationManager();
            //context = authManager.GetSharePointOnlineAuthenticatedContextTenant(siteUrl, userName, password);
            return context;
        }

        public static void Autorize1()
        {
            string siteUrl = "https://utpl.sharepoint.com/sites/dev_CarpetaEstudiante/Becas";
            string userName = "jcsaavedra1@utpl.edu.ec";
            string password = "Jc@Loja.2019";

            //string userName = "pasarela@utpl.edu.ec";
            //string password = "ppd&amp;1234";

            SecureString securePassword = new NetworkCredential(userName, password).SecurePassword;


            // PnP component to set context 
            OfficeDevPnP.Core.AuthenticationManager authManager = new OfficeDevPnP.Core.AuthenticationManager();

            try
            {
                // Get and set the client context  
                // Connects to SharePoint online site using inputs provided  
                using (var clientContext = authManager.GetSharePointOnlineAuthenticatedContextTenant(siteUrl, userName, securePassword))
                {
                    // List name input  
                    string listName = "TestList";

                    // Checks the list exists  
                    bool listExists = clientContext.Site.RootWeb.ListExists(listName);
                    if (listExists)
                    {
                        Console.WriteLine("List is available on the site");
                    }
                    else
                    {
                        Console.WriteLine("List is not available on the site");
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}
