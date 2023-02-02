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
    public class MacroProyectosController : ValidatorController
    {
        srvMacroProyectos srv = new srvMacroProyectos();

        
        //
        // GET: /MacroProyectos/
        public ActionResult Index()
        {
            if (myCookie != null)
            {
                List<EntOrganigrama> listOrganigrama = new List<EntOrganigrama>();
                listOrganigrama = srv.GetListOrganigrama();

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var json = new HtmlString(serializer.Serialize(listOrganigrama));

                return View("Index", json);
            }
            else
            {
                return RedireccionaLogin();
            }
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
	}
}