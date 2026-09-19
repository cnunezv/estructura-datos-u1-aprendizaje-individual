"""Tipos de datos del ejercicio 14 reescrito con registros y objetos."""
from dataclasses import dataclass
from enum import Enum
from typing import List, Tuple


class Orden(Enum):
    """Clasificacion del orden de una secuencia."""
    ASCENDENTE = "Ascendente"
    DESCENDENTE = "Descendente"
    CONSTANTE = "Constante"
    DESORDENADA = "Desordenada"
    INDETERMINADA = "Indeterminada"

    def __str__(self) -> str:
        return self.value


@dataclass(frozen=True)
class SecuenciaRecord:
    """REGISTRO INMUTABLE usado como ITEM de una lista.

    Representa una secuencia con nombre. Equivale al record de C#: para
    cambiar los terminos se crea una copia con dataclasses.replace.
    """
    nombre: str
    terminos: Tuple[int, ...]

    def __str__(self) -> str:
        return f"{self.nombre}: [{', '.join(str(t) for t in self.terminos)}]"


@dataclass(frozen=True)
class ResultadoAnalisis:
    """REGISTRO INMUTABLE con el resultado del analisis de una secuencia."""
    orden: Orden
    es_aritmetica: bool
    razon_d: int
    es_geometrica: bool
    razon_numerador: int
    razon_denominador: int

    def razon_geometrica_texto(self) -> str:
        """Razon geometrica como entero o como fraccion irreducible."""
        if not self.es_geometrica:
            return "-"
        if self.razon_denominador == 1:
            return str(self.razon_numerador)
        return f"{self.razon_numerador}/{self.razon_denominador}"

    def describir(self) -> str:
        aritmetica = f"SI, d = {self.razon_d}" if self.es_aritmetica else "NO"
        geometrica = f"SI, r = {self.razon_geometrica_texto()}" if self.es_geometrica else "NO"
        return f"orden: {str(self.orden):<13} aritmetica: {aritmetica:<12} geometrica: {geometrica}"


class LoteSecuencias:
    """OBJETO que contiene una MATRIZ como campo.

    Cubre el mini-proyecto integrador sugerido: una lista de objetos donde cada
    objeto guarda una matriz irregular; cada fila de la matriz es una secuencia.
    """

    def __init__(self, nombre: str, matriz: List[List[int]]) -> None:
        self._nombre = nombre
        self._matriz = matriz

    @property
    def nombre(self) -> str:
        return self._nombre

    @property
    def cantidad_filas(self) -> int:
        return len(self._matriz)

    def a_tabla(self) -> List[SecuenciaRecord]:
        """Convierte cada fila de la matriz en un registro con nombre."""
        return [
            SecuenciaRecord(f"{self._nombre}-fila{i}", tuple(fila))
            for i, fila in enumerate(self._matriz)
        ]

    def imprimir_matriz(self) -> None:
        print(f"  Lote '{self._nombre}' ({len(self._matriz)} filas):")
        for fila in self._matriz:
            print("   " + "".join(f"{valor:5d}" for valor in fila))
