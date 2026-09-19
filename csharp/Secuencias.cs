namespace AprendizajeIndividual;

/// <summary>Clasificacion del orden de una secuencia.</summary>
public enum Orden
{
    Ascendente,
    Descendente,
    Constante,
    Desordenada,
    Indeterminada
}

/// <summary>
/// RECORD usado como ITEM de un arreglo.
/// Representa una secuencia con nombre. Es inmutable: los terminos no se
/// reemplazan, se crea un record nuevo con 'with'.
/// </summary>
public record SecuenciaRecord(string Nombre, int[] Terminos)
{
    public override string ToString() => $"{Nombre}: [{string.Join(", ", Terminos)}]";
}

/// <summary>
/// STRUCT usado como ITEM de un arreglo.
/// Guarda el resultado del analisis de una secuencia. Es un readonly struct:
/// tipo de valor inmutable, adecuado para un dato pequeno y sin identidad
/// propia, que se copia en lugar de compartirse.
/// </summary>
public readonly struct ResultadoAnalisis
{
    public Orden Orden { get; }
    public bool EsAritmetica { get; }
    public int RazonD { get; }
    public bool EsGeometrica { get; }
    public int RazonNumerador { get; }
    public int RazonDenominador { get; }

    public ResultadoAnalisis(Orden orden, bool esAritmetica, int razonD,
                             bool esGeometrica, int razonNumerador, int razonDenominador)
    {
        Orden = orden;
        EsAritmetica = esAritmetica;
        RazonD = razonD;
        EsGeometrica = esGeometrica;
        RazonNumerador = razonNumerador;
        RazonDenominador = razonDenominador;
    }

    /// <summary>Razon geometrica como entero o como fraccion irreducible.</summary>
    public string RazonGeometricaTexto()
    {
        if (!EsGeometrica)
        {
            return "-";
        }
        return RazonDenominador == 1
            ? RazonNumerador.ToString()
            : $"{RazonNumerador}/{RazonDenominador}";
    }

    public string Describir()
    {
        string aritmetica = EsAritmetica ? $"SI, d = {RazonD}" : "NO";
        string geometrica = EsGeometrica ? $"SI, r = {RazonGeometricaTexto()}" : "NO";
        return $"orden: {Orden,-13} aritmetica: {aritmetica,-12} geometrica: {geometrica}";
    }
}

/// <summary>
/// OBJETO que contiene una MATRIZ como campo.
/// Cubre el mini-proyecto integrador sugerido: un arreglo de objetos donde cada
/// objeto guarda una matriz irregular; cada fila de la matriz es una secuencia.
/// </summary>
public class LoteSecuencias
{
    private readonly string nombre;
    private readonly int[][] matriz;   // campo que es a su vez una matriz

    public LoteSecuencias(string nombre, int[][] matriz)
    {
        this.nombre = nombre;
        this.matriz = matriz;
    }

    public string Nombre => nombre;

    public int CantidadFilas => matriz.Length;

    /// <summary>Convierte cada fila de la matriz en un record con nombre.</summary>
    public SecuenciaRecord[] ATabla()
    {
        var tabla = new SecuenciaRecord[matriz.Length];
        for (int i = 0; i < matriz.Length; i++)
        {
            tabla[i] = new SecuenciaRecord($"{nombre}-fila{i}", matriz[i]);
        }
        return tabla;
    }

    public void ImprimirMatriz()
    {
        Console.WriteLine($"  Lote '{nombre}' ({matriz.Length} filas):");
        foreach (int[] fila in matriz)
        {
            var linea = new System.Text.StringBuilder("   ");
            foreach (int valor in fila)
            {
                linea.Append($"{valor,5}");
            }
            Console.WriteLine(linea.ToString());
        }
    }
}
