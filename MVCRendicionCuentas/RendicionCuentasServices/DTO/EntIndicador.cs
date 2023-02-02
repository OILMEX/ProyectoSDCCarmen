using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RendicionCuentasServices.DTO
{
    public class EntIndicador
    {
        public int? indicador_id { get; set; }
        public int? IdConfigIndicadorResponsable { get; set; }
        public int? Idelemento { get; set; }
        public string indicador_clave { get; set; }
        public string indicador_nombre { get; set; }
        public string indicador_estatus { get; set; }
        public string indicador_fechactualizado { get; set; }
        public string indicador_acttualizacion { get; set; }
        public string indicador_numero { get; set; }
        public string indicador_meta { get; set; }
        public string indicador_tipo { get; set; }
        public string indicador_tipo_dato { get; set; }
        public string indicador_soporte { get; set; }       
        public string indicador_info { get; set; }
        public string indicador_check { get; set; }
        public string indicador_checklist { get; set; }
        public string indicador_notas { get; set; }
        public string indicador_semaforo { get; set; }
        public bool? indicador_creacionproy { get; set; }
        public DateTime? indicador_fechacompromiso { get; set; }
        public Nullable<int> ValorProcesado { get; set; }
        public List<EntResponsablesIndicador> Responsables { get; set; }
    }
}
