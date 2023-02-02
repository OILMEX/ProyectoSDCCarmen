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
    public class PublicarTableroController : ValidatorController
    {
        //Instanciamiento de servicios para Pubicación Tableros
        srvPublicarTableros srv = new srvPublicarTableros();
        

        public ActionResult Index()
        {
            if (myCookie != null)
            {
                IdUbicacion = Rol == "Administrador" ? 0 : IdUbicacion;
                List<SP_Select_Publicacion_Result> ListPublicaciones = srv.GetPublicaciones("Produccion").Where(x => x.IdUbicacion == IdUbicacion).ToList();
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

        
        [HttpPost]
        public ActionResult deletePublicacion(int datepublicacion)
        {
            try
            {
                srv.DeletePublicacion(datepublicacion);
                return RedirectToAction("Index", "PublicarTablero");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "PublicarTablero");
            }
            
            
        }
         

        [HttpGet]
        public JsonResult Publicacion(string TipoPublicacion)
        {
            try
            {
                if(TipoPublicacion=="1")
                {
                    TipoPublicacion = "Produccion";
                }
                else
                {
                    TipoPublicacion = "Prueba";
                }
                srv.InsertPublicacion(TipoPublicacion);
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        #region Creación de JSON para vistas

        [HttpGet]
        public JsonResult TablerosJsonInit()
        {
            JsRootObject root = new JsRootObject();
            root.tablero_subsistema = new List<JsTableroSubsistema>();
            JsTableroSubsistema Subsistemas;
            IdUbicacion = Rol == "Administrador" ? 0 : IdUbicacion;
            List<SP_Select_Publicacion_Result> publicacion = srv.GetPublicaciones("Prueba").Where(x => x.IdUbicacion == IdUbicacion).ToList();


            if (publicacion != null)
            {
                publicacion = publicacion.OrderByDescending(x => x.FechaPublicacion).ToList();
                foreach (SP_Select_Publicacion_Result pub in publicacion)
                {
                    List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result> ListaPublicacionxSubsistema = srv.GetPublicacionesSubsistemaXIdPublicacion(pub.IdPublicacion);

                    Subsistemas = new JsTableroSubsistema();
                    Subsistemas.avances_mes = new List<JsAvancesMes>();
                    Subsistemas.tablero_subsistema = new List<JsTableroSubsistema2>();
                    Subsistemas.fecha = pub.FechaPublicacion != null ? pub.FechaPublicacion.Value.ToString() : "";
                    Subsistemas.anio = pub.FechaPublicacion != null ? pub.FechaPublicacion.Value.Year.ToString() : "";
                    Subsistemas.idpublicacion = pub.IdPublicacion;

                    if (ListaPublicacionxSubsistema.Count > 0)
                    {
                        //Rellenamos el pastel general
                        var publiGlobal = ListaPublicacionxSubsistema.Where(x => x.IdSubsistema == 5);
                        var publiGlobalMensual = srv.GetPublicacionesXMes(publiGlobal.Last().IdPublicacion).Where(x => x.IdSubsistema == 5);
                        Subsistemas.avance = publiGlobal.Last().Valor;
                        Subsistemas.estatus = publiGlobal.Last().Semaforo.ToLower();
                        Subsistemas.mejora = publiGlobal.Last().EstatusMejora == null ? 0 : (int)publiGlobal.Last().EstatusMejora;
                        srvTableros srvTableros = new srvTableros();
                        Subsistemas.TotalProyectos = srvTableros.GetAllPublicacionProyectos(pub.IdPublicacion).Count();
                        JsAvancesMes aMes;

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
                            ts2.subsistema_idpublicacion = PubliSubsistema.Last().IdPublicacion;
                            ts2.subsistema_id = (int)PubliSubsistema.Last().IdSubsistema;
                            ts2.subsistema = srv.GetNombreSubsistema(PubliSubsistema.Last().IdSubsistema);
                            ts2.estatus = PubliSubsistema.Last().Semaforo.ToLower();
                            ts2.avance = PubliSubsistema.Last().Valor;
                            ts2.mejora = PubliSubsistema.Last().EstatusMejora == null ? 0 : (int)PubliSubsistema.Last().EstatusMejora;
                            ts2.avances_mes = new List<JsAvancesMes>();

                            //ts2.completados =
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
        public JsonResult TablerosJsonInit2(string status)
        {
            JsRootObject root = new JsRootObject();
            root.tablero_subsistema = new List<JsTableroSubsistema>();
            IdUbicacion = Rol == "Administrador" ? 0 : IdUbicacion;
            List<SP_Select_Publicacion_Result> publicacion = srv.GetPublicacionXStatus(status).Where(x => x.IdUbicacion == IdUbicacion).ToList();
            

            if (publicacion != null)
            {
                publicacion = publicacion.OrderByDescending(x => x.FechaPublicacion).ToList();
                foreach (SP_Select_Publicacion_Result pub in publicacion)
                {
                    List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result> ListaPublicacionxSubsistema = srv.GetPublicacionesSubsistemaXIdPublicacion(pub.IdPublicacion);
                    JsTableroSubsistema Subsistemas = new JsTableroSubsistema();
                    Subsistemas.avances_mes = new List<JsAvancesMes>();
                    Subsistemas.tablero_subsistema = new List<JsTableroSubsistema2>();

                    //Llenado de Subsistema aunque sea null
                    Subsistemas.fecha = pub.FechaPublicacion != null ? pub.FechaPublicacion.ToString() : "";
                    Subsistemas.anio = pub.FechaPublicacion != null ? pub.FechaPublicacion.Value.Year.ToString() : "";
                    Subsistemas.idpublicacion = pub.IdPublicacion;

                    if (ListaPublicacionxSubsistema.Count > 0)
                    {
                        //Rellenamos el pastel general
                        var publiGlobal = ListaPublicacionxSubsistema.Where(x => x.IdSubsistema == 5);
                        var publiGlobalMensual = srv.GetPublicacionesXMes(publiGlobal.Last().IdPublicacion).Where(x => x.IdSubsistema == 5);
                        Subsistemas.avance = publiGlobal.Last().Valor;                        
                        Subsistemas.estatus = publiGlobal.Last().Semaforo.ToLower();
                        Subsistemas.mejora = publiGlobal.Last().EstatusMejora == null ? 0 : (int)publiGlobal.Last().EstatusMejora;
                        srvTableros srvTableros = new srvTableros();
                        Subsistemas.TotalProyectos = srvTableros.GetAllPublicacionProyectos(pub.IdPublicacion).Count();
                        JsAvancesMes aMes;

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
                            for (int j = 1; j <= 12; j++)
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
        public JsonResult TablerosDetJson(int? idpublicacion, int idss)
        {
            //Recuperamos la publicación
            IdUbicacion = Rol == "Administrador" ? 0 : IdUbicacion;
            
            Dashboard_Publicaciones publicacion;
            if (idpublicacion != null)
            {
                 publicacion = srv.GetPublicacionesDet(idpublicacion);
            }
            else
            {
                publicacion = srv.GetPublicacionesDetPrueba();
            }
            
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

        #endregion

        #region Métodos Generales

        private int CheckMejora(int? ValorMes1, int? ValorMes2)
        {
            return ValorMes1 > ValorMes2 ? 1 : (ValorMes1 < ValorMes2 ? 2 : 3);
        }

        #endregion
    }
}