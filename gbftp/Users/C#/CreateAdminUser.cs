using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class CreateAdminUser
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                ICIAdminAccount adminUser = (ICIAdminAccount)m_server.CreateAdmin("Test_Administrator", "Tester!1", AdminAccountType.EFTAccount, false);
                Console.WriteLine(string.Format("Admin login {0} created.", adminUser.Login));
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
