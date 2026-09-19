# Estructura de Datos — Unidad 1 — Actividad de Aprendizaje Individual

Struct, record y objetos aplicados al ejercicio 14 del Protocolo Individual (Analizador de Secuencias Numéricas), implementados en **C#** y **Python**.

**Autor:** Carlos Andrés Núñez Vargas
**Tutor:** John Carlos Arrieta Arrieta
**Asignatura:** IX24453 — Estructura de Datos

El enunciado indica que Java es opcional y que la calificación mejora si se usa otro de los lenguajes listados, por lo que se eligieron dos: C#, que distingue `struct`, `record` y `class` como tres construcciones separadas, y Python, que aporta el contraste de un lenguaje dinámico sin tipos de valor.

## Estructura

```text
estructura-datos-u1-aprendizaje-individual/
├── csharp/
│   ├── AprendizajeIndividual.csproj
│   ├── Program.cs                 menú
│   ├── Modelos.cs                 EstudianteStruct, EstudianteRecord, Estudiante
│   ├── DemoRegistros.cs           puntos 3, 4 y 5
│   ├── Secuencias.cs              Orden, SecuenciaRecord, ResultadoAnalisis, LoteSecuencias
│   ├── AnalizadorSecuencias.cs    ejercicio 14 como objeto con estado
│   └── DemoPractica.cs            actividad práctica
├── python/
│   ├── main.py                    menú
│   ├── modelos.py                 EstudianteStruct, EstudianteRecord, Estudiante
│   ├── demo_registros.py          puntos 3, 4 y 5
│   ├── secuencias.py              Orden, SecuenciaRecord, ResultadoAnalisis, LoteSecuencias
│   └── analizador.py              ejercicio 14 como objeto + actividad práctica
└── docs/
    └── comparativa.md             tabla comparativa del punto 5
```

## Cómo ejecutar

### C# (requiere .NET 8 SDK)

```powershell
cd csharp
dotnet run
```

### Python (requiere Python 3.8 o superior)

```powershell
cd python
python main.py
```

Ambos programas presentan el mismo menú: los puntos 3, 4 y 5 por separado, la actividad práctica, y una opción que ejecuta todo de corrido.

## Qué demuestra cada punto

| Punto | Qué se muestra |
| --- | --- |
| 3 — Struct y record | Tres instancias guardadas en un arreglo, recorrido, y cambio del promedio de un estudiante específico. En C# el `struct` se modifica por índice (nunca desde el `foreach`) y el `record` se sustituye con `with`; en Python el `@dataclass` se modifica en sitio y el `NamedTuple` con `_replace`. |
| 4 — Objetos | Clase `Estudiante` con `MostrarInfo` y `SetPromedio`. El método valida el rango 0.0–5.0 y rechaza un promedio de 9.99, algo que un registro de campos públicos no puede impedir. |
| 5 — Diferencias | El mismo dato se pasa a una función que intenta subir el promedio a 5.00. En C# el `struct` no cambia y el objeto sí; en Python cambian los dos, porque no existen tipos de valor. |
| Práctica | El ejercicio 14 reescrito: las secuencias son registros inmutables guardados en un arreglo, el resultado del análisis es un registro, y el analizador es un objeto con estado. Se incluye un arreglo de objetos `LoteSecuencias` cuyo campo es una matriz irregular, según el mini-proyecto integrador sugerido. |

## Nota sobre el ejercicio 14

La detección de progresión geométrica no divide en punto flotante: usa multiplicación cruzada sobre enteros.

```text
la secuencia es geométrica  ⟺  s[i-1]² == s[i-2] · s[i]  para todo i, sin términos en cero
```

Así la secuencia `81, 27, 9, 3` se reconoce correctamente y la razón se informa como `1/3` en lugar de `0.3333333333333333`.
