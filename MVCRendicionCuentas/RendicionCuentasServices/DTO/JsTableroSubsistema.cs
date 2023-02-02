using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class JsTableroSubsistema
    {
        public int? idpublicacion { get; set; }
        public int? avance { get; set; }
        public string fecha { get; set; }
        public string anio { get; set; }
        public string estatus { get; set; }
        public int mejora { get; set; }
        public int TotalProyectos { get; set; }
        public List<JsAvancesMes> avances_mes { get; set; }
        public List<JsTableroSubsistema2> tablero_subsistema { get; set; }
    }
}
