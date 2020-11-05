using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio");
            string siteOnpremisesURL = "https://basedocumental.utpl.edu.ec/sites/carpetaEstudiante/";
            string usernameOnpremises = "pasarela@utpl.edu.ec";
            string passwordOnpremises = "ppd&1234";
            string directoryOnpremises = "Dev_SolicitudesERP";

            string siteOnlineURL = "https://utpl.sharepoint.com/sites/dev_CarpetaEstudiante/";
            string usernameOnline = "jcsaavedra1@utpl.edu.ec";
            string passwordOnline = "Jc@Loja.2019";
            string directoryOnline = "Becas";

            Console.WriteLine("Online");
            using (ClientContext Context = new ClientContext(siteOnlineURL))
            {
                Context.Credentials = CredentialsFactory.SharepointCredentialsFactory.CreateCredentials(usernameOnline, passwordOnline, CredentialsFactory.SharepointCredentialsFactory.SharePointAuthentication.SharePointOnline);
                try
                {
                    Web myWeb = Context.Web;
                    //myWeb.Title = "Nuevo Título";
                    //myWeb.Update();
                    //Context.ExecuteQuery();
                    Context.Load(myWeb);
                    Context.ExecuteQuery();
                    Console.WriteLine($"Title: {myWeb.Title}");
                    Console.WriteLine($"Description: {myWeb.Description}");

                    Console.WriteLine($"----------------------------KEYS-----------------------------");
                    Microsoft.SharePoint.Client.ListItem listMetadata = GetMetadata(Context, directoryOnline);
                    foreach (var item in listMetadata.FieldValues)
                    {
                        Console.WriteLine($"{item.Key}");
                    }

                    Console.WriteLine($"----------------------------CARPETAS-----------------------------");
                    Microsoft.SharePoint.Client.FolderCollection listFolders = GetFolders(Context, directoryOnline);
                    foreach (var item in listFolders)
                    {
                        Console.WriteLine($"{item.Name}");
                    }

                    Console.WriteLine($"----------------------------ARCHIVOS-----------------------------");
                    Microsoft.SharePoint.Client.FileCollection listFiles = GetFiles(Context, directoryOnline);
                    foreach (var item in listFiles)
                    {
                        Console.WriteLine($"{item.Name} - {item.Exists}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Onpremises");
            using (ClientContext Context = new ClientContext(siteOnpremisesURL))
            {
                Context.Credentials = CredentialsFactory.SharepointCredentialsFactory.CreateCredentials(usernameOnpremises, passwordOnpremises, CredentialsFactory.SharepointCredentialsFactory.SharePointAuthentication.SharePointActiveDirectory);
                try
                {
                    Web myWeb = Context.Web;
                    Context.Load(myWeb);
                    Context.ExecuteQuery();
                    Console.WriteLine($"Title: {myWeb.Title}");
                    Console.WriteLine($"Description: {myWeb.Description}");

                    Console.WriteLine($"----------------------------KEYS-----------------------------");
                    Microsoft.SharePoint.Client.ListItem listMetadata = GetMetadata(Context, directoryOnpremises);
                    foreach (var item in listMetadata.FieldValues)
                    {
                        Console.WriteLine($"{item.Key}");
                    }

                    Console.WriteLine($"----------------------------CARPETAS-----------------------------");
                    Microsoft.SharePoint.Client.FolderCollection listFolders = GetFolders(Context, directoryOnpremises);
                    foreach (var item in listFolders)
                    {
                        Console.WriteLine($"{item.Name}");
                    }

                    Console.WriteLine($"----------------------------ARCHIVOS-----------------------------");
                    Microsoft.SharePoint.Client.FileCollection listFiles = GetFiles(Context, directoryOnpremises);
                    foreach (var item in listFiles)
                    {
                        Microsoft.SharePoint.Client.ListItem meta = item.ListItemAllFields;
                        Context.Load(meta);
                        Context.Load(item.Author);
                        Context.ExecuteQuery();


                        Console.WriteLine($"{item.Name} - {meta["Pidm"]} - {meta["Created"]} - {meta["Modified"]} - {item.Author.Email} - {item.Author.LoginName} - {meta["NumeroServicio"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Fin");
            Console.ReadLine();
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
                Console.WriteLine($"Error GetFiles: {ex.Message}");
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

        private static Microsoft.SharePoint.Client.ListItem GetMetadata(ClientContext Context, string directory)
        {
            try
            {
                Microsoft.SharePoint.Client.ListItem listMetadata = GetFiles(Context, directory).FirstOrDefault().ListItemAllFields;
                Context.Load(listMetadata);
                Context.ExecuteQuery();
                return listMetadata;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

    }
}
