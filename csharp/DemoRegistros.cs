namespace AprendizajeIndividual;

/// <summary>
/// Puntos 3, 4 y 5 del enunciado: struct/record, objetos y sus diferencias.
/// </summary>
public static class DemoRegistros
{
    // ---------- Punto 3: struct y record ----------
    public static void DemostrarStructYRecord()
    {
        Console.WriteLine();
        Console.WriteLine("=== PUNTO 3: STRUCT Y RECORD ===");

        // Declaracion e inicializacion: 3 instancias guardadas en un arreglo.
        EstudianteStruct[] structs =
        {
            new EstudianteStruct("Ana", 19, 4.20),
            new EstudianteStruct("Bruno", 21, 3.45),
            new EstudianteStruct("Carla", 20, 4.80)
        };

        Console.WriteLine();
        Console.WriteLine("-- struct: recorrido del arreglo --");
        foreach (EstudianteStruct e in structs)
        {
            Console.WriteLine("  " + e);
        }

        // Modificacion: cambiar el promedio de un estudiante especifico.
        // Se accede por indice, NO con foreach: la variable de un foreach es una
        // copia del elemento y el compilador de C# prohibe asignarle campos.
        Console.WriteLine();
        Console.WriteLine("-- struct: cambiar el promedio de Bruno a 3.90 --");
        for (int i = 0; i < structs.Length; i++)
        {
            if (structs[i].Nombre == "Bruno")
            {
                structs[i].Promedio = 3.90;   // funciona: structs[i] es la posicion real
            }
        }
        foreach (EstudianteStruct e in structs)
        {
            Console.WriteLine("  " + e);
        }

        // ---------- record ----------
        EstudianteRecord[] records =
        {
            new EstudianteRecord("Ana", 19, 4.20),
            new EstudianteRecord("Bruno", 21, 3.45),
            new EstudianteRecord("Carla", 20, 4.80)
        };

        Console.WriteLine();
        Console.WriteLine("-- record: recorrido del arreglo --");
        foreach (EstudianteRecord e in records)
        {
            Console.WriteLine("  " + e);
        }

        Console.WriteLine();
        Console.WriteLine("-- record: cambiar el promedio de Bruno a 3.90 --");
        Console.WriteLine("   (el record es inmutable: se crea una copia con 'with')");
        for (int i = 0; i < records.Length; i++)
        {
            if (records[i].Nombre == "Bruno")
            {
                records[i] = records[i] with { Promedio = 3.90 };
            }
        }
        foreach (EstudianteRecord e in records)
        {
            Console.WriteLine("  " + e);
        }

        // Igualdad por valor: propia del record, no de la clase.
        Console.WriteLine();
        Console.WriteLine("-- record: igualdad por valor --");
        var r1 = new EstudianteRecord("Ana", 19, 4.20);
        var r2 = new EstudianteRecord("Ana", 19, 4.20);
        Console.WriteLine($"  r1 == r2 (dos records con los mismos datos): {r1 == r2}");
    }

    // ---------- Punto 4: objetos ----------
    public static void DemostrarObjetos()
    {
        Console.WriteLine();
        Console.WriteLine("=== PUNTO 4: OBJETOS (CLASES E INSTANCIAS) ===");

        Estudiante[] objetos =
        {
            new Estudiante("Ana", 19, 4.20),
            new Estudiante("Bruno", 21, 3.45),
            new Estudiante("Carla", 20, 4.80)
        };

        Console.WriteLine();
        Console.WriteLine("-- recorrido llamando a MostrarInfo() --");
        foreach (Estudiante e in objetos)
        {
            e.MostrarInfo();
        }

        Console.WriteLine();
        Console.WriteLine("-- modificacion con SetPromedio() --");
        foreach (Estudiante e in objetos)
        {
            if (e.Nombre == "Bruno")
            {
                e.SetPromedio(3.90);   // funciona: la variable del foreach es la referencia
            }
        }
        foreach (Estudiante e in objetos)
        {
            e.MostrarInfo();
        }

        Console.WriteLine();
        Console.WriteLine("-- el objeto valida; el struct de campos publicos no --");
        objetos[0].SetPromedio(9.99);
        objetos[0].MostrarInfo();

        // Igualdad por referencia: dos objetos con los mismos datos NO son iguales.
        Console.WriteLine();
        Console.WriteLine("-- clase: igualdad por referencia --");
        var o1 = new Estudiante("Ana", 19, 4.20);
        var o2 = new Estudiante("Ana", 19, 4.20);
        Console.WriteLine($"  o1 == o2 (dos objetos con los mismos datos): {o1 == o2}");
    }

    // ---------- Punto 5: diferencias ----------
    public static void DemostrarDiferencias()
    {
        Console.WriteLine();
        Console.WriteLine("=== PUNTO 5: VALOR FRENTE A REFERENCIA ===");
        Console.WriteLine("Se pasa el mismo dato a un metodo que intenta subir el promedio a 5.00.");

        var comoStruct = new EstudianteStruct("Diana", 22, 3.00);
        var comoObjeto = new Estudiante("Diana", 22, 3.00);

        Console.WriteLine();
        Console.WriteLine($"  Antes  - struct: {comoStruct.Promedio:F2}   objeto: {comoObjeto.GetPromedio():F2}");

        SubirPromedio(comoStruct);   // recibe una COPIA: no afecta al original
        SubirPromedio(comoObjeto);   // recibe la REFERENCIA: si afecta al original

        Console.WriteLine($"  Despues- struct: {comoStruct.Promedio:F2}   objeto: {comoObjeto.GetPromedio():F2}");
        Console.WriteLine();
        Console.WriteLine("  El struct no cambio porque el metodo recibio una copia del valor.");
        Console.WriteLine("  El objeto si cambio porque el metodo recibio la referencia.");

        // Misma comprobacion al copiar una variable.
        Console.WriteLine();
        Console.WriteLine("-- copia de variable --");
        var structA = new EstudianteStruct("Eva", 20, 4.00);
        var structB = structA;              // copia independiente
        structB.Promedio = 1.00;

        var objetoA = new Estudiante("Eva", 20, 4.00);
        var objetoB = objetoA;              // misma instancia
        objetoB.SetPromedio(1.00);

        Console.WriteLine($"  structA quedo en {structA.Promedio:F2} y structB en {structB.Promedio:F2}");
        Console.WriteLine($"  objetoA quedo en {objetoA.GetPromedio():F2} y objetoB en {objetoB.GetPromedio():F2}");
    }

    private static void SubirPromedio(EstudianteStruct estudiante)
    {
        estudiante.Promedio = 5.00;
    }

    private static void SubirPromedio(Estudiante estudiante)
    {
        estudiante.SetPromedio(5.00);
    }
}
