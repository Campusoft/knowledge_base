# invoiceninja


Invoices, expenses & time-tracking built with Laravel
https://github.com/invoiceninja/invoiceninja

# Features

•	Built using Laravel 5.2
•	Live PDF generation using pdfmake
•	Supports 50+ payment gateways with Omnipay
•	Integrate with hundreds of apps with Zapier and Integromat
•	Recurring invoices with auto-billing
•	Expenses and vendors
•	Import bank statements with OFX
•	Tasks with time-tracking
•	File Attachments
•	Multi-user/multi-company support
•	Tax rates and payment terms
•	Reminder emails
•	Partial payments
•	Client portal
•	Custom email templates
•	D3.js visualizations
Otras Caracteristicas
•	Permite editar la plantilla de la factura (En línea), con un editor JSON. 
•	Permite configurar: Googletagmanager, google-analytics.com, para monitorear.
•	Recolecta informacion de errores, por medio de eventos de google-analytics
•	No posee campos personalizados dinámicos. Permite agregar dos campos  exactamente personalizados a Clientes y Permite agregar dos campos personalizados a Facturas. 
•	Exportar/Importar Clients, Productos, Facturas.

Faltantes
•	Proveedores
o	Registro de Facturas de Proveedores. (Posee una funcionalidad de Gastos). 
o	Permitir colocar campos personalizados a Proveedores. Igual que Clientes. (Dos fijos)
•	Impuestos.
o	Visualizar subtotal. (IVA 12%), Visualizar Subtotal (IVA 0%)
•	Facturas.
o	No permitir modificar el precio de los productos. Si posee permisos. (*)
o	Documento Factura. Visualizar el Total de la Factura.
o	Visualizar Total en Letras. 

# Arquitectura

Built PHP, using Laravel 5.2
ORM (eloquent)
Utiliza pdfmake, para generar los pdf con javascript. 

Posee REST (Api)
•	client 
•	document 
•	expense 
•	expense_category 
•	invoice 
•	payment 
•	product 
•	quote 
•	task 
•	tax_rate 
•	vendor

Gestion de Errores/javascript
stacktrace.js
Framework-agnostic, micro-library for getting stack traces in all web browsers
Debug and profile your JavaScript with a stack trace of function calls leading to an error (or any condition you specify).
stacktrace.js uses browsers' Error.stack mechanism to generate stack traces, parses them, enhances them with source maps and uses Promises to return an Array of StackFrames.

# Modificaciones/Adaptaciones

Aumentar una columna en los listados de entidades

•	Para aumentar una columna, se debe modificar
o	El archivo “EntityDatatable” de la entidad correspondiente, aumentando la columna que se desee. En la función “columns”.
o	Verificar si la columna agregada, se recuperar en el archivo “BaseRepository” de la entidad en la función “find”. 
o	Agregar el nombre de la etiqueta en localization correspondiente
Ejemplo aumentar una columna de Client.
•	Modificar el archivo “<PROYECTO>\app\Ninja\Datatables\ClientDatatable.php” , aumentando la columna en la función “columns”
•	Modificar el archivo “<PROYECTO>\app\Ninja\Repositories\ClientRepository.php”  aumentando la columna en la función “find” si no se encuentra en la consulta
•	Aumentar en el archivo de localization “<PROYECTO>\resources\lang\es\texts.php” y “<PROYECTO”>\resources\lang\es_ES\texts.php”, la traducion de la etiqueta de la columna utilizada. 

Quitar Marca en el pie. 
El objeto factura, posee un campo features, y dentro existe “remove_created_by”, que debe estar true, para quitar
invoice
	features
		customize_invoice_design:true
		invoice_settings:true
		remove_created_by:false

En la vista: 		
ninja\resources\views\invoices\pdf.blade.php		

Existe una variable:
ninja\public\images\report_logo1.jpg		
ninja\public\images\report_logo2.jpg		
ninja\public\images\report_logo3.jpg		

---
En el archivo:
ninja\resources\views\invoices\edit.blade.php

Existe el codigo:

function createInvoiceModel() {
        var model = ko.toJS(window.model);
        if(!model)return;
		var invoice = model.invoice;
		invoice.features = {
            customize_invoice_design:{{ Auth::user()->hasFeature(FEATURE_CUSTOMIZE_INVOICE_DESIGN) ? 'true' : 'false' }},
            remove_created_by:{{ Auth::user()->hasFeature(FEATURE_REMOVE_CREATED_BY) ? 'true' : 'false' }},
            invoice_settings:{{ Auth::user()->hasFeature(FEATURE_INVOICE_SETTINGS) ? 'true' : 'false' }}
        };

Laboratorio

# Instacion


Docker for Invoice Ninja
- Tiene archivo docker-compose
https://github.com/invoiceninja/dockerfiles


Errores:
Connection.php [Line 499] => Maximum execution time of 30 seconds exceeded  
The Maximum execution time of 30 seconds exceeded error is not related to Laravel but rather your PHP configuration.
Cambiar:
max_execution_time = 30 ; Maximum execution time of each script, in seconds
Por:
max_execution_time = 300 ; Maximum execution time of each script, in seconds
https://stackoverflow.com/questions/30270316/how-to-solve-a-timeout-error-in-laravel-5/30290770#30290770


# Application

Invoice Ninja: Web admin portal built with React

- react-query
https://github.com/invoiceninja/ui