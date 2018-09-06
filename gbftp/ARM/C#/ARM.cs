using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class ARM
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                if (m_server.ARMTestConnection())
                {
                    Console.WriteLine("ARM database connection successful.");
                }
                else
                {
                    Console.WriteLine("Could not connect to ARM database.");
                }

                if (m_server.ARMReconnect())
                {
                    Console.WriteLine("ARM database re-connection successful.");
                }
                else
                {
                    Console.WriteLine("Could not re-connect to ARM database.");
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
