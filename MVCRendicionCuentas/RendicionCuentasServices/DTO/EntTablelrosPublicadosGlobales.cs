using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RendicionCuentasServices.Model;

namespace RendicionCuentasServices.DTO
{
    public class EntTablelrosPublicadosGlobales
    {
        public int TotalProyectos { get; set; }
        public List<EntTablerosSubsistemasPublicados> Subsistemas { get; set; }
        public List<EntTablerosProyectosPublicados> Proyectos { get; set; }
    }
}
