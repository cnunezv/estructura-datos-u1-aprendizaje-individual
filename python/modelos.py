"""Los tres modelos del enunciado, en Python: registro mutable, registro
inmutable y objeto.

Python no tiene tipos de valor: toda variable guarda una referencia. Por eso no
existe un equivalente exacto del struct de C#. Lo que ofrece el lenguaje son
dos formas de declarar un registro:

    @dataclass              -> registro MUTABLE   (el papel del struct)
    NamedTuple / frozen     -> registro INMUTABLE (el papel del record)

La inmutabilidad de NamedTuple y de @dataclass(frozen=True) se impone en tiempo
de ejecucion: intentar asignar un campo lanza una excepcion, pero el objeto
sigue estando en el heap y sigue pasandose por referencia.
"""
from dataclasses import dataclass
from typing import NamedTuple


@dataclass
class EstudianteStruct:
    """Registro MUTABLE: el papel que cumple un struct de campos publicos.

    Es un contenedor de datos relacionados, sin comportamiento ni validacion.
    """
    nombre: str
    edad: int
    promedio: float

    def __str__(self) -> str:
        return f"{self.nombre:<10} edad: {self.edad:2}   promedio: {self.promedio:5.2f}"


class EstudianteRecord(NamedTuple):
    """Registro INMUTABLE con igualdad por valor: el papel del record.

    Para "modificar" un campo se crea una copia con el metodo _replace,
    equivalente a la expresion 'with' de C#.
    """
    nombre: str
    edad: int
    promedio: float

    def __str__(self) -> str:
        return f"{self.nombre:<10} edad: {self.edad:2}   promedio: {self.promedio:5.2f}"


class Estudiante:
    """OBJETO: estado encapsulado y comportamiento propio."""

    def __init__(self, nombre: str, edad: int, promedio: float) -> None:
        self._nombre = nombre
        self._edad = edad
        self._promedio = promedio

    @property
    def nombre(self) -> str:
        return self._nombre

    def get_promedio(self) -> float:
        return self._promedio

    def set_promedio(self, nuevo_promedio: float) -> None:
        """Cambia el promedio validando el rango.

        Esta validacion es justamente lo que un registro de campos publicos no
        puede garantizar.
        """
        if nuevo_promedio < 0.0 or nuevo_promedio > 5.0:
            print(f"  [RECHAZADO] {nuevo_promedio:.2f} esta fuera del rango 0.0 - 5.0")
            return
        self._promedio = nuevo_promedio

    def mostrar_info(self) -> None:
        """Metodo de comportamiento exigido por el enunciado."""
        print(f"  {self._nombre:<10} edad: {self._edad:2}   promedio: {self._promedio:5.2f}")
