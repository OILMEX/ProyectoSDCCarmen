using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RendicionCuentasServices.Services;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace MVCRendicionCuentas.Controllers
{
    public class PublicacionController : ValidatorController
    {
        srvPublicacion srv = new srvPublicacion();
        //
        // GET: /Publicacion/
        public ActionResult Index()
        {
            if (myCookie != null)
            {
                Dashboard_Publicaciones Publicacion = new Dashboard_Publicaciones();
                IdUbicacion = Rol == "Administrador" ? 0 : IdUbicacion;
                Publicacion = srv.GetPublicacionActual("Produccion", IdUbicacion);

                if (Publicacion.IdPublicacion != 0)
                {
                    ViewData["Anio"] = Publicacion.FechaPublicacion.Value.Year;
                    ViewData["FechaActual"] = Publicacion.FechaPublicacion.Value.ToShortDateString();
                    ViewData["IdPublicacion"] = Publicacion.IdPublicacion;
                }

                return View("Index");
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        [HttpGet]
        public JsonResult TablerosPublicadosActuales(int IdPublicacion)
        {
            try
            {
                EntTablelrosPublicadosGlobales JsonTablerosSubsistemas = new EntTablelrosPublicadosGlobales();
                JsonTablerosSubsistemas = srv.GetTablerosPublicadosActuales(IdPublicacion);

                return Json(new JavaScriptSerializer().Serialize(JsonTablerosSubsistemas), JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                return Json("No se encontraron subsistemas");
            }

        }
	}
}