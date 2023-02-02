using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RendicionCuentasServices.Model;

namespace RendicionCuentasServices.DTO
{
    public class EntResponsablesIndicador
    {
        public int? id_responsable { get; set; }
        public int? IdIndicador { get; set; }
        public int? IdEtapa { get; set; }
        public int? IdSubetapa { get; set; }
        public string ficha { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        //public SP_VistaCargaIndicadoresProyectos_Result DataIndicador { get; set; }
        public SP_VistaparaPivotGridProyectos_Result DataIndicador { get; set; }
        public SP_VistaCargaIndicadores12MPIxReponsable_Result DataIndicador12MPI { get; set; }
    }
}
