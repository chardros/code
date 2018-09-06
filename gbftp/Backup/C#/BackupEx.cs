using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class BackupEx
    {

        CIServer m_server = new CIServer();
        CIBackupArchiveInfo archive = null;

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                m_server.BackupServerConfiguration(@"C:\MyServerBackup.bck");

                archive = (CIBackupArchiveInfo)m_server.GetBackupArchiveInfo(@"C:\MyServerBackup.bck", AdminLoginType.EFTLogin, "Administrator", "Tester!1");

                //Set our ARM parameters
                archive.ARMAuthenticationType = ARMAuthenticationType.SQLServerAuthentication;
                archive.ARMDatabaseName = "ARMDatabaseName";
                archive.ARMServerName = "ARMServerName";
                archive.ARMUserName = "ARMDBUsername";
                archive.ARMPassword = "ARMDBPswd";
                archive.EnableARM = true;

                //Restore our configuration
                m_server.RestoreServerConfigurationEx(archive);

                //Restore does not start the service, so start it ourselves
                m_server.StartServerService("localhost");
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
