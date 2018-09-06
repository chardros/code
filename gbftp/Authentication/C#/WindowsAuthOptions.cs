using System;
using SFTPCOMINTERFACELib;
using System.Reflection;

namespace GSQA.EFT.COM
{
    public class WindowsAuthOptions
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
                        //AuthManager type values:
                        //Local Site = 0
                        //AD Site = 1
                        //ODBC Site = 2
                        //LDAP Site = 3
                        if(selectedSite.GetAuthManagerID() == 1)
                        {
                            ICIADAuthManagerSettings authManagerSettings = (ICIADAuthManagerSettings)selectedSite.GetAMParams();
                            PropertyInfo property = PropertyByName<CIADAuthManagerSettings>("AssignHomeFolderFromUserProfile");

                            bool oldValue = authManagerSettings.AssignHomeFolderFromUserProfile;                          

                            authManagerSettings.Domain = "YourDomain";	//To set this to Default use ""
                            authManagerSettings.Group = "";	//To set this to Everyone use ""

                            //Logon Attribute type values:
                            //NT4AccountName = 0
                            //DisplayName = 1
                            //UserPrincipalName = 2
                            //CommonName = 3
                            authManagerSettings.LogonAttribute = ADAuthManagerLogonAttribute.CommonName;

                            authManagerSettings.RefreshIntervalMinutes = 6;

                            //the following two values must be set to the same value either true or false
                            authManagerSettings.AssignHomeFolderFromUserProfile = false;
                            authManagerSettings.SkipDomainPrefix = true;

                            //NTAuthManager type values:
                            //AuthManagerAD = 0,
                            //AuthManagerNTLM = 1,
                            authManagerSettings.type = ADAuthManagerType.AuthManagerNTLM;

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

        public PropertyInfo PropertyByName<ObjectType>(string propertyName)
        {
            PropertyInfo property = null;
            foreach (PropertyInfo property_ in typeof(ObjectType).GetProperties())
            {
                if (property_.Name == propertyName)
                {
                    property = property_;
                    break;
                }
            }
            return property;
        }
    }
}
