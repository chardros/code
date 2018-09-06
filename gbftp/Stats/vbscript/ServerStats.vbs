Set SFTPServer = WScript.CreateObject("SFTPCOMInterface.CIServer")

CRLF = (Chr(13)& Chr(10))
txtServer = "localhost"
txtPort =  "1100"
txtAdminUserName = "Administrator"
txtPassword = "Tester!1"

If Not Connect(txtServer, txtPort, txtAdminUserName, txtPassword) Then
  WScript.Quit(0)
End If

seconds = CLng(SFTPServer.Uptime)
minutes = CLng(seconds / 60) 'Calculate minutes
seconds = CLng(seconds mod 60) 'leftover seconds after taking minutes out
hours = CLng(minutes / 60) 'Calculate hours
minutes = CLng(minutes mod 60) 'leftover minutes after taking hours out
days = CLng(hours / 24) 'Calculate days
hours = CLng(hours mod 24) 'leftover hours after taking days out

WScript.Echo "EFT Uptime: " + CStr(days) + " day(s) " + CStr(hours) + " hour(s) " + CStr(minutes) + " minute(s) " + CStr(seconds) + " second(s)"

lastModifiedBy = SFTPServer.LastModifiedBy
lastModifiedTime = FormatDateTime(SFTPServer.LastModifiedTime, 1)
WScript.Echo "EFT Server properties Last Modified: " + lastModifiedTime + " by " + lastModifiedBy

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