using System.Windows;
using System.Windows.Controls;

namespace Registro_de_carnets;

public partial class RegistroIndividual : UserControl
{
    public RegistroIndividual()
    {
        InitializeComponent();
        
        
    }


    private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new RegistroExitosoDialog();
        dialog.Owner = Window.GetWindow(this);
        dialog.ShowDialog();
    
    }
}