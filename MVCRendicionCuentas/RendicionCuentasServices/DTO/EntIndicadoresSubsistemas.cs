using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntIndicadoresSubsistemas
    {
        public int IdSubsistema { get; set; }
        public string NombreSubsistema { get; set; }
        public string DescripcionLargaSubsistema { get; set; }
        public string Semaforo { get; set; }
        public bool CheckAplicaProyecto { get; set; }
        public bool Estatus { get; set; }
        public List<EntElementos> Elementos { get; set; }
    }
}
