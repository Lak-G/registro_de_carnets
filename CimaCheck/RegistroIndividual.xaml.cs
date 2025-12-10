using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Registro_de_carnets.Services;

namespace Registro_de_carnets;

public partial class RegistroIndividual : UserControl
{
    public RegistroIndividual()
    {
        InitializeComponent();
        
        
    }
    
    private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        

        if (NombreCompletoTextBox.Text.Trim() == "" || CorreoElectronicoTextBox.Text.Trim() == "" || GenderComboBox.SelectedIndex == 0)
        {
            NombreCompletoLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            CorreoElectronicoLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            GenderLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            return;
        }
        else
        {
            NombreCompletoLabel.Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#FF555555"));
            CorreoElectronicoLabel.Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#FF555555"));
            GenderLabel.Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#FF555555"));
        }
        
        var dialog = new RegistroExitosoDialog();
        dialog.Owner = Window.GetWindow(this);
        dialog.ShowDialog();

        string nombre = NombreCompletoTextBox.Text;
        
        string genero = "";
        if (GenderComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            genero = selectedItem.Content.ToString() switch
            {
                "Masculino" => "M",
                "Femenino" => "F",
                "Otro" => "O",
                _ => ""
            };
        }
        
        string correo = CorreoElectronicoTextBox.Text;
        
        _ = DataManager.RegistrarIndividualAsync(nombre, genero, correo);
    }
    
    

    private void LimpiarFormulario()
    {
        NombreCompletoTextBox.Text = "";
        CorreoElectronicoTextBox.Text = "";
        GenderComboBox.SelectedIndex = 0;
    }
}