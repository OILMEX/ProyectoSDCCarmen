using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntResponsableIndicador
    {
        public int? responsable_id { get; set; }
        public string responsable_nombre { get; set; }
        public int responsable_count_sasp { get; set; }
        public int responsable_count_saa { get; set; }
        public int responsable_count_sast { get; set; }
        public List<SP_VistaCargaIndicadores12MPIxReponsable_Result> Indicadores { get; set; }
        public List<EntProyecto> proyectos { get; set; }
    }
}
