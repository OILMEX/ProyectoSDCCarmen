//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RendicionCuentasServices.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Indicadores
    {
        public tbl_Indicadores()
        {
            this.Config_IndicadorResponsable = new HashSet<Config_IndicadorResponsable>();
            this.Config_SubEtapaIndicador = new HashSet<Config_SubEtapaIndicador>();
            this.Rel_ProgramaAsociadoIndicador = new HashSet<Rel_ProgramaAsociadoIndicador>();
        }
    
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
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<int> UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<int> UsuarioActualizacion { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public string TipoIndicador { get; set; }
        public Nullable<int> TipoFrecuencia { get; set; }
        public Nullable<int> Meta { get; set; }
        public string TipoCalculo { get; set; }
        public string DescripcionValorFormula { get; set; }
        public string EtiquetaValorProgramado { get; set; }
        public string EtiquetaValorReal { get; set; }
        public Nullable<bool> CheckAplica { get; set; }
        public Nullable<bool> IndicadorFijo { get; set; }
        public Nullable<System.DateTime> FechaCompromiso { get; set; }
        public string Ayuda { get; set; }
    
        public virtual ICollection<Config_IndicadorResponsable> Config_IndicadorResponsable { get; set; }
        public virtual ICollection<Config_SubEtapaIndicador> Config_SubEtapaIndicador { get; set; }
        public virtual ICollection<Rel_ProgramaAsociadoIndicador> Rel_ProgramaAsociadoIndicador { get; set; }
    }
}
