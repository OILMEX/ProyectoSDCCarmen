using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntOrganigramaGerencias:tbl_Gerencias
    {
        public List<EntOrganigramaCoordinaciones> Coordinaciones { set; get; }
        public List<tbl_PuestoOrganigrama> Puestos { set; get; }
    }
}
