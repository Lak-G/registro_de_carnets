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

namespace Registro_de_carnets;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
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
    }
    
    private void NavigateToRegistroGrupal()
    {
        var registroGrupal = new RegistroGrupal();
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
}