using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Registro_de_carnets.Modelos;
using Registro_de_carnets.Services;
using Color = System.Windows.Media.Color;

namespace Registro_de_carnets;

public partial class CimaRegistro : UserControl
{
    #region Listas
    private List<String> listFacultades = new List<string>()
    {
        "Facultad de ingenieria Mexicali (FIM)",
        "Facultad de arquitectura y diseño (FAD)",
        "Facultad de derecho",
        "Facultad de Pedagogía e Innovación Educativa (FPIE)",
        "Facultad de Idiomas",
        "Facultad de Artes",
        "Facultad de Deportes"
        
    };

    private List<String> listIngenieria = new List<string>()
    {
        "Licenciatura en Sistemas Computacionales",
        "Ingeniería Aeroespacial",
        "Ingeniería Civil",
        "Ingeniería Eléctrica",
        "Ingeniería Electrónica",
        "Ingeniería en Computación",
        "Ingeniería en Energías Renovables",
        "Ingeniería en Semiconductores y Microelectrónica",
        "Ingeniería Industrial",
        "Ingeniería Mecánica",
        "Ingeniería Mecatrónica",
        "Bioingeniería"
    };
    
    private List<String> listArquitectura = new List<string>()
    {
        "Arquitectura",
        "Licenciatura en Diseño Gráfico",
        "Licenciatura en Diseño Industrial"
    };

    private List<String> listDerecho = new List<string>()
    {
        "Licenciatura en Derecho"
    };

    private List<String> listFPIE = new List<string>()
    {
        "Licenciatura en Psicopedagogía",
        "Licenciatura en Docencia de la Matemática",
        "Licenciatura en Docencia de la Lengua y la Literatura",
        "Licenciatura en Docencia de las Ciencias"
    };
    
    private List<String> listIdiomas = new List<string>()
    {
        "Licenciatura en Traducción",
        "Licenciatura en Enseñanza de Lenguas"
    };

    private List<String> listArtes = new List<string>()
    {
        "Licenciatura en Artes Cinematográficas y Producción Audiovisual",
        "Licenciatura en Animación",
        "Licenciatura en Artes Visuales"
    };


    private List<String> listDeportes = new List<string>()
    {
        "Licenciatura en Actividad Física y Deporte"
    };
    #endregion

    

    public CimaRegistro()
    {
        InitializeComponent();
        LoadFacultyList();
    }

    /// <summary>
    /// Sube los datos a la base de datos.
    /// Maneja las validaciones de los campos de texto y las selecciones obligatorias
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MatriculaTextBox.Text == "" || GenderComboBox.SelectedIndex == 0 || ProEdComboBox.SelectedIndex == 0 || FacultyComboBox.SelectedIndex == 0 || FullNameTextBox.Text.Trim() == "")
        {
            MatriculaLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            GenderLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            FacultyLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            ProEdLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            FullNameLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            return;
        }
        
        MatriculaLabel.Foreground = new SolidColorBrush(Colors.Black);
        GenderLabel.Foreground = new SolidColorBrush(Colors.Black);
        FacultyLabel.Foreground = new SolidColorBrush(Colors.Black);
        ProEdLabel.Foreground = new SolidColorBrush(Colors.Black);
        FullNameLabel.Foreground = new SolidColorBrush(Colors.Black);
        
        //funcionalidad para subir a base de datos *En trabajo*
    }
    /// <summary>
    /// Metodo para que el textbox solo tenga letras
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MatriculaTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Solo permite números
        e.Handled = !IsTextNumeric(e.Text);
    }

    private bool IsTextNumeric(string text)
    {
        return text.All(char.IsDigit);
    }

    // private async void FacultyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    // {
    //     switch (FacultyComboBox.SelectedItem.ToString())
    //     {
    //         case "Facultad de ingenieria Mexicali (FIM)":
    //             var lsCarreras = DataManager.ObtenerCarrerasPorFacultadAsync(FacultyComboBox.SelectedIndex);
    //
    //             LoadProEdList(lsCarreras);
    //             break;
    //         case "Facultad de arquitectura y diseño (FAD)":
    //             LoadProEdList(listArquitectura);
    //             break;
    //         case "Facultad de derecho":
    //             LoadProEdList(listDerecho);
    //             ProEdComboBox.SelectedIndex = 1;
    //             break;
    //         case "Facultad de Pedagogía e Innovación Educativa (FPIE)":
    //             LoadProEdList(listFPIE);
    //             break;
    //         case "Facultad de Idiomas":
    //             LoadProEdList(listIdiomas);
    //             break;
    //         case "Facultad de Artes":
    //             LoadProEdList(listArtes);
    //             break;
    //         case "Facultad de Deportes":
    //             LoadProEdList(listDeportes);
    //             ProEdComboBox.SelectedIndex = 1;
    //             break;
    //     }
    // }

    private async void FacultyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        List<Carrera> lsCarreras = await DataManager.ObtenerCarrerasPorFacultadAsync(FacultyComboBox.SelectedIndex);

        LoadProEdList(lsCarreras);
        
        object primerItem = ProEdComboBox.Items[0];
    
        ProEdComboBox.Items.Clear();
    
        ProEdComboBox.Items.Add(primerItem);
    
        foreach (var carrera in lsCarreras)
        {
            ProEdComboBox.Items.Add(carrera.Nombre);
        }
    
        ProEdComboBox.SelectedIndex = 0;
    }
    
    /// <summary>
    /// Metodo para agregar las carreras al comboBox de programa educativo
    /// </summary>
    /// <param name="listCarreras"></param>
    private void LoadProEdList(List<Carrera> listCarreras)
    {

        object primerItem = ProEdComboBox.Items[0];
    
        ProEdComboBox.Items.Clear();
    
        ProEdComboBox.Items.Add(primerItem);
    
        foreach (var carrera in listCarreras)
        {
            ProEdComboBox.Items.Add(carrera.Nombre);
        }
    
        ProEdComboBox.SelectedIndex = 0;
    }

    /// <summary>
    /// Metodo para cargar las carreras al inicio de la pantalla
    /// </summary>
    private async void LoadFacultyList()
    {
        try
        {
            List<Facultad> lsFacultades = await DataManager.ObtenerFacultadesAsync();
            
            foreach (var facultad in lsFacultades)
            {
                FacultyComboBox.Items.Add(facultad.Nombre);
                Console.WriteLine(facultad.Nombre);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error \"{e.Message}\" al obtener las Facultades");
        }
        
        
        
        FacultyComboBox.SelectedIndex = 0;
    }
}