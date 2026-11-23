# Arquitectura Vertical Slice

Vertical Slice organiza el sistema por **funcionalidad** (caso de uso) y no por capa técnica. Cada *slice* contiene todo lo necesario para atender una solicitud: entrada, validación, lógica y acceso a datos. El término se asocia a Jimmy Bogard, quien lo popularizó en el ecosistema .NET, pero el enfoque no depende de ningún lenguaje ni tecnología. En Java se conoce como *package by feature*.

## Idea central

En una arquitectura por capas, un cambio funcional atraviesa varias capas horizontales (presentación, negocio, datos). En Vertical Slice se corta el sistema en rebanadas verticales, y cada una cruza todas las capas para una sola funcionalidad.

```text
Por capas (horizontal)              Vertical Slice

 [ Presentación ]                    CrearPedido  | ListarPedidos | CancelarPedido
 [ Negocio      ]                    -------------+---------------+---------------
 [ Datos        ]                    entrada      | entrada       | entrada
                                     lógica       | lógica        | lógica
                                     datos        | datos         | datos
```

La regla de diseño es **maximizar la cohesión dentro del slice y minimizar el acoplamiento entre slices**.

## Características

- **Organización por caso de uso.** Cada operación del sistema (`CrearPedido`, `ConsultarSaldo`) es una unidad con nombre propio.
- **Cambios localizados.** Modificar o eliminar una funcionalidad afecta una sola carpeta o módulo.
- **Autonomía de implementación.** Cada slice elige la forma de resolver su problema. Uno puede usar un modelo de dominio rico y otro una consulta directa a la base de datos.
- **Pocas abstracciones compartidas.** No se crean repositorios o servicios genéricos de antemano. El código común se extrae cuando se repite y su forma ya es clara.
- **Contrato explícito por slice.** Cada slice define su propia solicitud y su propia respuesta. No reutiliza modelos de otro slice.
- **Afinidad con CQRS.** Un slice suele ser un comando o una consulta, lo que facilita aplicar [CQRS](cqrs.md) sin ceremonia adicional.
- **Compatible con otros estilos.** Puede usarse dentro de un [monolito modular](monolito-modular.md), o como forma interna de cada microservicio.

## Anatomía de un slice

Un slice agrupa, según lo que el caso de uso necesite:

| Elemento | Responsabilidad |
|---|---|
| Punto de entrada | Recibe la solicitud (HTTP, mensaje, comando de consola) y la traduce al slice. |
| Solicitud y respuesta | Estructuras de datos propias del caso de uso. |
| Validación | Reglas sobre la forma y el contenido de la solicitud. |
| Lógica del caso de uso | Orquesta el flujo. Delega las reglas de negocio complejas al dominio cuando existe. |
| Acceso a datos | Lectura y escritura que el slice necesita, sin pasar por una capa genérica. |

Lo que **no** pertenece a un slice son los aspectos transversales: autenticación, autorización, registro de logs, manejo de errores, transacciones y validación común. Se resuelven con un mecanismo compartido que envuelve a todos los slices (ver paso 4 de la implementación).

## Casos de uso recomendados

- **Aplicaciones orientadas a casos de uso o a API**, donde cada endpoint es una operación bien definida.
- **Sistemas con muchas funcionalidades independientes** y poca lógica compartida entre ellas (back offices, portales, sistemas de gestión).
- **Equipos que trabajan en paralelo**, porque cada persona toca una carpeta distinta y se reducen los conflictos de integración.
- **Sistemas que cambian con frecuencia**, donde el costo de modificar una funcionalidad debe ser bajo.
- **Aplicaciones con requisitos de lectura y escritura muy distintos**, por ejemplo consultas con SQL optimizado y comandos con modelo de dominio.
- **Sistemas que podrían dividirse en servicios más adelante.** Un slice bien aislado es un buen candidato para extraer.
- **Reemplazo gradual de un sistema por capas.** Las funcionalidades nuevas se escriben como slices y las antiguas se migran por demanda.

## Limitaciones y riesgos

- **Duplicación de código.** Slices distintos pueden repetir lógica similar. Es un costo aceptado, pero debe vigilarse para que no se vuelva duplicación de reglas de negocio.
- **Inconsistencia entre slices.** Con autonomía total, cada slice puede resolver lo mismo de forma diferente. Se necesitan convenciones mínimas del equipo.
- **Requiere disciplina para evitar dependencias cruzadas.** Si un slice llama directamente a otro, se pierde el aislamiento y se obtiene un acoplamiento difícil de ver.
- **Reglas de negocio compartidas.** Cuando muchas funcionalidades comparten un dominio complejo con invariantes fuertes, el modelo de dominio debe vivir en un lugar común. Un slice no debe reimplementarlo.
- **Curva de adopción.** Los equipos acostumbrados a capas pueden percibir como desorden la ausencia de repositorios y servicios compartidos.
- **No define la estructura global.** Indica cómo organizar el código de cada funcionalidad, pero no resuelve por sí solo los límites de dominio, la integración ni el despliegue.
- **Poco beneficio en sistemas muy pequeños o dominados por CRUD genérico.** Una estructura simple suele bastar.

## Cómo implementarlo (guía abstracta)

### 1. Identificar los casos de uso

Lista las operaciones que el sistema ofrece, nombradas con verbo y objeto: `RegistrarCliente`, `CancelarPedido`, `ConsultarFacturasPendientes`. Cada una será un slice. Clasifícalas como **comando** (cambia estado) o **consulta** (solo lee).

### 2. Definir la estructura de carpetas

Agrupa primero por área de negocio y luego por caso de uso. Todo lo del caso de uso queda junto.

```text
sistema/
  pedidos/
    crear-pedido/
      entrada
      solicitud-respuesta
      validacion
      logica
      acceso-datos
    cancelar-pedido/
      ...
    listar-pedidos/
      ...
  clientes/
    registrar-cliente/
      ...
  compartido/
    infraestructura/      # logging, errores, transacciones, seguridad
    dominio/              # solo reglas realmente comunes
```

Los nombres son ilustrativos. Lo importante es que abrir una carpeta de slice muestre todo el caso de uso.

### 3. Definir el flujo de un slice

Todos los slices siguen el mismo patrón general:

```text
solicitud --> [validar] --> [ejecutar lógica] --> [leer/escribir datos] --> respuesta
```

Ejemplo de `CrearPedido`:

1. La entrada recibe `{cliente, líneas}` y construye la solicitud.
2. La validación comprueba que el cliente exista y que las cantidades sean positivas.
3. La lógica calcula el total y aplica las reglas de negocio.
4. El acceso a datos guarda el pedido.
5. La respuesta devuelve el identificador del pedido creado.

Una consulta como `ListarPedidos` omite la lógica de negocio y lee los datos directamente en la forma que necesita la pantalla.

### 4. Resolver los aspectos transversales

Coloca lo común en un mecanismo que rodee a todos los slices, sin incluirlo en ninguno. Una forma habitual es una **cadena de interceptores** (*pipeline*) que se ejecuta antes y después de cada slice:

```text
solicitud --> [autenticación] --> [validación] --> [transacción] --> [slice] --> [registro de eventos] --> respuesta
```

Así cada slice se concentra en su caso de uso y no repite autenticación, logging ni manejo de errores.

### 5. Establecer reglas de dependencia

- Un slice **puede** depender de `compartido/`.
- Un slice **no debe** depender de otro slice. Si necesita información de otra área, la obtiene a través de una consulta propia, de un contrato público explícito o de un evento.
- `compartido/` **no debe** depender de ningún slice.

Estas reglas pueden verificarse de forma automática con pruebas de arquitectura o con reglas del analizador de dependencias.

### 6. Compartir con criterio

Aplica la **regla de tres**: tolera la duplicación hasta la tercera repetición y recién entonces extrae el código común. Antes de extraer, confirma que ambos usos cambian por la misma razón. Las reglas de negocio con invariantes (por ejemplo, "un pedido pagado no se modifica") van al dominio compartido. Los detalles de presentación o de consulta se quedan en el slice.

### 7. Probar por slice

Prueba cada slice de punta a punta con su solicitud y su respuesta, usando datos reales o un sustituto cercano. Las reglas del dominio común tienen sus propias pruebas unitarias. Esto da una cobertura que sigue al comportamiento del sistema, no a su estructura interna.

### 8. Evolucionar

- Si un slice crece, divídelo en pasos internos sin cambiar su contrato.
- Si varios slices de un área comparten un modelo estable, promuévelo a un módulo de dominio.
- Si un área necesita escalar o desplegarse aparte, extrae sus slices como un servicio.

## Relación con otros estilos

| Estilo | Relación |
|---|---|
| [Arquitectura por capas](capas-n-tier.md) | Alternativa. Organiza por responsabilidad técnica y no por funcionalidad. |
| [CQRS](cqrs.md) | Complementario. Cada slice suele ser un comando o una consulta. |
| [Monolito modular](monolito-modular.md) | Complementario. Los módulos definen los límites y los slices organizan el interior. |
| [Microservicios](microservicios/README.md) | Un slice aislado puede evolucionar a servicio. |
| Clean, Hexagonal u Onion | Se pueden combinar. Se mantiene el dominio en el centro y se organizan los casos de uso como slices. |

## Cuándo elegirlo

Elígelo cuando el cambio frecuente de funcionalidades, el trabajo en paralelo y la baja ceremonia pesen más que la uniformidad estricta. Prefiere capas, o un dominio central fuerte con slices a su alrededor, cuando el sistema tenga un modelo de negocio complejo y compartido, o cuando el equipo aún no pueda sostener las convenciones que Vertical Slice requiere.

## Referencias

- Jimmy Bogard, *Vertical Slice Architecture*: https://www.jimmybogard.com/vertical-slice-architecture/
