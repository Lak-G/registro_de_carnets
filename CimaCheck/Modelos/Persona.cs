namespace Registro_de_carnets.Modelos;

/// <summary>
/// Esta clase quedo obsoleta con la actualizacion de la base de datos en la version 2.0
/// </summary>
public class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Matricula { get; set; }
    public int? Edad { get; set; }
    public string Genero { get; set; } // "M", "F", "O"
    public string Email { get; set; }
    
    // Para CIMA
    public int? FacultadId { get; set; }
    public int? CarreraId { get; set; }
    public string NombreFacultad { get; set; }
    public string NombreCarrera { get; set; }
    
    // Para GRUPAL
    public int? EscuelaId { get; set; }
    public string NombreEscuela { get; set; }
    public string GrupoResponsable { get; set; }
    
    // Tipo y asistencia
    public string TipoRegistro { get; set; } // "individual", "grupal", "cima"
    public bool Asistencia { get; set; }
    public DateTime? FechaAsistencia { get; set; }
    public DateTime FechaRegistro { get; set; }
    
    // Para UI
    public bool Seleccionado { get; set; }
    
}