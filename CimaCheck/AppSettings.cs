namespace Registro_de_carnets;

public class AppSettings
{
    public required SupabaseSettings Supabase { get; set; }
    public required string AppName { get; set; }
    public required string Version { get; set; }
}