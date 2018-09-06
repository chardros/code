using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class EventRuleCreation
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                CreateOnTimerEventRule();
                CreateDownloadEventRule();
                CreateUploadEventRule();
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

        public void CreateWindowsEventLogRule()
        {
            CISites sites = m_server.Sites();

            if (sites.Count() > 0)
            {
                CISite site = sites.Item(0);
                CIEventRules rules = (CIEventRules)site.EventRules(EventType.MonitorFolder);

                CIFolderMonitorEventRuleParams objParams = new CIFolderMonitorEventRuleParams();

                objParams.Name = "WindowsEventLogTestRule";
                objParams.Enabled = true;
                objParams.Description = "This is a test event rule for the Windows Event Log Action type.";
                objParams.Path = @"C:\EFTMonitoredFolder";

                CIEventRule rule = (CIEventRule)rules.Add(rules.Count(), objParams);

                CIWindowsEventLogActionParams eventLogActionParams = new CIWindowsEventLogActionParams();
                eventLogActionParams.Description = "Test action param";
                //eventLogActionParams.EventID = 2;
                eventLogActionParams.type = EventLogType.EventError;

                rule.AddActionStatement(rule.StatementsCount(), eventLogActionParams);
            }
        }

        public void CreateDownloadEventRule()
        {
            object[] objEvents = (object[])m_server.AvailableEvents;

            CISites sites = m_server.Sites();

            if (sites.Count() > 0)
            {
                CISite site = sites.Item(0);
                CIEventRules rules = (CIEventRules)site.EventRules(EventType.MonitorFolder);

                CIFolderMonitorEventRuleParams objParams = new CIFolderMonitorEventRuleParams();

                objParams.Name = "DownloadTestEventRule";
                objParams.Enabled = true;
                objParams.Description = "This is a test event rule";
                objParams.Path = @"C:\EFTMonitoredFolder";

                CIEventRule eventRule = (CIEventRule)rules.Add(rules.Count(), (object)objParams);

                CIDownloadActionParams downloadParams = new CIDownloadActionParams();

                downloadParams.RemotePath = "test.txt";
                downloadParams.LocalPath = @"c:\";
                downloadParams.Host = "192.168.100.155";
                downloadParams.User = "test1";
                downloadParams.Password = "Tester!1";
                downloadParams.ProxyAdvAuthenticationType = ProxyAuthenticationType.ProxyAuthCustom;
                downloadParams.OverwriteType = TransferOverwriteType.TransferOTOverwrite;
                downloadParams.SOCKSType = SOCKSType.SOCKS5;
                downloadParams.SOCKSHostName = "mySOCKSHost";
                downloadParams.SOCKSUserName = "me";
                downloadParams.SOCKSUseAuthentication = true;
                downloadParams.SOCKSPassword = "Tester!1";
                downloadParams.UseSOCKS = true;

                downloadParams.MaxConcurrentThreads = 10;
                downloadParams.ConnectionTimeoutSeconds = 2;
                downloadParams.ConnectionRetryAttempts = 44;
                downloadParams.RetryDelaySeconds = 7;
                downloadParams.ValidateIntegrity = true;
                downloadParams.TextFileTypes = "None";
                downloadParams.FTPDataConnectionMode = FTPDataConnectionMode.FTPMode_PORT;
                downloadParams.FTPDataConnectionPortMax = 2200;
                downloadParams.FTPDataConnectionPortMin = 100;
                downloadParams.FTPSClearCommandChannel = false;
                downloadParams.FTPSClearDataChannel = false;
                downloadParams.PreserveFileTime = true;
                downloadParams.OverwriteType = TransferOverwriteType.TransferOTOverwrite;

                eventRule.AddActionStatement(0, downloadParams);
            }
        }

        public void CreateUploadEventRule()
        {
            object[] objEvents = (object[])m_server.AvailableEvents;

            CISites sites = m_server.Sites();

            if (sites.Count() > 0)
            {
                CISite site = sites.Item(0);
                CIEventRules rules = (CIEventRules)site.EventRules(EventType.MonitorFolder);

                CIFolderMonitorEventRuleParams objParams = new CIFolderMonitorEventRuleParams();

                objParams.Name = "UploadTestEventRule";
                objParams.Enabled = true;
                objParams.Description = "This is a test event rule";
                objParams.Path = @"C:\EFTMonitoredFolder";

                CIEventRule eventRule = (CIEventRule)rules.Add(rules.Count(), (object)objParams);

                CIUploadActionParams uploadParams = new CIUploadActionParams();

                uploadParams.RemotePath = "test.txt";
                uploadParams.LocalPath = @"c:\";
                uploadParams.Host = "192.168.100.155";
                uploadParams.User = "test1";
                uploadParams.Password = "Tester!1";
                uploadParams.ProxyAdvAuthenticationType = ProxyAuthenticationType.ProxyAuthCustom;
                uploadParams.OverwriteType = TransferOverwriteType.TransferOTOverwrite;
                uploadParams.SOCKSType = SOCKSType.SOCKS5;
                uploadParams.SOCKSHostName = "mySOCKSHost";
                uploadParams.SOCKSUserName = "me";
                uploadParams.SOCKSUseAuthentication = true;
                uploadParams.SOCKSPassword = "Tester!1";
                uploadParams.UseSOCKS = true;

                uploadParams.MaxConcurrentThreads = 10;
                uploadParams.ConnectionTimeoutSeconds = 2;
                uploadParams.ConnectionRetryAttempts = 44;
                uploadParams.RetryDelaySeconds = 7;
                uploadParams.ValidateIntegrity = true;
                uploadParams.TextFileTypes = "None";
                uploadParams.FTPDataConnectionMode = FTPDataConnectionMode.FTPMode_PORT;
                uploadParams.FTPDataConnectionPortMax = 2200;
                uploadParams.FTPDataConnectionPortMin = 100;
                uploadParams.FTPSClearCommandChannel = false;
                uploadParams.FTPSClearDataChannel = false;
                uploadParams.PreserveFileTime = true;
                uploadParams.OverwriteType = TransferOverwriteType.TransferOTOverwrite;

                eventRule.AddActionStatement(0, uploadParams);
            }
        }

        public void CreatePGPEventRule()
        {
            object[] objEvents = (object[])m_server.AvailableEvents;

            CISites sites = m_server.Sites();

            if (sites.Count() > 0)
            {
                CISite site = sites.Item(0);
                CIEventRules rules = (CIEventRules)site.EventRules(EventType.MonitorFolder);

                CIFolderMonitorEventRuleParams objParams = new CIFolderMonitorEventRuleParams();

                objParams.Name = "TestEventRule";
                objParams.Enabled = true;
                objParams.Description = "This is a test event rule";
                objParams.Path = @"C:\EFTMonitoredFolder";

                CIEventRule eventRule = (CIEventRule)rules.Add(rules.Count(), (object)objParams);

                CIPgpActionParams pgpParams = new CIPgpActionParams();

                pgpParams.Operation = PGPOperation.VerifyOnly;

                eventRule.AddActionStatement(0, pgpParams);
            }
        }

        public void CreateOnTimerEventRule()
        {
            object[] objEvents = (object[])m_server.AvailableEvents;

            CISites sites = m_server.Sites();

            if (sites.Count() > 0)
            {
                CISite site = sites.Item(0);

                CIEventRules rules = (CIEventRules)site.EventRules(EventType.OnTimer);

                CITimerEventRuleParams objParams = new CITimerEventRuleParams();

                objParams.Name = "TestEventRule";
                objParams.Enabled = true;
                objParams.Description = "This is a test event rule";
                objParams.DateTimeStart = new DateTime(2011, 1, 1, 0, 0, 1);
                objParams.Recurrence = Recurrence.Recurrence_Once;

                CIEventRule eventRule = (CIEventRule)rules.Add(rules.Count(), (object)objParams);

                CIMailActionParams mail = new CIMailActionParams();

                mail.Body = "Test email";
                mail.Subject = "Test";
                mail.TOAddresses = "youremail@youdomain.com";

                eventRule.AddActionStatement(eventRule.StatementsCount(), mail);

            }
        }
    }
}
