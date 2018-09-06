Set SFTPServer = WScript.CreateObject("SFTPCOMInterface.CIServer")

CRLF = (Chr(13)& Chr(10))
txtServer = "localhost"
txtPort =  "1100"
txtAdminUserName = "Administrator"
txtPassword = "Tester!1"

If Not Connect(txtServer, txtPort, txtAdminUserName, txtPassword) Then
  WScript.Quit(0)
End If

Set sites = SFTPServer.Sites()
For i = 0 to sites.Count - 1
  Set site = sites.Item(i)
  users = site.GetUsers()

  WScript.Echo "User status for site " + site.Name
  For each name in users

    set userSettings = site.GetUserSettings(name)
    lastModifiedBy = userSettings.LastModifiedBy
    lastModifiedTime = FormatDateTime(userSettings.LastModificationTime, 1)
    lastconnectionTime = FormatDateTime(userSettings.LastConnectionTime, 1)
    accountCreationTime = FormatDateTime(userSettings.AccountCreationTime, 1)

    If userSettings.IsLocked Then
        WScript.Echo "  Account is locked"
    Else
        WScript.Echo "  Account is not locked"
    End If

    WScript.Echo "  Account created " + accountCreationTime
    WScript.Echo "  Last connection time " + lastconnectionTime
    WScript.Echo "  Last Modification: " + lastModifiedTime + " by " + lastModifiedBy

  Next

Next

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