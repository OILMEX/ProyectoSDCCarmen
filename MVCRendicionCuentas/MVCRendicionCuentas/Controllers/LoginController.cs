using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace MVCRendicionCuentas.Controllers
{
    public class LoginController : Controller
    {
        
        public ActionResult Salir()
        {
            if (Request.Cookies["myCookie"] != null)
            {
                HttpCookie myCookie = new HttpCookie("myCookie");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Index()
        {
            return View("_Login");
        }

        #region Login

        [HttpPost]
        public ActionResult Valid(EntUsuario model)
        {
            if (model.Email != null && model.Password != null)
                return getlogin(model);
            else
            {
                ViewBag.Msj = "Usuario/Password Incorrecto";
                return View("_Login");
            }
        }

        private static byte[] salt = Encoding.ASCII.GetBytes("somerandomstuff");

        public string Encrypt(string plainText, string keyString)
        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(keyString, salt);

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(new CryptoStream(ms, new RijndaelManaged().CreateEncryptor(key.GetBytes(32), key.GetBytes(16)), CryptoStreamMode.Write));
            sw.Write(plainText);
            sw.Close();
            ms.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string base64Text, string keyString)
        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(keyString, salt);

            ICryptoTransform d = new RijndaelManaged().CreateDecryptor(key.GetBytes(32), key.GetBytes(16));
            byte[] bytes = Convert.FromBase64String(base64Text);
            return new StreamReader(new CryptoStream(new MemoryStream(bytes), d, CryptoStreamMode.Read)).ReadToEnd();
        }  

        public void crearCookie(EntUsuario sesion)
        {
            //string key = "paranquitamucuero";
            //string namecookie=this.Encrypt("text" + sesion.IdUsuario, key);
            //cookiename = namecookie;
            HttpCookie myCookie = new HttpCookie("myCookie");

            //Add key-values in the cookie
            myCookie.Values.Add("IdUsuario", sesion.IdUsuario.ToString());
            myCookie.Values.Add("IdUbicacion", sesion.idUbicacion.ToString());
            myCookie.Values.Add("Nombre", HttpUtility.UrlEncode(sesion.Nombre.ToString()));
            myCookie.Values.Add("Rol", sesion.Rol.ToString());
            myCookie.Values.Add("Usuario", sesion.Usuario.ToString());



            //set cookie expiry date-time. Made it to last for next 12 hours.
            myCookie.Expires = DateTime.Now.AddHours(12);

            //Most important, write the cookie to client.
            System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
            
        }

        private ActionResult getlogin(EntUsuario model)
        {
            srvlogin srv = new srvlogin();

            EntUsuario sesion = srv.IsLogin(model);

            if (sesion != null)
            {


                crearCookie(sesion);



                if (model.Email == "display@gmail.com") {
                    return RedirectToAction("Index", "Publicacion");
                }
                else
                {
                    return RedirectToAction("Index", "Principal");
                }
            }
            else
            {
                ViewBag.Msj = "Usuario/Password Incorrecto";
                return View("_Login");
            }

        }



        public ActionResult logoutAction()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }

        #endregion

        #region Recuperar Contraseña

        public ActionResult RecuperarPass()
        {
            return View("_RecoverPass");
        }

        [HttpPost]
        public ActionResult ValidMail(EntUsuario model)
        {
            if (model.Email != null)
                return getMail(model);
            else
            {
                ViewBag.Msj = "Email Requerido";
                return View("_RecoverPass");
            }
        }

        private ActionResult getMail(EntUsuario model)
        {
            srvlogin srv = new srvlogin();
            EntUsuario sesion = srv.IsMail(model);

            if (sesion != null)
            {
                sesion.Password = srv.SetNewPass(model);
                srv.SendMail(sesion);
                //Response.Write("<script type='Text/JavaScript'>alert('¡Se ha enviado la petición, en breve contactaremos con usted!')</script>");
                ViewBag.Msj = "¡Se ha enviado la petición, en breve contactaremos con usted!";
               // return RedirectToAction("Index", "Login");
                //ViewBag.Msj = "Usuario/Password Incorrecto";
                return View("_Login");
            }
            else
            {
                ViewBag.Msj = "El Email proporcionado no está en nuestra lista de usuarios activos";
                return View("_RecoverPass");
            }
        }


        public ActionResult Login2Pass(string User, string Pass)
        {
            srvlogin srv = new srvlogin();
            //srvPrincipal srvP = new srvPrincipal();
            EntUsuario sesion = null;
            string msj = "¡Unknow error!, No found ConnectionString";


            //Administrador
            string Usuario = User;
            string Passw = Pass;
            sesion = srv.IsLogin(Usuario, Passw);


            if (sesion != null)
            {
               crearCookie(sesion);
                return RedirectToAction("Index", "Principal");
            }
            else
            {
                ViewBag.Msj = "¡Es necesario loguearse!";
                return View("_Login");
            }
        }

        #endregion 

        #region Cronjob Actualización de estatus
        public JsonResult cronjobEstatusProyecto()
        {
            try
            {
                srvConfigProyecto srv = new srvConfigProyecto();
                foreach (var proy in srv.ProyectosPorEstatus("En Proceso"))
                {
                    srv.updateEstatusTiempoSubEtapaProyecto(proy.proyecto_id);
                }
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}