"""Puntos 3, 4 y 5 del enunciado: registros, objetos y sus diferencias."""
from modelos import Estudiante, EstudianteRecord, EstudianteStruct


# ---------- Punto 3: registro mutable e inmutable ----------
def demostrar_struct_y_record() -> None:
    print()
    print("=== PUNTO 3: REGISTRO MUTABLE (dataclass) E INMUTABLE (NamedTuple) ===")

    registros = [
        EstudianteStruct("Ana", 19, 4.20),
        EstudianteStruct("Bruno", 21, 3.45),
        EstudianteStruct("Carla", 20, 4.80),
    ]

    print()
    print("-- dataclass mutable: recorrido de la lista --")
    for e in registros:
        print("  " + str(e))

    # Modificacion: al ser mutable, se asigna el campo directamente. La variable
    # del for es una referencia al mismo objeto, no una copia.
    print()
    print("-- dataclass mutable: cambiar el promedio de Bruno a 3.90 --")
    for e in registros:
        if e.nombre == "Bruno":
            e.promedio = 3.90
    for e in registros:
        print("  " + str(e))

    # ---------- NamedTuple: inmutable ----------
    inmutables = [
        EstudianteRecord("Ana", 19, 4.20),
        EstudianteRecord("Bruno", 21, 3.45),
        EstudianteRecord("Carla", 20, 4.80),
    ]

    print()
    print("-- NamedTuple inmutable: recorrido de la lista --")
    for e in inmutables:
        print("  " + str(e))

    print()
    print("-- NamedTuple inmutable: intentar asignar el campo --")
    try:
        inmutables[1].promedio = 3.90
    except AttributeError as error:
        print(f"  Excepcion esperada: {type(error).__name__}: {error}")

    print()
    print("-- NamedTuple inmutable: cambiar el promedio con _replace --")
    for i, e in enumerate(inmutables):
        if e.nombre == "Bruno":
            inmutables[i] = e._replace(promedio=3.90)
    for e in inmutables:
        print("  " + str(e))

    print()
    print("-- igualdad por valor --")
    r1 = EstudianteRecord("Ana", 19, 4.20)
    r2 = EstudianteRecord("Ana", 19, 4.20)
    print(f"  r1 == r2 (dos registros con los mismos datos): {r1 == r2}")
    print(f"  r1 is r2 (son el mismo objeto en memoria):     {r1 is r2}")


# ---------- Punto 4: objetos ----------
def demostrar_objetos() -> None:
    print()
    print("=== PUNTO 4: OBJETOS (CLASES E INSTANCIAS) ===")

    objetos = [
        Estudiante("Ana", 19, 4.20),
        Estudiante("Bruno", 21, 3.45),
        Estudiante("Carla", 20, 4.80),
    ]

    print()
    print("-- recorrido llamando a mostrar_info() --")
    for e in objetos:
        e.mostrar_info()

    print()
    print("-- modificacion con set_promedio() --")
    for e in objetos:
        if e.nombre == "Bruno":
            e.set_promedio(3.90)
    for e in objetos:
        e.mostrar_info()

    print()
    print("-- el objeto valida; el registro de campos publicos no --")
    objetos[0].set_promedio(9.99)
    objetos[0].mostrar_info()

    print()
    print("-- igualdad por referencia --")
    o1 = Estudiante("Ana", 19, 4.20)
    o2 = Estudiante("Ana", 19, 4.20)
    print(f"  o1 == o2 (dos objetos con los mismos datos): {o1 == o2}")
    print("  La clase no define __eq__, asi que Python compara identidad.")


# ---------- Punto 5: diferencias ----------
def demostrar_diferencias() -> None:
    print()
    print("=== PUNTO 5: EN PYTHON TODO SE PASA POR REFERENCIA ===")
    print("Se pasa el mismo dato a una funcion que intenta subir el promedio a 5.00.")

    como_registro = EstudianteStruct("Diana", 22, 3.00)
    como_objeto = Estudiante("Diana", 22, 3.00)

    print()
    print(f"  Antes   - registro: {como_registro.promedio:.2f}   objeto: {como_objeto.get_promedio():.2f}")

    _subir_registro(como_registro)
    _subir_objeto(como_objeto)

    print(f"  Despues - registro: {como_registro.promedio:.2f}   objeto: {como_objeto.get_promedio():.2f}")
    print()
    print("  Los DOS cambiaron. En C# el struct no habria cambiado, porque alli")
    print("  es un tipo de valor y la funcion habria recibido una copia.")
    print("  En Python no existen tipos de valor: la unica forma de impedir el")
    print("  cambio es declarar el registro inmutable (NamedTuple o frozen).")

    print()
    print("-- copia de variable --")
    registro_a = EstudianteStruct("Eva", 20, 4.00)
    registro_b = registro_a          # misma instancia, no una copia
    registro_b.promedio = 1.00

    print(f"  registro_a quedo en {registro_a.promedio:.2f} y registro_b en {registro_b.promedio:.2f}")
    print("  Para obtener una copia independiente hay que pedirla explicitamente:")

    import copy
    registro_c = copy.copy(registro_a)
    registro_c.promedio = 9.99
    print(f"  tras copy.copy: registro_a = {registro_a.promedio:.2f}, registro_c = {registro_c.promedio:.2f}")

    print()
    print("-- el registro inmutable si impide el cambio --")
    inmutable = EstudianteRecord("Eva", 20, 4.00)
    try:
        inmutable.promedio = 1.00
    except AttributeError as error:
        print(f"  Excepcion esperada: {type(error).__name__}")
    print(f"  el registro sigue en {inmutable.promedio:.2f}")


def _subir_registro(estudiante: EstudianteStruct) -> None:
    estudiante.promedio = 5.00


def _subir_objeto(estudiante: Estudiante) -> None:
    estudiante.set_promedio(5.00)
