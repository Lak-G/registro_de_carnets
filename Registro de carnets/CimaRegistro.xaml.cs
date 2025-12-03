using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Registro_de_carnets;

public partial class CimaRegistro : UserControl
{
    public CimaRegistro()
    {
        InitializeComponent();
    }

    private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MatriculaTextBox.Text == "" || GenderComboBox.SelectedIndex == 0)
        {
            MessageBox.Show("Rellene los campos Faltantes");

            MatriculaLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            GenderLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            return;
        }
    }
    
    private void MatriculaTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Solo permite números
        e.Handled = !IsTextNumeric(e.Text);
    }

    private bool IsTextNumeric(string text)
    {
        return text.All(char.IsDigit);
    }
    
}