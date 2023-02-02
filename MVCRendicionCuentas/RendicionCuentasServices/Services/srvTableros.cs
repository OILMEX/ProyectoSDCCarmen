using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvTableros
    {
        #region Variables Globales

        dbSDCEntities db = new dbSDCEntities();

        #endregion

        #region Métodos para Tablero Global

        public List<SP_Select_Publicacion_Result> GetPublicaciones(string Parameter)
        {
            List<SP_Select_Publicacion_Result> result = db.SP_Select_Publicacion(Parameter).ToList();
            return result;
            
        }

        public List<SP_Select_Publicacion_Result> GetPublicaciones()
        {
            List<SP_Select_Publicacion_Result> result = db.SP_Select_Publicacion("Produccion").ToList();
            return result;
        }

        public SP_Select_Publicacion_Result GetPublicacionesDet()
        {
            List<SP_Select_Publicacion_Result> result = db.SP_Select_Publicacion("Produccion").ToList();
            if (result.Count > 0)
                return result.First();
            return null;
        }

        public Dashboard_Publicaciones GetPublicacionesDet(int idpublicacion)
        {

            Dashboard_Publicaciones result = db.Dashboard_Publicaciones.FirstOrDefault(x => x.IdPublicacion == idpublicacion);
            
                
            return result;
        }

        public List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result> GetPublicacionesSubsistemaXIdPublicacion(int idPublicacion)
        {
            List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result> list = new List<SP_Select_PublicacionValoresSemaforosXSubsistemas_Result>();
            SP_Select_PublicacionValoresSemaforosXSubsistemas_Result ent;
            var result = db.SP_Select_PublicacionValoresSemaforosXSubsistemas(idPublicacion);
            foreach(var res in result)
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
        
        public List<EntProcesos> GetProcesosXIdSubsistema(int idPublicacion, int? idSubsistema)
        {
            List<EntProcesos> list = new List<EntProcesos>();
            EntProcesos ent;
            List<SP_Select_PublicacionElementoxSubsistema_Result> result = db.SP_Select_PublicacionElementoxSubsistema(idPublicacion, idSubsistema).ToList();
            foreach(SP_Select_PublicacionElementoxSubsistema_Result res in result)
            {
                ent = new EntProcesos();
                ent.proceso_id = (int)res.IdElemento;
                ent.proceso_nombre = res.DescripcionElemento;
                ent.proceso_sigla = res.NombreElemento;
                ent.proceso_avance = res.Valor != null ? (int)res.Valor : 0;
                ent.proceso_mejora = res.EstatusMejora == null ? 0 : (int)res.EstatusMejora ;
                ent.proceso_color = res.Semaforo.ToLower();
                list.Add(ent);
            }

            return list;
        }

        public List<SP_Select_PublicacionSubsistemasxMesTabular_Result> GetPublicacionesXMes(int? idPub)
        {
            List<SP_Select_PublicacionSubsistemasxMesTabular_Result> lstResult = new List<SP_Select_PublicacionSubsistemasxMesTabular_Result>();
            SP_Select_PublicacionSubsistemasxMesTabular_Result ent;
            var list =  db.SP_Select_PublicacionSubsistemasxMesTabular(idPub);
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

        #endregion

        #region Métodos Por Proyecto

        public List<EntProyecto> ProyectosPorEstatus(string estatus)
        {
            List<EntProyecto> lstProyectos = new List<EntProyecto>();
            try
            {
                var splstElementos = db.SP_SelectAllProyectosByEstatus(estatus).ToList();
                foreach (var elem in splstElementos)
                {
                    //tbl_Areas area = db.tbl_Areas.FirstOrDefault(x=> x.idArea == elem.NombreArea);
                    EntProyecto proyecto = new EntProyecto();
                    proyecto.proyecto_id = elem.IdProyecto;
                    proyecto.proyecto_indicador_global = (elem.Semaforo != null) ? elem.Semaforo : "Gris";
                    proyecto.proyecto_nombre = elem.NombreProyecto;
                    proyecto.proyecto_responsable = elem.NombreArea;
                    proyecto.proyecto_fecha_ini = (elem.FechaInicio != null) ? elem.FechaInicio : null;
                    proyecto.proyecto_fecha_fin = (elem.FechaFin != null) ? elem.FechaFin : null;
                    proyecto.porcentaje_12MPI = (elem.Porcentaje12MPI != null) ? elem.Porcentaje12MPI : 0;
                    proyecto.porcentaje_SAA = (elem.PorcentajeSAA != null) ? elem.PorcentajeSAA : 0;
                    proyecto.porcentaje_SASP = (elem.PorcentajeSASP != null) ? elem.PorcentajeSASP : 0;
                    proyecto.porcentaje_SAST = (elem.PorcentajeSAST != null) ? elem.PorcentajeSAST : 0;
                    proyecto.semaforo_12MPI = (elem.Porcentaje12MPI != null) ? elem.Semaforo12MPI : "Gris";
                    proyecto.semaforo_SAA = (elem.PorcentajeSAA != null) ? elem.SemaforoSAA : "Gris";
                    proyecto.semaforo_SASP = (elem.PorcentajeSASP != null) ? elem.SemaforoSASP : "Gris";
                    proyecto.semaforo_SAST = (elem.PorcentajeSAST != null) ? elem.SemaforoSAST : "Gris";
                    lstProyectos.Add(proyecto);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return lstProyectos;
        }



        #endregion

        #region Métodos para sección "Por Proyecto"

        public List<EntProyecto> GetAllPublicacionProyectos(int? idPublicacion)
        {
            List<EntProyecto> lstResult = new List<EntProyecto>();
            EntProyecto proyecto;
            var result = db.SP_SelectAllPublicacionProyectos(idPublicacion);

            foreach(var SP in result)
            {
                proyecto = new EntProyecto();

                proyecto.proyecto_id = SP.IdPublicacionProyecto;
                //proyecto.proyecto_clave = SP.Clave;
                proyecto.proyecto_idpublicacion = SP.IdPublicacion;
                proyecto.proyecto_indicador_global = (SP.Semaforo != null) ? SP.Semaforo : "Gris";
                proyecto.proyecto_nombre = SP.NombreProyecto;
                proyecto.proyecto_responsable = SP.Responsable;
                proyecto.proyecto_fecha_ini = (SP.FechaInicio != null) ? SP.FechaInicio : null;
                proyecto.proyecto_fecha_fin = (SP.FechaFin != null) ? SP.FechaFin : null;
                proyecto.porcentaje_12MPI = (SP.Valor12MPI != null) ? SP.Valor12MPI : 0;
                proyecto.porcentaje_SAA = (SP.ValorSAA != null) ? SP.ValorSAA : 0;
                proyecto.porcentaje_SASP = (SP.ValorSASP != null) ? SP.ValorSASP : 0;
                proyecto.porcentaje_SAST = (SP.ValorSAST != null) ? SP.ValorSAST : 0;
                proyecto.semaforo_12MPI = (SP.Valor12MPI != null) ? SP.Semaforo12MPI : "Gris";
                proyecto.semaforo_SAA = (SP.ValorSAA != null) ? SP.SemaforoSAA : "Gris";
                proyecto.semaforo_SASP = (SP.ValorSASP != null) ? SP.SemaforoSASP : "Gris";
                proyecto.semaforo_SAST = (SP.ValorSAST != null) ? SP.SemaforoSAST : "Gris";
                proyecto.proyecto_idTipoProyecto = SP.IdTipoProyecto != null ? int.Parse(SP.IdTipoProyecto) : 0;
                proyecto.proyecto_trace = SP.ElementoOrganigrama;

                lstResult.Add(proyecto);
            }

            return lstResult;
        }

        public List<EntProyecto> GetOnePublicacionProyectos(int idpublicacion)
        {
            List<EntProyecto> lstResult = new List<EntProyecto>();
            EntProyecto proyecto;
            var result = db.SP_SelectOne_PublicacionProyectoSeleccionado(idpublicacion);

            try
            {

                foreach (var SP in result)
                {
                    proyecto = new EntProyecto();

                    proyecto.proyecto_id = SP.IdPublicacionProyecto;
                    //proyecto.proyecto_clave = SP.Clave;
                    proyecto.proyecto_idpublicacion = SP.IdPublicacion;
                    proyecto.proyecto_indicador_global = (SP.Semaforo != null) ? SP.Semaforo : "Gris";
                    proyecto.proyecto_nombre = SP.NombreProyecto;
                    proyecto.proyecto_responsable = SP.Responsable;
                    proyecto.proyecto_fecha_ini = (SP.FechaInicio != null) ? SP.FechaInicio : null;
                    proyecto.proyecto_fecha_fin = (SP.FechaFin != null) ? SP.FechaFin : null;
                    proyecto.porcentaje_12MPI = (SP.Valor12MPI != null) ? SP.Valor12MPI : 0;
                    proyecto.porcentaje_SAA = (SP.ValorSAA != null) ? SP.ValorSAA : 0;
                    proyecto.porcentaje_SASP = (SP.ValorSASP != null) ? SP.ValorSASP : 0;
                    proyecto.porcentaje_SAST = (SP.ValorSAST != null) ? SP.ValorSAST : 0;
                    proyecto.semaforo_12MPI = (SP.Valor12MPI != null) ? SP.Semaforo12MPI : "Gris";
                    proyecto.semaforo_SAA = (SP.ValorSAA != null) ? SP.SemaforoSAA : "Gris";
                    proyecto.semaforo_SASP = (SP.ValorSASP != null) ? SP.SemaforoSASP : "Gris";
                    proyecto.semaforo_SAST = (SP.ValorSAST != null) ? SP.SemaforoSAST : "Gris";
                    proyecto.indicadorescargados = SP.IndicadoresCargados.GetValueOrDefault();
                    proyecto.indicadoresnocargados = SP.IndicadoresNoCargados.GetValueOrDefault();

                    lstResult.Add(proyecto);
                }
            } catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return lstResult;
        }

        public List<EntProyecto> GetEtapasxProyecto(int idproyecto)
        {
            List<EntProyecto> lstResult = new List<EntProyecto>();
            EntProyecto proyecto;
            var result = db.SP_SelectAll_PublicacionProyectoEtapas(idproyecto);

            try
            {

                foreach (var SP in result)
                {
                    proyecto = new EntProyecto();

                    proyecto.proyecto_id = SP.IdPublicacionProyectoEtapa;
                    proyecto.proyecto_idpublicacion = SP.IdPublicacion;
                    proyecto.proyecto_clave = SP.Clave != null ? SP.Clave.ToString() : "";
                    proyecto.proyecto_indicador_global = "Gris";
                    proyecto.proyecto_nombre = SP.NombreEtapa;
                    proyecto.proyecto_responsable = null;
                    proyecto.proyecto_fecha_ini = null;
                    proyecto.proyecto_fecha_fin = (SP.FechaFin != null) ? SP.FechaFin : null;
                    proyecto.porcentaje_12MPI = (SP.Valor12MPI != null) ? SP.Valor12MPI : 0;
                    proyecto.porcentaje_SAA = (SP.ValorSAA != null) ? SP.ValorSAA : 0;
                    proyecto.porcentaje_SASP = (SP.ValorSASP != null) ? SP.ValorSASP : 0;
                    proyecto.porcentaje_SAST = (SP.ValorSAST != null) ? SP.ValorSAST : 0;
                    proyecto.semaforo_12MPI = (SP.Valor12MPI != null) ? SP.Semaforo12MPI : "Gris";
                    proyecto.semaforo_SAA = (SP.ValorSAA != null) ? SP.SemaforoSAA : "Gris";
                    proyecto.semaforo_SASP = (SP.ValorSASP != null) ? SP.SemaforoSASP : "Gris";
                    proyecto.semaforo_SAST = (SP.ValorSAST != null) ? SP.SemaforoSAST : "Gris";
                    proyecto.EstatusTiempo = SP.EstatusTiempo;
                    proyecto.indicadorescargados = SP.IndicadoresCargados.GetValueOrDefault();
                    proyecto.indicadoresnocargados = SP.IndicadoresNoCargados.GetValueOrDefault();

                    lstResult.Add(proyecto);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return lstResult;
        }

        public List<EntProyecto> GetOneEtapaPublicacionProyectos(int idpublicacion)
        {
            List<EntProyecto> lstResult = new List<EntProyecto>();
            EntProyecto proyecto;
            var result = db.SP_SelectOne_PublicacionProyectoEtapaSeleccionada(idpublicacion);

            try
            {

                foreach (var SP in result)
                {
                    proyecto = new EntProyecto();

                    proyecto.proyecto_id = SP.IdPublicacionProyectoEtapa;
                    proyecto.proyecto_idpublicacion = SP.IdPublicacion;
                    proyecto.proyecto_indicador_global = "Gris";
                    proyecto.proyecto_nombre = SP.NombreEtapa;
                    proyecto.proyecto_responsable = null;
                    proyecto.proyecto_fecha_ini = null;
                    proyecto.proyecto_fecha_fin = (SP.FechaFin != null) ? SP.FechaFin : null;
                    proyecto.porcentaje_12MPI = (SP.Valor12MPI != null) ? SP.Valor12MPI : 0;
                    proyecto.porcentaje_SAA = (SP.ValorSAA != null) ? SP.ValorSAA : 0;
                    proyecto.porcentaje_SASP = (SP.ValorSASP != null) ? SP.ValorSASP : 0;
                    proyecto.porcentaje_SAST = (SP.ValorSAST != null) ? SP.ValorSAST : 0;
                    proyecto.semaforo_12MPI = (SP.Valor12MPI != null) ? SP.Semaforo12MPI : "Gris";
                    proyecto.semaforo_SAA = (SP.ValorSAA != null) ? SP.SemaforoSAA : "Gris";
                    proyecto.semaforo_SASP = (SP.ValorSASP != null) ? SP.SemaforoSASP : "Gris";
                    proyecto.semaforo_SAST = (SP.ValorSAST != null) ? SP.SemaforoSAST : "Gris";
                    proyecto.indicadorescargados = SP.IndicadoresCargados.GetValueOrDefault();
                    proyecto.indicadoresnocargados = SP.IndicadoresNoCargados.GetValueOrDefault();

                    lstResult.Add(proyecto);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return lstResult;
        }

        public List<EntProyecto> GetSubEtapasxProyecto(int idproyecto)
        {
            List<EntProyecto> lstResult = new List<EntProyecto>();
            EntProyecto proyecto;
            var result = db.SP_SelectAll_PublicacionProyectoSubEtapas(idproyecto);

            try
            {

                foreach (var SP in result)
                {
                    proyecto = new EntProyecto();

                    proyecto.proyecto_id = (int)SP.IdPublicacionProyectoEtapa;
                    proyecto.proyecto_idpublicacion = SP.IdPublicacion;
                    proyecto.proyecto_clave = SP.Clave != null ? SP.Clave.ToString().Replace(',','.') : "";
                    proyecto.proyecto_indicador_global = "Gris";
                    proyecto.proyecto_nombre = SP.NombreSubEtapa;
                    proyecto.proyecto_responsable = null;
                    proyecto.proyecto_fecha_ini = null;
                    proyecto.proyecto_fecha_fin = (SP.FechaFinSubEtapa != null) ? SP.FechaFinSubEtapa : null;
                    proyecto.porcentaje_12MPI = (SP.Valor12MPI != null) ? SP.Valor12MPI : 0;
                    proyecto.porcentaje_SAA = (SP.ValorSAA != null) ? SP.ValorSAA : 0;
                    proyecto.porcentaje_SASP = (SP.ValorSASP != null) ? SP.ValorSASP : 0;
                    proyecto.porcentaje_SAST = (SP.ValorSAST != null) ? SP.ValorSAST : 0;
                    proyecto.semaforo_12MPI = (SP.Valor12MPI != null) ? SP.Semaforo12MPI : "Gris";
                    proyecto.semaforo_SAA = (SP.ValorSAA != null) ? SP.SemaforoSAA : "Gris";
                    proyecto.semaforo_SASP = (SP.ValorSASP != null) ? SP.SemaforoSASP : "Gris";
                    proyecto.semaforo_SAST = (SP.ValorSAST != null) ? SP.SemaforoSAST : "Gris";
                    proyecto.EstatusTiempo = SP.EstatusTiempo;
                    proyecto.indicadorescargados = SP.IndicadoresCargados.GetValueOrDefault();
                    proyecto.indicadoresnocargados = SP.IndicadoresNoCargados.GetValueOrDefault();

                    lstResult.Add(proyecto);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return lstResult;
        }

        public List<SP_SelectAll_TipoProyectos_Result> getAllTiposProyecto()
        {
            List<SP_SelectAll_TipoProyectos_Result> listresult = new List<SP_SelectAll_TipoProyectos_Result>();
            try
            {
                listresult = db.SP_SelectAll_TipoProyectos().ToList();
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return listresult;
        }

        #endregion

        #region Métodos Generales

        public string GetNombreSubsistema(int? idSubsistema)
        {
            string nombress = string.Empty;
            if (idSubsistema > 0)
            {
                nombress = db.tbl_Subsistemas.SingleOrDefault(x => x.IdSubsistema == idSubsistema).NombreSubsistema;
            }
            return nombress;
        }

        public int GetProyectosEnEjecucion(string parameter)
        {
            var list = db.SP_SelectAllProyectosByEstatus(parameter);
            return list.Count();
        }

        #endregion

    }
}
