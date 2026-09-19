namespace AprendizajeIndividual;

/// <summary>
/// STRUCT: tipo de VALOR. Se copia al asignarlo o al pasarlo como parametro.
/// Sus campos son publicos y mutables, de modo que funciona como un simple
/// contenedor de datos relacionados, sin comportamiento propio.
/// </summary>
public struct EstudianteStruct
{
    public string Nombre;
    public int Edad;
    public double Promedio;

    public EstudianteStruct(string nombre, int edad, double promedio)
    {
        Nombre = nombre;
        Edad = edad;
        Promedio = promedio;
    }

    public override string ToString() =>
        $"{Nombre,-10} edad: {Edad,2}   promedio: {Promedio,5:F2}";
}

/// <summary>
/// RECORD: tipo de REFERENCIA inmutable con igualdad por valor.
/// Los campos se declaran en el encabezado (parametros posicionales) y no
/// pueden reasignarse: para "modificar" uno se crea una copia con la
/// expresion 'with'.
/// </summary>
public record EstudianteRecord(string Nombre, int Edad, double Promedio)
{
    public override string ToString() =>
        $"{Nombre,-10} edad: {Edad,2}   promedio: {Promedio,5:F2}";
}

/// <summary>
/// CLASE (objeto): tipo de REFERENCIA con estado encapsulado y comportamiento.
/// Los campos son privados y se exponen mediante metodos, lo que permite
/// validar los datos antes de aceptarlos.
/// </summary>
public class Estudiante
{
    private readonly string nombre;
    private readonly int edad;
    private double promedio;

    public Estudiante(string nombre, int edad, double promedio)
    {
        this.nombre = nombre;
        this.edad = edad;
        this.promedio = promedio;
    }

    public string Nombre => nombre;

    public double GetPromedio() => promedio;

    /// <summary>
    /// Cambia el promedio validando el rango. Esta validacion es justamente lo
    /// que un struct de campos publicos no puede garantizar.
    /// </summary>
    public void SetPromedio(double nuevoPromedio)
    {
        if (nuevoPromedio < 0.0 || nuevoPromedio > 5.0)
        {
            Console.WriteLine($"  [RECHAZADO] {nuevoPromedio:F2} esta fuera del rango 0.0 - 5.0");
            return;
        }
        promedio = nuevoPromedio;
    }

    /// <summary>Metodo de comportamiento exigido por el enunciado.</summary>
    public void MostrarInfo()
    {
        Console.WriteLine($"  {nombre,-10} edad: {edad,2}   promedio: {promedio,5:F2}");
    }
}
