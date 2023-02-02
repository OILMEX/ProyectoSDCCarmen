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
    
    public partial class Dashboard_PublicacionProyectos
    {
        public Dashboard_PublicacionProyectos()
        {
            this.Dashboard_PublicacionProyectoEtapa = new HashSet<Dashboard_PublicacionProyectoEtapa>();
            this.Dashboard_PublicacionProyectoSubetapa = new HashSet<Dashboard_PublicacionProyectoSubetapa>();
        }
    
        public int IdPublicacionProyecto { get; set; }
        public string Semaforo { get; set; }
        public string NombreProyecto { get; set; }
        public string Responsable { get; set; }
        public Nullable<int> IdResponsable { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public Nullable<int> IdPublicacion { get; set; }
        public Nullable<int> IdProyecto { get; set; }
        public string Semaforo12MPI { get; set; }
        public Nullable<int> Valor12MPI { get; set; }
        public string SemaforoSASP { get; set; }
        public Nullable<int> ValorSASP { get; set; }
        public string SemaforoSAA { get; set; }
        public Nullable<int> ValorSAA { get; set; }
        public string SemaforoSAST { get; set; }
        public Nullable<int> ValorSAST { get; set; }
        public string IdTipoProyecto { get; set; }
        public string ElementoOrganigrama { get; set; }
        public Nullable<int> IndicadoresCargados { get; set; }
        public Nullable<int> IndicadoresNoCargados { get; set; }
    
        public virtual Dashboard_Publicaciones Dashboard_Publicaciones { get; set; }
        public virtual ICollection<Dashboard_PublicacionProyectoEtapa> Dashboard_PublicacionProyectoEtapa { get; set; }
        public virtual tbl_Proyectos tbl_Proyectos { get; set; }
        public virtual tbl_Responsables tbl_Responsables { get; set; }
        public virtual ICollection<Dashboard_PublicacionProyectoSubetapa> Dashboard_PublicacionProyectoSubetapa { get; set; }
    }
}
