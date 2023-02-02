using RendicionCuentasServices.Model;
using RendicionCuentasServices.Services;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Entity.Validation;
using GsgServicios.objetos;

namespace RendicionCuentasServices.Services
{
    public class srvlogin
    {
        dbSDCEntities db = new dbSDCEntities();
        public EntUsuario IsLogin(EntUsuario usuario)
        {
            EntUsuario sesion = null;
            try
            {                
                var us = db.tbl_Responsables.FirstOrDefault(x => x.Correo.ToLower() == usuario.Email.ToLower() && x.Estatus==true );

                //si el usuario existe y la contraseña es la misma
                //chkPass(us.Contrasenia, usuario.Password)
                if (us != null && chkPass(us.Contrasenia, usuario.Password))
                {
                    tbl_Responsables s = getidRol(us.idResponsable);
                    if (s != null)
                    {
                        sesion = new EntUsuario();
                        sesion.Usuario = us.Usuario;
                        //sesion.IdRol = us;
                        sesion.Nombre = us.NombreResponsable;
                        sesion.IdUsuario = us.idResponsable;
                        sesion.Rol = us.Rol;
                        sesion.idArea = us.IdArea;
                        sesion.ficha = us.Ficha;
                        sesion.idPuesto = us.IdPuesto;
                        sesion.idUbicacion = us.IdUbicacion;


                        //sesion.IdEmpleado = us.IdEmpleado;
                    }
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return sesion;
        }

        public EntUsuario IsLogin(string Usuario, string Pass)
        {
            EntUsuario sesion = null;
            try
            {
                var us = db.tbl_Responsables.SingleOrDefault(x => x.Correo.ToLower() == Usuario); //&& x.Activo != 0);

                //si el usuario existe y la contraseña es la misma
                //chkPass(us.Password, usuario.Password)
                if (us != null && chkPass(us.Contrasenia, Pass))
                {
                    tbl_Responsables s = getidRol(us.idResponsable);
                    if (s != null)
                    {
                        sesion = new EntUsuario();
                        sesion.Usuario = us.Usuario;
                        //sesion.IdRol = us.IdRol;
                        sesion.Nombre = us.NombreResponsable;
                        //sesion.IdEmpleado = us.IdEmpleado;
                    }
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return sesion;
        }

        public EntUsuario IsMail(EntUsuario usuario)
        {
            EntUsuario sesion = null;
            try
            {
                var us = db.tbl_Responsables.First(x => x.Correo.ToLower() == usuario.Email.ToLower());
                if (us != null)
                {
                    sesion = new EntUsuario();
                    sesion.Email = us.Correo;
                }
            }
            catch (Exception ex)
            {

            }
            return sesion;
        }

        private tbl_Responsables getidRol(int IdUser)
        {

            var e = db.tbl_Responsables.First(x => x.idResponsable == IdUser);

            if (e != null)
            {
                return e;
            }
            else
            {
                return null;
            }
        }

        //Función que regresa true o false si las contraseñan coninciden, convirtiendo una de ellas codificadas
        private bool chkPass(string passG, string passS)
        {
            return string.Equals(passG, setPassword(passS));
        }

        //Función que regresa true o false si las contraseñan coninciden, convirtiendo una de ellas codificadas
        private bool chkPass2(string passG, string passS)
        {
            return string.Equals(passG, passS);
        }

        //Codificar string para contraseña
        private string setPassword(string pass)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass)));
        }

        public EntUsuario SendMail(EntUsuario model)
        {
            srvMail srvMail = new srvMail("~/siteconfig");

            EntUsuario sesion = null;
            try
            {
                email correo = new email();

                //Preparamos correo para enviar
                correo.De = "soporte@gsgpetroleum.com";
                correo.Asunto = "Tableros SDC: Recuperar Contraseña";
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("~/siteconfig");

                correo.Para = model.Email;
                correo.Cuerpo = "Nuestro sistema detecto una solicitud para cambio de contraseña. Está es la nueva contraseña asignada: " + model.Password;

                srvMail.SendMail(correo);
            }
            catch (Exception ex)
            {

            }
            return sesion;
        }

        public string SetNewPass(EntUsuario model)
        {
            string PassReturn = string.Empty;
            string NPass = "PJ" + DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Year + "$";

            PassReturn = NPass;
            NPass = setPassword(NPass);
            UpdatePass(model.Email, NPass);

            return PassReturn;
        }

        private void UpdatePass(string Email, string NPass)
        {
            var Usuarios = db;
            var e = db.tbl_Responsables.Single(x => x.Correo == Email);

            if (e != null)
            {
                e.Contrasenia = NPass;
                //e.PasswordC = NPass;

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException exp)
                {
                    foreach (var eve in exp.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }


        }
    }
}
