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
    
    public partial class tbl_Organigrama
    {
        public int idOrganigrama { get; set; }
        public string ClaveElemento { get; set; }
        public string NombreElemento { get; set; }
        public Nullable<int> NodoPadre { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<int> UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<int> UsuarioActualizacion { get; set; }
    
        public virtual tbl_Responsables tbl_Responsables { get; set; }
        public virtual tbl_Responsables tbl_Responsables1 { get; set; }
    }
}
