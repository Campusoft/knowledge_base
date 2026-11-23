<#
.SYNOPSIS
  Valida que las carpetas de doc/ correspondan a las declaradas en el manifiesto.
  Estandar: CAMP-DOC-STRUCT-001 (seccion Control de cumplimiento).

.DESCRIPTION
  Reglas:
    1. doc/ contiene README.md y el manifiesto.
    2. Toda carpeta bajo doc/ esta declarada en el manifiesto.
    3. Cada area (carpeta de primer nivel) tiene su README.md.
    4. No hay archivos sueltos en doc/, salvo README.md y el manifiesto.

  Manifiesto (doc/.estructura.txt): una ruta por linea, relativa a doc/.
    - Lineas vacias y las que empiezan con # se ignoran.
    - Una ruta con * admite comodin. Una entrada "a/b/*" admite tambien "a/b".
  Declarar una carpeta no la hace obligatoria; solo la permite.

  Compatible con Windows PowerShell 5.1 y PowerShell 7+. Codigo de salida 1 si hay errores.

.PARAMETER RutaDoc
  Carpeta de documentacion. Por defecto: doc

.PARAMETER Manifiesto
  Ruta del manifiesto. Por defecto: <RutaDoc>/.estructura.txt
#>
[CmdletBinding()]
param(
    [string]$RutaDoc = 'doc',
    [string]$Manifiesto = ''
)

$ErrorActionPreference = 'Stop'
$errores = New-Object System.Collections.Generic.List[string]

if (-not (Test-Path -LiteralPath $RutaDoc -PathType Container)) {
    Write-Host "ERROR: no existe la carpeta '$RutaDoc'."
    exit 1
}
$raiz = (Resolve-Path -LiteralPath $RutaDoc).Path.TrimEnd('\', '/')
if (-not $Manifiesto) { $Manifiesto = Join-Path $raiz '.estructura.txt' }
$nombreManifiesto = Split-Path -Leaf $Manifiesto

# Regla 1: archivos base
if (-not (Test-Path -LiteralPath (Join-Path $raiz 'README.md') -PathType Leaf)) {
    $errores.Add("Falta doc/README.md (indice de la documentacion).")
}
if (-not (Test-Path -LiteralPath $Manifiesto -PathType Leaf)) {
    Write-Host "ERROR: falta el manifiesto '$Manifiesto'."
    exit 1
}

# Lectura del manifiesto
$permitidas = New-Object System.Collections.Generic.List[string]
foreach ($linea in (Get-Content -LiteralPath $Manifiesto)) {
    $e = $linea.Trim()
    if ($e -eq '' -or $e.StartsWith('#')) { continue }
    $e = $e.Replace('\', '/').Trim('/')
    $permitidas.Add($e)
    if ($e.EndsWith('/*')) { $permitidas.Add($e.Substring(0, $e.Length - 2)) }
}

function Test-Permitida([string]$ruta) {
    foreach ($p in $permitidas) {
        if ($p.Contains('*')) { if ($ruta -clike $p) { return $true } }
        elseif ($ruta -ceq $p) { return $true }
    }
    return $false
}

# Regla 2: carpetas declaradas
function Test-Carpetas([string]$dir, [string]$prefijo) {
    foreach ($c in (Get-ChildItem -LiteralPath $dir -Directory -Force)) {
        $rel = $prefijo + $c.Name
        if (-not (Test-Permitida $rel)) {
            $errores.Add("Carpeta no declarada en $nombreManifiesto : doc/$rel")
        }
        Test-Carpetas $c.FullName ($rel + '/')
    }
}
Test-Carpetas $raiz ''

# Regla 3: README.md en cada area
foreach ($area in (Get-ChildItem -LiteralPath $raiz -Directory -Force)) {
    if (-not (Test-Path -LiteralPath (Join-Path $area.FullName 'README.md') -PathType Leaf)) {
        $errores.Add("Falta README.md en el area: doc/$($area.Name)")
    }
}

# Regla 4: sin archivos sueltos
foreach ($f in (Get-ChildItem -LiteralPath $raiz -File -Force)) {
    if ($f.Name -cne 'README.md' -and $f.Name -cne $nombreManifiesto) {
        $errores.Add("Archivo suelto en la raiz de doc/: doc/$($f.Name)")
    }
}

if ($errores.Count -gt 0) {
    foreach ($m in $errores) {
        if ($env:GITHUB_ACTIONS -eq 'true') { Write-Host "::error::$m" } else { Write-Host "ERROR: $m" }
    }
    Write-Host ""
    Write-Host "Estructura de doc/ no valida: $($errores.Count) error(es)."
    Write-Host "Si la carpeta es necesaria, declarala en $nombreManifiesto y registrala como adaptacion en doc/README.md."
    exit 1
}

Write-Host "Estructura de doc/ valida."
exit 0
