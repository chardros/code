using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class RemoveSite
    {

        CIServer m_server = new CIServer();
        ICIAdminAccount result = null;

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                string siteName = "MySite";
                CISite siteToRemove = null;
                CISites sites = m_server.Sites();
                for(int i = 0; i < sites.Count(); i++)
                {
                    CISite site = sites.Item(i);
                    if (site.Name == siteName)
                    {
                        siteToRemove = site;
                        break;
                    }
                }

                if (siteToRemove != null)
                {
                    try
                    {
                        siteToRemove.Remove();
                        Console.WriteLine(string.Format("Site {0} removed.", siteName));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Could not remove site. Error: {0}", ex.Message));
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
