using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvUbicacion
    {
        dbSDCEntities db = new dbSDCEntities();

        public List<tbl_Ubicaciones> getUbicaciones()
        {
            List<tbl_Ubicaciones> lstUbicacion = new List<tbl_Ubicaciones>();

            return lstUbicacion;
        }

        public List<SP_SelectAll_Ubicaciones_Result> getListUbicacion() 
        {
            List<SP_SelectAll_Ubicaciones_Result> ListUbicacion = new List<SP_SelectAll_Ubicaciones_Result>();
            ListUbicacion = db.SP_SelectAll_Ubicaciones().ToList();
            return ListUbicacion;
        }

        public int existUbicacion(string nombre_ubicacion) 
        {
            return db.tbl_Ubicaciones.Where(x => x.Ubicacion.ToLower() == nombre_ubicacion.ToLower()).Count();
        }

        public int addUbicacion(tbl_Ubicaciones ubicacion) 
        {
            int idUbicacion = 0;
            try
            {
                if (ubicacion != null)
                {
                    db.tbl_Ubicaciones.Add(ubicacion);
                    db.SaveChanges();
                    idUbicacion = ubicacion.idUbicacion;
                }
            }
            catch (Exception e)
            {
            }

            return idUbicacion;
        }

        //Función para actualizar datos de Ubicación
        public int updateUbicacion(tbl_Ubicaciones ubicacion)
        {
            var e = db.tbl_Ubicaciones.Single(x => x.idUbicacion == ubicacion.idUbicacion);
            int idUbicacion = 0;
            try
            {
                if (e != null)
                {
                    e.Ubicacion = ubicacion.Ubicacion;
                    e.UsuarioActualizacion = ubicacion.UsuarioActualizacion;
                    e.FechaActualizacion = ubicacion.FechaActualizacion;
                    db.SaveChanges();
                }
                idUbicacion = ubicacion.idUbicacion;
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            return idUbicacion;
        }

        //Función para actualizar datos de Ubicación
        public int deleteUbicacion(tbl_Ubicaciones ubicacion)
        {
            var e = db.tbl_Ubicaciones.Single(x => x.idUbicacion == ubicacion.idUbicacion);
            int ridUbicacion = 0;
            try
            {
                if (e != null)
                {
                    e.UsuarioActualizacion = ubicacion.UsuarioActualizacion;
                    e.FechaActualizacion = ubicacion.FechaActualizacion;
                    e.Estatus = false;    
                    db.SaveChanges();
                }
                ridUbicacion = ubicacion.idUbicacion;
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            return ridUbicacion;
        }

    }
}
