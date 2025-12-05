using System.Windows;
using System.Windows.Input;

namespace Registro_de_carnets;

public partial class RegistroExitosoDialog : Window
{
    public RegistroExitosoDialog()
    {
        InitializeComponent();
        _ = ProgressBarInicio();
    }
    
    /// <summary>
    /// Metodo asincrono para que la barra se llene en automatico
    /// </summary>
    private async Task ProgressBarInicio()
    {
        for (int i = 0; i < 200; ++i)
        {
            ProgressBar1.Value += 1;
            await Task.Delay(5);
        }
        
        Close();
    }

    /// <summary>
    /// Evento de click para cerrar la ventana
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProgressBar1_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        Close();
    }
}