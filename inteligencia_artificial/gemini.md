# Gemini

## Modelos de Gemini para generar Embeddings

Los modelos de embeddings de Gemini convierten contenido en vectores numericos que capturan significado semantico. Estos vectores se usan para busqueda semantica, RAG, clasificacion, clustering, deteccion de similitud y comparacion entre contenido textual o multimodal.

### Modelos disponibles

| Modelo | Tipo de entrada | Dimension de salida | Limite de entrada | Uso recomendado |
| --- | --- | --- | --- | --- |
| `gemini-embedding-2` | Texto, imagen, video, audio y PDF | Flexible: 128 a 3072 dimensiones. Recomendadas: 768, 1536 o 3072 | 8,192 tokens | Modelo principal para nuevos proyectos, especialmente busqueda multimodal o RAG moderno. |
| `gemini-embedding-001` | Texto | Flexible: 128 a 3072 dimensiones. Recomendadas: 768, 1536 o 3072 | 2,048 tokens | Proyectos solo texto que ya lo usan o integraciones donde no se necesita soporte multimodal. |

### Recomendaciones de uso

- Usar `gemini-embedding-2` para desarrollos nuevos, porque permite representar texto, imagenes, audio, video y PDF en un espacio vectorial unificado.
- Usar 768 dimensiones cuando se quiere menor costo de almacenamiento y buena calidad para busqueda semantica.
- Usar 1536 o 3072 dimensiones cuando se prioriza calidad de recuperacion y se dispone de mas almacenamiento en la base vectorial.
- No mezclar embeddings generados con `gemini-embedding-001` y `gemini-embedding-2`: sus espacios vectoriales son incompatibles. Si se migra de un modelo a otro, se debe regenerar todo el indice vectorial.
- Para RAG, generar embeddings separados para documentos y consultas, almacenar los vectores de documentos en una base vectorial y recuperar los fragmentos mas similares antes de llamar al modelo generativo.

### Ejemplo con Python

```python
from google import genai
from google.genai import types

client = genai.Client()

result = client.models.embed_content(
    model="gemini-embedding-2",
    contents="Texto a convertir en embedding",
    config=types.EmbedContentConfig(output_dimensionality=768),
)

embedding = result.embeddings[0].values
print(len(embedding))
```

### Ejemplo con C# .NET 8/10

Instalar el SDK oficial:

```bash
dotnet add package Google.GenAI
```

Ejemplo de `Program.cs`:

```csharp
using Google.GenAI;
using Google.GenAI.Types;

var apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY")
    ?? Environment.GetEnvironmentVariable("GOOGLE_API_KEY")
    ?? throw new InvalidOperationException("Configura GEMINI_API_KEY o GOOGLE_API_KEY.");

var client = new Client(apiKey: apiKey);

var response = await client.Models.EmbedContentAsync(
    model: "gemini-embedding-2",
    contents: "Texto a convertir en embedding",
    config: new EmbedContentConfig
    {
        OutputDimensionality = 768
    }
);

var embedding = response.Embeddings[0].Values;

Console.WriteLine($"Dimensiones: {embedding.Count}");
Console.WriteLine(string.Join(", ", embedding.Take(5)));
```

### Ejemplo con REST

```bash
curl "https://generativelanguage.googleapis.com/v1beta/models/gemini-embedding-2:embedContent" \
  -H "Content-Type: application/json" \
  -H "x-goog-api-key: ${GEMINI_API_KEY}" \
  -d '{
    "content": {
      "parts": [
        { "text": "Texto a convertir en embedding" }
      ]
    },
    "output_dimensionality": 768
  }'
```

Fuente oficial: <https://ai.google.dev/gemini-api/docs/embeddings>

