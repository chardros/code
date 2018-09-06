using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class ConfigureAdminUser
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

                object[] accounts = (object[])m_server.AdminAccounts;
                foreach (ICIAdminAccount admin in accounts)
                {
                    Console.WriteLine(string.Format("Admin user [{0}] has the following permissions:", admin.Login));
                    for (int i = 0; i < admin.GetPermissionsCount(); i++)
                    {
                        CIAdminPermission permission = (CIAdminPermission)admin.GetPermission((uint)i);
                        switch (permission.Permission)
                        {
                            case AdminPermissionsPolicy.ServerManagement:
                                Console.WriteLine("  Server management");
                                break;
                            case AdminPermissionsPolicy.SiteManagement:
                                Console.WriteLine(string.Format("  Site management for site [{0}]", permission.SiteName));
                                break;
                            case AdminPermissionsPolicy.STManagement:
                                Console.WriteLine(string.Format("  Template management [{0}] for site [{1}]", permission.TemplateName, permission.SiteName));
                                break;
                            case AdminPermissionsPolicy.UserCreation:
                                Console.WriteLine(string.Format("  User creation for template [{0}] for site [{1}]", permission.TemplateName, permission.SiteName));
                                break;
                            case AdminPermissionsPolicy.ChangePassword:
                                Console.WriteLine(string.Format("  Change password management for template [{0}] for site [{1}]", permission.TemplateName, permission.SiteName));
                                break;
                            case AdminPermissionsPolicy.COMManagement:
                                Console.WriteLine("  Management via COM");
                                break;
                            case AdminPermissionsPolicy.ReportManagement:
                                Console.WriteLine("  Report management");
                                break;
                        }
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
