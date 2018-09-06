using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class SiteStats
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

                    string lastModifiedBy = site.LastModifiedBy;
                    string lastModifiedTime = site.LastModifiedTime.ToString("MM/dd/yyyy HH:mm:ss");
                    Console.WriteLine(string.Format("Site properties for {0} Last Modified: {1} by {2}", site.Name, lastModifiedTime, lastModifiedBy));
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
