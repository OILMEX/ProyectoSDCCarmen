using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRendicionCuentas.Controllers
{
    public class ValidatorController : Controller
    {
        public int IdCargaResponsable;
        public int IdUbicacion;
        public string Rol;
        public HttpCookie myCookie;

        public ValidatorController()
        {
            myCookie=getCookie();
            if(myCookie!=null)
            {
                IdCargaResponsable = Convert.ToInt32(myCookie["IdUsuario"].ToString());
                IdUbicacion = Convert.ToInt32(myCookie["IdUbicacion"].ToString());
                Rol = myCookie["Rol"].ToString();
            }
            
        }

       
        public HttpCookie getCookie()
        {
            HttpCookie myCookie = System.Web.HttpContext.Current.Request.Cookies["myCookie"];

            if (myCookie != null)
            {
                return myCookie;
            }
            else
            {
                return null;

            }

        }

        public ActionResult RedireccionaLogin()
        {
            return RedirectToAction("Index", "Login");
        }
	}
}