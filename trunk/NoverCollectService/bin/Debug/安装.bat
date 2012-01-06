sc create NoverCollectService binpath= "D:\My Documents\Visual Studio 2008\Projects\KCMS\NoverCollectService\bin\Debug\NoverCollectService.exe --service -r " displayname= "NoverCollectService" depend= Tcpip
sc config NoverCollectService start= auto
net start NoverCollectService