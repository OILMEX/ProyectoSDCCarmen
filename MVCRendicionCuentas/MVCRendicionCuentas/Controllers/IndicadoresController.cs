using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Services;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace MVCRendicionCuentas.Controllers
{
    public class IndicadoresController : ValidatorController
    {
        srvIndicadores srvIndicadores = new srvIndicadores();

       
        public ActionResult Index()
        {
            if (myCookie != null)
            {
                return View();
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        public ActionResult Carga()
        {
            return View();
        }

        [HttpGet]
        public JsonResult IndicadoresSubsistemas()
        {
            try {
                List<EntIndicadoresSubsistemas> JsonIndicadoresSubsistemas = new List<EntIndicadoresSubsistemas>();
                List<tbl_Subsistemas> lstSubsistemas = srvIndicadores.Subsistemas();
                foreach(tbl_Subsistemas Subsistema in lstSubsistemas){
                    EntIndicadoresSubsistemas IndicadoresSubsistemas = new EntIndicadoresSubsistemas();
                    IndicadoresSubsistemas.IdSubsistema = Subsistema.IdSubsistema;
                    IndicadoresSubsistemas.NombreSubsistema = Subsistema.NombreSubsistema;
                    IndicadoresSubsistemas.DescripcionLargaSubsistema = Subsistema.DescripcionLargaSubsistema;
                    IndicadoresSubsistemas.CheckAplicaProyecto = Subsistema.CheckAplicaProyecto.Value;
                    IndicadoresSubsistemas.Estatus = Subsistema.Estatus.Value;
                    IndicadoresSubsistemas.Elementos = srvIndicadores.ElementosPorSubsistema(Subsistema.IdSubsistema);
                    JsonIndicadoresSubsistemas.Add(IndicadoresSubsistemas);
                }

                return Json(new JavaScriptSerializer().Serialize(JsonIndicadoresSubsistemas), JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                return Json("No se encontraron subsistemas");
            }

        }

        [HttpGet]
        public JsonResult addElemento(string idEtapa, string claveEtapa, string nombreEtapa)
        {
            try
            {
                tbl_Elementos item = new tbl_Elementos();
                item.IdSubsistema = int.Parse(idEtapa);
                item.NombreElemento = claveEtapa;
                item.DescripcionElemento = nombreEtapa;
                if (IdCargaResponsable != null)
                {
                    item.UsuarioActualizacion = IdCargaResponsable;
                    item.UsuarioCreacion = IdCargaResponsable;
                }
                item.FechaCreacion = DateTime.Now;
                item.FechaActualizacion = DateTime.Now;
                item.Estatus = true;
                int IdElemento = srvIndicadores.AddElementos(item);
                if (IdElemento > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(IdElemento), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult editElemento(string idEtapa, string claveEtapa, string nombreEtapa)
        {
            try
            {
                tbl_Elementos item = new tbl_Elementos();
                item.IdElemento = int.Parse(idEtapa);
                item.NombreElemento = claveEtapa;
                item.DescripcionElemento = nombreEtapa;
                if (IdCargaResponsable != null)
                {
                    item.UsuarioActualizacion = IdCargaResponsable;
                }
                item.FechaActualizacion = DateTime.Now;
                int IdElemento = srvIndicadores.EditElementos(item);
                if (IdElemento > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(IdElemento), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult deleteElemento(string idEtapa)
        {
            try
            {
                tbl_Elementos item = new tbl_Elementos();
                item.IdElemento = int.Parse(idEtapa);
                if (IdCargaResponsable != null)
                {
                    item.UsuarioActualizacion = IdCargaResponsable;
                }
                item.FechaActualizacion = DateTime.Now;
                srvIndicadores.DeleteElementos(item);

                return Json(new JavaScriptSerializer().Serialize(int.Parse(idEtapa)), JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult IndicadoresElemento(int IdElemento)
        {
            try
            {
                List<EntElementos> JsonIndicadoresElemento = new List<EntElementos>();
                JsonIndicadoresElemento = srvIndicadores.GetIndicadoresElemento(IdElemento);

                return Json(new JavaScriptSerializer().Serialize(JsonIndicadoresElemento), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("No se encontraron Indicadores");
            }

        }

        [HttpGet]
        public JsonResult editIndicador(string idIndicador, string Clave, string Prefijo, string DescripcionCorta, string DescripcionLarga, string Ayuda)
        {
            try
            {
                tbl_Indicadores item = new tbl_Indicadores();
                item.IdIndicador = int.Parse(idIndicador);
                item.Clave = Clave;
                item.Prefijo = Prefijo;
                item.DescripcionCorta = DescripcionCorta;
                item.DescripcionLarga = DescripcionLarga;
                item.Ayuda = Ayuda;
                item.FechaActualizacion = DateTime.Now;
                if (IdCargaResponsable != null)
                {
                    item.UsuarioActualizacion = IdCargaResponsable;
                }
                int IdElemento = srvIndicadores.EditInidicador(item,"sencillo");
                if (IdElemento > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(IdElemento), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult deleteIndicador(string idIndicador)
        {
            try
            {
                tbl_Indicadores item = new tbl_Indicadores();
                item.IdIndicador = int.Parse(idIndicador);
                if (IdCargaResponsable != null)
                {
                    item.UsuarioActualizacion = IdCargaResponsable;
                }
                item.FechaActualizacion = DateTime.Now;
                srvIndicadores.DeleteIndicador(item);

                return Json(new JavaScriptSerializer().Serialize(int.Parse(idIndicador)), JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult addIndicador(tbl_Indicadores Indicador, string CheckSoporte, string CheckComentarios, string CheckReqSoporte, string CheckReqComentario)
        {
            try
            {
                tbl_Indicadores item = new tbl_Indicadores();
                item = Indicador;
                if (IdCargaResponsable != null)
                {
                    item.UsuarioCreacion = IdCargaResponsable;
                }
                item.FechaCreacion = DateTime.Now;
                item.CheckSoporte = (CheckSoporte == "on") ? true : false;
                item.CheckComentarios = (CheckComentarios == "on") ? true : false;
                item.CheckReqSoporte = (CheckReqSoporte == "on") ? true : false;
                item.CheckReqComentario = (CheckReqComentario == "on") ? true : false;
                item.Estatus = true;
                item.CheckCreacionDesdeProyecto = false;
                var IdIndicador = srvIndicadores.AddIndicador(item);
                if (IdIndicador > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(IdIndicador), JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ProgramaAsociadoIndicador(int IdIndicador)
        {
            try
            {
                EntProgramaAsociadoIndicador JsonProgramaAsociadoIndicador = new EntProgramaAsociadoIndicador();
                JsonProgramaAsociadoIndicador = srvIndicadores.GetProgramaAsociadoIndicador(IdIndicador);

                return Json(new JavaScriptSerializer().Serialize(JsonProgramaAsociadoIndicador), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult EditProgramaAsociadoIndicador(EntProgramaAsociadoIndicador Programa)
        {
            try
            {
                tbl_Indicadores item = new tbl_Indicadores();
                if (IdCargaResponsable != null)
                {
                    Programa.UsuarioActualizacion = IdCargaResponsable;
                }
                Programa.FechaActualizacion = DateTime.Now;
                int IdIndicador = srvIndicadores.EditProgramaAsociadoIndicador(Programa);
                if (IdIndicador > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(IdIndicador), JsonRequestBehavior.AllowGet);
                }
                else { 
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
	}
}