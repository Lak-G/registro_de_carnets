using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro_de_carnets.Modelos
{
    class Alumno
    {
        public int Id { get; set; }
        public int IdEscuela { get; set; }
        public int VisistanteId { get; set; }

        public string NombreAlumno { get; set; }
        public bool Asistencia { get; set; }

    }
}
