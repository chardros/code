using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class CreateUsers
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
                        for(int j = 0; j <= 1000; j++)
                        {
                            selectedSite.CreateUserEx(j.ToString(),j.ToString(),0,j.ToString(),j.ToString(),true, false,"Default settings", SFTPAdvBool.abFalse);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Could not configure site. Error: {0}", ex.Message));
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
