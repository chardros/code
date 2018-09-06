using System;
using SFTPCOMINTERFACELib;
using System.Collections.Generic;

namespace GSQA.EFT.COM
{
    public class SSH
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {

            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                CISites sites = m_server.Sites();
                if (sites.Count() > 0)
                {
                    object[] keyIdList;
                    CISite site = sites.Item(0);

                    //export each SSH key and re-import it with an alteration to the name
                    //to demonstrate export/import/rename functionality
                    keyIdList = (object[])m_server.AvailableSSHKeys;
                    foreach (CISSHKeyInfo key in keyIdList)
                    {
                        string keyPath = string.Format(@"c:\key_{0}.pub", key.Name);
                        m_server.ExportSSHKey(key.ID, keyPath);
                        m_server.RemoveSSHKey(key.ID);

                        int keyId = m_server.ImportSSHKey(keyPath);
                        m_server.RenameSSHKey(keyId, string.Format("{0}_new", key.Name));
                    }

                    //Create a list to hold our key ids
                    List<object> keyIds = new List<object>();

                    //Add all available keys to a user named "test1" to demonstrate how to add multiple keys
                    keyIdList = (object[])m_server.AvailableSSHKeys;
                    foreach (CISSHKeyInfo key in keyIdList)
                    {
                        keyIds.Add(key.ID);
                    }

                    //Get our user
                    CIClientSettings userSettings = site.GetUserSettings("test1");

                    //Assign all our keys in our list to our client
                    userSettings.SetSSHKeyIDs(keyIds.ToArray());
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
