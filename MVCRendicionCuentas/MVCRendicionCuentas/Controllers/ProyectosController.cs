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
    public class ProyectosController : Controller
    {
        srvCatProyecto srv = new srvCatProyecto();
        
        private int IdCargaResponsable;
        private int IdUbicacion;

        public ProyectosController()
        {
            getCookie();
        }

        public void getCookie()
        {
            HttpCookie myCookie = System.Web.HttpContext.Current.Request.Cookies["myCookie"];

            if (myCookie != null)
            {
                IdCargaResponsable = Convert.ToInt32(myCookie["IdUsuario"]);
                IdUbicacion = Convert.ToInt32(myCookie["IdUbicacion"]);
            }

        }

        public ActionResult Index()
        {

            List<EntEtapa> etapas = srv.GetEtapasProyecto();
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //String json = serializer.Serialize(list);
            var json = new HtmlString(serializer.Serialize(etapas));

            return View(json);
        }

        public ActionResult addEtapa(string claveEtapa, string nombreEtapa)
        {
            tbl_Etapas item = new tbl_Etapas();
           
            item.Clave = Int32.Parse(claveEtapa);
            item.NombreEtapa = nombreEtapa;
            
            item.FechaCreacion = DateTime.Now;
            if (IdCargaResponsable != null)
            {
                item.UsuarioActualizacion = IdCargaResponsable;
                item.UsuarioCreacion = IdCargaResponsable;
            }
            item.FechaActualizacion = DateTime.Now;
            item.Estatus = true;

            int idNew = srv.AddEtapa(item);

            if (idNew > 0)
            {
                return RedirectToAction("Index", "Proyectos");
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult editEtapa(int idEtapa, string claveEtapa, string nombreEtapa)
        {
            tbl_Etapas item = new tbl_Etapas();
            
            item.IdEtapa = idEtapa;
            item.Clave = Int32.Parse(claveEtapa);
            item.NombreEtapa = nombreEtapa;
            if (IdCargaResponsable != null)
            {
                item.UsuarioActualizacion = IdCargaResponsable;
            }
            item.FechaActualizacion = DateTime.Now;

            srv.UpdateEtapa(item);

            return RedirectToAction("Index", "Proyectos");
        }

        public ActionResult deleteEtapa(int idEtapa)
        {
            tbl_Etapas item = new tbl_Etapas();
           
            item.IdEtapa = idEtapa;
            if (IdCargaResponsable != null)
            {
                item.UsuarioActualizacion = IdCargaResponsable;
            }
            item.FechaActualizacion = DateTime.Now;
            srv.DeleteEtapa(item);

            return RedirectToAction("Index", "Proyectos");

        }

        public ActionResult addSubetapa(int idEtapa, string claveSubEtapa, string nombreSubEtapa)
        {
            tbl_SubEtapas item = new tbl_SubEtapas();
            
            item.IdEtapa = idEtapa;
            item.Clave = float.Parse(claveSubEtapa);
            item.NombreSubEtapa = nombreSubEtapa;
            if (IdCargaResponsable != null)
            {
                item.UsuarioCreacion = IdCargaResponsable;
                item.UsuarioActualizacion = IdCargaResponsable;
            }
            
            item.FechaCreacion = DateTime.Now;
            
            item.FechaActualizacion = DateTime.Now;
            item.Estatus = true;

            int idNew = srv.AddSubetapa(item);

            if (idNew > 0)
            {
                return RedirectToAction("Index", "Proyectos");
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult editSubetapa(int idSubEtapa, string claveSubEtapa, string nombreSubEtapa)
        {
            tbl_SubEtapas item = new tbl_SubEtapas();
            
            item.IdSubEtapa = idSubEtapa;
            item.Clave = double.Parse(claveSubEtapa);
            item.NombreSubEtapa = nombreSubEtapa;
            if (IdCargaResponsable != null)
            {
                item.UsuarioActualizacion = IdCargaResponsable;
            }
            item.FechaActualizacion = DateTime.Now;

            srv.UpdateSubtapa(item);

            return RedirectToAction("Index", "Proyectos");
        }

        public ActionResult deleteSubetapa(int idSubEtapa)
        {
            tbl_SubEtapas item = new tbl_SubEtapas();
            
            item.IdSubEtapa = idSubEtapa;
            if (IdCargaResponsable != null)
            {
                item.UsuarioActualizacion = IdCargaResponsable;
            }
            item.FechaActualizacion = DateTime.Now;
            srv.DeleteSubetapa(item);


            return RedirectToAction("Index", "Proyectos");

        }

        [HttpPost]
        public JsonResult GetUsers(int idIndicador, int idconfig)
        {
            int iduser = 0;
            int idUbicacion = 0;

            if (IdCargaResponsable != null)
            {
                iduser = IdCargaResponsable;
                idUbicacion = Convert.ToInt32(IdUbicacion);
            }

            string Msj = string.Empty;
            var obj = new RendicionCuentasServices.Services.srvConfigProyecto();
            List<EntUserResponsable> Users = obj.GetListUsers(idIndicador, idconfig, idUbicacion);
            return Json(new JavaScriptSerializer().Serialize(Users));
        }

    }
}