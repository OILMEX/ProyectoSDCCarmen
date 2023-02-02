using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RendicionCuentasServices.DTO
{
    public class EntSubEtapa
    {
        public int? subetapa_id { get; set; }
        public string subetapa_nombre { get; set; }
        public double? clave { get; set; }
        public bool? estatus { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public List<EntSubsistema> Subsistemas { get; set; }
    }
}
