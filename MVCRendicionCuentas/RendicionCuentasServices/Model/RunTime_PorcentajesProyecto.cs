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
    
    public partial class RunTime_PorcentajesProyecto
    {
        public int IdPorcentajesProyecto { get; set; }
        public Nullable<int> IdProyecto { get; set; }
        public Nullable<int> IdSubsistema { get; set; }
        public Nullable<int> Porcentaje { get; set; }
        public string Semaforo { get; set; }
    
        public virtual tbl_Proyectos tbl_Proyectos { get; set; }
        public virtual tbl_Subsistemas tbl_Subsistemas { get; set; }
    }
}