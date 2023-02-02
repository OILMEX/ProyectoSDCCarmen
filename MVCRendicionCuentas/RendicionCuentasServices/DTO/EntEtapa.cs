using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RendicionCuentasServices.DTO
{
    public class EntEtapa
    {
        public int? etapa_id { get; set;}
        public string etapa_nombre { get; set; }
        public float? clave { get; set; }
        public int? UsuarioCreacion { get; set; }
        public int? UsuarioActualizacion { get; set; }
        public bool? estatus { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public List<EntSubEtapa> SubEtapas { get; set; }
    }
}
