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
    
    public partial class SP_SelectAll_FechasSubEtapasxProyecto_Result
    {
        public int IdConfigFechasSubEtapasProyecto { get; set; }
        public Nullable<int> IdProyecto { get; set; }
        public Nullable<int> IdSubEtapa { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public string EstatusTiempo { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<double> ClaveSubEtapa { get; set; }
        public string NombreSubEtapa { get; set; }
        public Nullable<int> IdEtapa { get; set; }
        public Nullable<int> ClaveEtapa { get; set; }
        public string NombreEtapa { get; set; }
        public Nullable<System.DateTime> FechaInicioEtapa { get; set; }
        public Nullable<System.DateTime> FechaFinEtapa { get; set; }
        public Nullable<bool> EstatusEtapa { get; set; }
    }
}
