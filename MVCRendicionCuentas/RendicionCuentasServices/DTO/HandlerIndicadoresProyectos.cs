using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RendicionCuentasServices.Model;
using System.Data.Entity.Core.Objects;

namespace RendicionCuentasServices.DTO
{
    public class HandlerIndicadoresProyectos
    {
        public static List<SP_VistaCargaIndicadoresProyectos_Result> dataIndicadoresProyecto { get; set; }
        public static List<SP_VistaCargaIndicadores12MPIxReponsable_Result> dataIndicadoresProyecto12MPI { get; set; }
        public static List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> dataIndicadoresProyXResponsable { get; set; }
        public static int IdResponsable { get; set; }
      
        
    }
}
