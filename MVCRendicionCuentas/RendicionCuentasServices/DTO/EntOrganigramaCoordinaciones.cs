using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntOrganigramaCoordinaciones:tbl_Coordinaciones
    {
       public List<EntOrganigramaSuperintendencias> Superintendecias { set; get; }
       public List<tbl_PuestoOrganigrama> Puestos { set; get; }
    }
}
