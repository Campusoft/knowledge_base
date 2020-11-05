using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CredentialsFactory
{
    public class SharepointCredentialsFactory
    {
        public enum SharePointAuthentication
        {
            SharePointOnline,
            SharePointActiveDirectory
        }

        public static ICredentials CreateCredentials(string Username, string Password, SharePointAuthentication AuthenticationType)
        {
            ICredentials credentials = null;

            switch (AuthenticationType)
            {
                case SharePointAuthentication.SharePointOnline:
                    try
                    {
                        SecureString SecurePassword = new SecureString();
                        Password.ToCharArray().ToList().ForEach(s => SecurePassword.AppendChar(s));
                        credentials = new SharePointOnlineCredentials(Username, SecurePassword);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case SharePointAuthentication.SharePointActiveDirectory:
                    try
                    {
                        credentials = new NetworkCredential(Username, Password);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                default:
                    break;
            }
            return credentials;
        }
    }
}
