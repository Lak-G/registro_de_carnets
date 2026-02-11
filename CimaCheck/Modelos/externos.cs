using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro_de_carnets.Modelos
{
    internal class externos
    {
        public int Id { get; set; }

        public int VisitanteId { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int IdProcedencia { get; set; }
        public string Edad { get; set; }

    }
}
