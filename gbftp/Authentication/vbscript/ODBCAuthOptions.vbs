Set SFTPServer = WScript.CreateObject("SFTPCOMInterface.CIServer")

CRLF = (Chr(13)& Chr(10))
txtServer = "localhost"
txtPort =  "1100"
txtAdminUserName = "Administrator"
txtPassword = "Tester!1"

If Not Connect(txtServer, txtPort, txtAdminUserName, txtPassword) Then
  WScript.Quit(0)
End If

siteName = "MySite"
set selectedSite = Nothing
set sites = SFTPServer.Sites()
For i = 0 To sites.Count -1
  set site = sites.Item(i)
  If site.Name = siteName Then
    set selectedSite = site
    Exit For
  End If
Next

If Not selectedSite Is Nothing Then

    'AuthManager type values:
    'Local Site = 0
    'AD Site = 1
    'ODBC Site = 2
    'LDAP Site = 3
    If selectedSite.GetAuthManagerID() = 2 Then
        Set authManagerSettings = selectedSite.GetAMParams()

        authManagerSettings.RefreshIntervalMinutes = 5
        authManagerSettings.UserDatabaseConnectionString = "DRIVER={SQL Server};Provider=MSDASQL;SERVER=127.0.0.1\SQLEXPRESS;DATABASE=eft;UID=eft;PWD=MyPassword;"
        selectedSite.SetAMParams authManagerSettings
    End If
    
End If

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