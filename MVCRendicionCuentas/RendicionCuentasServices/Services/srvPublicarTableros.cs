using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvPublicarTableros
    {
        dbSDCEntities db = new dbSDCEntities();


        public List<SP_Select_Publicacion_Result> GetPublicaciones(string Parameter)
        {
            List<SP_Select_Publicacion_Result> result = db.SP_Select_Publicacion(Parameter).ToList();
            return result;

        }

        public void DeletePublicacion(int IdPublicacion)
        {
            
            var publicacion = db.Dashboard_Publicaciones.FirstOrDefault(x => x.IdPublicacion == IdPublicacion);
            if(publicacion!=null)
            {
                db.Dashboard_Publicaciones.Remove(publicacion);
                db.SaveChanges();
            }

        }

        public Dashboard_Publicaciones GetPublicacionesDet(int? idpublicacion)
        {
            Dashboard_Publicaciones result = db.Dashboard_Publicaciones.FirstOrDefault(x => x.IdPublicacion == idpublicacion);


            return result;
        }

        public Dashboard_Publicaciones GetPublicacionesDetPrueba()
        {
            var result = db.Dashboard_Publicaciones.Where(x => x.TipoPublicacion=="Prueba").OrderByDescending(x=>x.FechaPublicacion);
            Dashboard_Publicaciones objPublicacion = new Dashboard_Publicaciones();
            int i = 0;
            foreach (var item in result)
            {
                if(i<1)
                {
                    objPublicacion.FechaPublicacion = item.FechaPublicacion;
                    objPublicacion.IdPublicacion = item.IdPublicacion;
                    objPublicacion.TipoPublicacion = item.TipoPublicacion;
                    
                }
                i++;
            }


            return objPublicacion;
        }

        public void InsertPublicacion(string TipoPublicacion)
        {
            db.SP_ProcesodePublicacion(TipoPublicacion);

        }

        public List<SP_Select_Publicacion_Result> GetPublicacionXStatus(string Parameter)
        {
            List<SP_Select_Publicacion_Result> result = db.SP_Select_Publicacion(Parameter).ToList();
            return result;

        }

        public SP_Select_Publicacion_Result GetPublicaciones2()
        {
            List<SP_Select_Publicacion_Result> result = db.SP_Select_Publicacion("Prueba").ToList();
            if (result.Count > 0)
                return result.First();
            return null;
        }

        public List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result> GetPublicacionesSubsistemaXIdPublicacion(int idPublicacion)
        {
            List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result> list = new List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result>();
            SP_Select_PublicacionValoresSemaforosXSubsistemas_Result ent;
            var result = db.SP_Select_PublicacionValoresSemaforosXSubsistemas(idPublicacion);
            foreach (var res in result)
            {
                ent = new SP_Select_PublicacionValoresSemaforosXSubsistemas_Result();
                ent.EstatusMejora = res.EstatusMejora;
                ent.IdPublicacion = res.IdPublicacion;
                ent.IdPublicacionAvanceSubsistema = res.IdPublicacionAvanceSubsistema;
                ent.IdSubsistema = res.IdSubsistema;
                ent.Semaforo = res.Semaforo;
                ent.Valor = res.Valor;
                list.Add(ent);
            }
            return list;
        }

        public string GetNombreSubsistema(int? idSubsistema)
        {
            string nombress = string.Empty;
            if (idSubsistema > 0)
            {
                nombress = db.tbl_Subsistemas.SingleOrDefault(x => x.IdSubsistema == idSubsistema).NombreSubsistema;
            }
            return nombress;
        }


        public List<EntProcesos> GetProcesosXIdSubsistema(int idPublicacion, int? idSubsistema)
        {
            List<EntProcesos> list = new List<EntProcesos>();
            EntProcesos ent;
            List<SP_Select_PublicacionElementoxSubsistema_Result> result = db.SP_Select_PublicacionElementoxSubsistema(idPublicacion, idSubsistema).ToList();
            foreach (var res in result)
            {
                ent = new EntProcesos();
                ent.proceso_id = (int)res.IdElemento;
                ent.proceso_nombre = res.DescripcionElemento;
                ent.proceso_sigla = res.NombreElemento;
                ent.proceso_avance = res.Valor != null ? (int)res.Valor : 0;
                ent.proceso_mejora = res.EstatusMejora == null ? 0 : (int)res.EstatusMejora;
                ent.proceso_color = res.Semaforo.ToLower();
                list.Add(ent);
            }

            return list;
        }

        public List<SP_Select_PublicacionSubsistemasxMesTabular_Result> GetPublicacionesXMes(int? idPub)
        {
            List<SP_Select_PublicacionSubsistemasxMesTabular_Result> lstResult = new List<SP_Select_PublicacionSubsistemasxMesTabular_Result>();
            SP_Select_PublicacionSubsistemasxMesTabular_Result ent;
            var list = db.SP_Select_PublicacionSubsistemasxMesTabular(idPub);
            foreach (var li in list)
            {
                ent = new SP_Select_PublicacionSubsistemasxMesTabular_Result();
                ent.IdPublicacion = li.IdPublicacion;
                ent.IdPublicacionTabular = li.IdPublicacionTabular;
                ent.IdSubsistema = li.IdSubsistema;
                ent.Mes = li.Mes;
                ent.Semaforo = li.Semaforo;
                ent.Valor = li.Valor;
                lstResult.Add(ent);
            }
            return lstResult;
        }

        public int GetProyectosEnEjecucion(string parameter)
        {
            var list = db.SP_SelectAllProyectosByEstatus(parameter);
            return list.Count();
        }
    }
}
