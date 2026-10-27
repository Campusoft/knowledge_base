# Plantilla de ficha de componente

Usar esta plantilla para documentar un componente en el catálogo o en un archivo propio cuando requiera más espacio. Mantener la descripción introductoria concisa y enlazar la información profunda en su categoría temática.

## Información recomendada

1. **Nombre y categoría:** nombre habitual, siglas y grupo del catálogo.
2. **Descripción:** qué es y cuál es su responsabilidad principal.
3. **Problema que resuelve:** necesidad concreta que atiende.
4. **Casos de uso:** situaciones donde aporta valor y señales para considerarlo.
5. **Cuándo evitarlo:** casos en que añade costo, complejidad o duplicación innecesaria.
6. **Ubicación típica:** dónde aparece en la arquitectura y qué componentes lo rodean.
7. **Recomendaciones:** decisiones de diseño y operación que conviene considerar, sin entrar en instrucciones específicas de un proveedor.
8. **Ventajas y límites:** beneficios esperados, riesgos, responsabilidades que no cubre y dependencias.
9. **Alternativas y componentes relacionados:** diferencias funcionales relevantes y enlaces a fichas relacionadas.
10. **Documentación detallada:** enlaces a la carpeta temática (seguridad, datos, cloud, comunicación, operación u otra) y a fuentes autorizadas.
11. **Diagrama o flujo:** incluir solo cuando ayude a entender ubicación o interacciones.

## Plantilla para copiar

```markdown
# Nombre del componente

**Categoría:**

## Descripción
Qué es y qué responsabilidad cumple.

## Problema que resuelve
Necesidad que atiende.

## Casos de uso
- Cuándo considerarlo.

## Cuándo evitarlo
- Situaciones en que no aporta valor o duplica capacidades existentes.

## Ubicación y relaciones
Dónde aparece y con qué otros componentes interactúa.

## Recomendaciones
- Aspectos de diseño u operación que conviene evaluar.

## Ventajas y límites
- Ventajas:
- Límites:

## Alternativas y componentes relacionados
- Componente relacionado — diferencia o relación.

## Documentación detallada
- Tema especializado: `ruta/al/documento.md`

## Diagrama
Opcional.
```

En el catálogo general, resumir descripción, casos de uso, recomendación y detalle relacionado en una fila. Crear una ficha independiente solo si el componente necesita explicaciones que dificultan la consulta rápida.
