using AprendizajeIndividual;

Console.OutputEncoding = System.Text.Encoding.UTF8;

bool continuar = true;
while (continuar)
{
    Console.WriteLine();
    Console.WriteLine("=========================================================");
    Console.WriteLine(" ESTRUCTURA DE DATOS - UNIDAD 1");
    Console.WriteLine(" ACTIVIDAD DE APRENDIZAJE INDIVIDUAL - C#");
    Console.WriteLine(" Carlos Andres Nunez Vargas - Universidad de Cartagena");
    Console.WriteLine("=========================================================");
    Console.WriteLine(" 1. Punto 3: struct y record");
    Console.WriteLine(" 2. Punto 4: objetos (clases e instancias)");
    Console.WriteLine(" 3. Punto 5: diferencias valor / referencia");
    Console.WriteLine(" 4. Practica: ejercicio 14 con record, struct y objetos");
    Console.WriteLine(" 5. Ejecutar todo");
    Console.WriteLine(" 0. Salir");
    Console.Write("Seleccione una opcion: ");

    string? opcion = Console.ReadLine()?.Trim();
    switch (opcion)
    {
        case "1": DemoRegistros.DemostrarStructYRecord(); break;
        case "2": DemoRegistros.DemostrarObjetos(); break;
        case "3": DemoRegistros.DemostrarDiferencias(); break;
        case "4": DemoPractica.Demostrar(); break;
        case "5":
            DemoRegistros.DemostrarStructYRecord();
            DemoRegistros.DemostrarObjetos();
            DemoRegistros.DemostrarDiferencias();
            DemoPractica.Demostrar();
            break;
        case "0":
            continuar = false;
            Console.WriteLine("Fin del programa.");
            break;
        default:
            Console.WriteLine("Opcion no valida.");
            break;
    }
}
