using System;
using SFTPCOMINTERFACELib;

namespace GSQA.EFT.COM
{
    public class ServerStats
    {

        CIServer m_server = new CIServer();

        public void Run(string serverNameOrAddress, int remoteAdminPort, string username, string password)
        {
            m_server.Connect(serverNameOrAddress, remoteAdminPort, username, password);

            try
            {
                uint seconds;
                uint minutes;
                uint hours;
                uint days;

                seconds = m_server.Uptime;
                minutes = seconds / 60; //Calculate minutes
                seconds = seconds % 60; //leftover seconds after taking minutes out
                hours = minutes / 60; //Calculate hours
                minutes = minutes % 60; //leftover minutes after taking hours out
                days = hours / 24; //Calculate days
                hours = hours % 24; //leftover hours after taking days out

                Console.WriteLine(string.Format("EFT Uptime: {0} day(s) {1} hour(s) {2} minute(s) {3} second(s)", days, hours, minutes, seconds));

                string lastModifiedBy = m_server.LastModifiedBy;
                string lastModifiedTime = m_server.LastModifiedTime.ToString("MM/dd/yyyy HH:mm:ss");
                Console.WriteLine(string.Format("EFT Server properties Last Modified: {0} by {1}", lastModifiedTime, lastModifiedBy));
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
