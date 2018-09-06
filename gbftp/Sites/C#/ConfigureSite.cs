using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class ConfigureSite
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
                    try
                    {
                        if (selectedSite.GetAllowFXP())
                        {
                            Console.WriteLine(string.Format("Site {0} allows site to site transfers.", siteName));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Site {0} does not permit site to site transfers", siteName));
                        }

                        if (selectedSite.GetAllowCOMB())
                        {
                            Console.WriteLine(string.Format("Site {0} allows multi-part transfers (COMB).", siteName));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Site {0} does not permit multi-part transfers (COMB).", siteName));
                        }

                        //Set inactivity monitoring
                        selectedSite.RemoveInactiveAccounts = true;
                        selectedSite.MaxInactivePeriod = 30;
                        selectedSite.InactiveAccountsMonitoring = true;

                        //selectedSite.SetAllowCOMB(true);
                        //selectedSite.SetAllowFXP(true);
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
