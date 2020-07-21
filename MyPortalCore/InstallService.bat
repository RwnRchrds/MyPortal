sc create MyPortal binPath= %~dp0MyPortal.exe
sc failure MyPortal actions= restart/60000/restart/60000/""/60000 reset= 86400
sc start MyPortal
sc config MyPortal start=auto