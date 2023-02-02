using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntOrganigramaNivelGenerico
    {
        public int IdNivel { set; get; }
        public string NombreNivel { set; get; }
        public string ClaveNivel { set; get; }
        public int IdRelacion { set; get; }
        public DateTime FechaCreacion { set; get; }
        public int UsuarioCreacion { set; get; }
        public DateTime FechaActualizacion { set; get; }
        public int UsuarioActualizacion { set; get; }
    }
}
