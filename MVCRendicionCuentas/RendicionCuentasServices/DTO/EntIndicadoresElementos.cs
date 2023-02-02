using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntIndicadoresElementos
    {
        public int? indicador_IdIndicador { get; set; }
        public int? indicador_IdElemento { get; set; }
        public string indicador_Clave { get; set; }
        public string indicador_Prefijo { get; set; }
        public string indicador_DescripcionCorta { get; set; }
        public string indicador_DescripcionLarga { get; set; }
        //public bool? indicador_CheckSoporte { get; set; }
        //public bool? indicador_CheckComentarios { get; set; }
        //public bool? indicador_CheckReqSoporte { get; set; }
        //public bool? indicador_CheckReqComentario { get; set; }
        //public bool? indicador_CheckCreacionDesdeProyecto { get; set; }
        public string indicador_FechaCreacion { get; set; }
        public int? indicador_UsuarioCreacion { set; get; }
        public string indicador_FechaActualizacion { get; set; }
        public int? indicador_UsuarioActualizacion { get; set; }
        public bool? indicador_Estatus { get; set; }
        public string indicador_TipoIndicador { get; set; }
        public string indicador_TipoFrecuencia { get; set; }
        public int? indicador_Meta { get; set; }
        public string indicador_TipoCalculo { get; set; }
        public string indicador_Ayuda { get; set; }
        public string indicador_FechaCompromisoFormat { get; set; }
        public DateTime indicador_FechaCompromiso { get; set; }
        public bool? indicador_InidicadorFijo { get; set; }
        //public string indicador_DescripcionValorFormula { get; set; }
        //public string indicador_EtiquetaValorProgramado { get; set; }
        //public string indicador_EtiquetaValorReal { get; set; }
        //public bool? indicador_CheckAplica { get; set; }
    }
}
