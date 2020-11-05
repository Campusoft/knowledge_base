using System;
using System.Linq;
using System.Security;
using System.Net;
using Microsoft.SharePoint.Client;

namespace CSOMAuthentication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Inicio");
            // Provide Site URL
            string SiteURL = "https://utpl.sharepoint.com/sites/dev_CarpetaEstudiante/";
            //string SiteURL = "https://utpl.sharepoint.com/sites/ReportesBanner/";

            // Provide the environment in which the site resides. One of the below options.
            // (i) onpremises (ii) o365 (iii) extranet
            string Environmentvalue = "onpremises";
            //string Environmentvalue = "o365";
            //string Environmentvalue = "extranet";
            //string username = "pasarela@utpl.edu.ec";
            //string password = "ppd&amp;1234";
            string username = "jcsaavedra1@utpl.edu.ec";
            string password = "Jc@Loja.2019";
            string directory = "Becas";
            //string username = "jpcorrea4@utpl.edu.ec";
            //string password = "Utpl%J745";
            //string directory = "Documentos";

            ClientContext Context = AuthenticateUser(new Uri(SiteURL), Environmentvalue, username, password);
            if (Context != null)
            {
                int countFields = !directory.Equals(String.Empty) ? CountFields(Context, directory) : CountFields(Context);
                Console.WriteLine($"Fields: {countFields}");

                Console.WriteLine($"----------------------------METADATA-----------------------------");
                Microsoft.SharePoint.Client.List listMetadata = GetMetadata(Context, directory);
                foreach (var item in listMetadata.Fields)
                {
                    Console.WriteLine($"{item.InternalName}");
                }

                Console.WriteLine($"----------------------------CARPETAS-----------------------------");
                Microsoft.SharePoint.Client.FolderCollection listFolders = GetFolders(Context, directory);
                foreach (var item in listFolders)
                {
                    Console.WriteLine($"{item.Name}");
                }

                Console.WriteLine($"----------------------------ARCHIVOS-----------------------------");
                Microsoft.SharePoint.Client.FileCollection listFiles = GetFiles(Context, directory);
                foreach (var item in listFiles)
                {
                    Console.WriteLine($"{item.Name} - {item.Exists}");
                }
            }
            Console.WriteLine($"Fin");
            Console.ReadKey();
        }

        private static int CountFields(ClientContext Context, string directory)
        {
            try
            {
                Microsoft.SharePoint.Client.List listFields = Context.Web.Lists.GetByTitle(directory);
                Context.Load(listFields);
                Context.ExecuteQuery();
                return listFields.ItemCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }

        private static int CountFields(ClientContext Context)
        {
            try
            {
                Microsoft.SharePoint.Client.ListCollection listFields = Context.Web.Lists;
                Context.Load(listFields);
                Context.ExecuteQuery();
                return listFields.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }

        private static Microsoft.SharePoint.Client.List GetMetadata(ClientContext Context, string directory)
        {
            try
            {
                Microsoft.SharePoint.Client.List listMeta = Context.Web.Lists.GetByTitle(directory);
                Context.Load(listMeta.Fields);
                Context.ExecuteQuery();
                return listMeta;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        private static Microsoft.SharePoint.Client.FolderCollection GetFolders(ClientContext Context, string directory)
        {
            try
            {
                Microsoft.SharePoint.Client.FolderCollection listFolders = Context.Web.Lists.GetByTitle(directory).RootFolder.Folders;
                Context.Load(listFolders);
                Context.ExecuteQuery();
                return listFolders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        private static Microsoft.SharePoint.Client.FileCollection GetFiles(ClientContext Context, string directory)
        {
            try
            {
                Microsoft.SharePoint.Client.FileCollection listFiles = Context.Web.Lists.GetByTitle(directory).RootFolder.Files;
                Context.Load(listFiles);
                Context.ExecuteQuery();
                return (listFiles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        private static ClientContext AuthenticateUser(Uri TargetSiteUrl, string Environmentvalue, string username, string password)
        {
            ClientContext Context = null;
            try
            {
                // Based on the environmentvalue provided it execute the function.
                if (string.Compare(Environmentvalue, "onpremises", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Console.WriteLine($"onpremises");
                    Context = LogOn(username, password, TargetSiteUrl);

                    // isAuthenticated = true;
                    // You can write additional methods here which you want to use after authentication
                }
                else if (string.Compare(Environmentvalue, "o365", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Console.WriteLine($"o365");
                    Context = O365LogOn(username, password, TargetSiteUrl);

                    // isAuthenticated = true;
                    // You can write additional methods here which you want to use after authentication
                }
                else if (string.Compare(Environmentvalue, "extranet", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Console.WriteLine($"extranet");
                    Context = ExtranetLogOn(username, password, TargetSiteUrl);

                    // isAuthenticated = true;
                    // You can write additional methods here which you want to use after authentication
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return Context;
        }

        private static ClientContext LogOn(string userName, string password, Uri url)
        {
            ClientContext clientContext = null;
            ClientContext ctx;
            try
            {
                clientContext = new ClientContext(url);

                // Condition to check whether the user name is null or empty.
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    SecureString securestring = new SecureString();
                    password.ToCharArray().ToList().ForEach(s => securestring.AppendChar(s));
                    clientContext.Credentials = new System.Net.NetworkCredential(userName, securestring);
                    clientContext.ExecuteQuery();
                }
                else
                {
                    clientContext.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    clientContext.ExecuteQuery();
                }

                ctx = clientContext;
            }
            finally
            {
                if (clientContext != null)
                {
                    clientContext.Dispose();
                }
            }

            return ctx;
        }

        private static ClientContext O365LogOn(string userName, string password, Uri url)
        {
            ClientContext clientContext = null;
            ClientContext ctx = null;
            try
            {
                clientContext = new ClientContext(url);

                // Condition to check whether the user name is null or empty.
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    SecureString securestring = new SecureString();
                    password.ToCharArray().ToList().ForEach(s => securestring.AppendChar(s));
                    clientContext.Credentials = new SharePointOnlineCredentials(userName, securestring);
                    clientContext.ExecuteQuery();
                }
                else
                {
                    clientContext.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    clientContext.ExecuteQuery();
                }
                ctx = clientContext;
            }
            finally
            {
                if (clientContext != null)
                {
                    clientContext.Dispose();
                }
            }
            return ctx;
        }

        private static ClientContext ExtranetLogOn(string userName, string password, Uri url)
        {
            ClientContext clientContext = null;
            ClientContext ctx;
            try
            {
                clientContext = new ClientContext(url);

                // Condition to check whether the user name is null or empty.
                if (!string.IsNullOrEmpty(userName))
                {
                    NetworkCredential networkCredential = new NetworkCredential(userName, password);
                    CredentialCache cc = new CredentialCache();
                    cc.Add(url, "NTLM", networkCredential);
                    clientContext.Credentials = cc;
                    clientContext.ExecuteQuery();
                }
                else
                {
                    CredentialCache cc = new CredentialCache();
                    cc.Add(url, "NTLM", System.Net.CredentialCache.DefaultNetworkCredentials);
                    clientContext.Credentials = cc;
                    clientContext.ExecuteQuery();
                }
                ctx = clientContext;
            }
            finally
            {
                if (clientContext != null)
                {
                    clientContext.Dispose();
                }
            }
            return ctx;
        }
    }
}