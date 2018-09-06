using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class UserStats
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                CISites sites = m_server.Sites();
                for (int i = 0; i < sites.Count(); i++)
                {
                    CISite site = sites.Item(i);
                    Array users = (object[])site.GetUsers();

                    Console.WriteLine(string.Format("User status for site {0}", site.Name));
                    foreach (string name in users)
                    {
                        CIClientSettings userSettings = site.GetUserSettings(name);

                        string lastModifiedBy = userSettings.LastModifiedBy;
                        string lastModifiedTime = userSettings.LastModificationTime.ToString("MM/dd/yyyy HH:mm:ss");
                        string lastconnectionTime = userSettings.LastConnectionTime.ToString("MM/dd/yyyy HH:mm:ss");
                        string accountCreationTime = userSettings.AccountCreationTime.ToString("MM/dd/yyyy HH:mm:ss");
                        if (userSettings.IsLocked)
                        {
                            Console.WriteLine("  Account is locked");
                        }
                        else
                        {
                            Console.WriteLine("  Account is not locked");
                        }
                        Console.WriteLine(string.Format("  Account created {0}", accountCreationTime));
                        Console.WriteLine(string.Format("  Last connection time {0}", lastconnectionTime));
                        Console.WriteLine(string.Format("  Last Modification: {0} by {1}", lastModifiedTime, lastModifiedBy));
                    }
                }
            }
            catch (Exception ex)
            {
                //Log our error
                throw ex;
            }
            finally
            {
                m_server.Close();
                m_server = null;
            }
        }
    }
}
