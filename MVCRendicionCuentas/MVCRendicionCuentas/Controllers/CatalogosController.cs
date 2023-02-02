using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCRendicionCuentas.Controllers
{
    public class CatalogosController : ValidatorController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios(int IdUbicacion = 0, string Nombre = null)
        {
            if (myCookie != null)
            {
                srvUsuarios srv = new srvUsuarios();
                List<tbl_Ubicaciones> Ubicaciones = srv.getUbicaciones();
                ViewData["Ubicaciones"] = Ubicaciones;
                Nombre = (Nombre == "") ? null : Nombre;
                List<SP_SelectAll_Responsables_Result> LResp = new List<SP_SelectAll_Responsables_Result>();
                if(IdUbicacion > 0 || Nombre != null){
                    LResp = srv.BuscarUsuario(IdUbicacion, Nombre);
                }else{
                    LResp = srv.GetALLUsuariosSP(true);
                }
                List<SP_SelectAll_Responsables_Result> Rsp = LResp.OrderBy(x => x.NombreResponsable.Trim()).ToList();
                return View("_Usuarios", Rsp);
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        [HttpPost]
        public ActionResult NuevoResponsable(string InputPersonal, string InputUsuario, string InputPassword1, string InputPassword2, string InputEmail1, string InputFicha, int? idJefe, string idRol, int idUbicacion, int idPuesto) //int idPuesto,
        {
            srvUsuarios srv = new srvUsuarios();
            tbl_Responsables item = new tbl_Responsables();
            item.NombreResponsable = InputPersonal;
            item.Usuario = InputUsuario;
            item.Contrasenia = InputPassword1;
            item.Correo = InputEmail1;
            item.Ficha = InputFicha;
            item.IdPuesto = idPuesto;
            //item.IdArea = idArea;
            item.IdAquienReporta = idJefe;
            item.Rol=idRol;
            item.IdUbicacion = idUbicacion;
            item.Estatus = true;
            int idNew = 0;
           
            idNew = srv.AddUsuario(item);
            if (idNew >= 0)
            {
                return RedirectToAction("Usuarios", "Catalogos");
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult validarMail(string mail) {
            srvUsuarios srv = new srvUsuarios();
            int existeMail = srv.ExisteMail(mail);
            if (existeMail == 0)
            {
                return Json(new JavaScriptSerializer().Serialize(0), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new JavaScriptSerializer().Serialize(-1), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult validarNombre(string nombre) {
            srvUsuarios srv = new srvUsuarios();
            List<SP_SelectAll_Responsables_Result> UsuEncontrado = new List<SP_SelectAll_Responsables_Result>();
            //Esta búsqueda válida que no exista el nombre
            UsuEncontrado = srv.BuscarUsuario(0, nombre);
            if (UsuEncontrado.Count > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(-1), JsonRequestBehavior.AllowGet);
            }
            else    
            {
                return Json(new JavaScriptSerializer().Serialize(0), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateResponsable(int idResponsable, string InputPersonal, string InputUsuario, string InputPassword1, string InputPassword2, string InputEmail1, string InputFicha, int? idJefe, string idRol, int idUbicacion, int idPuesto)
        {
            srvUsuarios srv = new srvUsuarios();
            tbl_Responsables item = new tbl_Responsables();
            item.idResponsable = idResponsable;
            item.NombreResponsable = InputPersonal;
            item.Usuario = InputUsuario;
            item.Contrasenia = InputPassword1;
            item.Correo = InputEmail1;
            item.Ficha = InputFicha;
            item.IdPuesto = idPuesto;
            //item.IdArea = idArea;
            item.IdAquienReporta = idJefe;
            item.Rol = idRol;
            item.IdUbicacion = idUbicacion;
            item.UsuarioActualizacion = IdCargaResponsable;
            //item.Estatus = true;

            int R = srv.UpdateUsuario(item);

            if (R==1)
            {
                return RedirectToAction("Usuarios", "Catalogos");
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult eliminarResponsables(int id)
        {
            srvUsuarios srv = new srvUsuarios();
            tbl_Responsables tr = new tbl_Responsables();
            tr.idResponsable = id;
            tr.UsuarioActualizacion = IdCargaResponsable;
            int idNew = srv.DeleteUsuario(tr);

            if (idNew > 0)
            {
                return RedirectToAction("Usuarios", "Catalogos");
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult CatalogosPuestos()
        {
            srvUsuarios srv = new srvUsuarios();
            var x = srv.llenarPuestos();
            return Json(new JavaScriptSerializer().Serialize(x), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CatalogosAreas()
        {
            srvUsuarios srv = new srvUsuarios();
            var x = srv.llenarAreas();
            return Json(new JavaScriptSerializer().Serialize(x), JsonRequestBehavior.AllowGet);
        }

       [HttpGet]
        public JsonResult CatalogosJefes()
        {
            srvUsuarios srv = new srvUsuarios();
            var x = srv.llenarJefes();
            return Json(new JavaScriptSerializer().Serialize(x), JsonRequestBehavior.AllowGet);
        }
       [HttpGet]
       public JsonResult CatalogosRoles()
       {
           srvUsuarios srv = new srvUsuarios();
           var x = srv.llenarcmbRoles();
           return Json(new JavaScriptSerializer().Serialize(x), JsonRequestBehavior.AllowGet);
       }
        public JsonResult GetUsuarios(int id)
        {
            srvUsuarios srv = new srvUsuarios();
            var x = srv.BuscarUsuario(id);
            return Json(new JavaScriptSerializer().Serialize(x), JsonRequestBehavior.AllowGet);
        }

        public FilePathResult CargaJson()
        {
            return File("~/Models/datos.json", "text/x-json");
        }

        [HttpPost]
        public JsonResult getDependencias(int idpuesto)
        {
            srvUsuarios srv = new srvUsuarios();
            var x = srv.getDependencias(idpuesto);
            return Json(new JavaScriptSerializer().Serialize(x));
        }


        #region Catalogo de Tipo de proyectos
        public ActionResult TipoProyectos()
        {
            if (myCookie != null)
            {
                srvCatalogos srv = new srvCatalogos();
                List<tbl_TipoProyectos> list = srv.getListTipoProyectos();
                return View("_TipoProyectos", list.OrderBy(x=>x.TipoProyecto).ToList());
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        public JsonResult addTipoProyecto(tbl_TipoProyectos obj)
        {
            tbl_TipoProyectos item = new tbl_TipoProyectos();
            item.TipoProyecto = obj.TipoProyecto.Trim();
            item.Estatus = true;
            item.UsuarioCreacion = IdCargaResponsable;
            srvCatalogos srv = new srvCatalogos();
            int idNew = srv.addTipoProyecto(item);
            if (idNew > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult editTipoProyecto(tbl_TipoProyectos obj)
        {
            tbl_TipoProyectos item = new tbl_TipoProyectos();
            item.IdTipoProyecto = obj.IdTipoProyecto;
            item.TipoProyecto = obj.TipoProyecto.Trim();
            item.Estatus = true;
            item.UsuarioActualizacion = IdCargaResponsable;
            srvCatalogos srv = new srvCatalogos();
            int idNew = srv.updateTipoProyecto(item);
            if (idNew > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteTipoProyecto(tbl_TipoProyectos obj)
        {
            tbl_TipoProyectos item = new tbl_TipoProyectos();
            item.IdTipoProyecto = obj.IdTipoProyecto;
            item.TipoProyecto = obj.TipoProyecto;
            item.Estatus = false;
            item.UsuarioActualizacion = IdCargaResponsable;
            srvCatalogos srv = new srvCatalogos();
            int idNew = srv.deleteTipoProyecto(item);
            if (idNew > 0)
            {
                return Json(new JavaScriptSerializer().Serialize(idNew), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region Catalogo Usuarios Eliminados
        public ActionResult UsuariosEliminados(int[] idUsers = null)
        {
            if (myCookie != null)
            {
                srvUsuarios srv = new srvUsuarios();
                if(idUsers != null)
                {
                    if (srv.ReactivarUsuario(idUsers, IdCargaResponsable))
                    {
                        ViewData["msj"] = "Se restaurarón " + idUsers.Length + ((idUsers.Length > 1)? " usuarios" : " usuario");
                        ViewData["class"] = "alert alert-success";
                    }
                    else
                    {
                        ViewData["msj"] = "No se pudierón restaurar los usuarios.";
                        ViewData["class"] = "alert alert-warning";
                    }
                    ViewData["showmsg"] = "true";
                }
                else
                {
                    ViewData["showmsg"] = "false";
                }
                
                List<EntUsuariosEliminados> list = srv.GetUsuariosEliminados();
                return View("_UsuariosEliminados", list);
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        #endregion

    }
}