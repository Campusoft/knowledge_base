#Reporting_services


# Herramientas

- SQL Server Data Tools (SSDT) for Visual Studio
  - with Extensions: Analysis Services,Integration Services, Reporting Services

# Reporting Services 2019

## Migración 2009 a 2019

### Requisitos
1. Instalación SQL Server Reporting Service 2019
2. Instalación de Oracle ODAC 12.2c Release 1 (http://download.oracle.com/otn/other/ole-oo4o/ODAC122011_x64.zip) que es requerido para crear orígenes de datos de Oracle Database (equivalente a oracle en 2009).
3. Permisos de salida hacia servidores de base de datos.
4. Esquemas de conexión del ambiente respectivo.

### Proceso
1. Creación de orígenes de datos a partir de esquemas de conexión necesarios y de acuerdo al ambiente a utilizar.
2. Descarga de reportes (.rdl) y datasets (.rds) de servidor 2009
3. Importación de reportes (.rdl) y datasets (.rds) en servidor 2019
4. Cambio de origen de datos en los reportes para que apunten a los orígenes de datos creados en el nuevo servidor.

Labs

Unable to connect to data source 'DataSource1'.The selected data extension ORACLE is not installed or cannot be loaded. 


Oracle Connection

ConnectingMicrosoft SQL ServerReportingServices to Oracle Autonomous Database
https://www.oracle.com/a/otn/docs/database/connecting-ssrs-to-oracle-adb.pdf

Oracle Connection Type (SSRS & Power BI Report Server)
To use data from an Oracle database in your report, you must have a dataset that's based on a report data source of type Oracle. This built-in data source type uses the Oracle Data Provider directly and requires an Oracle client software component. 
https://docs.microsoft.com/en-us/sql/reporting-services/report-data/oracle-connection-type-ssrs?view=sql-server-ver15



- Report Builder

Report Builder is a tool for authoring paginated reports, for business users who prefer to work in a stand-alone environment instead of using Report Designer in Visual Studio / SSDT. 
