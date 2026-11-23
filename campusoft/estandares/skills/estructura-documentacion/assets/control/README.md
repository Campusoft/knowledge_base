# Control de estructura de doc/

Archivos para detectar carpetas de `doc/` que no estén declaradas. Parte de CAMP-DOC-STRUCT-001 v1.0.0, agregada el 2026-11-23 sin cambiar la versión. Ver la sección «Control de cumplimiento» del estándar `campusoft/estandares/documentacion-proyectos/ESTANDAR-ESTRUCTURA-DOCUMENTACION.md`.

## Contenido

| Archivo | Destino en el repositorio del proyecto | Función |
|---|---|---|
| `../doc/.estructura.txt` | `doc/.estructura.txt` | Manifiesto de carpetas permitidas (viene con la estructura) |
| `scripts/validar-estructura.ps1` | `scripts/validar-estructura.ps1` | Validador (PowerShell 5.1 o 7+) |
| `.github/workflows/validar-doc.yml` | `.github/workflows/validar-doc.yml` | Ejecuta el validador en cada push a `main` y en cada PR |
| `.github/CODEOWNERS` | `.github/CODEOWNERS` | Asigna revisor a los cambios del manifiesto, script y workflow |

## Instalación

1. Copiar `assets/doc/` a `doc/` (incluye `.estructura.txt`, que es un archivo oculto).
2. Copiar el contenido de esta carpeta `control/` a la raíz del repositorio.
3. Reemplazar `@ORGANIZACION/equipo-documentacion` en `CODEOWNERS`.
4. Borrar del manifiesto las áreas que el proyecto no use.

## Uso

```powershell
./scripts/validar-estructura.ps1
```

Termina con código 0 si la estructura es válida y con 1 si hay errores.

## Reglas que valida

1. `doc/README.md` y el manifiesto existen.
2. Toda carpeta bajo `doc/` está declarada en el manifiesto.
3. Cada área (carpeta de primer nivel) tiene `README.md`.
4. No hay archivos sueltos en `doc/`, salvo `README.md` y el manifiesto.

## Añadir una carpeta

1. Declararla en `doc/.estructura.txt`.
2. Registrarla como adaptación en `doc/README.md`.
3. Abrir un PR; el CODEOWNER revisa el cambio del manifiesto.

## Alcance

Con GitHub Free y repositorios privados, el workflow marca el PR en rojo pero no impide fusionarlo, y CODEOWNERS asigna revisor sin hacerlo obligatorio. El control detecta; no bloquea. En un plan con protección de ramas, se marca el check `validar` como obligatorio sin cambiar estos archivos.
