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
    
    public partial class tbl_Areas
    {
        public tbl_Areas()
        {
            this.tbl_Proyectos = new HashSet<tbl_Proyectos>();
        }
    
        public int idArea { get; set; }
        public string NombreArea { get; set; }
    
        public virtual ICollection<tbl_Proyectos> tbl_Proyectos { get; set; }
    }
}