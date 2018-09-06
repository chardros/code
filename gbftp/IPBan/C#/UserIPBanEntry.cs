using System;
using SFTPCOMINTERFACELib;
using System.Collections.Generic;

namespace GSQA.EFT.COM
{
    public class UserIPBanEntry
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {

            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                string userName = "test1";
                string siteName = "MySite";
                CISite selectedSite = null;
                CISites sites = m_server.Sites();
                for (int i = 0; i < sites.Count(); i++)
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
                    try
                    {
                        CIClientSettings userSettings = selectedSite.GetUserSettings(userName);

                        //To create an entry use the line below
                        userSettings.AddIPAccessRule("1.1.1.*", true, 0);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Could not configure user. Error: {0}", ex.Message));
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
