using Microsoft.SharePoint.Client;
using System;
using System.Security;
using SharePointPnP;
using OfficeDevPnP.Core;
using System.Net;
using System.IO;
using System.Web.UI.WebControls;

namespace Sharepoint.PnP.Auth
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio");

            // Provide Site URL
            string SiteURL = "https://utpl.sharepoint.com/sites/dev_CarpetaEstudiante/";
            //string SiteURL = "https://utpl.sharepoint.com/sites/ReportesBanner/";

            // Provide the environment in which the site resides. One of the below options.
            // (i) onpremises (ii) o365 (iii) extranet
            //string Environmentvalue = "onpremises";
            string Environmentvalue = "o365";
            //string Environmentvalue = "extranet";
            //string username = "pasarela@utpl.edu.ec";
            //string password = "ppd&amp;1234";
            string username = "jcsaavedra1@utpl.edu.ec";
            string password = "Jc@Loja.2019";
            string directory = "Becas";
            string fileUpload = "c:\\temp\\6833.jpg";
            //string username = "jpcorrea4@utpl.edu.ec";
            //string password = "Utpl%J745";
            //string directory = "Documentos";

            ClientContext Context = AuthenticateUser(new Uri(SiteURL), Environmentvalue, username, password);
            if (Context != null)
            {
                //SPUploader(Context, fileUpload);
                SPUploaderWithMetadata(Context, fileUpload);

                //int countFields = !directory.Equals(String.Empty) ? CountFields(Context, directory) : CountFields(Context);
                //Console.WriteLine($"Fields: {countFields}");

                //Console.WriteLine($"----------------------------METADATA-----------------------------");
                //Microsoft.SharePoint.Client.List listMetadata = GetMetadata(Context, directory);
                //foreach (var item in listMetadata.Fields)
                //{
                //    Console.WriteLine($"{item.InternalName}");
                //}

                //Console.WriteLine($"----------------------------CARPETAS-----------------------------");
                //Microsoft.SharePoint.Client.FolderCollection listFolders = GetFolders(Context, directory);
                //foreach (var item in listFolders)
                //{
                //    Console.WriteLine($"{item.Name}");
                //}

                //Console.WriteLine($"----------------------------ARCHIVOS-----------------------------");
                //Microsoft.SharePoint.Client.FileCollection listFiles = GetFiles(Context, directory);
                //foreach (var item in listFiles)
                //{
                //    Console.WriteLine($"{item.Name} - {item.Exists}");
                //}
            }
            Console.WriteLine("Fin");
            Console.ReadKey();
        }

        private static ClientContext AuthenticateUser(Uri TargetSiteUrl, string Environmentvalue, string userName, string password)
        {
            ClientContext Context = null;

            SecureString securePassword = new NetworkCredential(userName, password).SecurePassword;

            OfficeDevPnP.Core.AuthenticationManager authManager = new OfficeDevPnP.Core.AuthenticationManager();

            try
            {
                Context = authManager.GetSharePointOnlineAuthenticatedContextTenant(TargetSiteUrl.ToString(), userName, securePassword);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
                Console.ReadKey();
            }
            return Context;
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

        private static void SPUploader(ClientContext Context, string fn)
        {
            //System.Net.ICredentials creds = System.Net.CredentialCache.DefaultCredentials;

            //Context.Credentials = creds;
            //Context.RequestTimeout = 60000000; // Time in milliseconds

            var lib = Context.Web.Lists.GetByTitle("becas");
            Context.Load(lib);
            Context.Load(lib.RootFolder);
            Context.ExecuteQuery();

            string url = lib.RootFolder.ServerRelativeUrl;
            string fileName = Path.GetFileName(fn);

            string fnUrl = url + "/" + fileName;

            FileStream fs = new FileStream(fn, FileMode.Open, FileAccess.Read);

            Microsoft.SharePoint.Client.File.SaveBinaryDirect(Context, fnUrl, fs, true);
        }

        private static void SPUploaderWithMetadata(ClientContext Context, string fn)
        {
            //System.Net.ICredentials creds = System.Net.CredentialCache.DefaultCredentials;

            //Context.Credentials = creds;
            //Context.RequestTimeout = 60000000; // Time in milliseconds

            var lib = Context.Web.Lists.GetByTitle("becas");
            Context.Load(lib);
            Context.Load(lib.RootFolder);
            Context.ExecuteQuery();

            string url = lib.RootFolder.ServerRelativeUrl;
            string fileName = Path.GetFileName(fn);

            string fnUrl = url + "/" + fileName;

            FileStream fs = new FileStream(fn, FileMode.Open, FileAccess.Read);

            Microsoft.SharePoint.Client.File.SaveBinaryDirect(Context, fnUrl, fs, true);

            string uniqueRefNo = Guid.NewGuid().ToString("N"); 

            Microsoft.SharePoint.Client.Web web = Context.Web;

            Microsoft.SharePoint.Client.File newFile = web.GetFileByServerRelativeUrl(fnUrl);
            //Context.Load(newFile, f => f.ListItemAllFields);
            Context.Load(newFile);
            Context.ExecuteQuery();

            Context.Load(newFile.Properties);
            Context.ExecuteQuery();
            var listMeta = newFile.Properties.FieldValues;

            foreach (var item1 in listMeta)
            {
                Console.WriteLine($"Fields: {item1}");
            }

            Microsoft.SharePoint.Client.ListItem item = newFile.ListItemAllFields;
            Context.Load(item);
            Context.ExecuteQuery();

            Console.WriteLine($"newFile.Name: {newFile.Name}");
            Console.WriteLine($"newFile.UniqueId: {newFile.UniqueId}");
            Console.WriteLine($"newFile.ServerRelativeUrl: {newFile.ServerRelativeUrl}");
            Console.WriteLine($"newFile.TimeLastModified: {newFile.TimeLastModified}");
            Console.WriteLine($"newFile.TimeCreated: {newFile.TimeCreated}");

            Console.WriteLine($"Title: {item.FieldValues["Title"]}");

            Microsoft.SharePoint.Client.User author = newFile.Author;
            Context.Load(author);
            Context.ExecuteQuery();

            Console.WriteLine($"Author Email: {author.Email}");
            Console.WriteLine($"LoginName: {author.LoginName}");
            Console.WriteLine($"UserPrincipalName: {author.UserPrincipalName}");

            Console.WriteLine($"Editor: {item.FieldValues["Editor"]}");
            Console.WriteLine($"StudentName: {item.FieldValues["StudentName"]}");
            Console.WriteLine($"Pidm: {item.FieldValues["Pidm"]}");
            Console.WriteLine($"ID: {item.FieldValues["ID"]}");
            Console.WriteLine($"Identificacion: {newFile.ListItemAllFields["Identificacion"]}");
            Console.WriteLine($"Author: {newFile.ListItemAllFields["Editor"]}");

            //Establecer propiedades
            //newFile.ListItemAllFields["StudentName"] = "Juan Pablo Correa";
            //newFile.ListItemAllFields["Pidm"] = 123456;
            //newFile.ListItemAllFields.Update();
            //Context.Load(newFile);
            //Context.ExecuteQuery();

            //item.FieldValues["StudentName"] = "Juan Pablo Correa";
            //item.Update();
            //Context.Load(item);
            //Context.ExecuteQuery();

            //Console.WriteLine($"StudentName: {item.FieldValues["StudentName"]}");

            //newFile.ListItemAllFields.Update();
            //Context.Load(newFile);
            //Context.ExecuteQuery();


            //Web site = Context.Web;
            //List docList = site.Lists.GetByTitle(fnUrl);

            //Context.Load(docList);
            //Context.ExecuteQuery();


            //Context.Load(docList.Fields.GetByTitle("Workflow Number"));
            //Context.Load(docList.Fields.GetByTitle("Agreement Number"));
            //Context.Load(docList.Fields.GetByTitle("First Name"));
            //Context.Load(docList.Fields.GetByTitle("Surname"));
            //Context.Load(docList.Fields.GetByTitle("ID Number"));
            //Context.Load(docList.Fields.GetByTitle("Date of Birth"));
            //Context.Load(docList.Fields.GetByTitle("Country"));
            //Context.Load(docList.Fields.GetByTitle("Document Description"));
            //Context.Load(docList.Fields.GetByTitle("Document Type"));
            //Context.Load(docList.Fields.GetByTitle("Document Group"));

            //Context.ExecuteQuery();

            ////*********NEED TO GET THE INTERNAL COLUMN NAMES FROM SHAREPOINT************
            //var name = docList.Fields.GetByTitle("Workflow Number").InternalName;
            //var name2 = docList.Fields.GetByTitle("Agreement Number").InternalName;
            //var name3 = docList.Fields.GetByTitle("First Name").InternalName;
            //var name4 = docList.Fields.GetByTitle("Surname").InternalName;
            //var name5 = docList.Fields.GetByTitle("ID Number").InternalName;
            //var name6 = docList.Fields.GetByTitle("Date of Birth").InternalName;
            //var name7 = docList.Fields.GetByTitle("Country").InternalName;
            //var name8 = docList.Fields.GetByTitle("Document Description").InternalName;
            //var name9 = docList.Fields.GetByTitle("Document Type").InternalName;
            //var name10 = docList.Fields.GetByTitle("Document Group").InternalName;
            //var name11 = docList.Fields.GetByTitle("Unique Document Ref No").InternalName;

            ////**********NOW SAVE THE METADATA TO SHAREPOINT**********
            //newFile.ListItemAllFields[name] = "927015";
            //newFile.ListItemAllFields[name2] = "1234565";
            //newFile.ListItemAllFields[name3] = "Joe";
            //newFile.ListItemAllFields[name4] = "Soap";
            //newFile.ListItemAllFields[name5] = "7502015147852";
            //newFile.ListItemAllFields[name6] = "1975-02-01";
            //newFile.ListItemAllFields[name7] = "ZA";
            //newFile.ListItemAllFields[name8] = "Test";
            //newFile.ListItemAllFields[name9] = "Requirements";
            //newFile.ListItemAllFields[name10] = "Requirements";
            //newFile.ListItemAllFields[name11] = uniqueRefNo;

            //newFile.ListItemAllFields.Update();
            //Context.Load(newFile);
            //Context.ExecuteQuery();
        }
    }
}
