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
    public class UbicacionController : ValidatorController
    {
        srvUbicacion srvUbicacion = new srvUbicacion();

        public ActionResult Index()
        {
            if (myCookie != null)
            {
                List<SP_SelectAll_Ubicaciones_Result> listUbicacion = new List<SP_SelectAll_Ubicaciones_Result>();
                listUbicacion = srvUbicacion.getListUbicacion();
                return View("Index", listUbicacion);
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        public JsonResult saveUbicacion(string nombre_ubicacion){
            try
            {
                if (srvUbicacion.existUbicacion(nombre_ubicacion) == 0)
                {                
                    tbl_Ubicaciones ubicacion = new tbl_Ubicaciones();
                    ubicacion.Ubicacion = nombre_ubicacion;
                    ubicacion.UsuarioCreacion = IdCargaResponsable;
                    ubicacion.UsuarioActualizacion = IdCargaResponsable;
                    ubicacion.FechaCreacion = DateTime.Now;
                    ubicacion.FechaActualizacion = DateTime.Now;
                    ubicacion.Estatus = true;
                    int idUbicacion = srvUbicacion.addUbicacion(ubicacion);
                    if (idUbicacion > 0)
                    {
                        return Json(new JavaScriptSerializer().Serialize(idUbicacion), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("false", JsonRequestBehavior.AllowGet);
                    }                
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }    
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }  
        }

        public JsonResult updateUbicacion(int idUbicacion, string nombre_ubicacion)
        {
            try
            {
                if (srvUbicacion.existUbicacion(nombre_ubicacion) == 0)
                {
                    tbl_Ubicaciones ubicacion = new tbl_Ubicaciones();
                    ubicacion.idUbicacion = idUbicacion;
                    ubicacion.Ubicacion = nombre_ubicacion;
                    ubicacion.UsuarioActualizacion = IdCargaResponsable;
                    ubicacion.FechaActualizacion = DateTime.Now;
                    int ridUbicacion = srvUbicacion.updateUbicacion(ubicacion);
                    if (ridUbicacion > 0)
                    {
                        return Json(new JavaScriptSerializer().Serialize(idUbicacion), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("false", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteUbicacion(int idUbicacion)
        {
            try
            {
                tbl_Ubicaciones ubicacion = new tbl_Ubicaciones();
                ubicacion.idUbicacion = idUbicacion;
                ubicacion.UsuarioActualizacion = IdCargaResponsable;
                ubicacion.FechaActualizacion = DateTime.Now;
                int ridUbicacion = srvUbicacion.deleteUbicacion(ubicacion);
                if (ridUbicacion > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(idUbicacion), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getListUbicacion()
        {
            try
            {
                List<SP_SelectAll_Ubicaciones_Result> listUbicacion = new List<SP_SelectAll_Ubicaciones_Result>();
                listUbicacion = srvUbicacion.getListUbicacion().OrderBy(x=>x.Ubicacion).ToList();
                if (listUbicacion.Count > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(listUbicacion), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

	}
}