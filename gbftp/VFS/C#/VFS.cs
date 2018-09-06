using System;
using SFTPCOMINTERFACELib;
using System.Collections.Generic;

namespace GSQA.EFT.COM
{
    public class VFS
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
                    CISite site = sites.Item(0);

                    string PFRelativePath = "\\Test_Physical_Folder";
                    string PFFullPath = site.GetRootFolder() + PFRelativePath;
                    string VFAliasRoot = "/Usr";

                    Console.WriteLine(string.Format("Information for {0}:", VFAliasRoot));

                    string folderList = site.GetFolderList(VFAliasRoot);
                    string[] folders = folderList.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string folder in folders)
                    {
                        string folderPath = string.Format(@"{0}/{1}", VFAliasRoot, folder);

                        Console.Write(" Is virtual: ");
                        Console.WriteLine(site.IsFolderVirtual(folderPath) ? "Yes" : "No");

                        Console.Write(" Is encrypted with EFS: ");
                        Console.WriteLine(site.IsEncrypted(folderPath) ? "Yes" : "No");

                        Console.WriteLine(" -Permissions List-");
                        Console.WriteLine(" User            Source Inherited");

                        Permission[] folderPermissions = (Permission[])site.GetFolderPermissions(folderPath);
                        foreach (Permission perm in (object[])site.GetFolderPermissions(folderPath))
                        {
                            Console.Write(" " + perm.Client.PadRight(16, ' '));
                            Console.Write((perm.IsGroup) ? "group  " : "user   ");
                            Console.Write((perm.IsInherited) ? "Yes  " : "No   ");
                            Console.Write(perm.InheritedFrom);

                            Console.WriteLine();
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

        public static bool IsFullPermissions(IPermission p)
        {
            return p.DirCreate
                && p.DirDelete
                && p.DirList
                && p.DirShowHidden
                && p.DirShowInList
                && p.DirShowReadOnly
                && p.FileAppend
                && p.FileDelete
                && p.FileDownload
                && p.FileRename
                && p.FileUpload;
        }
    }
}
