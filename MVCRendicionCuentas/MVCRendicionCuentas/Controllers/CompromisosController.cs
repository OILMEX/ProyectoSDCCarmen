using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCRendicionCuentas.Controllers
{
    public class CompromisosController : ValidatorController
    {
        srvCompromiso srvCompromisos = new srvCompromiso();
        
        

        public ActionResult Index()
        {
             if (myCookie!=null)
            {
            return View();
            }
             else
             {
                 return RedireccionaLogin();
             }

        }

        [HttpGet]
        public JsonResult GetCompromisosInit()
        {
            try
            {
                EntJsonCompromiso JsonCompromisos = new EntJsonCompromiso();
                JsonCompromisos.subsistemas = new List<EntSubsistemaC>();
                List<tbl_Subsistemas> lstSubsistemas = srvCompromisos.Subsistemas(true);
                foreach (tbl_Subsistemas Subsistema in lstSubsistemas)
                {
                    if(Subsistema.IdSubsistema!=5)
                    { 
                    EntSubsistemaC ss = new EntSubsistemaC();
                    ss.subsistema_id = Subsistema.IdSubsistema;
                    ss.subsistema_nombre = Subsistema.NombreSubsistema;
                    ss.subsistema_nombre_largo = Subsistema.DescripcionLargaSubsistema;
                    ss.subsistema_checkaplicaproyecto = Subsistema.CheckAplicaProyecto;
                    ss.color = getColor(ss.subsistema_nombre);
                    ss.elementos = srvCompromisos.getElementos(Subsistema.IdSubsistema);
                    JsonCompromisos.etapas = srvCompromisos.getEtapas();
                    JsonCompromisos.subsistemas.Add(ss);
                    }
                }



                return Json(new JavaScriptSerializer().Serialize(JsonCompromisos), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("No se encontraron subsistemas");
            }
        }

        [HttpGet]
        public JsonResult GetCompromisos(int idSubetapa)
        {
            try
            {
                EntJsonCompromiso JsonCompromisos = new EntJsonCompromiso();
                JsonCompromisos.subsistemas = new List<EntSubsistemaC>();
                List<tbl_Subsistemas> lstSubsistemas = srvCompromisos.Subsistemas(false);
                foreach (tbl_Subsistemas Subsistema in lstSubsistemas)
                {
                    EntSubsistemaC ss = new EntSubsistemaC();
                    ss.subsistema_id = Subsistema.IdSubsistema;
                    ss.subsistema_nombre = Subsistema.NombreSubsistema;
                    ss.subsistema_nombre_largo = Subsistema.DescripcionLargaSubsistema;
                    ss.subsistema_checkaplicaproyecto = Subsistema.CheckAplicaProyecto;
                    ss.color = getColor(ss.subsistema_nombre);
                    ss.elementos = srvCompromisos.getElementos(Subsistema.IdSubsistema, idSubetapa);
                    JsonCompromisos.etapas = srvCompromisos.getEtapas();
                    JsonCompromisos.subsistemas.Add(ss);
                }



                return Json(new JavaScriptSerializer().Serialize(JsonCompromisos), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("No se encontraron subsistemas",  JsonRequestBehavior.AllowGet);
            }
        }

        private string getColor(string ss)
        {
            string str = string.Empty;
            switch (ss)
            {
                case "12 MPI":
                    str = "#D64148";
                    break;
                case "SASP":
                    str = "#008EB2";
                    break;
                case "SAA":
                    str = "#38A125";
                    break;
                case "SAST":
                    str = "#74716A";
                    break;
            }

            return str;
        }

        [HttpPost]
        public string UpdateIndicador12mpi(int idIndicador, string Status)
        {
            string M = string.Empty;
            if (idIndicador > 0)
            {
                M = srvCompromisos.SetEstatusIndicador(idIndicador, bool.Parse(Status));
            }
            return M;
        }

        [HttpPost]
        public JsonResult UpdateAllIndicador12mpi(string Status)
        {
            int r = 0;
            if (Status != null)
            {            
                List<JsEstatusIndicadores> ListStatus = new JavaScriptSerializer().Deserialize<List<JsEstatusIndicadores>>(Status);
                foreach (JsEstatusIndicadores elem in ListStatus)
                {
                   srvCompromisos.SetEstatusIndicador(elem.idIndicador, bool.Parse(elem.Estatus)); 
                }
                r = 1;
            }
            var json = Json(r);
            return json;
        }

        [HttpPost]
        public JsonResult UpdateAllIndicador(string Status)
        {
            int r = 0;
            if (Status != null)
            {
                List<JsEstatusIndicadores> ListStatus = new JavaScriptSerializer().Deserialize<List<JsEstatusIndicadores>>(Status);
                foreach (JsEstatusIndicadores elem in ListStatus)
                {
                    srvCompromisos.SetEstatusIndicador(elem.idIndicador, elem.idSubetapa,bool.Parse(elem.Estatus));
                }
                r = 1;
            }
            var json = Json(r);
            return json;
        }

        [HttpPost]
        public string UpdateIndicador(int idIndicador, int idSubetapa, string Status)
        {
            string M = string.Empty;
            if (idIndicador > 0)
            {
                M = srvCompromisos.SetEstatusIndicador(idIndicador, idSubetapa, bool.Parse(Status));
            }
            return M;
        }

        [HttpPost]
        public JsonResult SaveUsers(int idIndicador, string[] User, string[] NoUser)
        {
            string Msj = string.Empty;
            Object documentos = new Object();
            if(idIndicador > 0)
            {
                srvCompromisos.SaveUsers(idIndicador, User, NoUser);
                documentos = new { accion = true, msj = "Informacion guardada correctamente" };
                var json = Json(new JavaScriptSerializer().Serialize(documentos));
                return json;
            }
            else
            {
                documentos = new { accion = false, msj = "El ID del indicador viene en 0" };
                var json = Json(new JavaScriptSerializer().Serialize(documentos));
                return json;
            }
        }

        [HttpPost]
        public JsonResult GetUsers(int idIndicador)
        {
            srvCompromisos.setRol(Rol);
           string Msj = string.Empty;
           List<EntUserResponsable> Users = srvCompromisos.GetListUsers(idIndicador, IdUbicacion);
           return Json(new JavaScriptSerializer().Serialize(Users));
        }

        [HttpPost]
        public JsonResult infoIndicador(int idIndicador)
        {
            EntIndicadorDetalle Indicador = srvCompromisos.GetInfoIndicador(idIndicador);
            return Json(new JavaScriptSerializer().Serialize(Indicador));
        }

        public JsonResult addFechaCompromiso(int IdElemento, string FechaCompromiso)
        {
            int IdResult = 1;
            try
            {
                srvCompromisos.setFechaCompromiso(IdElemento, FechaCompromiso);
                return Json(new JavaScriptSerializer().Serialize(IdResult), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                IdResult = 0;
                return Json(new JavaScriptSerializer().Serialize(IdResult), JsonRequestBehavior.AllowGet);
            }
        }
    }
}