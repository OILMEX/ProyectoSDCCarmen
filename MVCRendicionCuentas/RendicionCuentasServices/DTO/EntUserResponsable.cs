using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntUserResponsable
    {
        public int Listaid { get; set; }
        public string ListaNomnbre { get; set; }
        public List<EntUsuario> User { get; set; }
    }
}
