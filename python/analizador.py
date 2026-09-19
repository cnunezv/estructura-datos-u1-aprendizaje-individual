"""Ejercicio 14 del Protocolo Individual, reescrito como OBJETO.

En la version original el analizador era un conjunto de funciones sueltas sin
estado. Aqui es una instancia que guarda su propio catalogo de secuencias
(una lista de registros inmutables) y produce una lista de resultados.
"""
from dataclasses import replace
from math import gcd
from typing import List, Sequence, Tuple

from secuencias import LoteSecuencias, Orden, ResultadoAnalisis, SecuenciaRecord


class AnalizadorSecuencias:
    """Objeto con estado: mantiene y analiza un catalogo de secuencias."""

    def __init__(self, catalogo: List[SecuenciaRecord]) -> None:
        self._catalogo = list(catalogo)

    @property
    def cantidad(self) -> int:
        return len(self._catalogo)

    def agregar(self, secuencia: SecuenciaRecord) -> None:
        """Agrega una secuencia al catalogo."""
        self._catalogo.append(secuencia)

    def reemplazar_terminos(self, nombre: str, nuevos_terminos: Sequence[int]) -> bool:
        """Reemplaza los terminos de una secuencia buscandola por nombre.

        Como el registro es inmutable, se construye una copia con
        dataclasses.replace y se sustituye la posicion de la lista.
        """
        for i, secuencia in enumerate(self._catalogo):
            if secuencia.nombre == nombre:
                self._catalogo[i] = replace(secuencia, terminos=tuple(nuevos_terminos))
                return True
        return False

    def analizar_todas(self) -> List[ResultadoAnalisis]:
        """Analiza todo el catalogo y devuelve una lista de registros."""
        return [self.analizar(s.terminos) for s in self._catalogo]

    def mostrar_informe(self) -> None:
        for secuencia, resultado in zip(self._catalogo, self.analizar_todas()):
            print(f"  {secuencia}")
            print(f"     {resultado.describir()}")

    # ---------- Logica del ejercicio 14 ----------

    @staticmethod
    def analizar(s: Sequence[int]) -> ResultadoAnalisis:
        if s is None or len(s) < 2:
            return ResultadoAnalisis(Orden.INDETERMINADA, False, 0, False, 0, 1)

        orden = AnalizadorSecuencias.clasificar_orden(s)

        aritmetica = AnalizadorSecuencias.es_progresion_aritmetica(s)
        d = s[1] - s[0] if aritmetica else 0

        geometrica = AnalizadorSecuencias.es_progresion_geometrica(s)
        numerador, denominador = (
            AnalizadorSecuencias._reducir_fraccion(s[1], s[0]) if geometrica else (0, 1)
        )

        return ResultadoAnalisis(orden, aritmetica, d, geometrica, numerador, denominador)

    @staticmethod
    def clasificar_orden(s: Sequence[int]) -> Orden:
        if s is None or len(s) < 2:
            return Orden.INDETERMINADA
        subio = False
        bajo = False
        for i in range(1, len(s)):
            if s[i] > s[i - 1]:
                subio = True
            elif s[i] < s[i - 1]:
                bajo = True
        if subio and bajo:
            return Orden.DESORDENADA
        if subio:
            return Orden.ASCENDENTE
        if bajo:
            return Orden.DESCENDENTE
        return Orden.CONSTANTE

    @staticmethod
    def es_progresion_aritmetica(s: Sequence[int]) -> bool:
        if s is None or len(s) < 2:
            return False
        diferencia = s[1] - s[0]
        for i in range(2, len(s)):
            if s[i] - s[i - 1] != diferencia:
                return False
        return True

    @staticmethod
    def es_progresion_geometrica(s: Sequence[int]) -> bool:
        """Se comprueba con multiplicacion cruzada sobre enteros en lugar de
        dividir: la secuencia es geometrica si y solo si
        s[i-1]**2 == s[i-2] * s[i], sin terminos en cero.
        """
        if s is None or len(s) < 2:
            return False
        if any(valor == 0 for valor in s):
            return False   # el cociente quedaria indefinido
        for i in range(2, len(s)):
            if s[i - 1] * s[i - 1] != s[i - 2] * s[i]:
                return False
        return True

    @staticmethod
    def _reducir_fraccion(numerador: int, denominador: int) -> Tuple[int, int]:
        if denominador < 0:
            numerador = -numerador
            denominador = -denominador
        divisor = gcd(abs(numerador), denominador)
        return numerador // divisor, denominador // divisor


# ---------- ACTIVIDAD PRACTICA ----------
def demostrar_practica() -> None:
    """Ejercicio 14 con registros y objetos como items de listas y matrices."""
    print()
    print("=== ACTIVIDAD PRACTICA: EJERCICIO 14 CON REGISTROS Y OBJETOS ===")

    # 1. Lista de REGISTROS INMUTABLES: cada item es una secuencia con nombre.
    catalogo = [
        SecuenciaRecord("aritmetica-1", (2, 5, 8, 11, 14)),
        SecuenciaRecord("aritmetica-2", (20, 15, 10, 5)),
        SecuenciaRecord("geometrica-1", (3, 6, 12, 24, 48)),
        SecuenciaRecord("geometrica-2", (81, 27, 9, 3)),
        SecuenciaRecord("constante", (7, 7, 7, 7)),
        SecuenciaRecord("desordenada", (4, 9, 2, 15, 1)),
        SecuenciaRecord("casi-geom", (1, 2, 4, 8, 15)),
        SecuenciaRecord("con-cero", (5, 0, 5)),
    ]

    # 2. OBJETO analizador: guarda la lista de registros como estado propio.
    analizador = AnalizadorSecuencias(catalogo)

    print()
    print(f"-- informe del catalogo ({analizador.cantidad} secuencias) --")
    analizador.mostrar_informe()

    # 3. Modificacion: el registro es inmutable, se sustituye con replace.
    print()
    print("-- se reemplazan los terminos de 'casi-geom' por 1, 2, 4, 8, 16 --")
    analizador.reemplazar_terminos("casi-geom", (1, 2, 4, 8, 16))
    analizador.mostrar_informe()

    # 4. Se agrega una secuencia nueva al estado del objeto.
    print()
    print("-- se agrega la secuencia 'nueva' = 100, 50, 25 --")
    analizador.agregar(SecuenciaRecord("nueva", (100, 50, 25)))
    print(f"  el catalogo pasa a tener {analizador.cantidad} secuencias")

    # 5. Lista de OBJETOS con un campo MATRIZ (mini-proyecto integrador).
    print()
    print("-- lista de objetos cuyo campo es una matriz --")
    lotes = [
        LoteSecuencias("loteA", [[1, 3, 5, 7], [2, 4, 8, 16], [9, 9, 9]]),
        LoteSecuencias("loteB", [[10, 7, 4, 1], [6, 1, 8]]),
    ]

    for lote in lotes:
        print()
        lote.imprimir_matriz()
        AnalizadorSecuencias(lote.a_tabla()).mostrar_informe()

    # 6. El resultado es inmutable: no puede alterarse despues de creado.
    print()
    print("-- el resultado es un registro inmutable --")
    resultados = analizador.analizar_todas()
    print(f"  resultados[0]: {resultados[0].describir()}")
    try:
        resultados[0].es_aritmetica = False
    except Exception as error:
        print(f"  Intentar modificarlo lanza {type(error).__name__}, como se espera.")
