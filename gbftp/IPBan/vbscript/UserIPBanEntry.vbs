Set SFTPServer = WScript.CreateObject("SFTPCOMInterface.CIServer")

CRLF = (Chr(13)& Chr(10))
txtServer = "localhost"
txtPort =  "1100"
txtAdminUserName = "Administrator"
txtPassword = "Tester!1"

If Not Connect(txtServer, txtPort, txtAdminUserName, txtPassword) Then
  WScript.Quit(0)
End If

userName = "test1"
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

  set userSettings = selectedSite.GetUserSettings(userName)

  'To create an entry use the lines below
  Dim isAllowEntry : isAllowEntry = True
  userSettings.AddIPAccessRule "1.1.1.*", isAllowEntry, 0

'Old functions:
'  allowedIPs = userSettings.GetAllowedMasks
'  For each ip in allowedIPs
'    WScript.Echo "Allowed: " + ip
'  Next

'  deniedIPs = userSettings.GetDeniedMasks
'  For each ip in deniedIPs
'    WScript.Echo "Denied: " + ip
'  Next

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