using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCRendicionCuentas.Controllers
{
    public class TablerosController : ValidatorController
    {
        

        #region Vistas Iniciales

        srvTableros srv = new srvTableros();
        srvUsuarios srvUser = new srvUsuarios();

        public ActionResult Index()
        {
            if (myCookie != null)
            {
                List<SP_Select_Publicacion_Result> ListPublicaciones = srv.GetPublicaciones("Produccion");
                return View(ListPublicaciones);
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        public ActionResult Subsistemas()
        {
            return View();
        }

        #endregion

        #region Tablero Global

        [HttpGet]
        public JsonResult TablerosJsonInit()
        {
            JsRootObject root = new JsRootObject();
            root.tablero_subsistema = new List<JsTableroSubsistema>();
            JsTableroSubsistema Subsistemas;
            IdUbicacion = Rol == "Administrador" ? 0 : IdUbicacion;
            List<SP_Select_Publicacion_Result> publicacion = srv.GetPublicaciones().Where(x=>x.IdUbicacion==IdUbicacion).ToList();

            if (publicacion.Count > 0)
            {
                publicacion = publicacion.OrderByDescending(x => x.FechaPublicacion).ToList();
                foreach (SP_Select_Publicacion_Result pub in publicacion)
                {
                    List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result> ListaPublicacionxSubsistema = srv.GetPublicacionesSubsistemaXIdPublicacion(pub.IdPublicacion);

                    Subsistemas = new JsTableroSubsistema();
                    Subsistemas.avances_mes = new List<JsAvancesMes>();
                    Subsistemas.tablero_subsistema = new List<JsTableroSubsistema2>();
                    Subsistemas.idpublicacion = pub.IdPublicacion;
                    Subsistemas.fecha = pub.FechaPublicacion != null ? pub.FechaPublicacion.Value.ToString() : "";
                    Subsistemas.anio = pub.FechaPublicacion != null ? pub.FechaPublicacion.Value.Year.ToString() : "";

                    if (ListaPublicacionxSubsistema.Count > 0)
                    {
                        //Rellenamos el pastel general
                        var publiGlobal = ListaPublicacionxSubsistema.Where(x => x.IdSubsistema == 5);
                        List<SP_Select_PublicacionSubsistemasxMesTabular_Result> publiGlobalMensual = srv.GetPublicacionesXMes(publiGlobal.Last().IdPublicacion).Where(x => x.IdSubsistema == 5).ToList();
                        Subsistemas.avance = publiGlobal.Last().Valor;
                        Subsistemas.estatus = publiGlobal.Last().Semaforo.ToLower();
                        Subsistemas.mejora = publiGlobal.Last().EstatusMejora == null ? 0 : (int)publiGlobal.Last().EstatusMejora;
                        Subsistemas.TotalProyectos = srv.GetAllPublicacionProyectos(pub.IdPublicacion).Count();
                        JsAvancesMes aMes;

                        //Rellenamos el valor mensual. Si PubliGlobalMensual devuelve un valor cuando sea == j se llena el valor del mes, de los contrario llena un mes en 0 (cero)
                        for (int j = 1; j <= 12; j++) 
                        {
                            aMes = new JsAvancesMes();
                            SP_Select_PublicacionSubsistemasxMesTabular_Result publiMes = publiGlobalMensual.FirstOrDefault(x => x.Mes == j);

                            if (publiMes != null)
                            {
                                aMes.valor = (publiMes.Valor != null) ? (int)publiMes.Valor : 0;
                                aMes.color = (publiMes.Semaforo != null) ? publiMes.Semaforo.ToLower() : null;
                            }
                            else
                            {
                                aMes.valor = 0;
                                aMes.color = null;
                            }

                            Subsistemas.avances_mes.Add(aMes);
                        }


                        //Rellenamos los demás subsistemas
                        JsTableroSubsistema2 ts2;
                        for (int i = 1; i <= 4; i++)
                        {
                            ts2 = new JsTableroSubsistema2();
                            var PubliSubsistema = ListaPublicacionxSubsistema.Where(x => x.IdSubsistema == i);
                            var PubliSubsistemaMensual = srv.GetPublicacionesXMes(PubliSubsistema.Last().IdPublicacion).Where(x => x.IdSubsistema == i);
                            ts2.subsistema_id = (int)PubliSubsistema.Last().IdSubsistema;
                            ts2.subsistema_idpublicacion = PubliSubsistema.Last().IdPublicacion;
                            ts2.subsistema = srv.GetNombreSubsistema(PubliSubsistema.Last().IdSubsistema);
                            ts2.estatus = PubliSubsistema.Last().Semaforo.ToLower();
                            ts2.avance = PubliSubsistema.Last().Valor;
                            ts2.mejora = PubliSubsistema.Last().EstatusMejora == null ? 0 : (int)PubliSubsistema.Last().EstatusMejora;
                            ts2.avances_mes = new List<JsAvancesMes>();

                            //Rellenamos el valor mensual. Si PubliGlobalMensual devuelve un valor cuando sea == j se llena el valor del mes, de los contrario llena un mes en 0 (cero)
                            for (int j = 1; j <= 12; j++) //var MesSS in PubliSubsistema)
                            {
                                aMes = new JsAvancesMes();
                                SP_Select_PublicacionSubsistemasxMesTabular_Result publiMes = PubliSubsistemaMensual.FirstOrDefault(x => x.Mes == j);

                                if (publiMes != null)
                                {
                                    aMes.valor = (publiMes.Valor != null) ? (int)publiMes.Valor : 0;
                                    aMes.color = (publiMes.Semaforo != null) ? publiMes.Semaforo.ToLower() : null;
                                }
                                else
                                {
                                    aMes.valor = 0;
                                    aMes.color = null;
                                }

                                ts2.avances_mes.Add(aMes);
                            }


                            Subsistemas.tablero_subsistema.Add(ts2);
                        }
                    }

                    root.tablero_subsistema.Add(Subsistemas);
                }
            }
            return Json(new JavaScriptSerializer().Serialize(root), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TablerosDetJson(int idpublicacion, int idss)
        {
            //Recuperamos la publicación
            Dashboard_Publicaciones publicacion = srv.GetPublicacionesDet(idpublicacion);
            JsTableroSubsistema2 ts2 = new JsTableroSubsistema2();

            if (publicacion != null)
            {
                List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result> ListaPublicacionxSubsistema = srv.GetPublicacionesSubsistemaXIdPublicacion(publicacion.IdPublicacion);
                //descartamos id = 5 para no recuperar el global
                SP_Select_PublicacionValoresSemaforosXSubsistemas_Result ListaFiltrar;

                ListaFiltrar = ListaPublicacionxSubsistema.SingleOrDefault(x => x.IdSubsistema == idss);

                if (ListaFiltrar != null)
                {
                    ts2 = new JsTableroSubsistema2();
                    ts2.subsistema_id = ListaFiltrar.IdSubsistema != null ? (int)ListaFiltrar.IdSubsistema : 0;
                    ts2.subsistema = srv.GetNombreSubsistema(ListaFiltrar.IdSubsistema);
                    ts2.estatus = ListaFiltrar.Semaforo.ToLower();
                    ts2.Fecha = publicacion.FechaPublicacion != null ? publicacion.FechaPublicacion.Value.ToShortDateString() : "";
                    ts2.avance = ListaFiltrar.Valor;
                    ts2.mejora = ListaFiltrar.EstatusMejora != null ? (int)ListaFiltrar.EstatusMejora : 0;
                    ts2.color = ListaFiltrar.Semaforo.ToLower(); 
                    ts2.procesos = srv.GetProcesosXIdSubsistema(publicacion.IdPublicacion, ListaFiltrar.IdSubsistema);
                }
                
            }
            

            return Json(new JavaScriptSerializer().Serialize(ts2), JsonRequestBehavior.AllowGet);
        }

        private int CheckMejora(int? ValorMes1, int? ValorMes2)
        {
            return ValorMes1 > ValorMes2 ? 1 : (ValorMes1 < ValorMes2 ? 2 : 3);
        }

        #endregion

        #region Por Proyecto

        [HttpGet]
        public JsonResult PorProyecto(int selUbicacion = 0)
        {
            srvUsuarios srvUser = new srvUsuarios();
            if (Rol == "Administrador")
            {
                IdUbicacion = (selUbicacion == 0) ? 0 : selUbicacion;
            }
            else
            {
                IdUbicacion = IdUbicacion;
            }

            List<SP_Select_Publicacion_Result> Publicaciones = srv.GetPublicaciones().Where(x => x.IdUbicacion == IdUbicacion).ToList();
            List<EntListProyecto> proyectos = new List<EntListProyecto>();
            List<tbl_Ubicaciones> ubicaciones = srvUser.getUbicaciones();
            bool isAdministrador = (Rol == "Administrador") ? true : false;
            EntListProyecto proy;
            Object proyects = new Object();

            foreach(SP_Select_Publicacion_Result pub in Publicaciones)
            {
                proy = new EntListProyecto();
                proy.IdPublicacion = pub.IdPublicacion;
                proy.FechaPublicacion = pub.FechaPublicacion;
                proy.Proyectos = srv.GetAllPublicacionProyectos(pub.IdPublicacion);

                proyectos.Add(proy);
            }

            //Se ingresan Tipos de Proyecto
            List<SP_SelectAll_TipoProyectos_Result> TiposProyecto = srv.getAllTiposProyecto();

            proyectos = proyectos.OrderByDescending(x => x.FechaPublicacion).ToList();
            proyects = new { enproceso = proyectos, subfiltros = TiposProyecto, ubicaciones = ubicaciones, admin = isAdministrador };
            return Json(new JavaScriptSerializer().Serialize(proyects), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetEtapasxIdProy(int idproyecto, int idpublicacion)
        {
            List<EntProyecto> DetalleProyecto = srv.GetOnePublicacionProyectos(idproyecto);
            List<EntProyecto> Etapas = srv.GetEtapasxProyecto(idproyecto);

            Object Detalle = new { Proyecto = DetalleProyecto, Etapas = Etapas };

            return Json(new JavaScriptSerializer().Serialize(Detalle), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubEtapasxIdProy(int idproyecto, int idpublicacion)
        {
            List<EntProyecto> DetalleEtapa = srv.GetOneEtapaPublicacionProyectos(idproyecto);
            List<EntProyecto> SubEtapas = srv.GetSubEtapasxProyecto(idproyecto);

            Object Detalle = new { Proyecto = DetalleEtapa, Etapas = SubEtapas };

            return Json(new JavaScriptSerializer().Serialize(Detalle), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}