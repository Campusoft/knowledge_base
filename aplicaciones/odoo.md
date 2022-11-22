# Odoo


# Install


Odoo Nightly builds

Every night, a new set of packages is generated for the branches listed below. This set consists of deb and rpm packages for Debian and RedHat distributions, an exe package for Windows and a source package.
https://nightly.odoo.com/


-----------------
Windows. Iniciar odoo:

Use Services to Start/Restart/Stop odoo-server

Menu Start --> tape Services --> search odoo-Server --> Select line and click on Restart/Start/Stop's buttons above

---------------------

How To Enable Full Accounting Features In Odoo 16 Community Edition || Odoo 16 Full Accounting 
https://www.youtube.com/watch?v=UDwF6FkMCmo

------------------------

Errores:
------------

Database creation error: FATAL: password authentication failed for user "openpg" 

-------------------


# Developer


https://www.odoo.com/documentation/16.0/developer.html


## Modulos/App

Installar un Modulo
•	Buscar la carpeta de  modulos. En el archivo  de configuración "..\server\odoo.conf" buscar la entrada "addons_path".
•	Copiar el modulo nuevo a esta carpeta


## Objectos Negocio

res.company
- Compania. Empresa 

res.partner
- Partner (Clientes, Proveedores, etc.)

Account_Move 
- Asientos Contables.

account.journal
- Diarios.

account.tax
- Impuestos

account.config.settings
- Configuraciones de cuentas. 

account.invoice
- Facturas
  - Type: 'in_invoice'. Factura de Compra, 'in_refund'. Reembolso: out_invoice. Factura de Venta
  - Metodos: get_taxes_values

account.invoice.tax
- Impuestos de una factura

res.groups
- Grupos de Seguridad. 

### res.partner

base\models\res_partner.py
- Campos. 
  - vat. => he Tax Identification Number. Complete it if the contact is subjected to government taxes. Used in some legal statements.
  - verifica que no no exista dos registros con el mismo vat.
- Un partner, posee una lista de contactos que son partner con partner_id a quien corresponde dichos contactos.

l10n_latam_base\models\res_partner.py
- l10n_latam_identification_type_id
- vat (Value Added Tax). IVA
- Revisiones
  - Si un partner, posee 4 contactos, cada uno de estos contactos va posee el vat igual que el padre. En los contactos no se puede cambiar la informacion del identificador, al momento de cambiar el identificador en padre, se cambian a todos los hijos (contactos) que posee este padre. 

l10n_ec\models\res_partner.py
- check_vat
- is_valid_ruc_ec
- verify_final_consumer. (Verificar si se trata de un identificador de consumir final)

