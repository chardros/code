using System;
using SFTPCOMINTERFACELib;
using System.Collections.Generic;

namespace GSQA.EFT.COM
{
    public class PGP
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {

            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                m_server.ImportSSHKey(@"C:\SSHKey.pub");
                object publicPath = new object();
                object privatePath = new object();
                m_server.GetPGPKeyringSettings(out publicPath, out privatePath);
                m_server.SetPGPKeyringSettings(@"C:\filecryptdata\pubring.pgp", @"C:\filecryptdata\secring.pgp");
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
