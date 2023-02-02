using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntUsuariosEliminados
    {
        public int idResponsable { get; set; }
        public string Ficha { get; set; }
        public string NombreResponsable { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public string NombreArea { get; set; }
        public string UsuarioElimino { get; set; }
        public Nullable<DateTime> FechaActualizacion { get; set; }
    }
}
