using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RendicionCuentasServices.Model;
using System.Data.Entity.Core.Objects;

namespace RendicionCuentasServices.DTO
{
    public class HandlerDataProyecto
    {
        public static List<SP_SelectAllResponsablesIndicadorXProyecto_Result> listaResponsables { get; set; }
        public static List<SP_SelectAll_ConfiguracionIndicadoresSubEtapasxProyecto_Result> listaConfiguracionIndicadoresSubEtapasProyecto { get; set; }
        public static List<SP_SelectAll_FechasSubEtapasxProyecto_Result> listaFechasSubEtapasxProyecto { get; set; }
    }
}
