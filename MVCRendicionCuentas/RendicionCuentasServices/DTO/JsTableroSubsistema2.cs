using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class JsTableroSubsistema2
    {
        public int subsistema_id { get; set; }
        public int? subsistema_idpublicacion { get; set; }
        public string subsistema { get; set; }
        public string estatus { get; set; }
        public string Fecha { get; set; }
        public int? avance { get; set; }
        public int mejora { get; set; }
        public string completados { get; set; }
        public string color { get; set; }
        public List<JsAvancesMes> avances_mes { get; set; }
        public List<EntProcesos> procesos { get; set; }
    }
}
