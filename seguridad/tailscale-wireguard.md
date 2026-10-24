# Tailscale y WireGuard

> Documento revisado: 2026-10-22
> Productos documentados: [Tailscale](https://tailscale.com/) y [WireGuard](https://www.wireguard.com/)

## Descripción

Tailscale y WireGuard resuelven el mismo problema —crear una red privada entre máquinas que están en redes distintas— con enfoques diferentes.

- **WireGuard** es el protocolo y la implementación de VPN. Es código abierto, ligero y usa criptografía moderna (Curve25519, ChaCha20-Poly1305). Tú defines los pares, las claves y el direccionamiento.
- **Tailscale** es una capa de gestión construida **sobre** WireGuard. Añade autenticación con identidades, NAT traversal automático, DNS interno (MagicDNS), ACLs y una consola de administración. El plano de datos sigue siendo WireGuard.

En corto: WireGuard te da el túnel; Tailscale te da ese túnel funcionando entre máquinas que no pueden alcanzarse directamente, sin que tengas que mantener servidores ni reglas de red.

```
Sin VPN:
  laptop (192.168.1.10) ──X── VPS (203.0.113.10)

Con Tailscale (malla):
  laptop ──WireGuard──┐
                      ├── VPS
  móvil  ──WireGuard──┘
  (100.x.y.z, NAT traversal o relay DERP cuando no hay ruta directa)

Con WireGuard propio (hub-and-spoke):
  laptop ──UDP 51820──> VPS con IP pública ──> red interna
```

## Cuándo usar cada uno

| Necesidad | Tailscale | WireGuard puro |
|-----------|-----------|----------------|
| Conectar equipos y servidores en minutos | Sí, sin tocar el router | Requiere IP pública/puerto y configuración manual |
| Evitar exponer SSH a Internet | Sí, con ACLs y Tailscale SSH | Sí, pero tú gestionas claves y reglas |
| Control total de la infraestructura | Parcial (o Headscale autoalojado) | Total |
| NAT traversal entre clientes | Automático (incluye relays DERP) | No; necesitas un endpoint alcanzable |
| DNS interno | MagicDNS integrado | Configuración manual |
| Equipos pequeños o uso personal | Plan gratuito suficiente | VPS propio + mantenimiento |
| Cumplimiento y auditoría estricta | Planes de pago con logs | Depende de tu infraestructura |

Recomendación práctica: **empieza con Tailscale** para acceso administrativo y pruebas. **Pasa a WireGuard** si necesitas control completo, no quieres depender de un servicio de coordinación o debes integrarlo con infraestructura existente.

## Requisitos

- Un VPS o servidor con IP pública para WireGuard (Tailscale no lo necesita).
- Acceso SSH al servidor y permisos de administrador.
- Cliente en cada equipo: Linux, Windows, macOS, Android o iOS.
- Puertos: WireGuard usa **UDP 51820** por defecto; Tailscale usa **UDP 41641** y HTTPS saliente (443) para la coordinación.

## Tailscale

### Instalación

```bash
# Linux (script oficial)
curl -fsSL https://tailscale.com/install.sh | sh

# Debian / Ubuntu desde el repositorio
curl -fsSL https://pkgs.tailscale.com/stable/ubuntu/noble.noarmor.gpg \
  | sudo tee /usr/share/keyrings/tailscale-archive-keyring.gpg >/dev/null
curl -fsSL https://pkgs.tailscale.com/stable/ubuntu/noble.tailscale-keyring.list \
  | sudo tee /etc/apt/sources.list.d/tailscale.list
sudo apt update && sudo apt install -y tailscale
```

En Windows y macOS se instala desde el paquete oficial o la tienda de aplicaciones; en móviles, desde Google Play o App Store. También existe imagen oficial de Docker.

### Primer arranque

```bash
sudo tailscale up          # abre una URL para autenticarte
tailscale status           # equipos de la tailnet y su estado
tailscale ip -4            # IP 100.x.y.z de este equipo
```

Opcionalmente, permite que tu usuario use `tailscale` sin `sudo`:

```bash
sudo tailscale set --operator=$USER
```

Si el servidor es desatendido, usa una **auth key** creada en la consola (Settings → Keys):

```bash
sudo tailscale up --auth-key=tskey-auth-XXXX --hostname=vps-agente
```

### Comandos habituales

| Acción | Comando |
|--------|---------|
| Ver estado | `tailscale status` |
| Ver IP asignada | `tailscale ip -4` |
| Probar conectividad | `tailscale ping otro-equipo` |
| Diagnosticar NAT | `tailscale netcheck` |
| Desconectar sin cerrar sesión | `tailscale down` |
| Reconectar | `tailscale up` |
| Cerrar sesión | `tailscale logout` |
| Ver versión | `tailscale version` |

### Casos de uso

**Nodo de salida (exit node).** Todo el tráfico del cliente sale por el VPS:

```bash
# En el VPS
sudo tailscale up --advertise-exit-node
# Aprobar la ruta en la consola: Machines → equipo → Edit route settings

# En el cliente
sudo tailscale up --exit-node=100.x.y.z --exit-node-allow-lan-access
```

**Router de subred.** Publica una red interna sin instalar Tailscale en cada máquina:

```bash
sudo tailscale up --advertise-routes=192.168.1.0/24
# Aprobar la ruta en la consola

# En el cliente que debe alcanzarla
sudo tailscale up --accept-routes
```

**SSH por la tailnet.** Evita abrir el puerto 22 a Internet:

```bash
sudo tailscale up --ssh
ssh usuario@vps-agente    # con MagicDNS activado
```

Tailscale SSH exige una regla `ssh` en la política de acceso; sin ella, la conexión se rechaza aunque el equipo esté en la tailnet. El SSH normal por la IP `100.x.y.z` sigue funcionando si el servidor tiene `sshd` escuchando.

**Servir una interfaz web sin abrir puertos.** `serve` solo expone dentro de la tailnet; `funnel` la publica en Internet y debe evitarse salvo necesidad justificada:

```bash
tailscale serve --bg 8080        # accesible solo desde la tailnet
tailscale serve status
# tailscale funnel --bg 8080     # público: úsalo con precaución
```

### ACLs

Las ACLs se definen en la consola (Access controls) y permiten limitar qué equipo accede a qué puerto. Ejemplo mínimo con etiquetas:

```json
{
  "tagOwners": {
    "tag:server": ["autogroup:admin"]
  },
  "acls": [
    {
      "action": "accept",
      "src": ["autogroup:member"],
      "dst": ["tag:server:22"]
    }
  ]
}
```

Asigna la etiqueta al servidor con `sudo tailscale up --advertise-tags=tag:server`. Para uso personal o equipos pequeños, la política por defecto (todos contra todos) es aceptable; en cuanto haya servidores compartidos, conviene restringir.

### Persistencia y claves

- En Linux, el servicio `tailscaled` se instala habilitado con systemd.
- Las claves de los dispositivos **caducan** por defecto. En servidores permanentes se puede desactivar la caducidad desde la consola, asumiendo el riesgo.
- Genera auth keys distintas por entorno (servidor, CI, portátil) y usa claves efímeras para contenedores o runners.

## WireGuard

### Instalación

```bash
# Debian / Ubuntu
sudo apt update && sudo apt install -y wireguard qrencode

# RHEL / Fedora
sudo dnf install -y wireguard-tools qrencode

# Arch
sudo pacman -S wireguard-tools qrencode
```

En Windows y macOS hay cliente oficial; en móviles, aplicación oficial que importa el `.conf` o un QR.

### Generar claves

```bash
umask 077
wg genkey | tee servidor.key | wg pubkey > servidor.pub
wg genkey | tee cliente.key  | wg pubkey > cliente.pub
```

Cada par debe tener su propia clave. La privada **nunca** se comparte ni se sube al repositorio.

### Configurar el servidor

`/etc/wireguard/wg0.conf`:

```ini
[Interface]
Address = 10.8.0.1/24
ListenPort = 51820
PrivateKey = <clave-privada-del-servidor>

[Peer]
# portátil
PublicKey = <clave-publica-del-cliente>
AllowedIPs = 10.8.0.2/32
```

### Configurar el cliente

```ini
[Interface]
Address = 10.8.0.2/32
PrivateKey = <clave-privada-del-cliente>
DNS = 10.8.0.1

[Peer]
PublicKey = <clave-publica-del-servidor>
Endpoint = vps.ejemplo.com:51820
AllowedIPs = 10.8.0.0/24
PersistentKeepalive = 25
```

- `AllowedIPs = 10.8.0.0/24` → **split tunnel**: solo el tráfico de la VPN pasa por el túnel.
- `AllowedIPs = 0.0.0.0/0, ::/0` → **full tunnel**: todo el tráfico sale por el servidor; requiere NAT y `ip_forward` en el servidor.
- `PersistentKeepalive = 25` mantiene vivo el NAT del cliente.

### Levantar y persistir

```bash
sudo wg-quick up wg0
sudo wg-quick down wg0
sudo systemctl enable --now wg-quick@wg0
wg show
```

Abre el puerto en el firewall y en el grupo de seguridad del proveedor cloud:

```bash
sudo ufw allow 51820/udp
```

Si el servidor hará de puerta a Internet (full tunnel o subred), habilita el reenvío y el NAT:

```bash
echo 'net.ipv4.ip_forward=1' | sudo tee /etc/sysctl.d/99-wireguard.conf
sudo sysctl --system
```

```ini
# Añadir al [Interface] del servidor, ajustando la interfaz de salida
PostUp   = iptables -A FORWARD -i %i -j ACCEPT; iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE
PostDown = iptables -D FORWARD -i %i -j ACCEPT; iptables -t nat -D POSTROUTING -o eth0 -j MASQUERADE
```

La interfaz (`eth0`, `ens3`, `enp0s3`…) se obtiene con `ip route show default`.

### Añadir o revocar pares

```bash
# Añadir en caliente
sudo wg set wg0 peer <clave-publica> allowed-ips 10.8.0.3/32
sudo wg-quick save wg0        # solo si SaveConfig = true

# Alternativa: editar wg0.conf y recargar
sudo systemctl restart wg-quick@wg0

# Revocar
sudo wg set wg0 peer <clave-publica> remove
```

Genera el QR para móviles:

```bash
qrencode -t ansiutf8 < cliente.conf
```

### Diagnóstico

`wg show` muestra el último handshake y los bytes transferidos por par. Si `latest handshake` está vacío, el túnel no se ha establecido:

- Endpoint mal escrito o DNS que no resuelve.
- UDP 51820 bloqueado en firewall, NAT del proveedor o red del cliente.
- Claves intercambiadas de forma incorrecta.

Si hay handshake pero no pasa tráfico: revisa `AllowedIPs`, `ip_forward`, reglas NAT y MTU.

## Seguridad operativa

- Usa Tailscale o WireGuard en lugar de exponer SSH y paneles administrativos a Internet.
- Con Tailscale, aplica **mínimo privilegio** en las ACLs y separa servidores con etiquetas.
- Con WireGuard, una clave por par, permisos `600`, rotación cuando un equipo se pierde y revisión periódica de `wg show`.
- No publiques con `tailscale funnel` lo que solo debe verse en la tailnet.
- No guardes claves privadas ni archivos `.conf` en el repositorio; usa un gestor de secretos.
- En el VPS, mantén actualizado el sistema, limita el acceso por usuario y revisa los logs.
- Considera **Headscale** si quieres la experiencia de Tailscale con coordinación autoalojada.
- Vigila quién puede aprobar rutas, exit nodes y etiquetas: son cambios con impacto de red.

## Integración con agentes remotos en VPS

Este documento complementa la guía de [agentes IA remotos en VPS](../inteligencia_artificial/multi-agente/agentes-remotos/README.md). El patrón habitual es:

1. Instalar Tailscale en el VPS y en tu equipo.
2. Desactivar el acceso SSH público (o restringirlo al proveedor).
3. Conectarte por la IP `100.x.y.z` o por MagicDNS.
4. Ejecutar tmux y OpenCode dentro de esa conexión privada.

Con WireGuard, el VPS actúa como servidor y tú importas un `.conf` por equipo; el resto del flujo es idéntico.

## Solución de problemas

| Síntoma | Causa probable | Solución |
|---------|----------------|----------|
| Tailscale no aparece conectado | Sesión caducada o sin autenticar | `tailscale status`, luego `tailscale up` |
| `tailscale ping` falla pero `ping` normal funciona | ACL, equipo offline o ruta bloqueada | Revisa estado del equipo y ACLs en la consola |
| No resuelven los nombres | MagicDNS desactivado o `--accept-dns=false` | Activa MagicDNS y revisa la configuración DNS del cliente |
| No se alcanza una subred | Ruta no aprobada o cliente sin `--accept-routes` | Aprueba la ruta y activa `--accept-routes` |
| El exit node no enruta | Nodo no aprobado o cliente sin `--exit-node` | Revisa la consola y `tailscale status` |
| WireGuard sin handshake | UDP 51820 bloqueado, endpoint o claves incorrectas | `wg show`, abre el puerto, verifica claves |
| Handshake correcto pero sin tráfico | `AllowedIPs`, `ip_forward` o NAT mal configurados | Revisa rutas, sysctl y reglas `PostUp` |
| La conexión se corta o va lenta | MTU incorrecta | Añade `MTU = 1380` (o 1280) en el `[Interface]` |
| Sin DNS en full tunnel | El cliente no usa el DNS del servidor | Añade `DNS = 10.8.0.1` y un resolver en el servidor |
| El servidor no arranca el túnel | Configuración inválida o interfaz ocupada | `journalctl -u wg-quick@wg0`, `wg-quick strip wg0` |

## Checklist

- [ ] Tailscale instalado y autenticado en VPS y equipos, o WireGuard configurado con claves propias.
- [ ] SSH público cerrado o restringido; acceso por tailnet o VPN.
- [ ] ACLs o `AllowedIPs` con mínimo privilegio.
- [ ] Claves privadas fuera del repositorio y con permisos `600`.
- [ ] Servicio habilitado al arranque (`tailscaled` o `wg-quick@wg0`).
- [ ] Rutas, exit nodes o subredes aprobadas y verificadas.
- [ ] DNS funcionando (MagicDNS o `DNS =` en el cliente).
- [ ] Firewall y grupo de seguridad del cloud con solo los puertos necesarios.
- [ ] Prueba de reconexión tras reiniciar el VPS.
- [ ] Procedimiento de revocación de un equipo documentado.

## Ver también

- [Agentes IA remotos en VPS](../inteligencia_artificial/multi-agente/agentes-remotos/README.md): modelo conceptual y capas de acceso.
- [tmux + OpenCode en un VPS](../inteligencia_artificial/multi-agente/agentes-remotos/tmux-opencode.md): sesiones persistentes sobre el acceso privado.
- [tmux](../sistema_operativo/linux/tmux.md): multiplexor usado en el VPS.
- [Cloudflare Tunnel](../cloud/Cloudflare/Cloudflare-Tunnel.md): alternativa para publicar un servicio sin abrir puertos.
- [SSH](../cloud/SSH.md): acceso remoto y buenas prácticas.

## Referencias

- Tailscale: <https://tailscale.com/>
- Documentación de Tailscale: <https://tailscale.com/kb/>
- ACLs de Tailscale: <https://tailscale.com/kb/1018/acls>
- Exit nodes: <https://tailscale.com/kb/1103/exit-nodes>
- Subnet routers: <https://tailscale.com/kb/1019/subnets>
- WireGuard: <https://www.wireguard.com/>
- WireGuard quickstart: <https://www.wireguard.com/quickstart/>
- `wg-quick`: <https://man7.org/linux/man-pages/man8/wg-quick.8.html>
- Headscale (coordinación autoalojada): <https://github.com/juanfont/headscale>
