using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RendicionCuentasServices.Model;

namespace RendicionCuentasServices.DTO
{
    public class EntProcesos
    {
        public int? proceso_id { get; set; }
        public string proceso_nombre { get; set; }
        public string proceso_sigla { get; set; }
        public int proceso_avance { get; set; }
        public int? proceso_mejora { get; set; }
        public string proceso_color { get; set; }
        public List<EntIndicador> Indicadores { get; set; }
        public List<SP_VistaCargaIndicadoresProyectos_Result> IndicadoresProy { get; set; }
        public List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> IndicadoresGrup { get; set; }
    }
}
