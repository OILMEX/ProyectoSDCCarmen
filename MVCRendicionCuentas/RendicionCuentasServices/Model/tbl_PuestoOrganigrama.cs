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
    
    public partial class tbl_PuestoOrganigrama
    {
        public int idPuestoOrganigrama { get; set; }
        public string NombrePuesto { get; set; }
        public Nullable<int> IdElemento { get; set; }
        public Nullable<int> IdTipoElementoOrganigrama { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public string Clave { get; set; }
    }
}
