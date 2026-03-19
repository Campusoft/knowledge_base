# Azure Key Vault


Conceptualmente, es un servicio centralizado para el almacenamiento seguro de secretos, claves criptográficas y certificados, eliminando la necesidad de guardar información sensible en el código fuente o en la configuración de la infraestructura.

Principales Características

- Gestión de Secretos: Almacena de forma segura tokens de API, contraseñas y cadenas de conexión.
- Gestión de Claves: Creación y control de claves de cifrado. Puedes usar incluso módulos de seguridad de hardware (HSM) certificados.
- Gestión de Certificados: Facilita la inscripción, renovación y administración de certificados SSL/TLS.
- Segregación de Funciones: Permite definir quién tiene acceso a qué (lectura, escritura, borrado) mediante políticas de acceso o RBAC (Role-Based Access Control).


Varios
- Auditoría: Tienes logs detallados de quién, cuándo y desde dónde se accedió a un secreto.


 
Componente en Azure Key Vault | Equivalente en AWS | Función
-- | -- | --
Keys (Llaves) | AWS KMS | Llaves criptográficas maestras para cifrar discos (SSE) o datos.
Secrets (Secretos) | AWS Secrets Manager | Almacenamiento de contraseñas, tokens y cadenas de conexión.
Certificates (Certificados) | AWS Certificate Manager | Gestión y renovación de certificados SSL/TLS.

 
 


## Crear Key Vault
https://docs.microsoft.com/en-us/azure/key-vault/general/overview

## Leer Key Vault sin credenciales
https://docs.microsoft.com/en-us/azure/key-vault/secrets/quick-create-net


