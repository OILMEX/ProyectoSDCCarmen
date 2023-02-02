using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntElementos
    {
        public int IdElemento { get; set; }
        public int IdSubsistema { get; set; }
        public string NombreElemento { get; set; }
        public string DescripcionElemento { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaActualizacion { get; set; }
        public int UsuarioActualizacion { get; set; }
        public string ResponsableCreacion { get; set; }
        public string ResponsableActualizacion { get; set; }
        public bool Estatus { get; set; }
        public List<EntIndicadoresElementos> Indicadores { get; set; }
    }
}
