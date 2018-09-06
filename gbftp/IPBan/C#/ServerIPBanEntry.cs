using System;
using SFTPCOMINTERFACELib;
using System.Collections.Generic;

namespace GSQA.EFT.COM
{
    public class ServerIPBanEntry
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {

            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                
                string siteName = "MySite";
                CISite selectedSite = null;
                CISites sites = m_server.Sites();
                for(int i = 0; i < sites.Count(); i++)
                {
                    CISite site = sites.Item(i);
                    if (site.Name == siteName)
                    {
                        selectedSite = site;
                        break;
                    }
                }

                if (selectedSite != null)
                {
                    object[] users = (object[])selectedSite.GetUsers();
                    ICIClientSettings client = selectedSite.GetUserSettings((string)users[0]);
                    client.AddIPAccessRule("1.1.1.2", true, 0);
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
