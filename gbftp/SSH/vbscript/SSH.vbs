Set SFTPServer = WScript.CreateObject("SFTPCOMInterface.CIServer")

CRLF = (Chr(13)& Chr(10))
txtServer = "localhost"
txtPort =  "1100"
txtAdminUserName = "Administrator"
txtPassword = "Tester!1"

If Not Connect(txtServer, txtPort, txtAdminUserName, txtPassword) Then
  WScript.Quit(0)
End If

set Sites=SFTPServer.Sites 
set Site = Sites.Item(0)

For Each key In SFTPServer.AvailableSSHKeys
  keyPath = "c:\key_" + key.Name + ".pub"
  SFTPServer.ExportSSHKey key.ID, keyPath
  SFTPServer.RemoveSSHKey key.ID
  keyId = SFTPServer.ImportSSHKey(keyPath)
  SFTPServer.RenameSSHKey keyId, "key_" + key.Name + "_new.pub"
Next

Dim i : i = 0
keyIds = Array()
For Each key In SFTPServer.AvailableSSHKeys
  ReDim Preserve keyIds(i)
  keyIds(i) = key.ID
  i = i + 1
Next

'Get our user
Set userSettings = Site.GetUserSettings("test1")

'Assign all our keys in our list to our client
userSettings.SetSSHKeyIDs(keyIds)

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