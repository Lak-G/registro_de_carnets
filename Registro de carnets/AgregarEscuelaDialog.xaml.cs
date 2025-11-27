using System.Windows;


namespace Registro_de_carnets;

    public partial class AgregarEscuelaDialog : Window
    {
        public string NombreEscuela { get; private set; }

        public AgregarEscuelaDialog()
        {
            InitializeComponent();
            
            // Enfocar el textbox al abrir
            NombreEscuelaTextBox.Focus();
            
            // Suscribir eventos
            GuardarButton.Click += GuardarButton_Click;
            CancelarButton.Click += CancelarButton_Click;
            
            // Permitir presionar Enter para guardar
            NombreEscuelaTextBox.KeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    GuardarButton_Click(null, null);
                }
            };
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NombreEscuelaTextBox.Text))
            {
                MessageBox.Show("Por favor, ingresa el nombre de la escuela.", 
                    "Campo requerido", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Warning);
                return;
            }

            NombreEscuela = NombreEscuelaTextBox.Text.Trim();
            DialogResult = true;
            Close();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
