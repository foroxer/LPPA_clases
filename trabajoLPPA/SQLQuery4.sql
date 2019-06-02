use master


alter database  [C:\USERS\AMARRACO\DOCUMENTS\LPPA_CLASES\TRABAJOLPPA\PROYECTOLPPA\APP_DATA\BASEDEDATOS.MDF] set single_user with rollback immediate;
go 
alter database  [C:\USERS\AMARRACO\DOCUMENTS\LPPA_CLASES\TRABAJOLPPA\PROYECTOLPPA\APP_DATA\BASEDEDATOS.MDF] modify name = LPPA
go
alter database LPPA set multi_user
go

SELECT * FROM master.sys.databases 