using RendicionCuentasServices.Services;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RendicionCuentasServices.DTO;
using System.Web.Script.Serialization;

namespace MVCRendicionCuentas.Controllers
{
    public class OrganigramaController : Controller
    {
        private int IdCargaResponsable;
        private int IdUbicacion;

        public OrganigramaController()
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

        srvOrganigrama srv = new srvOrganigrama();
        public ActionResult Index()
        {
            List<EntOrganigrama> listOrganigrama = new List<EntOrganigrama>();
            listOrganigrama = srv.GetListOrganigrama();

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var json = new HtmlString(serializer.Serialize(listOrganigrama));

            return View("Index", json);
        }

        public JsonResult addNivel(EntOrganigramaNivelGenerico nivel, int tipo)
        {
            EntOrganigramaNivelGenerico item = new EntOrganigramaNivelGenerico();
            
            item.ClaveNivel = nivel.ClaveNivel;
            item.NombreNivel = nivel.NombreNivel;
            item.IdRelacion = nivel.IdRelacion;
            item.UsuarioCreacion = IdCargaResponsable;
            item.UsuarioActualizacion = IdCargaResponsable;

            int idNew = srv.AddNivelOrganigrama(item, tipo);
            return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);

        }

        public JsonResult updateNivel(EntOrganigramaNivelGenerico nivel, int tipo)
        {
            EntOrganigramaNivelGenerico item = new EntOrganigramaNivelGenerico();
            
            item.IdNivel = nivel.IdNivel;
            item.ClaveNivel = nivel.ClaveNivel;
            item.NombreNivel = nivel.NombreNivel;
            item.UsuarioActualizacion = IdCargaResponsable;

            int idNew = srv.UpdateNivelOrganigrama(item, tipo);
            if (idNew > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteNivel(int idNivel, int tipo)
        {
            EntOrganigramaNivelGenerico item = new EntOrganigramaNivelGenerico();
            
            item.IdNivel = idNivel;
            item.UsuarioActualizacion = IdCargaResponsable;

            int idNew = srv.DeleteNivelOrganigrama(item, tipo);
            if (idNew > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult addPuesto(tbl_PuestoOrganigrama puesto)
        {
            tbl_PuestoOrganigrama item = new tbl_PuestoOrganigrama();
            
            item.Clave = puesto.Clave;
            item.NombrePuesto = puesto.NombrePuesto;
            item.IdElemento = puesto.IdElemento;
            item.IdTipoElementoOrganigrama = puesto.IdTipoElementoOrganigrama;

            int idNew = srv.AddPuestoOrganigrama(item);
            if (idNew > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult updatePuesto(tbl_PuestoOrganigrama puesto)
        {
            tbl_PuestoOrganigrama item = new tbl_PuestoOrganigrama();

            item.idPuestoOrganigrama = puesto.idPuestoOrganigrama;
            item.Clave = puesto.Clave;
            item.NombrePuesto = puesto.NombrePuesto;

            int idNew = srv.UpdatePuestoOrganigrama(item);
            if (idNew > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deletePuesto(int idPuestoOrganigrama)
        {
            tbl_PuestoOrganigrama item = new tbl_PuestoOrganigrama();

            int idNew = srv.DeletePuestoOrganigrama(idPuestoOrganigrama);
            if (idNew > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
    }
}