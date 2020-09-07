
#  Grant SQL Permissions to IIS AppPool User 

Si el pool aplicacion IIS, esta configurado con Identity "ApplicationPoolIdentity", el acceso base de datos Sql Server, se tiene que dar permisos.

Error Access:


Cannot open database "<Name-db>" requested by the login. The login failed.
Login failed for user 'IIS APPPOOL\<AppPool-Name>'.

Si la base de datos se llama System-Foo, y el nombre app pool es "system-pool", las instrucciones sql para dar acceso es:

    USE [System-Foo];
    GO
     
    CREATE LOGIN [IIS APPPOOL\system-pool] FROM WINDOWS;
    CREATE USER System-Pool-User FOR LOGIN [IIS APPPOOL\system-pool];
    GO
     
    ALTER ROLE [db_owner] ADD MEMBER [System-Pool-User];
    GO

----------------
Cannot open database "System-Foo" requested by the login. The login failed.
Login failed for user 'NT AUTHORITY\IUSR'

Script

    USE [System-Foo];
    GO
         
    CREATE LOGIN [NT AUTHORITY\IUSR] FROM WINDOWS;
    GO
    
    EXEC master..sp_addsrvrolemember @loginame = N'NT AUTHORITY\IUSR', @rolename = N'sysadmin'
    GO


 
Referencias

[Grant SQL Permissions to IIS AppPool User](http://engram404.net/grant-sql-permissions-to-iis-apppool-user/)

[SQL SERVER – Add Any User to SysAdmin Role – Add Users to System Roles](https://blog.sqlauthority.com/2008/12/27/sql-server-add-any-user-to-sysadmin-role-add-users-to-system-roles/)

[Login failed for user ‘NT Authority\IUSR’](https://piyushksingh.com/2017/04/02/login-failed-for-user-nt-authorityiusr/)
