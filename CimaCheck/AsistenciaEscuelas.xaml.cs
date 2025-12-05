using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Registro_de_carnets.Modelos;


namespace Registro_de_carnets;

public partial class AsistenciaEscuelas : UserControl
{
    #region Listas
    List<Persona> personas = new List<Persona>
    {
        new Persona { Nombre = "Ana Sofía Rodríguez García", Edad = 18, Genero = "F", Seleccionado = false },
        new Persona { Nombre = "Carlos Alberto Pérez López", Edad = 19, Genero = "M", Seleccionado = false },
        new Persona { Nombre = "María Fernanda González Martínez", Edad = 18, Genero = "F", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "M", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "M", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "O", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "M", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "M", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "M", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "M", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "M", Seleccionado = false },
        new Persona { Nombre = "Javier Hernández Cruz", Edad = 20, Genero = "M", Seleccionado = false }
    };

    private List<List<Persona>> listaEscuelas = new List<List<Persona>>()
    { };
    #endregion
    
    
    public AsistenciaEscuelas()
    {
        InitializeComponent();
        CargarTarjetas();
        

    }



    /// <summary>
    /// Crea las tarjetas de manera dinamica
    /// </summary>
    /// <param name="persona"></param>
    private void AgregarTarjeta(Persona persona)
    {
        // Border principal (la tarjeta)
        Border tarjeta = new Border
        {
            Background = new SolidColorBrush(Colors.White),
            BorderBrush = new SolidColorBrush(Color.FromRgb(230, 230, 230)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Margin = new Thickness(0, 0, 0, 12),
            Padding = new Thickness(16),
            Height = 70
        };

        // Grid interno
        Grid gridInterno = new Grid();
        gridInterno.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        gridInterno.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        // StackPanel para nombre y edad
        StackPanel infoPanel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Center
        };

        // Nombre
        TextBlock nombre = new TextBlock
        {
            Text = persona.Nombre,
            FontSize = 16,
            FontWeight = FontWeights.SemiBold,
            Foreground = new SolidColorBrush(Color.FromRgb(30, 30, 30))
        };

        // Panel horizontal para edad y género
        StackPanel edadGeneroPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(0, 4, 0, 0)
        };

        // Edad
        TextBlock edad = new TextBlock
        {
            Text = $"{persona.Edad} años",
            FontSize = 13,
            Foreground = new SolidColorBrush(Color.FromRgb(120, 120, 120)),
            Margin = new Thickness(0, 0, 8, 0)
        };

        // Icono de género
        Image iconoGenero = new Image
        {
            Width = 15,
            Height = 15,
            Source = new BitmapImage(new Uri(
                persona.Genero switch
                {
                    "M" => "Resources/icons/male.png",
                    "F" => "Resources/icons/female.png",
                    "O" => "Resources/icons/other.png"
                },
                UriKind.Relative
            ))
        };


        TextBlock genderTextBlock = new TextBlock
        {
            Text = (persona.Genero == "M") ? "Masculino" : (persona.Genero == "F") ? "Femenino" : "Otro",
            FontSize = 13,
            Foreground = new SolidColorBrush(Color.FromRgb(120, 120, 120)),
            Margin = new Thickness(0, 0, 8, 0)
        };

        edadGeneroPanel.Children.Add(edad);
        edadGeneroPanel.Children.Add(iconoGenero);
        edadGeneroPanel.Children.Add(genderTextBlock);

        infoPanel.Children.Add(nombre);
        infoPanel.Children.Add(edadGeneroPanel);

        // CheckBox a la derecha
        CheckBox checkBox = new CheckBox
        {
            IsChecked = persona.Seleccionado,
            VerticalAlignment = VerticalAlignment.Center,
            Width = 20,
            Height = 20,
            Style = (Style)FindResource("CheckBoxUABC") 
        };

        // Evento para manejar selección
        checkBox.Checked += (s, e) => persona.Seleccionado = true;
        checkBox.Unchecked += (s, e) => persona.Seleccionado = false;

        // Agregar al Grid
        Grid.SetColumn(infoPanel, 0);
        Grid.SetColumn(checkBox, 1);
        gridInterno.Children.Add(infoPanel);
        gridInterno.Children.Add(checkBox);

        // Agregar Grid al Border
        tarjeta.Child = gridInterno;

        // Agregar tarjeta al contenedor
        ContenedorTarjetas.Children.Add(tarjeta);
    }

    // Método para cargar todas las tarjetas
    private void CargarTarjetas()
    {
        // Limpiar tarjetas existentes
        ContenedorTarjetas.Children.Clear();

        // Lista de ejemplo
        
        // Agregar cada tarjeta
        foreach (var persona in personas)
        {
            AgregarTarjeta(persona);
        }
    }
    
    /// <summary>
    /// metodo para manejar el filtro en TextBox NombreCompletoTextBox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NombreCompletoTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ContenedorTarjetas.Children.Clear();
    
        string filtro = NombreCompletoTextBox.Text.Trim().ToLower();
    
        foreach (var asistente in personas)
        {
            if (asistente.Nombre.ToLower().Contains(filtro))
            {
                AgregarTarjeta(asistente);
            }
        }
    }
}
