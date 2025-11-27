using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Registro_de_carnets;

public partial class RegistroGrupal : UserControl
{
    public RegistroGrupal()
    {
        InitializeComponent();
            
        // Ocultar placeholder cuando se escribe
        GroupSizeTextBox.TextChanged += (s, e) =>
        {
            GroupSizePlaceholder.Visibility = string.IsNullOrEmpty(GroupSizeTextBox.Text) 
                ? Visibility.Visible 
                : Visibility.Collapsed;
                    
            // Ocultar error al escribir
            GroupSizeErrorMessage.Visibility = Visibility.Collapsed;
        };
            
        // Manejar la opción "Agregar nueva escuela"
        SchoolNameComboBox.SelectionChanged += SchoolNameComboBox_SelectionChanged;
            
        SubmitButton.Click += SubmitButton_Click;
        BackButton.Click += BackButton_Click;
    }
    
    private void SchoolNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SchoolNameComboBox.SelectedItem == AddNewSchoolItem)
        {
            // Abrir diálogo
            var dialog = new AgregarEscuelaDialog();
            dialog.Owner = Window.GetWindow(this); // Centrar sobre la ventana principal
        
            if (dialog.ShowDialog() == true)
            {
                // El usuario guardó la nueva escuela
                var nuevoItem = new ComboBoxItem 
                { 
                    Content = dialog.NombreEscuela 
                };
            
                // Insertar antes del item "Agregar nueva escuela"
                int insertIndex = SchoolNameComboBox.Items.Count - 1;
                SchoolNameComboBox.Items.Insert(insertIndex, nuevoItem);
            
                // Seleccionar la nueva escuela automáticamente
                SchoolNameComboBox.SelectedItem = nuevoItem;
            }
            else
            {
                // El usuario canceló, volver al placeholder
                SchoolNameComboBox.SelectedIndex = 0;
            }
        }
    }
        
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(GroupLeaderNameTextBox.Text))
            {
                MessageBox.Show("Por favor, ingresa el nombre del responsable.", "Campo requerido", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (SchoolNameComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("Por favor, selecciona una escuela.", "Campo requerido", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (EducationLevelComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("Por favor, selecciona un nivel educativo.", "Campo requerido", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(GroupSizeTextBox.Text))
            {
                GroupSizeErrorMessage.Visibility = Visibility.Visible;
                return;
            }
            
            int groupSize;
            if (!int.TryParse(GroupSizeTextBox.Text, out groupSize) || groupSize < 2)
            {
                MessageBox.Show("El tamaño del grupo debe ser al menos 2 personas.", "Valor inválido", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Aquí iría tu lógica de registro
            MessageBox.Show($"Registro grupal exitoso!\n\nResponsable: {GroupLeaderNameTextBox.Text}\nTamaño: {groupSize} personas", 
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Aquí navegarías de regreso a la página principal
            // Lo implementaremos cuando agregues las animaciones
        }
        
        // Validación para solo permitir números
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        
        
}