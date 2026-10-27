# Gestión de Secretos

Tienes tres niveles de seguridad para guardar la clave que el sistema debe "conocer":

Nivel Básico (Variables de Entorno): La clave se guarda en el servidor, en una variable de entorno (ENV_VAR). El código lee la variable al iniciarse.

- Pros: Fácil de implementar.
- Contras: Si hackean el servidor y tienen acceso a la consola, pueden ver las variables.


Nivel Recomendado (Gestores de Secretos / Vaults): Usar servicios como AWS KMS, Azure Key Vault, Google Secret Manager o HashiCorp Vault.

-  Cómo funciona: Tu sistema se autentica contra estos servicios y pide la clave (o pide al servicio que encripte/desencripte por él). La clave nunca se guarda en el disco duro de tu servidor.


Nivel Máximo (HSM): 
- Hardware dedicado físico para guardar claves. Usado por bancos.

