using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Registro_de_carnets.Services;


namespace Registro_de_carnets;

public partial class PaginaPrincipal : UserControl
{
    public PaginaPrincipal()
    {
        InitializeComponent();
        checkConnection();
        
    }
    
    //los eventos de los botones se cargan directamente en la pagina principal (MainWindow.xalm.cs)

    public void btnSoporte_Click(object o, RoutedEventArgs routedEventArgs)
    {
        
    }

    private async void checkConnection()
    {
        
        bool result = await DataManager.ProbarConexionAsync();
        if (result)
        {
            checkImageOk.Visibility = Visibility.Visible;
        }
        else
        {
            checkImageError.Visibility = Visibility.Visible;
        }
        
        
        
    }
}