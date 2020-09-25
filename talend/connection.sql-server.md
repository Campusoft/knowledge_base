

Error:

--------------------------------

Connection failure. You must change the Database Settings.
  java.sql.SQLException: I/O Error: SSO Failed: Native SSPI library not loaded. Check the java.library.path system property.

Para este error:

Solucion. 

Descargar  jtds desde 
http://jtds.sourceforge.net/

Realice los siguientes pasos

Unzip and copy \jtds-1.3.1-dist\x64\SSO\ntlmauth.dll file to Windows\System32 folder  (for 64 bit OS)
Copy \jtds-1.3.1-dist\x64\SSO\ntlmauth.dll file to Program Files\jdk\jre\bin folder (for 64 bit OS)


Referencia:
http://helicaltech.com/talend-connection-with-sql-server-using-windows-authentication/
