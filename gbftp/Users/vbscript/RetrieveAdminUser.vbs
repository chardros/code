Set SFTPServer = WScript.CreateObject("SFTPCOMInterface.CIServer")

CRLF = (Chr(13)& Chr(10))
txtServer = "localhost"
txtPort =  "1100"
txtAdminUserName = "Administrator"
txtPassword = "Tester!1"

If Not Connect(txtServer, txtPort, txtAdminUserName, txtPassword) Then
  WScript.Quit(0)
End If

accounts = SFTPServer.AdminAccounts
For each admin in accounts
  WScript.Echo "Admin user [" + admin.Login + "] has the following permissions:"

  Dim count : count = admin.GetPermissionsCount()
  For i = 0 To CInt(count )- 1
    Set permission = admin.GetPermission(i)

'    AdminPermissionsPolicy:
'        ServerManagement = 0,
'        SiteManagement = 1,
'        STManagement = 2,
'        UserCreation = 3,
'        ChangePassword = 4,
'        COMManagement = 5,
'        ReportManagement = 6,

    Select case permission.Permission
      case 0:
        WScript.Echo "  Server management"
      case 1:
        WScript.Echo "  Site management for site " + permission.SiteName
      case 2:
        WScript.Echo "  Template management [" + permission.TemplateName + "] for site [" + permission.SiteName + "]"
      case 3:
        WScript.Echo "  User creation for template [" + permission.TemplateName + "] for site [" + permission.SiteName + "]"
      case 4:
        WScript.Echo "  Change password management for template [" + permission.TemplateName + "] for site [" + permission.SiteName + "]"
      case 5:
        WScript.Echo "  Management via COM"
      case 6:
        WScript.Echo "  Report management"
    End Select
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