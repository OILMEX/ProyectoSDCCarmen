using RendicionCuentasServices.Services;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCRendicionCuentas.Controllers
{
    public class ConfigProyectoController : ValidatorController
    {
        #region Variables Globales

        srvConfigProyecto srv = new srvConfigProyecto();
        srvCompromiso srvCompromiso = new srvCompromiso();
        srvUsuarios srvUser = new srvUsuarios();
        srvIndicadores srvIndicadores = new srvIndicadores();


        #endregion



        #region Tableros
        // GET: ConfigProyecto
        public ActionResult Index()
        {
            if (myCookie != null)
            {
                List<EntProyecto> proyectosproceso = new List<EntProyecto>();
                List<EntProyecto> proyectosconcluido = new List<EntProyecto>();

               
                    proyectosproceso = srv.ProyectosPorEstatusAndRol("En Proceso", IdCargaResponsable);
                    proyectosconcluido = srv.ProyectosPorEstatusAndRol("Concluido", IdCargaResponsable);

               

                List<EntAreas> areas = srvUser.llenarAreas();
                List<ClsJefes> responsables = srvUser.llenarJefes();
                List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> graficas = srv.getDataGraficas(IdCargaResponsable);
                List<SP_SelectAll_Ubicaciones_Result> ubicaciones = srv.getAllUbicaciones();
                List<SP_SelectAll_CoordinacionesParaProyecto_Result> Coordinaciones = srv.getAllCoordinaciones();
                List<SP_SelectAll_TipoProyectos_Result> TiposProyecto = srv.getAllTiposProyecto();

                Object proyectos = new Object();
                proyectos = new { enproceso = proyectosproceso, concluidos = proyectosconcluido, areas = areas, responsables = responsables, graficas = graficas, ubicaciones = ubicaciones, coordinaciones = Coordinaciones, tiposproyectos = TiposProyecto };

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var json = new HtmlString(serializer.Serialize(proyectos));
                return View(json);
            }
            else
            {
                return RedireccionaLogin();
            }



           
        }

        [HttpGet]
        public ActionResult EditarProyecto(int idproyecto)
        {
            EntProyecto dataproyecto = srv.GetDataProyectobyId(idproyecto);
            List<EntEtapa> etapas = srv.GetFechasEtapasProy(idproyecto);
            List<EntAreas> areas = srvUser.llenarAreas();
            List<ClsJefes> responsables = srvUser.llenarJefes();
            List<SP_SelectAll_Ubicaciones_Result> ubicaciones = srv.getAllUbicaciones();
            List<SP_SelectAll_CoordinacionesParaProyecto_Result> Coordinaciones = srv.getAllCoordinaciones();
            List<SP_SelectAll_SuperintendenciasParaProyecto_Result> Superintendencias = srv.getAllSuperintendenciasXId((int)dataproyecto.proyecto_idCoordinacion);
            List<SP_SelectAll_TipoProyectos_Result> TiposProyecto = srv.getAllTiposProyecto();

            Object proyectos = new Object();
            proyectos = new { dataproyecto = dataproyecto, etapas = etapas, areas = areas, responsables = responsables, ubicaciones = ubicaciones, coordinaciones = Coordinaciones, superintendencias = Superintendencias, tiposproyectos = TiposProyecto };

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var json = new HtmlString(serializer.Serialize(proyectos));

            return View(json);
        }

        [HttpPost]
        public JsonResult addProyecto(string EstatusProyecto, string NombreProyecto, int? AreaOperativa, int Responsable, string FechaInicio, string FechaFin, int Ubicaciones, int TiposProyecto, int Coordinaciones = 0, int Superintendencias = 0)
        {
            tbl_Proyectos proyecto = new tbl_Proyectos();
            Object documentos = new Object();
            

            try
            {

                proyecto.EstatusTiempo = EstatusProyecto;
                proyecto.NombreProyecto = NombreProyecto;
                proyecto.IdArea = AreaOperativa;
                proyecto.IdResponsable = Responsable;
                proyecto.IdUbicacion = Ubicaciones;
                proyecto.FechaInicio = Convert.ToDateTime(FechaInicio);
                proyecto.FechaFin = Convert.ToDateTime(FechaFin);
                if (IdCargaResponsable != null)
                {
                    proyecto.UsuarioCreacion = IdCargaResponsable;
                    proyecto.UsuarioActualizacion = IdCargaResponsable;
                }
                proyecto.FechaCreacion = DateTime.Now;
                proyecto.FechaActualizacion = DateTime.Now;
                proyecto.IdElementoOrganigrama = Coordinaciones > 0 ? ( Superintendencias > 0 ? Superintendencias : Coordinaciones ) : 0;
                proyecto.IdTipoElementoOrganigrama = Coordinaciones > 0 ? ( Superintendencias > 0 ? 4 : 3 ) : 0;
                proyecto.IdTipoProyecto = TiposProyecto;

                proyecto.Estatus = true;

                int idNew = srv.AddProyecto(proyecto);

                if (idNew > 0)
                {
                    //return RedirectToAction("Index", "ConfigProyecto");
                    documentos = new { accion = true, idproyecto = idNew };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos));
                    return json;
                }
                else
                {
                    documentos = new { accion = false };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos));
                    return json;
                }
            }
            catch (Exception ex)
            {
                documentos = new { accion = false, msj = ex.Message };
                var json = Json(new JavaScriptSerializer().Serialize(documentos));
                return json;
            }

        }

        [HttpPost]
        public ActionResult editProyecto(tbl_Proyectos Proyecto, int Coordinaciones, int TiposProyecto, int Superintendencias = 0)
        {

            if (IdCargaResponsable != null)
            {
                Proyecto.UsuarioActualizacion = IdCargaResponsable;
            }
            Proyecto.FechaActualizacion = DateTime.Now;
            srv.EditProyecto(Proyecto, Coordinaciones, TiposProyecto, Superintendencias);
            return RedirectToAction("EditarProyecto", "ConfigProyecto", new { idproyecto = Proyecto.IdProyecto });
        }

        [HttpPost]
        public ActionResult deleteProy(int idproyecto)
        {
            tbl_Proyectos item = new tbl_Proyectos();
            

            item.IdProyecto = idproyecto;
            item.Estatus = false;
            if (IdCargaResponsable != null)
            {
                item.UsuarioActualizacion = IdCargaResponsable;
            }
            item.FechaActualizacion = DateTime.Now;

            try
            {
                srv.DeleteProyecto(item);
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Index", "ConfigProyecto", new { idproyecto = idproyecto });
        }



        [HttpPost]
        public JsonResult SaveUsers(int idIndicador, string[] User, string[] NoUser, int idConfig, int idSubetapa, int idProyecto)
        {
            string Msj = string.Empty;
            Object documentos = new Object();
            try
            {

                if (idIndicador > 0)
                {
                    srv.SaveResponsables(idIndicador, User, NoUser, idConfig, idSubetapa, idProyecto);
                    documentos = new { accion = true, msj = "Informacion guardada correctamente"};
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
            catch(Exception ex)
            {
                documentos = new { accion = false, msj = ex.Message };
                var json = Json(new JavaScriptSerializer().Serialize(documentos));
                return json;
            }
        }

        [HttpPost]
        public ActionResult deleteResponsable(int idResposable, int idIndicador, int idConfig, int idproyecto)
        {
            Config_IndicadorResponsable item = new Config_IndicadorResponsable();
            item.IdResponsable = idResposable;
            item.IdIndicador = idIndicador;
            item.IdConfigRelacionIndicadorProyecto = idConfig;
            try
            {
                srv.DeleteReponsableIndicador(item);
            }
            catch(Exception e)
            {

            }

            return RedirectToAction("EditarProyecto", "ConfigProyecto", new { idproyecto = idproyecto });
        }

        [HttpGet]
        public JsonResult addIndicador(tbl_Indicadores Indicador, string CheckSoporte, string CheckComentarios, string CheckReqSoporte, string CheckReqComentario, int IdSubEtapa, int IdProyecto)
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
                item.Meta = (item.TipoIndicador == "Checkbox") ? 0 : item.Meta;
                item.Estatus = true;
                item.CheckCreacionDesdeProyecto = true;
                var IdIndicador = srv.AddIndicador(item, IdProyecto, IdSubEtapa);
                if (IdIndicador > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(IdIndicador), JsonRequestBehavior.AllowGet);
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
        public JsonResult editIndicador(string idIndicador, string Clave, string Prefijo, string DescripcionCorta, string DescripcionLarga)
        {
            try
            {
                tbl_Indicadores item = new tbl_Indicadores();
                item.IdIndicador = int.Parse(idIndicador);
                item.Clave = Clave;
                item.Prefijo = Prefijo;
                item.DescripcionCorta = DescripcionCorta;
                item.DescripcionLarga = DescripcionLarga;
                item.FechaActualizacion = DateTime.Now;
                if (IdCargaResponsable != null)
                {
                    item.UsuarioActualizacion = IdCargaResponsable;
                }
                int IdElemento = srv.EditInidicador(item, "sencillo");
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


        [HttpPost]
        public JsonResult actualizarEtapaSubEtapa(int id, int idproyecto, bool estatus, bool isetapa)
        {
            string Msj = string.Empty;
            Object documentos = new Object();
            try
            {

                if (srv.EditEstatusEtapasSubetapas(id,idproyecto, estatus, isetapa))
                {
                    documentos = new { accion = true, msj = "Informacion guardada correctamente" };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos));
                    return json;
                }
                else
                {
                    documentos = new { accion = false, msj = "Error en la actualizacion del estatus" };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos));
                    return json;
                }

            }
            catch (Exception ex)
            {
                documentos = new { accion = false, msj = ex.Message };
                var json = Json(new JavaScriptSerializer().Serialize(documentos));
                return json;
            }
        }

        [HttpPost]
        public JsonResult SelectAllSuperIntendencias(int IdCoordinacion)
        {
            List<SP_SelectAll_SuperintendenciasParaProyecto_Result> listresult = srv.getAllSuperintendenciasXId(IdCoordinacion);
            var json = Json(new JavaScriptSerializer().Serialize(listresult));
            return json;
        }

        #endregion

        #region Métodos Generales

        [HttpGet]
        public JsonResult getDataIndicador(int idIndicador)
        {
            string Msj = string.Empty;

            EntIndicadorDetalle indicador = srvCompromiso.GetInfoIndicador(idIndicador);

            return Json(new JavaScriptSerializer().Serialize(indicador), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult setFechaSubetapa(int idProyecto, int idSubetapa, int idEtapa, DateTime fechaini, DateTime fechafin, string motivo, DateTime? fechainiant, DateTime? fechafinant)
        {
            Config_FechasEtapasProyecto etapa = srv.EditFechaSubEtapa(idProyecto, idSubetapa, idEtapa, fechaini, fechafin);

            if (etapa.IdConfigFechasEtapasProyecto > 0)
            {
                tbl_HistorialCambioFechas tblHistorial = new tbl_HistorialCambioFechas();
                tblHistorial.idEtapa = idEtapa;
                tblHistorial.idProyecto = idProyecto;
                tblHistorial.idSubetapa = idSubetapa;
                tblHistorial.IdResponsable = IdCargaResponsable;
                tblHistorial.Motivo = motivo;
                tblHistorial.FechaInicio = fechaini;
                tblHistorial.FechaFin = fechafin;
                tblHistorial.FechaInicioAnt = fechainiant;
                tblHistorial.FechaFinAnt = fechafinant;
                tblHistorial.FechaCreacion = DateTime.Now;
                int idHistorial = srv.AddHistorialCambioFecha(tblHistorial);
            }
            return Json(new JavaScriptSerializer().Serialize(etapa), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult setEstatusIndicador(int idConfig, bool estatus)
        {
            srv.EditEstatusIndicador(idConfig, estatus);
            return Json("");
        }

        [HttpPost]
        public JsonResult setFrecuenciaIndicador(int idIndicador, int idConfigRelacionIndicadorProyecto, int idSubetapa, int idProyecto, int frecuencia)
        {
            Object documentos = new Object();
            try
            {
                srv.EditFrecuenciaIndicador(idConfigRelacionIndicadorProyecto, frecuencia, idProyecto, idSubetapa);
                documentos = new { accion = true, frecuencia = frecuencia, msj = "Informacion guardada correctamente" };
                var json = Json(new JavaScriptSerializer().Serialize(documentos));
                return json;
            }
            catch (Exception ex)
            {
                documentos = new { accion = false, msj = ex.Message };
                var json = Json(new JavaScriptSerializer().Serialize(documentos));
                return json;
            }
        }

        #endregion

        public JsonResult getSubsistema() {
            try
            {
                List<tbl_Subsistemas> Sub = srvIndicadores.allSubsistemas();
                return Json(new JavaScriptSerializer().Serialize(Sub), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getElementos(int IdSubsistema)
        {
            try
            {
                List<EntElementos> Elms = srvIndicadores.ElementosPorSubsistema(IdSubsistema);
                return Json(new JavaScriptSerializer().Serialize(Elms), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getIndicadoresFaltantes(int IdElemento, int IdSubEtapa, int IdProyecto)
        {
            try
            {
                List<tbl_Indicadores> Ind = srvIndicadores.GetIndicadoresFaltantes(IdElemento, IdSubEtapa, IdProyecto);
                return Json(new JavaScriptSerializer().Serialize(Ind), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult addIndicadoresFaltantes(int IdProyecto, int IdSubetapa, int[] Indicadores)
        {
            int IdResult = 0;
            try
            {
                IdResult = srvIndicadores.AddIndicadoresFaltantes(IdProyecto, IdSubetapa, Indicadores);
                return Json(new JavaScriptSerializer().Serialize(IdResult), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JavaScriptSerializer().Serialize(IdResult), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult addFechaCompromiso(int IdElemento, string FechaCompromiso)
        {
            int IdResult = 1;
            try
            {
                srv.UpdateFechaCompromisoEnProyecto(IdElemento, FechaCompromiso==""? (DateTime?) null : Convert.ToDateTime(FechaCompromiso));
                return Json(new JavaScriptSerializer().Serialize(IdResult), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                IdResult = 0;
                return Json(new JavaScriptSerializer().Serialize(IdResult), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult getHistorialFechas(int idProyecto, int idEtapa, int idSubetapa)
        {
            List<tbl_HistorialCambioFechas> rows = srv.getHistorialCambiosFechas(idProyecto, idEtapa, idSubetapa);
            Object historial = new Object();
            historial = new { historial = rows };
            var json = Json(new JavaScriptSerializer().Serialize(historial));
            return json;
        }
    }
}