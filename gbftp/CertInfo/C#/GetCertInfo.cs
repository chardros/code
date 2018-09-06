﻿using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class GetCertInfo
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
                        object[] list = (object[])selectedSite.GetTrustedCertificates();
                        object info = selectedSite.GetTrustedCertificateInfo((int)list[0]);

                        CIClientSettings userSettings = selectedSite.GetUserSettings(userName);
                        

                        object enabled;
                        if (userSettings.GetAppletEnabled(out enabled))
                        {
                            Console.WriteLine(string.Format("{0} is allowed WTC.", userName));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("{0} is not permitted to use WTC.", userName));
                        }

                        //Set inactivity monitoring
                        userSettings.RemoveInactiveAccounts = true;
                        userSettings.MaxInactivePeriod = 30;
                        userSettings.SetInactiveAccountsMonitoring(SFTPAdvBool.abTrue);

                        //Change reset password settings
                        CIResetPasswordSettings resetPswdSettings = userSettings.GetResetPasswordSettings();
                        resetPswdSettings.MaxPasswordAgeDays = 10;
                        resetPswdSettings.SendEMailBeforeExpiration = false;
                        resetPswdSettings.SendEMailUponExpiration = false;
                        resetPswdSettings.DaysPriorPasswordExpirationToRemindUser = 1;
                        userSettings.SetResetPasswordSettings(resetPswdSettings);

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
