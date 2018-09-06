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
    If selectedSite.GetAuthManagerID() = 1 Then
        set authManagerSettings = selectedSite.GetAMParams()

        authManagerSettings.AssignHomeFolderFromUserProfile = True
        authManagerSettings.Domain = "YourDomain"	'To set this to Default use ""
        authManagerSettings.Group = ""	'To set this to Everyone use ""

        'Logon Attribute type values:
        'NT4AccountName = 0
        'DisplayName = 1
        'UserPrincipalName = 2
        'CommonName = 3
        authManagerSettings.LogonAttribute = 3

        authManagerSettings.RefreshIntervalMinutes = 6
        authManagerSettings.SkipDomainPrefix = True

        'NTAuthManager type values:
        'AuthManagerAD = 0,
        'AuthManagerNTLM = 1,
        authManagerSettings.type = 1

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