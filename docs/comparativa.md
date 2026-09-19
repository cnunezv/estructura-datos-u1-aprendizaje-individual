# Comparativa: struct/record frente a objetos

## Tabla exigida por el punto 5

| Criterio | Struct / Record | Objeto (clase) |
| --- | --- | --- |
| **Definición** | Agrupación de datos relacionados, sin comportamiento propio. Se define por los campos que contiene. | Entidad con estado encapsulado y comportamiento. Se define por lo que sabe hacer, no solo por lo que guarda. |
| **Mutabilidad** | El `struct` de C# es mutable si sus campos son públicos; el `record` es inmutable y se "modifica" con `with`. En Python, `@dataclass` es mutable y `NamedTuple` o `frozen=True` son inmutables. | Mutable por diseño, pero el cambio pasa por métodos que pueden validar (`SetPromedio`). |
| **Tipado** | Estático y verificado al compilar en C#; anotaciones opcionales no verificadas en tiempo de ejecución en Python. | Igual en ambos lenguajes, con la diferencia de que la clase puede imponer invariantes que el tipo por sí solo no expresa. |
| **Uso en memoria** | El `struct` de C# es tipo de valor: vive en la pila o incrustado en su contenedor, y se copia al asignarlo. El `record` de C# y todos los registros de Python son tipos de referencia: viven en el heap. | Siempre tipo de referencia: el objeto vive en el heap y la variable guarda la dirección. |
| **Igualdad** | El `record` y el `NamedTuple` comparan por valor: dos instancias con los mismos datos son iguales. | Compara por referencia salvo que se redefina `Equals` / `__eq__`. |
| **Ejemplo estático** | C#: `public struct EstudianteStruct { ... }` y `public record EstudianteRecord(string Nombre, int Edad, double Promedio);` | C#: `public class Estudiante { private double promedio; public void SetPromedio(double p) { ... } }` |
| **Ejemplo dinámico** | Python: `@dataclass class EstudianteStruct` y `class EstudianteRecord(NamedTuple)` | Python: `class Estudiante` con `mostrar_info()` y `set_promedio()` |

## Diferencia de fondo entre C# y Python

C# distingue tipos de valor y tipos de referencia; Python no. En C#, pasar un `struct` a un método entrega una copia, de modo que el original no puede cambiar. En Python toda variable guarda una referencia, así que pasar un `@dataclass` mutable a una función sí permite modificarlo. La única forma de impedir el cambio en Python es declarar el registro inmutable.

El programa demuestra esta diferencia ejecutando el mismo experimento en los dos lenguajes:

```text
C#      Antes   - struct: 3.00   objeto: 3.00
        Despues - struct: 3.00   objeto: 5.00     <- el struct NO cambió

Python  Antes   - registro: 3.00   objeto: 3.00
        Despues - registro: 5.00   objeto: 5.00   <- los DOS cambiaron
```

## Cuándo usar cada uno

Un registro conviene cuando el dato no tiene identidad propia y se define por completo por sus valores: un punto, una fecha, un resultado de cálculo. Dos resultados con los mismos números son el mismo resultado, y por eso la igualdad por valor es la correcta.

Un objeto conviene cuando la entidad tiene identidad e invariantes que hay que sostener: dos estudiantes llamados igual siguen siendo personas distintas, y el promedio debe mantenerse entre 0.0 y 5.0 pase lo que pase. Esa garantía solo puede darla un método que valide antes de asignar.
