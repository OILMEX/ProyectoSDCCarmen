using RendicionCuentasServices.Services;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;

namespace MVCRendicionCuentas.Controllers
{
    public class RendicionCuentaController : ValidatorController
    {
        
        srvCargaIndicadores srv;
        srvRendicionCuentas srvRendicion;
        srvConfigProyecto srvConfigProyecto = new srvConfigProyecto();
        srvCompromiso srvCompromisos = new srvCompromiso();
        srvUsuarios srvUser = new srvUsuarios();
        GetsIndicadores objGetsIndicadores = new GetsIndicadores();

        public RendicionCuentaController()
        {
            srv = new srvCargaIndicadores(IdCargaResponsable);
            srvRendicion = new srvRendicionCuentas(IdCargaResponsable);
        }

        public ActionResult Index()
        {
            if (myCookie != null)
            {
                int iduser = 0;
                string rol = "Administrador";

               
                    iduser = IdCargaResponsable;
                    rol = Rol;
               
                List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> graficas = srvConfigProyecto.getDataGraficas(iduser);
                List<SP_VistaCargaIndicadores12MPIxReponsable_Result> indicadores12mpi = objGetsIndicadores.getIndicadores12MPI(iduser);

                List<EntProyecto> proyectosproceso = srvConfigProyecto.ProyectosPorEstatus("En Proceso");
                List<EntProyecto> proyectosresp = new List<EntProyecto>();
                List<EntElementosCompromiso> elementos = srvCompromisos.getElementos(1);

                List<ClsJefes> responsables = srvUser.llenarResponsablesxUbicacion(IdCargaResponsable);
                List<tbl_Ubicaciones> ubicaciones = srvUser.getUbicaciones();
                bool isAdministrador = (rol == "Administrador") ? true : false;
                srv.setListIndicadoresSubsistemas();
                foreach (EntProyecto proi in proyectosproceso)
                {
                    srvConfigProyecto.GetDataProyectobyId(proi.proyecto_id);
                    proi.Etapas = srv.GetFechasEtapasProy(proi.proyecto_id);
                    if (proi.Etapas.Count() > 0)
                    {
                        proyectosresp.Add(proi);
                    }
                }

                Object indicadores = new Object();
                indicadores = new { 
                    ind12mpi = indicadores12mpi, 
                    graficas = graficas, 
                    proyectos = proyectosresp, 
                    ubicaciones = ubicaciones,
                    responsables = responsables, 
                    elementos = elementos, 
                    admin = isAdministrador 
                };

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var json = new HtmlString(serializer.Serialize(indicadores));

                return View(json);
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        // GET: RendicionCuenta
        public ActionResult IndicadoresGerencia()
        {
            int iduser = 0;
            string rol = "Administrador";

            
                iduser = IdCargaResponsable;
                rol = Rol;
            
            List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> graficas = srvConfigProyecto.getDataGraficas(iduser);
            List<EntIndicador> indicadores12mpi = null;// srvRendicion.getIndicadores12MPIAgrupados();

            List<EntProyecto> proyectosproceso = srvConfigProyecto.ProyectosPorEstatus("En Proceso");
            List<EntProyecto> proyectosresp = new List<EntProyecto>();
            List<EntElementosCompromiso> elementos = new List<EntElementosCompromiso>();

            List<tbl_Ubicaciones> responsables = srvUser.getUbicaciones();
            bool isAdministrador = (rol == "Administrador") ? true : false;
            var ListaIndicadoresAgrupados = srvRendicion.getListaIndicadoresAgrupados("En Proceso", null);
            foreach (EntProyecto proi in proyectosproceso)
            {
                srvConfigProyecto.GetDataProyectobyId(proi.proyecto_id);
                proi.Etapas = srvRendicion.GetFechasEtapasProyAgrupado(proi.proyecto_id, ListaIndicadoresAgrupados);
                if (proi.Etapas.Count() > 0)
                {
                    proyectosresp.Add(proi);
                }
            }

            Object indicadores = new Object();
            indicadores = new { ind12mpi = indicadores12mpi, graficas = graficas, proyectos = proyectosresp, responsables = responsables, elementos = elementos, admin = isAdministrador };

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            var json = new HtmlString(serializer.Serialize(indicadores));

            return View(json);
        }

        public ActionResult IndicadoresGerencia12MPI()
        {
            int iduser = 0;
            string rol = "Administrador";


            iduser = IdCargaResponsable;
            rol = Rol;

            List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> graficas = srvConfigProyecto.getDataGraficas(iduser);
            List<EntIndicador> indicadores12mpi =  srvRendicion.getIndicadores12MPIAgrupados();

            List<EntProyecto> proyectosresp = new List<EntProyecto>();
            List<EntElementosCompromiso> elementos = new List<EntElementosCompromiso>();

            List<tbl_Ubicaciones> responsables = srvUser.getUbicaciones();
            bool isAdministrador = (rol == "Administrador") ? true : false;
            //var ListaIndicadoresAgrupados = null; // srvRendicion.getListaIndicadoresAgrupados("En Proceso", null);
            

            Object indicadores = new Object();
            indicadores = new { ind12mpi = indicadores12mpi, graficas = graficas, proyectos = proyectosresp, responsables = responsables, elementos = elementos, admin = isAdministrador };

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            var json = new HtmlString(serializer.Serialize(indicadores));

            return View(json);
        }


        [HttpPost]
        public JsonResult getIndicadoresxresponsable(int selResponsable)
        {
            int iduser = 0;

              iduser = IdCargaResponsable;
          

            Object indicadores = new Object();
            try
            {
                List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> graficas = srvConfigProyecto.getDataGraficas(iduser);
                List<SP_VistaCargaIndicadores12MPIxReponsable_Result> indicadores12mpi = null; // objGetsIndicadores.getIndicadores12MPI(selResponsable);
                List<EntElementosCompromiso> elementos = srvCompromisos.getElementos(1);

                List<EntProyecto> proyectosproceso = srvConfigProyecto.ProyectosPorEstatus("En Proceso").ToList();
                List<EntProyecto> proyectosresp = new List<EntProyecto>();

                foreach (EntProyecto proi in proyectosproceso)
                {
                    
                    srv.setListaIndicadoresSubsistema(selResponsable);
                    proi.Etapas = srv.GetFechasEtapasProy(proi.proyecto_id);
                    if (proi.Etapas.Count() > 0)
                    {
                        proyectosresp.Add(proi);
                    }
                }

                indicadores = new { ind12mpi = indicadores12mpi, proyectos = proyectosresp,elementos = elementos, accion = true };


                return Json(new JavaScriptSerializer().Serialize(indicadores));
            }
            catch (Exception ex)
            {
                indicadores = new { accion = false };
                var json = Json(new JavaScriptSerializer().Serialize(indicadores));
                return json;
            }
        }

        [HttpPost]
        public ActionResult getIndicadoresxgerencia(int selResponsable, string selEstatus)
        {
            int iduser = 0;
            iduser = IdCargaResponsable;
           

            Object indicadores = new Object();
            try
            {

                List<EntIndicador> indicadores12mpi = null;//srvRendicion.getIndicadores12MPIAgrupados();
                List<EntElementosCompromiso> elementos = new List<EntElementosCompromiso>();

                List<EntProyecto> proyectosproceso = srvConfigProyecto.ProyectosPorEstatus(selEstatus).ToList();
                List<EntProyecto> proyectosresp = new List<EntProyecto>();

                if(selResponsable > 0)
                    proyectosproceso = proyectosproceso.Where(x => x.proyecto_idubicacion == selResponsable).ToList();
                else
                    proyectosproceso = proyectosproceso.ToList();

                var ListaIndicadoresAgrupados = srvRendicion.getListaIndicadoresAgrupados(selEstatus, null);
                
                foreach (EntProyecto proi in proyectosproceso)
                {


                    proi.Etapas = srvRendicion.GetFechasEtapasProyAgrupado(proi.proyecto_id, ListaIndicadoresAgrupados);
                    if (proi.Etapas.Count() > 0)
                    {
                        proyectosresp.Add(proi);
                    }
                }

                indicadores = new { ind12mpi = indicadores12mpi, proyectos = proyectosresp, elementos = elementos, accion = true };
                //return Json(new JavaScriptSerializer().Serialize(indicadores));

               // var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //serializer.MaxJsonLength = int.MaxValue;
                //var json = serializer.Serialize(indicadores);

               // return Json(json);

                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var result = new ContentResult
                {
                    Content = serializer.Serialize(indicadores),
                    ContentType = "application/json"
                };

                return result;


            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpPost]
        public ActionResult getIndicadoresxgerencia12MPI(int? selResponsable, string selEstatus)
        {
            int iduser = 0;
            iduser = IdCargaResponsable;
            if (selResponsable == 0)
                selResponsable = null;

            Object indicadores = new Object();
            try
            {

                List<EntIndicador> indicadores12mpi = srvRendicion.getIndicadores12MPIAgrupados(selResponsable);
                List<EntElementosCompromiso> elementos = new List<EntElementosCompromiso>();

                
                List<EntProyecto> proyectosresp = new List<EntProyecto>();


                indicadores = new { ind12mpi = indicadores12mpi, proyectos = proyectosresp, elementos = elementos, accion = true };
                
                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var result = new ContentResult
                {
                    Content = serializer.Serialize(indicadores),
                    ContentType = "application/json"
                };

                return result;


            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }


        
    }
}