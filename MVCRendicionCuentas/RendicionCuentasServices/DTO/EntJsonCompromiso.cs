using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class EntJsonCompromiso
    {
        public List<EntSubsistemaC> subsistemas { get; set; }
        public List<EntEtapaC> etapas { get; set; }
    }

    public class EntSubsistemaC
    {
        public int subsistema_id { get; set; }
        public string subsistema_nombre { get; set; }
        public string subsistema_nombre_largo { get; set; }
        public bool? subsistema_checkaplicaproyecto { get; set; }
        public string color { get; set; }
        public List<EntElementosCompromiso> elementos { get; set; }
    }

    public class EntEtapaC
    {
        public int etapa_id { get; set; }
        public string etapa_nombre { get; set; }
        public bool etapa_estatus { get; set; }
        public string etapa_fechacreacion { get; set; }
        public int? etapa_usuariocreacion { get; set; }
        public string etapa_fechaactualizacion { get; set; }
        public int? etapa_usuarioactualizacion { get; set; }
        public int? etapa_clave { get; set; }
        public List<EntSubEtapaC> subetapas { get; set; }
    }

    public class EntSubEtapaC
    {
        public int etapa_id { get; set; }
        public string etapa_nombre { get; set; }
        public string etapa_estatus { get; set; }
        public string etapa_clave { get; set; }
    }

    public class EntElementosCompromiso
    {
        public int elemento_id { get; set; }
        public int elemento_idsubsistema { get; set; }
        public string elemento_nombre { get; set; }
        public string elemento_descripcionelemento { get; set; }
        public string elemento_estatus { get; set; }
        public string elemento_fechacreacion { get; set; }
        public int? elemento_usuariocreacion { get; set; }
        public string elemento_fechaactualizacion { get; set; }
        public int? elemento_usuarioactualizcion { get; set; }
        public List<EntIndicadorC> indicadores { get; set; }
    }

    public class EntIndicadorC
    {
        public int? indicador_IdIndicador { get; set; }
        public int? indicador_IdElemento { get; set; }
        public string indicador_Clave { get; set; }
        public string indicador_Prefijo { get; set; }
        public string indicador_DescripcionCorta { get; set; }
        public string indicador_DescripcionLarga { get; set; }
        public bool? indicador_CheckSoporte { get; set; }
        public bool? indicador_CheckComentarios { get; set; }
        public bool? indicador_CheckReqSoporte { get; set; }
        public bool? indicador_CheckReqComentario { get; set; }
        public bool? indicador_CheckCreacionDesdeProyecto { get; set; }
        public string indicador_FechaCreacion { get; set; }
        public int? indicador_UsuarioCreacion { set; get; }
        public string indicador_FechaActualizacion { get; set; }
        public int? indicador_UsuarioActualizacion { get; set; }
        public bool? indicador_Estatus { get; set; }
        public string indicador_TipoIndicador { get; set; }
        public string indicador_TipoFrecuencia { get; set; }
        public int? indicador_Meta { get; set; }
        public string indicador_TipoCalculo { get; set; }
        public string indicador_DescripcionValorFormula { get; set; }
        public string indicador_EtiquetaValorProgramado { get; set; }
        public string indicador_EtiquetaValorReal { get; set; }
        public bool? indicador_CheckAplica { get; set; }
    }
}
