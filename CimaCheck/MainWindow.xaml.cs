using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Registro_de_carnets.Services;

namespace Registro_de_carnets;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        //con esto prove la conexion pero no lo queiro borrar
        // Loaded += async (s, e) =>
        // {
        //     await DataManager.ProbarConexionAsync();
        // };
        
        // Cargar la página principal al inicio
        NavigateToPaginaPrincipal();
    }
    
    private void NavigateToPaginaPrincipal()
    {
        var paginaPrincipal = new PaginaPrincipal();
        PageContainer.Content = paginaPrincipal;

        // Suscribir eventos cada vez que se carga la página principal
        paginaPrincipal.GroupRegistrationButton.Click += (s, e) =>
        {
            NavigateToRegistroGrupal();
        };

        paginaPrincipal.IndividualRegistrationButton.Click += (s, e) =>
        {
            NavigateToRegistroIndividual();
        };

        paginaPrincipal.CimaRegistrationButton.Click += (sender, args) =>
        {
            NavigateToCimaRegistro();
        };
    }
    
    private void NavigateToRegistroGrupal()
    {
        var registroGrupal = new AsistenciaEscuelas();
        PageContainer.Content = registroGrupal;
        
        // Configurar botón de volver
        registroGrupal.BackButton.Click += (s, e) =>
        {
            NavigateToPaginaPrincipal();
        };
    }
    
    private void NavigateToRegistroIndividual()
    {
        var registroIndividual = new RegistroIndividual();
        PageContainer.Content = registroIndividual;
        
        // Configurar botón de volver
        registroIndividual.BackButton.Click += (s, e) =>
        {
            NavigateToPaginaPrincipal();
        };
    }

    private void NavigateToCimaRegistro()
    {
        var cimaRegistro = new CimaRegistro();
        PageContainer.Content = cimaRegistro;
        
        //Falta boton de volver
        cimaRegistro.BackButton.Click += (sender, args) =>
        {
            NavigateToPaginaPrincipal();
        };
    }
}