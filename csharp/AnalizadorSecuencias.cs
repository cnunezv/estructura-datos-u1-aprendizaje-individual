namespace AprendizajeIndividual;

/// <summary>
/// Ejercicio 14 del Protocolo Individual, reescrito como OBJETO.
///
/// En la version original el analizador era una clase de metodos estaticos sin
/// estado. Aqui es una instancia que guarda su propio catalogo de secuencias
/// (un arreglo de records) y produce un arreglo de resultados (structs).
/// </summary>
public class AnalizadorSecuencias
{
    private SecuenciaRecord[] catalogo;

    public AnalizadorSecuencias(SecuenciaRecord[] catalogo)
    {
        this.catalogo = catalogo;
    }

    public int Cantidad => catalogo.Length;

    /// <summary>Agrega una secuencia al catalogo.</summary>
    public void Agregar(SecuenciaRecord secuencia)
    {
        var ampliado = new SecuenciaRecord[catalogo.Length + 1];
        Array.Copy(catalogo, ampliado, catalogo.Length);
        ampliado[catalogo.Length] = secuencia;
        catalogo = ampliado;
    }

    /// <summary>
    /// Reemplaza los terminos de una secuencia buscandola por nombre. Como el
    /// record es inmutable, se construye una copia con 'with' y se sustituye la
    /// posicion del arreglo.
    /// </summary>
    public bool ReemplazarTerminos(string nombre, int[] nuevosTerminos)
    {
        for (int i = 0; i < catalogo.Length; i++)
        {
            if (catalogo[i].Nombre == nombre)
            {
                catalogo[i] = catalogo[i] with { Terminos = nuevosTerminos };
                return true;
            }
        }
        return false;
    }

    /// <summary>Analiza todo el catalogo y devuelve un arreglo de structs.</summary>
    public ResultadoAnalisis[] AnalizarTodas()
    {
        var resultados = new ResultadoAnalisis[catalogo.Length];
        for (int i = 0; i < catalogo.Length; i++)
        {
            resultados[i] = Analizar(catalogo[i].Terminos);
        }
        return resultados;
    }

    public void MostrarInforme()
    {
        ResultadoAnalisis[] resultados = AnalizarTodas();
        for (int i = 0; i < catalogo.Length; i++)
        {
            Console.WriteLine($"  {catalogo[i]}");
            Console.WriteLine($"     {resultados[i].Describir()}");
        }
    }

    // ---------- Logica del ejercicio 14 ----------

    public static ResultadoAnalisis Analizar(int[] s)
    {
        if (s == null || s.Length < 2)
        {
            return new ResultadoAnalisis(Orden.Indeterminada, false, 0, false, 0, 1);
        }

        Orden orden = ClasificarOrden(s);

        bool aritmetica = EsProgresionAritmetica(s);
        int d = aritmetica ? s[1] - s[0] : 0;

        bool geometrica = EsProgresionGeometrica(s);
        int numerador = 0;
        int denominador = 1;
        if (geometrica)
        {
            (numerador, denominador) = ReducirFraccion(s[1], s[0]);
        }

        return new ResultadoAnalisis(orden, aritmetica, d, geometrica, numerador, denominador);
    }

    public static Orden ClasificarOrden(int[] s)
    {
        if (s == null || s.Length < 2)
        {
            return Orden.Indeterminada;
        }
        bool subio = false;
        bool bajo = false;
        for (int i = 1; i < s.Length; i++)
        {
            if (s[i] > s[i - 1]) { subio = true; }
            else if (s[i] < s[i - 1]) { bajo = true; }
        }
        if (subio && bajo) { return Orden.Desordenada; }
        if (subio) { return Orden.Ascendente; }
        if (bajo) { return Orden.Descendente; }
        return Orden.Constante;
    }

    public static bool EsProgresionAritmetica(int[] s)
    {
        if (s == null || s.Length < 2) { return false; }
        int diferencia = s[1] - s[0];
        for (int i = 2; i < s.Length; i++)
        {
            if (s[i] - s[i - 1] != diferencia) { return false; }
        }
        return true;
    }

    /// <summary>
    /// Se comprueba con multiplicacion cruzada sobre enteros largos en lugar de
    /// dividir en punto flotante, para evitar errores de redondeo:
    /// la secuencia es geometrica si y solo si s[i-1]^2 == s[i-2] * s[i].
    /// </summary>
    public static bool EsProgresionGeometrica(int[] s)
    {
        if (s == null || s.Length < 2) { return false; }
        foreach (int valor in s)
        {
            if (valor == 0) { return false; }   // el cociente quedaria indefinido
        }
        for (int i = 2; i < s.Length; i++)
        {
            long izquierda = (long)s[i - 1] * s[i - 1];
            long derecha = (long)s[i - 2] * s[i];
            if (izquierda != derecha) { return false; }
        }
        return true;
    }

    private static (int, int) ReducirFraccion(int numerador, int denominador)
    {
        if (denominador < 0)
        {
            numerador = -numerador;
            denominador = -denominador;
        }
        int divisor = MaximoComunDivisor(Math.Abs(numerador), denominador);
        return (numerador / divisor, denominador / divisor);
    }

    private static int MaximoComunDivisor(int a, int b)
    {
        while (b != 0)
        {
            int resto = a % b;
            a = b;
            b = resto;
        }
        return a;
    }
}
