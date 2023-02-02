using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Model
{
    public class EntUsuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public string Email { get; set; }

        public string ficha { get; set; }
        public int? idPuesto { get; set; }
        public int? idArea { get; set; }
        public int? idAquienReporta { get; set; }
        public string Rol { get; set; }
        public int? idUbicacion { get; set; }
        public bool? Activo { get; set; }
        //public int? IdEmpleado { get; set; }
        //public bool isAdmin
        //{
        //    get { return this.IdRol == 1; }
        //}
    }
}
