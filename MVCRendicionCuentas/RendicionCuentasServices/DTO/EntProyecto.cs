using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RendicionCuentasServices.DTO
{
    public class EntProyecto
    {
        public int proyecto_id { get; set; }
        public string proyecto_clave { get; set; }
        public int? proyecto_idpublicacion { get; set; }
        public int? proyecto_idarea { get; set; }
        public int? proyecto_idresponsable { get; set; }
        public string proyecto_nombre { get; set; }
        public string proyecto_indicador_global { get; set; }
        public string proyecto_responsable { get; set; }
        public DateTime? proyecto_fecha_ini { get; set; }
        public DateTime? proyecto_fecha_fin { get; set; }
        public int? porcentaje_12MPI { get; set; }
        public string semaforo_12MPI { get; set; }
        public int? porcentaje_SAA { get; set; }
        public string semaforo_SAA { get; set; }
        public int? porcentaje_SASP { get; set; }
        public string semaforo_SASP { get; set; }
        public int? porcentaje_SAST { get; set; }
        public string semaforo_SAST { get; set; }
        public string EstatusTiempo { get; set; }
        public List<EntEtapa> Etapas { get; set; }
        public int? proyecto_idubicacion { get; set; }
        public int? proyecto_idCoordinacion { get; set; }
        public int? proyecto_idSuperintendencia { get; set; }
        public int? proyecto_idTipoProyecto { get; set; }
        public string proyecto_trace { get; set; }
        public int indicadorescargados { get; set; }
        public int indicadoresnocargados { get; set; }
    }
}
