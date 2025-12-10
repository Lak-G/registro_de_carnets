using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Registro_de_carnets.Services;

namespace Registro_de_carnets;

public partial class App : Application
{
    public static IConfiguration Configuration { get; private set; }
        
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
            
        // Cargar configuración
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
        Configuration = builder.Build();
        
        // Inicializar Supabase
        await DataManager.InicializarAsync();
    }
}