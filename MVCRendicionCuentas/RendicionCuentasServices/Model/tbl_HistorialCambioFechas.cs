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
    
    public partial class tbl_HistorialCambioFechas
    {
        public int idCambioFecha { get; set; }
        public int idProyecto { get; set; }
        public int idSubetapa { get; set; }
        public int idEtapa { get; set; }
        public Nullable<System.DateTime> FechaInicioAnt { get; set; }
        public Nullable<System.DateTime> FechaFinAnt { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public string Motivo { get; set; }
        public int IdResponsable { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    }
}