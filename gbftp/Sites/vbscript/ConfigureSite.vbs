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
  If selectedSite.GetAllowFXP() Then
    WScript.Echo "Site " + siteName + " allows site to site transfers."
  Else
    WScript.Echo "Site " + siteName + " does not permit site to site transfers."
  End If

  If selectedSite.GetAllowCOMB() Then
    WScript.Echo "Site " + siteName + " allows multi-part transfers (COMB)."
  Else
    WScript.Echo "Site " + siteName + " does not permit multi-part transfers (COMB)."
  End If

  'Set inactivity monitoring
  selectedSite.RemoveInactiveAccounts = True
  selectedSite.MaxInactivePeriod = 30
  selectedSite.InactiveAccountsMonitoring = True

  'selectedSite.SetAllowCOMB(True)
  'selectedSite.SetAllowFXP(True)

End If

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