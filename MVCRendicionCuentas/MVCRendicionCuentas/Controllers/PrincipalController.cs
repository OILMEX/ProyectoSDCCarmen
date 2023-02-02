using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRendicionCuentas.Controllers
{
    public class PrincipalController : Controller
    {

        private string Nombre;
        private string CookieName;

        public PrincipalController()
        {
            getCookie();

        }

        public void getCookie()
        {

            HttpCookie myCookie = System.Web.HttpContext.Current.Request.Cookies["myCookie"];

            if (myCookie != null)
            {
                Nombre = myCookie["Nombre"];
                CookieName = myCookie["Nombre"];

            }

        }

        public ActionResult Index(string parcookiename)
        {
            //Se instancia usuario activo
            Session["UsuarioActivo"] = "";

            if (Nombre != null)
            {
                Session["UsuarioActivo"] = Nombre;

            }

            return View();
        }
    }
}