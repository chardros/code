using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class CreateSite
    {

        CIServer m_server = new CIServer();
        ICIAdminAccount result = null;

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                CISite newSite = m_server.Sites().AddLocalSite("MySite", @"C:\Inetpub\EFTRoot\MySite", @"C:\Documents and Settings\All Users\Application Data\GlobalSCAPE\EFT Server Enterprise\MySite.aud", 0, 21, true, true, true, true);

                Console.WriteLine(string.Format("New site {0} created", newSite.Name));
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
