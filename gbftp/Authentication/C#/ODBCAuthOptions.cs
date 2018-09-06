using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class ODBCAuthOptions
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
                        if (selectedSite.GetAuthManagerID() == 2)
                        {
                            ICIODBCAuthManagerSettings authManagerSettings = (ICIODBCAuthManagerSettings)selectedSite.GetAMParams();

                            authManagerSettings.RefreshIntervalMinutes = 5;
                            authManagerSettings.UserDatabaseConnectionString = @"DRIVER={SQL Server};Provider=MSDASQL;SERVER=127.0.0.1\SQLEXPRESS;DATABASE=eft;UID=eft;PWD=MyPassword;";
                            selectedSite.SetAMParams(authManagerSettings);
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
