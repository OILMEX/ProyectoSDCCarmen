using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvCatProyecto
    {
        dbSDCEntities db = new dbSDCEntities();

        public List<EntEtapa> GetEtapasProyecto()
        {
            List<EntEtapa> listetapas = new List<EntEtapa>();
            List<tbl_Etapas> etapas =  db.tbl_Etapas.Where(x => x.Estatus == true).ToList();

            foreach(tbl_Etapas e in etapas){
                EntEtapa etapa = new EntEtapa();
                etapa.etapa_id = e.IdEtapa;
                etapa.clave = e.Clave;
                etapa.etapa_nombre = e.NombreEtapa;
                etapa.UsuarioCreacion = e.UsuarioCreacion;
                etapa.UsuarioActualizacion = e.UsuarioActualizacion;
                etapa.FechaCreacion = e.FechaCreacion;
                etapa.FechaActualizacion = e.FechaActualizacion;

                //obtenemos las subetapas
                List<EntSubEtapa> listsubetapas = new List<EntSubEtapa>();
                List<tbl_SubEtapas> subetapas = db.tbl_SubEtapas.Where(x => x.IdEtapa == e.IdEtapa & x.Estatus == true).ToList();
                foreach(tbl_SubEtapas sub in subetapas)
                {
                    EntSubEtapa entsubetapa = new EntSubEtapa();
                    entsubetapa.subetapa_id = sub.IdSubEtapa;
                    entsubetapa.clave = sub.Clave;
                    entsubetapa.subetapa_nombre = sub.NombreSubEtapa;
                    listsubetapas.Add(entsubetapa);
                }
                etapa.SubEtapas = listsubetapas;
                listetapas.Add(etapa);
                
            }

            return listetapas;
        }

        public int AddEtapa(tbl_Etapas item)
        {
            int IdNewEtapa = 0;

            //tbl_Organigrama newOrganigrama = new tbl_Organigrama();
            try
            {
                if (item != null)
                {
                    db.tbl_Etapas.Add(item);
                    db.SaveChanges();

                    IdNewEtapa = item.IdEtapa;
                }
            }
            catch (Exception e)
            {

            }

            return IdNewEtapa;
        }


        public void UpdateEtapa(tbl_Etapas item)
        {
            var e = db.tbl_Etapas.Single(x => x.IdEtapa == item.IdEtapa);

            if (e != null)
            {
                e.Clave = item.Clave;
                e.NombreEtapa = item.NombreEtapa;
                e.UsuarioActualizacion = item.UsuarioActualizacion;
                e.FechaActualizacion = item.FechaActualizacion;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Elimina fisicamente un registro del organigrama y los hijos dependientes de este elemento
        /// </summary>
        /// <param name="item">Objeto del elemento a eliminar</param>
        public void DeleteEtapa(tbl_Etapas item)
        {
            var ent = db.tbl_Etapas;
            try
            {
                if (ent != null)
                {
                    var e = ent.FirstOrDefault(X => X.IdEtapa == item.IdEtapa);
                    if (e != null)
                    {

                        //Buscar hijos de nodo
                        var n = db.tbl_SubEtapas.Where(n1 => n1.IdEtapa == e.IdEtapa);
                        if (n != null)
                        {
                            foreach (tbl_SubEtapas Nchild in n)
                            {
                                Nchild.Estatus = false;
                                Nchild.UsuarioActualizacion = item.UsuarioActualizacion;
                                Nchild.FechaActualizacion = item.FechaActualizacion;
                            }
                        }
                        e.Estatus = false;
                        e.UsuarioActualizacion = item.UsuarioActualizacion;
                        e.FechaActualizacion = item.FechaActualizacion;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        public int AddSubetapa(tbl_SubEtapas item)
        {
            int IdNewSubetapa = 0;

            //tbl_Organigrama newOrganigrama = new tbl_Organigrama();
            try
            {
                if (item != null)
                {
                    db.tbl_SubEtapas.Add(item);
                    db.SaveChanges();

                    IdNewSubetapa = item.IdSubEtapa;
                }
            }
            catch (Exception e)
            {

            }

            return IdNewSubetapa;
        }


        public void UpdateSubtapa(tbl_SubEtapas item)
        {
            var e = db.tbl_SubEtapas.Single(x => x.IdSubEtapa == item.IdSubEtapa);

            if (e != null)
            {
                e.Clave = item.Clave;
                e.NombreSubEtapa = item.NombreSubEtapa;
                e.UsuarioActualizacion = item.UsuarioActualizacion;
                e.FechaActualizacion = item.FechaActualizacion;
                db.SaveChanges();
            }
        }

        public void DeleteSubetapa(tbl_SubEtapas item)
        {
            var ent = db.tbl_SubEtapas;
            try
            {
                if (ent != null)
                {
                    var e = ent.FirstOrDefault(X => X.IdSubEtapa == item.IdSubEtapa);
                    if (e != null)
                    {
                        e.Estatus = false;
                        e.UsuarioActualizacion = item.UsuarioActualizacion;
                        e.FechaActualizacion = item.FechaActualizacion;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}
