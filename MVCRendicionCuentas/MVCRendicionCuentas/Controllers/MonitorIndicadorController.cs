using Newtonsoft.Json;
using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace MVCRendicionCuentas.Controllers
{
    public class MonitorIndicadorController : ValidatorController
    {
        public ActionResult Index()
        {
            if (myCookie!=null)
            {
                srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);

                srvUsuarios srv = new srvUsuarios();

                int contProyEjecucion = srv.GetContProyejecucion("En Proceso", IdCargaResponsable);
                String[] nombreProyectos = srv.GetNombreProyEjecucion("En Proceso", IdCargaResponsable);
                ViewData["LstFaltante"] = srv.GetALLIndicadoresFaltante(IdCargaResponsable);
                ViewData["ContPEjec"] = contProyEjecucion;
                ViewData["Proyectos"] = nombreProyectos;
                return View();
            }
            else
            {
                return RedireccionaLogin();
            }
          
           
        }

        public JsonResult GetJsonMonitoreoIndicadores()
        {
           
                srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);

                JsRootObject salida = srvMonitor.GetJsonMonitoreoInd(IdCargaResponsable);
                string result = JsonConvert.SerializeObject(salida, Formatting.Indented);
                return Json(salida, JsonRequestBehavior.AllowGet);
           
        }

        

        public JsonResult IndicadoresNoActualizados()
        {
            List<EntProcesos> Procesos = new List<EntProcesos>();

            return Json(new JavaScriptSerializer().Serialize(Procesos), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getIndicadores12mpiFaltantes()
        {
            srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);
            srvMonitor.setListIndicadores12MPI();
            Object indicadores = new Object();
            indicadores = srvMonitor.getListaIndicadores12MPIFaltantes();
            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getIndicadoresSubsistemasFaltantes()
        {
            srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);
            srvMonitor.setListIndicadoresSubsistemas();
            Object indicadores = new Object();
            indicadores = srvMonitor.getIndicadoresSubsistemasFaltantes();
            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult getSoportes12mpiFaltantes()
        {
            srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);
            srvMonitor.setListIndicadores12MPI();

            Object indicadores = new Object();
            indicadores = srvMonitor.getSoportes12mpiFaltantes();
            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getSoportesSubsistemasFaltantes()
        {
            srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);
            srvMonitor.setListIndicadoresSubsistemas();
            Object indicadores = new Object();
            indicadores = srvMonitor.getSoportesSubsistemasFaltantes();
            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getSistemasActivadosSinResponsable()
        {
            srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);
            var listIndicadorSinResponsable = (Rol== "Administrador") ? 
                srvMonitor.GetIndicadoresActivadosSinResponsables() : 
                srvMonitor.GetIndicadoresActivadosSinResponsables().Where(x => x.IdUbicacion == IdUbicacion).ToList();
            return Json(new JavaScriptSerializer().Serialize(listIndicadorSinResponsable), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getSistemas12mpiActivadosSinResponsable()
        {
            srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);
            var listIndicadorSinResponsable = srvMonitor.GetIndicadores12mpiActivadosSinResponsables();
            return Json(new JavaScriptSerializer().Serialize(listIndicadorSinResponsable), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult infoNota(int iddata)
        {
            srvMonitorIndicador srvMonitor = new srvMonitorIndicador(IdCargaResponsable);
            string nota = srvMonitor.getNota(iddata);
            Object indicadores = new Object();
            indicadores = new { nota = nota };

            return Json(new JavaScriptSerializer().Serialize(indicadores));

        }

    }
}