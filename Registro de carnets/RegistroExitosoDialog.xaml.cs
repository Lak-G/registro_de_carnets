using System.Windows;

namespace Registro_de_carnets;

public partial class RegistroExitosoDialog : Window
{
    public RegistroExitosoDialog()
    {
        InitializeComponent();
        ProgressBarInicio();
    }

    private void GuardarButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void ProgressBarInicio()
    {
        for (int i = 0; i < 200; ++i)
        {
            ProgressBar1.Value += 1;
            Thread.Sleep(50);
        }
        
        Close();
    }
}