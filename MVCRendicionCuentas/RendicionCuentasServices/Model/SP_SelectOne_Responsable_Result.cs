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
    
    public partial class SP_SelectOne_Responsable_Result
    {
        public int idResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Correo { get; set; }
        public string Ficha { get; set; }
        public Nullable<int> IdPuesto { get; set; }
        public Nullable<int> IdArea { get; set; }
        public Nullable<int> IdAquienReporta { get; set; }
        public string Rol { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<int> IdUbicacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<int> UsuarioActualizacion { get; set; }
    }
}
