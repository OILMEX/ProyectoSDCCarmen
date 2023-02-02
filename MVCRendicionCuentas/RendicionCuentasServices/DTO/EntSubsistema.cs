using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntSubsistema
    {
        public int? id_subsistema { get; set; }
        public string subsistema { get; set; }
        public string siglas { get; set; }
        public List<EntProcesos> Procesos { get; set; }
        public List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> Indicadores { get; set; }
        public List<SP_VistaCargaIndicadoresProyectos_Result> IndicadoresProy { get; set; }
    }
}
