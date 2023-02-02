using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RendicionCuentasServices.DTO
{
    public class EntJsonEtapa
    {
        public int etapa_idetapa { get; set;}
        public string etapa_nombre { get; set; }
        public string etapa_nombre_largo { get; set; }
        public string etapa_indicador { get; set; }
        public string etapa_color { get; set; }
        public string etapa_actualizado { get; set; }
        public List<EntIndicador> indicadores { get; set; }
    }
}
