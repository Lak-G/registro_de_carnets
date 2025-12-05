namespace Registro_de_carnets.Modelos;

public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Genero { get; set; } // "M" o "F"
    public bool Seleccionado { get; set; }
    
    /// <summary>
    /// Constructor de la clase Persona
    /// </summary>
    public Persona(string nombre, int edad, string genero, bool seleccionado)
    {
        Nombre = nombre;
        Edad = edad;
        Genero = genero;
        Seleccionado = seleccionado;
    }

    /// <summary>
    /// Constructor vacio de la clase Persona
    /// </summary>
    public Persona()
    {
        Nombre = "";
        Edad = 0;
        Genero = "";
        Seleccionado = true;
    }
    
}