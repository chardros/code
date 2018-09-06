Set SFTPServer = WScript.CreateObject("SFTPCOMInterface.CIServer")

CRLF = (Chr(13)& Chr(10))
txtServer = "localhost"
txtPort =  "1100"
txtAdminUserName = "Administrator"
txtPassword = "Tester!1"

If Not Connect(txtServer, txtPort, txtAdminUserName, txtPassword) Then
  WScript.Quit(0)
End If

'AdminLoginType:
'EFTLogin = 0,
'IWALogin = 1,
'NetLogon = 2,

'ARMAuthenticationType:
'WindowsAuthentication = 0,
'SQLServerAuthentication = 1,

'Backup our EFT configuration
SFTPServer.BackupServerConfiguration "C:\MyServerBackup.bck"

'Get our archive information into a CIBackupArchiveInfo object
Set archive = SFTPServer.GetBackupArchiveInfo("C:\MyServerBackup.bck", 0, "Administrator", "Tester!1")

'Set our ARM parameters
archive.ARMAuthenticationType = 1   'ARMAuthenticationType
archive.ARMDatabaseName = "ARMDatabaseName"
archive.ARMServerName = "ARMServerName"
archive.ARMUserName = "ARMDBUsername"
archive.ARMPassword = "ARMDBPswd"
archive.EnableARM = False

'Restore our configuration
SFTPServer.RestoreServerConfigurationEx archive

'Restore does not start the service, so start it ourselves
SFTPServer.StartServerService "localhost"

WScript.Echo "Done"

SFTPServer.Close
Set SFTPServer = nothing

Function Connect (serverOrIpAddress, port, username, password)

  On Error Resume Next
  Err.Clear

  SFTPServer.Connect serverOrIpAddress, port, username, password

  If Err.Number <> 0 Then
    WScript.Echo "Error connecting to '" & serverOrIpAddress & ":" &  port & "' -- " & err.Description & " [" & CStr(err.Number) & "]", vbInformation, "Error"
    Connect = False
    Exit Function
  End If

  Connect = True
End Function