"""Actividad de Aprendizaje Individual - Unidad 1 - Estructura de Datos.

Universidad de Cartagena - Ingenieria de Software
Carlos Andres Nunez Vargas
"""
from analizador import demostrar_practica
from demo_registros import (
    demostrar_diferencias,
    demostrar_objetos,
    demostrar_struct_y_record,
)


def mostrar_menu() -> None:
    print()
    print("=========================================================")
    print(" ESTRUCTURA DE DATOS - UNIDAD 1")
    print(" ACTIVIDAD DE APRENDIZAJE INDIVIDUAL - PYTHON")
    print(" Carlos Andres Nunez Vargas - Universidad de Cartagena")
    print("=========================================================")
    print(" 1. Punto 3: registro mutable e inmutable")
    print(" 2. Punto 4: objetos (clases e instancias)")
    print(" 3. Punto 5: diferencias de semantica")
    print(" 4. Practica: ejercicio 14 con registros y objetos")
    print(" 5. Ejecutar todo")
    print(" 0. Salir")


def main() -> None:
    while True:
        mostrar_menu()
        opcion = input("Seleccione una opcion: ").strip()
        if opcion == "1":
            demostrar_struct_y_record()
        elif opcion == "2":
            demostrar_objetos()
        elif opcion == "3":
            demostrar_diferencias()
        elif opcion == "4":
            demostrar_practica()
        elif opcion == "5":
            demostrar_struct_y_record()
            demostrar_objetos()
            demostrar_diferencias()
            demostrar_practica()
        elif opcion == "0":
            print("Fin del programa.")
            break
        else:
            print("Opcion no valida.")


if __name__ == "__main__":
    main()
