using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;

namespace RendicionCuentasServices.Services
{
    public class WebDriver
    {
        const string localvarsesion = "localGSGClientes_UserSession";
        const string cadenasegura = "$up3r_cad3na$3gura";

        /// <summary>
        /// Verifica que existe una session, es usado para redigir el trafico al login
        /// </summary>
        /// <returns>retorna true solo si hay una sesion, no verifica si es valida</returns>
        public static bool HaySesionActiva()
        {
            EntUsuario e = (EntUsuario)System.Web.HttpContext.Current.Session[localvarsesion];
            return (e != null && e.IdRol != 0 && e.IdUsuario != 0);
        }

        /// <summary>
        /// Asigna el valor de user a la sesion activa.
        /// </summary>
        /// <param name="contexto">Controlador usado para loguear</param>
        /// <param name="user">usuario de la sesion</param>
        public static void InicializaSesionActiva(HttpContextBase contexto, EntUsuario user)
        {
            setSesionUsuario(contexto, user);
        }

        /// <summary>
        /// Limpia la variable de sesion para el usuario
        /// </summary>
        /// <param name="contexto">Controlador que invoca el proceso</param>
        /// <param name="user">usuario de la sesion que se termina</param>
        internal static void FinalizarSesionActiva(HttpContextBase contexto, EntUsuario user)
        {
            clearSesionUsuario(contexto);
        }

        /// <summary>
        /// Asigna el valor a la variable de session
        /// </summary>
        /// <param name="contexto">Controlador que ejecuta la accion</param>
        /// <param name="value">valor de la session</param>
        private static void setSesionUsuario(HttpContextBase contexto, EntUsuario value)
        {
            contexto.Session[localvarsesion] = value;
        }

        private static EntUsuario getSesionUsuario(HttpContextBase contexto)
        {
            var e = contexto.Session[localvarsesion];
            if (e == null)
            {
                return null;
            }
            else
            {
                return (EntUsuario)contexto.Session[localvarsesion];
            }
        }

        private static void clearSesionUsuario(HttpContextBase contexto)
        {
            contexto.Session.Remove(localvarsesion);
            contexto.Session.Clear();
        }

        public static EntUsuario SesionActiva(HttpContextBase context)
        {
            EntUsuario e = (EntUsuario)context.Session[localvarsesion]; //System.Web.HttpContext.Current.Session[localvarsesion];
            return e;
        }

        public static string GestorErrores(ModelStateDictionary ModelState, string Mensaje = "", bool MostrarDetalle = true)
        {
            string cad = "";
            string def = "Corrija, Todos los errores:";
            string detalle = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).Aggregate((a, b) => a + ", " + b).ToString();
            if (!string.IsNullOrWhiteSpace(Mensaje)) def = Mensaje;
            cad = def + Mensaje + detalle;
            return cad;
        }

        private string cifrar(string cadena)
        {

            byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.

            byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena); //Arreglo donde guardaremos la cadena descifrada.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(cadenasegura));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();

            return Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
        }

        private string descifrar(string cadena)
        {

            byte[] llave;

            byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(cadenasegura));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateDecryptor();
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
            tripledes.Clear();

            string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena
            return cadena_descifrada; // Devolvemos la cadena
        }
    }
}
