namespace AprendizajeIndividual;

/// <summary>
/// ACTIVIDAD PRACTICA: el ejercicio 14 del Protocolo Individual reescrito con
/// record, struct y objetos como items guardados en arreglos y matrices.
/// </summary>
public static class DemoPractica
{
    public static void Demostrar()
    {
        Console.WriteLine();
        Console.WriteLine("=== ACTIVIDAD PRACTICA: EJERCICIO 14 CON RECORD, STRUCT Y OBJETOS ===");

        // 1. Arreglo de RECORDS: cada item es una secuencia con nombre.
        SecuenciaRecord[] catalogo =
        {
            new SecuenciaRecord("aritmetica-1",  new[] { 2, 5, 8, 11, 14 }),
            new SecuenciaRecord("aritmetica-2",  new[] { 20, 15, 10, 5 }),
            new SecuenciaRecord("geometrica-1",  new[] { 3, 6, 12, 24, 48 }),
            new SecuenciaRecord("geometrica-2",  new[] { 81, 27, 9, 3 }),
            new SecuenciaRecord("constante",     new[] { 7, 7, 7, 7 }),
            new SecuenciaRecord("desordenada",   new[] { 4, 9, 2, 15, 1 }),
            new SecuenciaRecord("casi-geom",     new[] { 1, 2, 4, 8, 15 }),
            new SecuenciaRecord("con-cero",      new[] { 5, 0, 5 })
        };

        // 2. OBJETO analizador: guarda el arreglo de records como estado propio.
        var analizador = new AnalizadorSecuencias(catalogo);

        Console.WriteLine();
        Console.WriteLine($"-- informe del catalogo ({analizador.Cantidad} secuencias) --");
        analizador.MostrarInforme();

        // 3. Modificacion: el record es inmutable, se sustituye con 'with'.
        Console.WriteLine();
        Console.WriteLine("-- se reemplazan los terminos de 'casi-geom' por 1, 2, 4, 8, 16 --");
        analizador.ReemplazarTerminos("casi-geom", new[] { 1, 2, 4, 8, 16 });
        analizador.MostrarInforme();

        // 4. Se agrega una secuencia nueva al estado del objeto.
        Console.WriteLine();
        Console.WriteLine("-- se agrega la secuencia 'nueva' = 100, 50, 25 --");
        analizador.Agregar(new SecuenciaRecord("nueva", new[] { 100, 50, 25 }));
        Console.WriteLine($"  el catalogo pasa a tener {analizador.Cantidad} secuencias");

        // 5. Arreglo de OBJETOS con un campo MATRIZ (mini-proyecto integrador).
        Console.WriteLine();
        Console.WriteLine("-- arreglo de objetos cuyo campo es una matriz --");
        LoteSecuencias[] lotes =
        {
            new LoteSecuencias("loteA", new int[][]
            {
                new[] { 1, 3, 5, 7 },
                new[] { 2, 4, 8, 16 },
                new[] { 9, 9, 9 }
            }),
            new LoteSecuencias("loteB", new int[][]
            {
                new[] { 10, 7, 4, 1 },
                new[] { 6, 1, 8 }
            })
        };

        foreach (LoteSecuencias lote in lotes)
        {
            Console.WriteLine();
            lote.ImprimirMatriz();

            // Cada fila de la matriz se convierte en un record y se analiza.
            var analizadorDelLote = new AnalizadorSecuencias(lote.ATabla());
            analizadorDelLote.MostrarInforme();
        }

        // 6. El resultado es un STRUCT: se copia, no se comparte.
        Console.WriteLine();
        Console.WriteLine("-- el resultado es un struct (tipo de valor) --");
        ResultadoAnalisis[] resultados = analizador.AnalizarTodas();
        ResultadoAnalisis copia = resultados[0];
        Console.WriteLine($"  resultados[0]: {resultados[0].Describir()}");
        Console.WriteLine($"  copia:         {copia.Describir()}");
        Console.WriteLine("  'copia' es un duplicado independiente, no una referencia compartida.");
    }
}
