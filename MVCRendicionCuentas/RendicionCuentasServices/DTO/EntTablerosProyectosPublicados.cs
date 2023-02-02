using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntTablerosProyectosPublicados : SP_SelectAllPublicacionProyectos_Result
    {
        public List<EntProyecto> Etapas { get; set; }
    }
}
