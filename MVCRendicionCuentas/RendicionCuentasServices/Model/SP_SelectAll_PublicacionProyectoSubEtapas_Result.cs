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
    
    public partial class SP_SelectAll_PublicacionProyectoSubEtapas_Result
    {
        public int IdPublicacionSemaforoSubsistemaProyectoSubetapa { get; set; }
        public Nullable<int> IdPublicacionProyectoEtapa { get; set; }
        public string NombreSubEtapa { get; set; }
        public Nullable<int> IdSubEtapa { get; set; }
        public Nullable<int> IdPublicacion { get; set; }
        public Nullable<int> IdEtapa { get; set; }
        public Nullable<int> IdPublicacionProyecto { get; set; }
        public string Semaforo12MPI { get; set; }
        public Nullable<int> Valor12MPI { get; set; }
        public string SemaforoSASP { get; set; }
        public Nullable<int> ValorSASP { get; set; }
        public string SemaforoSAA { get; set; }
        public Nullable<int> ValorSAA { get; set; }
        public string SemaforoSAST { get; set; }
        public Nullable<int> ValorSAST { get; set; }
        public string EstatusTiempo { get; set; }
        public Nullable<System.DateTime> FechaFinSubEtapa { get; set; }
        public Nullable<int> IndicadoresCargados { get; set; }
        public Nullable<int> IndicadoresNoCargados { get; set; }
        public Nullable<double> Clave { get; set; }
    }
}