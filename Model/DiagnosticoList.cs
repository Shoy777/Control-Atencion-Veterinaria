using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DiagnosticoList
    {
        public int DiagnosticoId { get; set; }
        public string VeterinarioNombre { get; set; }
        public string VeterinarioApellido { get; set; }
        public int ConsultaId { get; set; }
        public DateTime FechaDiagnostico { get; set; }
        public string Mascota { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }

    }
}
