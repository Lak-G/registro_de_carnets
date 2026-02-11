using Supabase;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Registro_de_carnets.Modelos;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Registro_de_carnets.Services;

public static class DataManager
{
    private static Supabase.Client? _supabase;

    #region Constructor
    public static async Task InicializarAsync()
    {
        var settings = App.Configuration.GetSection("Supabase").Get<SupabaseSettings>();

        if (settings == null || string.IsNullOrEmpty(settings.Url) || string.IsNullOrEmpty(settings.Key))
        {
            throw new InvalidOperationException("La configuración de Supabase no está completa. Verifica el archivo appsettings.json");
        }

        var options = new Supabase.SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = false
        };

        _supabase = new Supabase.Client(settings.Url, settings.Key, options);
        await _supabase.InitializeAsync();
    }
    #endregion

    #region Facultades

    public static async Task<List<Facultad>> ObtenerFacultadesAsync()
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            var response = await _supabase
                .From<FacultadDb>()
                //.Where(f => f.Activo == true)
                //.Order("nombre", Postgrest.Constants.Ordering.Ascending)
                .Get();
            
            return response.Models.Select(f => new Facultad
            {
                Id = f.Id,
                Nombre = f.Nombre,
                Id_facultad = f.Id_facultad,
                Id_programa = f.Id_programa
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar facultades: {ex.Message}");
            return new List<Facultad>();
        }
    }

    #endregion

    #region ObtenerEscuelas

    public static async Task<List<Escuela>> ObtenerEscuelasAsync(string nivelEducativo)
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            var response = await _supabase
                .From<EscuelaDb>()
                .Where(f => f.NivelEducativo == nivelEducativo)
                //.Order("nombre", Postgrest.Constants.Ordering.Ascending)
                .Get();
            
            return response.Models.Select(f => new Escuela
            {
                Id = f.Id,
                NombreEscuela = f.Nombre,
                NivelEducativo = f.NivelEducativo
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar escuelas: {ex.Message}");
            return new List<Escuela>();
        }
    }

    #endregion

    #region Carreras

    public static async Task<List<Carrera>> ObtenerCarrerasPorFacultadAsync(int facultadId)
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            var response = await _supabase
                .From<CarreraDb>()
                .Where(c => c.IdFacultad == facultadId)
                .Order("nombre", Postgrest.Constants.Ordering.Ascending)
                .Get();
            
            return response.Models.Select(c => new Carrera
            {
                Id = c.Id,
                IdPrograma = c.IdPrograma,
                FacultadId = c.IdFacultad,
                Nombre = c.Nombre
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar carreras: {ex.Message}");
            return new List<Carrera>();
        }
    }
    
    #endregion
    
    #region companies

    public static async Task<List<Companie>> ObtenerCompanies()
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            var response = await _supabase
                .From<CompanieDb>()
                //.Order("nombre_proc", Postgrest.Constants.Ordering.Ascending)
                .Get();
            
            return response.Models.Select(c => new Companie
            {
                Id = c.Id,
                IdProcedencia = c.IdProcedencia,
                NombreProc = c.NombreProc
            }).ToList();
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error al cargar Compañias: {e.Message}");
            return new List<Companie>();
        }
    }

    #endregion
    
    #region Registro CIMA
    
    public static async Task<bool> RegistrarCimaAsync(string nombre, string? genero, int facultadId, int carreraId)
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            var cimarron = new CimarronDb
            {
                VisitanteId = 3,
                Nombre = nombre,
                IdFacultad = facultadId,
                IdPrograma = carreraId,
                Genero = genero
            };
            
            await _supabase.From<CimarronDb>().Insert(cimarron);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al registrar: {ex.Message}");
            return false;
        }
    }
    
    #endregion
    
    #region Registro Individual
    
    /// <summary>
    /// Metodo para registrar a los asistentes externos
    /// </summary>
    /// <param name="nombreCompleto"></param>
    /// <param name="genero"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<bool> RegistrarIndividualAsync(string nombreCompleto, string genero, int idProcedencia, int edad)
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            //var persona = new PersonaDb
            //{
            //    NombreCompleto = nombreCompleto,
            //    Genero = genero,
            //    Email = email,
            //    Asistencia = true,
            //    TipoRegistro = "individual"
            //};

            var externos = new ExternosDb
            {
                VisitanteId = 2,
                Nombre = nombreCompleto,
                Genero = genero,
                IdProcedencia = idProcedencia,
                Edad = edad
            };

            await _supabase.From<ExternosDb>().Insert(externos);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al registrar: {ex.Message}");
            return false;
        }
    }
    
    #endregion
    
    #region Escuelas
    
    //Metodos obsoletos para el sistema actual 

    //public static async Task<List<Escuela>> ObtenerEscuelasAsync()
    //{
    //    try
    //    {
    //        if (_supabase == null)
    //            throw new InvalidOperationException("Supabase no ha sido inicializado");

    //        var response = await _supabase
    //            .From<EscuelaDb>()
    //            .Order("nombre", Postgrest.Constants.Ordering.Ascending)
    //            .Get();
            
    //        return response.Models.Select(e => new Escuela
    //        {
    //            Id = e.Id,
    //            NombreEscuela = e.Nombre,
    //            NivelEducativo = e.NivelEducativo
    //        }).ToList();
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show($"Error al cargar escuelas: {ex.Message}");
    //        return new List<Escuela>();
    //    }
    //}
    
    public static async Task<int> AgregarEscuelaAsync(string nombre, string nivelEducativo)
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            var escuela = new EscuelaDb
            {
                Nombre = nombre,
                NivelEducativo = nivelEducativo
            };
            
            var response = await _supabase.From<EscuelaDb>().Insert(escuela);
            return response.Models.First().Id;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al agregar escuela: {ex.Message}");
            return -1;
        }
    }
    
    #endregion
    
    #region Asistencias
    
    public static async Task<List<Persona>> ObtenerPersonasPorFacultadAsync(int facultadId)
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            var response = await _supabase
                .From<PersonaDb>()
                .Where(p => p.FacultadId == facultadId && p.TipoRegistro == "cima")
                .Order("nombre_completo", Postgrest.Constants.Ordering.Ascending)
                .Get();
            
            return response.Models.Select(p => new Persona
            {
                Id = p.Id,
                Nombre = p.NombreCompleto,
                Edad = p.Edad,
                Genero = p.Genero,
                Asistencia = p.Asistencia
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar personas: {ex.Message}");
            return new List<Persona>();
        }
    }
    
    public static async Task<bool> GuardarAsistenciaAsync(int personaId, bool asistio)
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");

            var persona = new PersonaDb
            {
                Id = personaId,
                Asistencia = asistio,
                FechaAsistencia = DateTime.UtcNow
            };
            
            await _supabase.From<PersonaDb>().Update(persona);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al guardar asistencia: {ex.Message}");
            return false;
        }
    }
    
    #endregion
    
    #region Prueba de Conexión
    
    public static async Task<bool> ProbarConexionAsync()
    {
        try
        {
            if (_supabase == null)
                throw new InvalidOperationException("Supabase no ha sido inicializado");
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error de conexión: {ex.Message}");
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    
    #endregion
}

#region Modelo facultad

[Table("facultad")]
public class FacultadDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("nombre")]
    public string Nombre { get; set; }
    
    [Column ("id_facultad")]
    public int Id_facultad { get; set; }
    
    [Column ("id_programa")]
    public int Id_programa { get; set; }
}
#endregion

#region Modelo programa

[Table("programas")]
public class CarreraDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column ("id_programa")]
    public int IdPrograma { get; set; }
    
    [Column("id_facultad")]
    public int IdFacultad { get; set; }
    
    [Column("nombre")]
    public string Nombre { get; set; }

}
#endregion

#region Modelo Escuela

[Table("escuela")]
public class EscuelaDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("nombre_escuela")]
    public string Nombre { get; set; }
    
    [Column("nivel_educativo")]
    public string NivelEducativo { get; set; }
}
#endregion

#region Modelo Persona

[Table("Personas")]
public class PersonaDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("nombre_completo")]
    public string NombreCompleto { get; set; }
    
    [Column("matricula")]
    public string Matricula { get; set; }
    
    [Column("edad")]
    public int? Edad { get; set; }
    
    [Column("genero")]
    public string Genero { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("facultad_id")]
    public int? FacultadId { get; set; }
    
    [Column("carrera_id")]
    public int? CarreraId { get; set; }
    
    [Column("escuela_id")]
    public int? EscuelaId { get; set; }
    
    [Column("grupo_responsable")]
    public string GrupoResponsable { get; set; }
    
    [Column("tipo_registro")]
    public string TipoRegistro { get; set; }
    
    [Column("asistencia")]
    public bool Asistencia { get; set; }
    
    [Column("fecha_asistencia")]
    public DateTime? FechaAsistencia { get; set; }
    
    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; }
}

#endregion

#region Modelo Externos
[Table("externos")]
public class ExternosDb : BaseModel
{
    [PrimaryKey ("id")]
    public int id { get; set; }

    [Column ("visitante_id")]
    public int VisitanteId { get; set; }

    [Column ("nombre")]
    public string Nombre { get; set; }

    [Column ("genero")]
    public string Genero { get; set; }

    [Column ("id_procedencia")]
    public int IdProcedencia { get; set; }

    [Column("edad")]
    public int Edad { get; set; }

}


#endregion

#region Modelo Cima

[Table("cimarron")]
public class CimarronDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("visitante_id")]
    public int VisitanteId { get; set; }
    
    [Column ("nombre")]
    public string Nombre { get; set; }
    
    [Column("id_facultad")]
    public int IdFacultad { get; set; }
    
    [Column("genero")]
    public string Genero { get; set; }
    
    [Column("id_programa")]
    public int IdPrograma { get; set; }
    
}

#endregion

#region Modelo Alumno Escuela


[Table("AlumnosEscuela")]
public class AlumnoEscuelaDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column ("id_escuela")]
    public int IdEscuela { get; set; }

    [Column("nombre_completo")]
    public string NombreCompleto { get; set; }

    [Column ("nivel_educativo")]
    public string NivelEducativo { get; set; }
}

#endregion

#region Modelo Companie

[Table("procedencia")]
public class CompanieDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("id_procedencia")]
    public int IdProcedencia { get; set; }
    
    [Column("nombre_proc")]
    public string NombreProc { get; set; }
}
#endregion

