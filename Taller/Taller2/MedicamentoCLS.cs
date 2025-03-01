using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class MedicamentoCLS
    {
        public int idMedicamento { get; set; }
        public string nombre { get; set; }
        public string nombreLaboratorio { get; set; }
        public string nombreTipoMedicamento { get; set; }
        public int idLab { get; set; }
        public int idTipoMed { get; set; }
        public string uso { get; set; }
        public string contenido { get; set; }
        public string codigo { get; set; }

    }
}
