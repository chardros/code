Dim fso
Set fso = WScript.CreateObject("Scripting.Filesystemobject")
Set f = fso.OpenTextFile("C:\scripts_ftp\output.txt", 2)

Set SFTPServer = WScript.CreateObject("SFTPCOMInterface.CIServer")

CRLF = (Chr(13)& Chr(10))
txtServer = "localhost"
txtPort =  "1100"
txtAdminUserName = "admin"
txtPassword = "Passw0rd"
txtUserUserName = "user1"
txtUserUserPass = "Passw0rd"

If Not Connect(txtServer, txtPort, txtAdminUserName, txtPassword) Then
 f.WriteLine  "Usuario no existe se procede a su creacion"
  siteName = "MySite"
  set selectedSite = Nothing
  set sites = SFTPServer.Sites()
  For i = 0 To sites.Count -1
   set site = sites.Item(i)
    If site.Name = siteName Then
     set selectedSite = site
     end if
  Exit For
  
  Next

  If Not selectedSite Is Nothing Then
    selectedSite.CreateUserEx Cstr(txtUserUserName),Cstr(txtUserUserPass),0,Cstr(j),Cstr(j),True,False,"Default settings"
    f.WriteLine "Usuario Creado: " txtUserUserName   
    SFTPServer.AutoSave = TRUE
    SFTPServer.ApplyChanges
  Else 
   f.WriteLine  "Usuario no existe y no ha podido ser Creado"
  End if 

Else
 Set sites = SFTPServer.Sites()
 For i = 0 to sites.Count - 1
  Set site = sites.Item(i)
  users = site.GetUsers()

  f.WriteLine txtAdminUserName & " User status in site: " + site.Name
  For each name in users

    set userSettings = site.GetUserSettings(name)
    lastModifiedBy = userSettings.LastModifiedBy
    lastModifiedTime = FormatDateTime(userSettings.LastModificationTime, 1)
    lastconnectionTime = FormatDateTime(userSettings.LastConnectionTime, 1)
    accountCreationTime = FormatDateTime(userSettings.AccountCreationTime, 1)

    If userSettings.IsLocked Then
        f.WriteLine "Account: " & txtAdminUserName & " is locked"
    Else
        f.WriteLine "Account: " & txtAdminUserName & " is not locked"
    End If

    f.WriteLine "Account created: "+ accountCreationTime
    f.WriteLine "Last connection time: "+ lastconnectionTime
    f.WriteLine "Last Modification: "+ lastModifiedTime + " by " + lastModifiedBy

  Next

 Next

SFTPServer.Close
Set SFTPServer = nothing

Function Connect (serverOrIpAddress, port, username, password)

  On Error Resume Next
  Err.Clear

  SFTPServer.Connect serverOrIpAddress, port, username, password

  If Err.Number <> 0 Then
    f.WriteLine "Error connecting to '" & serverOrIpAddress & ":" &  port & "' -- " & err.Description & " [" & CStr

(err.Number) & "]", vbInformation, "Error"
    Connect = False
    Exit Function
  End If

  Connect = True
End Function
End If
f.Close