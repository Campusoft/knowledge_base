

# ISO/IEC 11770


La ISO/IEC 11770 es una serie de normas internacionales que definen los mecanismos, protocolos y requisitos para la gestión de claves criptográficas en sistemas de información.

Es un conjunto de estándares elaborado por:

- ISO (International Organization for Standardization)
- IEC (International Electrotechnical Commission)

Su propósito es asegurar que la generación, distribución, almacenamiento, uso y destrucción de claves criptográficas se realice de manera segura, interoperable y estandarizada.


Forma parte de la serie:

- ISO/IEC 11770-1 — General
- ISO/IEC 11770-2 — Basado en claves simétricas
- ISO/IEC 11770-3 — Basado en claves públicas
- ISO/IEC 11770-4 — Técnicas no basadas en claves
- ISO/IEC 11770-5 — Secret Sharing


# Resumen Ejecutivo — ISO/IEC 11770: Gestión de Claves Criptográficas

La **ISO/IEC 11770** es una serie de normas internacionales que establece **métodos, protocolos y requisitos técnicos para la gestión segura de claves criptográficas** a lo largo de todo su ciclo de vida. Forma parte del conjunto de estándares ISO/IEC relacionados con la seguridad de la información, usados globalmente en sectores como banca, gobierno, telecomunicaciones, salud y sistemas distribuidos de alta seguridad.

---

## 1. Objetivo General
Proporcionar un marco estandarizado para:
- Generar claves criptográficas.
- Distribuir y acordar claves entre entidades.
- Almacenar, usar, actualizar y destruir claves de forma segura.
- Reducir riesgos de compromisos mediante técnicas probadas y formalizadas.

La norma garantiza que los sistemas implementen **gestión de claves robusta, interoperable y basada en buenas prácticas reconocidas internacionalmente**.

---

## 2. Estructura y Alcance de la Norma

La serie consta de **cinco partes principales**, cada una cubriendo un aspecto específico de los mecanismos de gestión de claves:

### **11770-1 — Generalidades**
Define conceptos, objetivos de seguridad, términos y modelos base para todos los mecanismos de gestión de claves.

### **11770-2 — Mecanismos basados en criptografía simétrica**
Incluye protocolos de distribución, transporte y actualización de claves utilizando algoritmos simétricos (por ejemplo AES). Típico en ambientes controlados como servidores internos y KDC (Key Distribution Centers).

### **11770-3 — Mecanismos basados en criptografía asimétrica**
Cubre protocolos que emplean criptografía de clave pública, como RSA, Diffie–Hellman y curvas elípticas (ECDH). Esta sección es fundamental para sistemas modernos como TLS/HTTPS, VPN y autenticación distribuida.

### **11770-4 — Mecanismos sin el uso de criptografía**
Abarca métodos operativos o físicos para el intercambio de claves, como canales fuera de banda, dispositivos seguros, métodos manuales o protocolos de transferencia física.

### **11770-5 — Compartición de secretos**
Define esquemas de reparto de secretos (Secret Sharing), como el método de Shamir, incluyendo configuraciones umbral (k, n), verificación de shares y recuperación distribuida, esenciales en entornos multiusuario y sistemas de custodia de claves.

---

## 3. Importancia para la Seguridad de la Información

Implementar ISO/IEC 11770 permite:

- Reducir fallos en el manejo de claves, una causa común de vulnerabilidades.
- Asegurar que los mecanismos usados cumplen **estándares internacionales de robustez**.
- Establecer procesos confiables para infraestructura PKI, módulos de seguridad (HSM), sistemas financieros, blockchain y redes corporativas.
- Mejorar la interoperabilidad entre productos y servicios de distintos proveedores.

---

## 4. Conclusión

La serie **ISO/IEC 11770** es un pilar fundamental en la gestión moderna de claves criptográficas. Su adopción permite a las organizaciones garantizar **confidencialidad, integridad, disponibilidad y confianza** en los sistemas que dependen de claves para proteger información crítica. Su enfoque modular y completo brinda lineamientos sólidos para cualquier entorno que requiera una gestión de claves segura y estandarizada.
