using RendicionCuentasServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCRendicionCuentas.Controllers
{
    public class EntListProyecto
    {
        public int? IdPublicacion {get; set;}
        public DateTime? FechaPublicacion { get; set; }
        public List<EntProyecto> Proyectos { get; set; }
    }
}
