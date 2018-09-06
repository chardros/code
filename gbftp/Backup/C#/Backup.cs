using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class Backup
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                //Backup our EFT configuration
                m_server.BackupServerConfiguration(@"C:\MyServerBackup.bck");

                //Restore our configuration
                m_server.RestoreServerConfiguration(@"C:\MyServerBackup.bck", AdminLoginType.EFTLogin, "Administrator", "Tester!1");

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
