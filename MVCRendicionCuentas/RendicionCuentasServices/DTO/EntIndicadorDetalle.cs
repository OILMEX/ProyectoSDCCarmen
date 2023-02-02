using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntIndicadorDetalle
    {
        public int IdIndicador { get; set; }
        public Nullable<int> IdElemento { get; set; }
        public string Clave { get; set; }
        public string Prefijo { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public Nullable<bool> CheckSoporte { get; set; }
        public Nullable<bool> CheckComentarios { get; set; }
        public Nullable<bool> CheckReqSoporte { get; set; }
        public Nullable<bool> CheckReqComentario { get; set; }
        public Nullable<bool> CheckCreacionDesdeProyecto { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public string TipoIndicador { get; set; }
        public Nullable<int> TipoFrecuencia { get; set; }
        public Nullable<int> Meta { get; set; }
        public string TipoCalculo { get; set; }
        public string DescripcionValorFormula { get; set; }
        public string EtiquetaValorProgramado { get; set; }
        public string EtiquetaValorReal { get; set; }
        public Nullable<bool> CheckAplica { get; set; }
        public string Ayuda { get; set; }
        public string FechaCompromiso { get; set; }
    }
}
