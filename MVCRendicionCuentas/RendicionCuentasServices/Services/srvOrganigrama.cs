using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvOrganigrama
    {
        dbSDCEntities db = new dbSDCEntities();

        /// <summary>
        /// funciona lista todo las localidades o nodos creados
        /// </summary>
        /// <returns>lista de objeto tipo tbl_menu</returns>
        public List<EntOrganigrama> GetListOrganigrama()
        {
            List<EntOrganigrama> Organigrama = new List<EntOrganigrama>();
            List<tbl_Subdirecciones> listSubdirecciones = db.tbl_Subdirecciones.Where(x => x.Estatus == true && x.Tipo == null).OrderBy(x=>x.NombreSubdireccion).ToList();
            foreach(tbl_Subdirecciones subdireccion in listSubdirecciones)
            {
                EntOrganigrama Org = new EntOrganigrama();
                Org.Grencias = new List<EntOrganigramaGerencias>();
                Org.ClaveSubdireccion = subdireccion.ClaveSubdireccion;
                Org.IdSubdireccion = subdireccion.IdSubdireccion;
                Org.NombreSubdireccion = subdireccion.NombreSubdireccion;
                List<tbl_Gerencias> listGerencias = db.tbl_Gerencias.Where(x => x.Estatus == true && x.IdSubdireccion == subdireccion.IdSubdireccion && x.Tipo == null).OrderBy(x => x.NombreGerencia).ToList();
                foreach(tbl_Gerencias gerencia in listGerencias)
                {
                    EntOrganigramaGerencias Ger = new EntOrganigramaGerencias();
                    Ger.Coordinaciones = new List<EntOrganigramaCoordinaciones>();
                    Ger.ClaveGerencias = gerencia.ClaveGerencias;
                    Ger.IdGerencia = gerencia.IdGerencia;
                    Ger.IdSubdireccion = gerencia.IdSubdireccion;
                    Ger.NombreGerencia = gerencia.NombreGerencia;
                    List<tbl_Coordinaciones> listCoordinaciones = db.tbl_Coordinaciones.Where(x => x.Estatus == true && x.IdGerencia == gerencia.IdGerencia && x.Tipo == null).OrderBy(x => x.NombreCoordinacion).ToList();
                    foreach (tbl_Coordinaciones coordinacion in listCoordinaciones)
                    {
                        EntOrganigramaCoordinaciones Coo = new EntOrganigramaCoordinaciones();
                        Coo.Superintendecias = new List<EntOrganigramaSuperintendencias>();
                        Coo.ClaveCoordinaciones = coordinacion.ClaveCoordinaciones;
                        Coo.IdCoordinacion = coordinacion.IdCoordinacion;
                        Coo.IdGerencia = coordinacion.IdGerencia;
                        Coo.NombreCoordinacion = coordinacion.NombreCoordinacion;
                        List<tbl_Superintendencias> listSuperintendencias = db.tbl_Superintendencias.Where(x => x.Estatus == true && x.IdCoordinacion == Coo.IdCoordinacion && x.Tipo == null).OrderBy(x => x.NombreSuperIntendencia).ToList();
                        foreach(tbl_Superintendencias superintendencia in listSuperintendencias)
                        {
                            EntOrganigramaSuperintendencias Sup = new EntOrganigramaSuperintendencias();
                            Sup.ClaveSuperintendencias = superintendencia.ClaveSuperintendencias;
                            Sup.IdSuperintendencia = superintendencia.IdSuperintendencia;
                            Sup.IdCoordinacion = superintendencia.IdCoordinacion;
                            Sup.NombreSuperIntendencia = superintendencia.NombreSuperIntendencia;
                            Sup.Puestos = getListPuestos(superintendencia.IdSuperintendencia, 4);
                            Coo.Superintendecias.Add(Sup);
                        }
                        Coo.Puestos = getListPuestos(coordinacion.IdCoordinacion, 3);
                        Ger.Coordinaciones.Add(Coo);    
                    }
                    Ger.Puestos = getListPuestos(gerencia.IdGerencia, 2);
                    Org.Grencias.Add(Ger);
                }
                Org.Puestos = getListPuestos(subdireccion.IdSubdireccion, 1);
                Organigrama.Add(Org);
            }
            
            return Organigrama;
        }

        private List<tbl_PuestoOrganigrama> getListPuestos(int IdElemento, int IdTipo)
        {
            List<tbl_PuestoOrganigrama> listPuestoSub = new List<tbl_PuestoOrganigrama>();
            try
            {
                listPuestoSub = db.tbl_PuestoOrganigrama.Where(x => x.IdElemento == IdElemento && x.IdTipoElementoOrganigrama == IdTipo && x.Estatus == true).OrderBy(x => x.NombrePuesto).ToList();
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            return listPuestoSub;
        }

        private int existNivelIn(string nombre, string clave, int tipo) {
            int nr = 0;
            try
            {
                switch(tipo)
                {
                    case 1: nr = db.tbl_Subdirecciones.Where(x => x.NombreSubdireccion.Replace(" ", "").ToLower() == nombre.Replace(" ", "").ToLower() && x.ClaveSubdireccion.Replace(" ", "").ToLower() == clave.Replace(" ", "").ToLower() && x.Estatus == true && x.Tipo == null).ToList().Count();
                        break;
                    case 2: nr = db.tbl_Gerencias.Where(x => x.NombreGerencia.Replace(" ", "").ToLower() == nombre.Replace(" ", "").ToLower() && x.ClaveGerencias.Replace(" ", "").ToLower() == clave.Replace(" ", "").ToLower() && x.Estatus == true && x.Tipo == null).ToList().Count();
                        break;
                    case 3: nr = db.tbl_Coordinaciones.Where(x => x.NombreCoordinacion.Replace(" ", "").ToLower() == nombre.Replace(" ", "").ToLower() && x.ClaveCoordinaciones.Replace(" ", "").ToLower() == clave.Replace(" ", "").ToLower() && x.Estatus == true && x.Tipo == null).ToList().Count();
                        break;
                    case 4: nr = db.tbl_Superintendencias.Where(x => x.NombreSuperIntendencia.Replace(" ", "").ToLower() == nombre.Replace(" ", "").ToLower() && x.ClaveSuperintendencias.Replace(" ", "").ToLower() == clave.Replace(" ", "").ToLower() && x.Estatus == true && x.Tipo == null).ToList().Count();
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            return nr;
        }

        private tbl_PuestoOrganigrama getPuesto( int idRelacion ,int tipo, string nombre)
        {
            tbl_PuestoOrganigrama puesto = new tbl_PuestoOrganigrama();
            try
            {
                SP_SelectAll_BusquedaPuestoBeforeInsert_Result rs = new SP_SelectAll_BusquedaPuestoBeforeInsert_Result();
                rs = db.SP_SelectAll_BusquedaPuestoBeforeInsert(idRelacion, tipo, nombre).FirstOrDefault();
                if (rs != null)
                {
                    puesto.idPuestoOrganigrama = rs.idPuestoOrganigrama;
                    puesto.IdElemento = rs.IdElemento;
                    puesto.IdTipoElementoOrganigrama = rs.IdTipoElementoOrganigrama;
                    puesto.Clave = rs.Clave;
                    puesto.NombrePuesto = rs.NombrePuesto;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            return puesto;
        }
       
        public int AddNivelOrganigrama(EntOrganigramaNivelGenerico item, int tipo)
        {
            int IdNewOrganigrama = 0;
            try
            {
                int n = 0;
                if (n == 0)
                {
                    switch (tipo)
                    {
                        case 1:
                            tbl_Subdirecciones Sub = new tbl_Subdirecciones();
                            Sub.NombreSubdireccion = item.NombreNivel;
                            Sub.ClaveSubdireccion = item.ClaveNivel;
                            Sub.FechaCreacion = DateTime.Now;
                            Sub.UsuarioCreacion = item.UsuarioCreacion;
                            Sub.FechaActualizacion = DateTime.Now;
                            Sub.UsuarioActualizacion = item.UsuarioActualizacion;
                            Sub.Estatus = true;
                            db.tbl_Subdirecciones.Add(Sub);
                            db.SaveChanges();
                            IdNewOrganigrama = Sub.IdSubdireccion;
                            break;
                        case 2:
                            tbl_Gerencias Ger = new tbl_Gerencias();
                            Ger.NombreGerencia = item.NombreNivel;
                            Ger.ClaveGerencias = item.ClaveNivel;
                            Ger.IdSubdireccion = item.IdRelacion;
                            Ger.FechaCreacion = DateTime.Now;
                            Ger.UsuarioCreacion = item.UsuarioCreacion;
                            Ger.FechaActualizacion = DateTime.Now;
                            Ger.UsuarioActualizacion = item.UsuarioActualizacion;
                            Ger.Estatus = true;
                            db.tbl_Gerencias.Add(Ger);
                            db.SaveChanges();
                            IdNewOrganigrama = Ger.IdGerencia;
                            break;
                        case 3:
                            tbl_Coordinaciones Coo = new tbl_Coordinaciones();
                            Coo.NombreCoordinacion = item.NombreNivel;
                            Coo.ClaveCoordinaciones = item.ClaveNivel;
                            Coo.IdGerencia = item.IdRelacion;
                            Coo.FechaCreacion = DateTime.Now;
                            Coo.UsuarioCreacion = item.UsuarioCreacion;
                            Coo.FechaActualizacion = DateTime.Now;
                            Coo.UsuarioActualizacion = item.UsuarioActualizacion;
                            Coo.Estatus = true;
                            db.tbl_Coordinaciones.Add(Coo);
                            db.SaveChanges();
                            IdNewOrganigrama = Coo.IdCoordinacion;
                            break;
                        case 4:
                            tbl_Superintendencias Sup = new tbl_Superintendencias();
                            Sup.NombreSuperIntendencia = item.NombreNivel;
                            Sup.ClaveSuperintendencias = item.ClaveNivel;
                            Sup.IdCoordinacion = item.IdRelacion;
                            Sup.FechaCreacion = DateTime.Now;
                            Sup.UsuarioCreacion = item.UsuarioCreacion;
                            Sup.FechaActualizacion = DateTime.Now;
                            Sup.UsuarioActualizacion = item.UsuarioActualizacion;
                            Sup.Estatus = true;
                            db.tbl_Superintendencias.Add(Sup);
                            db.SaveChanges();
                            IdNewOrganigrama = Sup.IdSuperintendencia;
                            break;
                    }
                }
                else {
                    IdNewOrganigrama = -1;
                }
            }
            catch(Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return IdNewOrganigrama;
        }

        public int UpdateNivelOrganigrama(EntOrganigramaNivelGenerico item, int tipo)
        {

            int IdNewOrganigrama = 0;
            try
            {
                switch (tipo)
                {
                    case 1:
                        tbl_Subdirecciones Sub = db.tbl_Subdirecciones.Where(x => x.IdSubdireccion == item.IdNivel && x.Estatus == true && x.Tipo == null).FirstOrDefault();
                        Sub.NombreSubdireccion = item.NombreNivel;
                        Sub.ClaveSubdireccion = item.ClaveNivel;
                        Sub.FechaActualizacion = DateTime.Now;
                        Sub.UsuarioActualizacion = item.UsuarioActualizacion;
                        db.SaveChanges();
                        IdNewOrganigrama = Sub.IdSubdireccion;
                        break;
                    case 2:
                        tbl_Gerencias Ger = db.tbl_Gerencias.Where(x => x.IdGerencia == item.IdNivel && x.Estatus == true && x.Tipo == null).FirstOrDefault();
                        Ger.NombreGerencia = item.NombreNivel;
                        Ger.ClaveGerencias = item.ClaveNivel;
                        Ger.FechaActualizacion = DateTime.Now;
                        Ger.UsuarioActualizacion = item.UsuarioActualizacion;
                        db.SaveChanges();
                        IdNewOrganigrama = Ger.IdGerencia;
                        break;
                    case 3:
                        tbl_Coordinaciones Coo = db.tbl_Coordinaciones.Where(x => x.IdCoordinacion == item.IdNivel && x.Estatus == true && x.Tipo == null).FirstOrDefault();
                        Coo.NombreCoordinacion = item.NombreNivel;
                        Coo.ClaveCoordinaciones = item.ClaveNivel;
                        Coo.FechaActualizacion = DateTime.Now;
                        Coo.UsuarioActualizacion = item.UsuarioActualizacion;
                        db.SaveChanges();
                        IdNewOrganigrama = Coo.IdCoordinacion;
                        break;
                    case 4:
                        tbl_Superintendencias Sup = db.tbl_Superintendencias.Where(x => x.IdSuperintendencia == item.IdNivel && x.Estatus == true && x.Tipo == null).FirstOrDefault();
                        Sup.NombreSuperIntendencia = item.NombreNivel;
                        Sup.ClaveSuperintendencias = item.ClaveNivel;
                        Sup.FechaActualizacion = DateTime.Now;
                        Sup.UsuarioActualizacion = item.UsuarioActualizacion;
                        db.SaveChanges();
                        IdNewOrganigrama = Sup.IdSuperintendencia;
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return IdNewOrganigrama;
        }

        public int DeleteNivelOrganigrama(EntOrganigramaNivelGenerico item, int tipo)
        {

            int IdNewOrganigrama = 0;
            try
            {
                switch (tipo)
                {
                    case 1:
                        tbl_Subdirecciones Sub = db.tbl_Subdirecciones.Where(x => x.IdSubdireccion == item.IdNivel && x.Estatus == true && x.Tipo == null).FirstOrDefault();
                        Sub.FechaActualizacion = DateTime.Now;
                        Sub.UsuarioActualizacion = item.UsuarioActualizacion;
                        Sub.Estatus = false;
                        db.SaveChanges();
                        IdNewOrganigrama = Sub.IdSubdireccion;
                        break;
                    case 2:
                        tbl_Gerencias Ger = db.tbl_Gerencias.Where(x => x.IdGerencia == item.IdNivel && x.Estatus == true && x.Tipo == null).FirstOrDefault();
                        Ger.FechaActualizacion = DateTime.Now;
                        Ger.UsuarioActualizacion = item.UsuarioActualizacion;
                        Ger.Estatus = false;
                        db.SaveChanges();
                        IdNewOrganigrama = Ger.IdGerencia;
                        break;
                    case 3:
                        tbl_Coordinaciones Coo = db.tbl_Coordinaciones.Where(x => x.IdCoordinacion == item.IdNivel && x.Estatus == true && x.Tipo == null).FirstOrDefault();
                        Coo.FechaActualizacion = DateTime.Now;
                        Coo.UsuarioActualizacion = item.UsuarioActualizacion;
                        Coo.Estatus = false;
                        db.SaveChanges();
                        IdNewOrganigrama = Coo.IdCoordinacion;
                        break;
                    case 4:
                        tbl_Superintendencias Sup = db.tbl_Superintendencias.Where(x => x.IdSuperintendencia == item.IdNivel && x.Estatus == true && x.Tipo == null).FirstOrDefault();
                        Sup.FechaActualizacion = DateTime.Now;
                        Sup.UsuarioActualizacion = item.UsuarioActualizacion;
                        Sup.Estatus = false;
                        db.SaveChanges();
                        IdNewOrganigrama = Sup.IdSuperintendencia;
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return IdNewOrganigrama;
        }

        public int AddPuestoOrganigrama(tbl_PuestoOrganigrama item)
        {
            int IdNewPuesto = 0;
            try
            {
                tbl_PuestoOrganigrama  p = getPuesto(item.IdElemento.Value, item.IdTipoElementoOrganigrama.Value, item.NombrePuesto);
                if (p.idPuestoOrganigrama == 0)
                {
                    p.IdElemento = item.IdElemento.Value;
                    p.IdTipoElementoOrganigrama = item.IdTipoElementoOrganigrama.Value;
                    p.NombrePuesto = item.NombrePuesto;
                    p.Clave = item.Clave;
                    p.Estatus = true;
                    db.tbl_PuestoOrganigrama.Add(p);
                    db.SaveChanges();
                    IdNewPuesto = p.idPuestoOrganigrama;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return IdNewPuesto;
        }

        public int UpdatePuestoOrganigrama(tbl_PuestoOrganigrama item)
        {
            int IdNewPuesto = 0;
            try
            {
                tbl_PuestoOrganigrama p = db.tbl_PuestoOrganigrama.Where(x => x.idPuestoOrganigrama == item.idPuestoOrganigrama && x.Estatus == true).FirstOrDefault();
                if (p.idPuestoOrganigrama > 0)
                {
                    p.NombrePuesto = item.NombrePuesto;
                    p.Clave = item.Clave;
                    db.SaveChanges();
                    IdNewPuesto = p.idPuestoOrganigrama;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return IdNewPuesto;
        }

        public int DeletePuestoOrganigrama(int idPuesto)
        {
            int IdNewPuesto = 0;
            try
            {
                tbl_PuestoOrganigrama p = db.tbl_PuestoOrganigrama.Where(x => x.idPuestoOrganigrama == idPuesto && x.Estatus == true).FirstOrDefault();
                if (p.idPuestoOrganigrama > 0)
                {
                    p.Estatus = false;
                    db.SaveChanges();
                    IdNewPuesto = p.idPuestoOrganigrama;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return IdNewPuesto;
        }

    }
}
