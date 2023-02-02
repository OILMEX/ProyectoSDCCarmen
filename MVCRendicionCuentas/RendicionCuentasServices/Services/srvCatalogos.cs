using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvCatalogos
    {
        dbSDCEntities db = new dbSDCEntities();

        #region Catalogo para tipo proyectos
        public List<tbl_TipoProyectos> getListTipoProyectos()
        {
            List<tbl_TipoProyectos> lstTipoProyectos = new List<tbl_TipoProyectos>();
            lstTipoProyectos = db.tbl_TipoProyectos.Where(x => x.Estatus == true).ToList();

            return lstTipoProyectos;
        }

        public int addTipoProyecto(tbl_TipoProyectos item)
        {
            int id = 0;
            try
            {
                tbl_TipoProyectos p = db.tbl_TipoProyectos.Where(x => x.TipoProyecto.ToLower() == item.TipoProyecto.ToLower()).FirstOrDefault();
                if (p == null)
                {
                    p = new tbl_TipoProyectos();
                    p.TipoProyecto = item.TipoProyecto;
                    p.Estatus = item.Estatus;
                    p.FechaActualizacion = DateTime.Now;
                    p.UsuarioActualizacion = item.UsuarioActualizacion;
                    db.tbl_TipoProyectos.Add(p);
                    db.SaveChanges();
                    id = p.IdTipoProyecto;
                }
                else
                {
                    throw new Exception("Ya existe el Tipo de Proyecto");
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return id;
        }

        public int updateTipoProyecto(tbl_TipoProyectos item)
        {
            int id = 0;
            try
            {
                tbl_TipoProyectos p = db.tbl_TipoProyectos.Where(x => x.IdTipoProyecto == item.IdTipoProyecto).FirstOrDefault();
                if(p != null)
                {
                    p.TipoProyecto = item.TipoProyecto;
                    p.Estatus = item.Estatus;
                    p.FechaActualizacion = DateTime.Now;
                    p.UsuarioActualizacion = item.UsuarioActualizacion;
                    db.SaveChanges();
                    id = p.IdTipoProyecto;
                }
            }
            catch(Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return id;
        }

        public int deleteTipoProyecto(tbl_TipoProyectos item)
        {
            int id = 0;
            try
            {
                tbl_TipoProyectos p = db.tbl_TipoProyectos.Where(x => x.IdTipoProyecto == item.IdTipoProyecto).FirstOrDefault();
                if (p != null)
                {
                    p.TipoProyecto = item.TipoProyecto;
                    p.Estatus = false;
                    p.FechaActualizacion = DateTime.Now;
                    p.UsuarioActualizacion = item.UsuarioActualizacion;
                    db.SaveChanges();
                    id = p.IdTipoProyecto;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return id;
        }

        #endregion
    }
}
