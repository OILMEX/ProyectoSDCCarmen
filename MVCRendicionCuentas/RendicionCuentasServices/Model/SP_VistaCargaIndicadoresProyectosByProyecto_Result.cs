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
    
    public partial class SP_VistaCargaIndicadoresProyectosByProyecto_Result
    {
        public Nullable<int> IdConfigIndicadorResponsable { get; set; }
        public string Semaforo { get; set; }
        public string Prefijo { get; set; }
        public string DescripcionCorta { get; set; }
        public string EstatusLlenado { get; set; }
        public Nullable<int> IdIndicador { get; set; }
        public Nullable<int> IdElemento { get; set; }
        public Nullable<int> IdSubsistema { get; set; }
        public string TipoIndicador { get; set; }
        public Nullable<int> TipoFrecuencia { get; set; }
        public string TipoCalculo { get; set; }
        public Nullable<int> Meta { get; set; }
        public Nullable<int> IdProyecto { get; set; }
        public Nullable<int> IdEtapa { get; set; }
        public Nullable<int> IdSubEtapa { get; set; }
        public Nullable<int> IdDataValoresIndicadores { get; set; }
        public Nullable<int> Valor { get; set; }
        public Nullable<bool> CheckSoporte { get; set; }
        public Nullable<bool> CheckComentarios { get; set; }
        public string SemaforoSoportes { get; set; }
        public string SemaforoComentarios { get; set; }
        public Nullable<int> ClaveEtapa { get; set; }
        public string NombreEtapa { get; set; }
        public Nullable<double> ClaveSubEtapa { get; set; }
        public string NombreSubEtapa { get; set; }
        public string NombreSubsistema { get; set; }
        public string DescripcionLargaSubsistema { get; set; }
        public string NombreElemento { get; set; }
        public string DescripcionElemento { get; set; }
        public Nullable<System.DateTime> FechaFinEtapa { get; set; }
        public Nullable<System.DateTime> FechaFinSubEtapa { get; set; }
        public Nullable<int> IdResponsable { get; set; }
        public Nullable<bool> EstatusSubEtapa { get; set; }
    }
}
