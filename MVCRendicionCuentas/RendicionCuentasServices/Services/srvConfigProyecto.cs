using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace RendicionCuentasServices.Services
{
    public class srvConfigProyecto
    {
        dbSDCEntities db = new dbSDCEntities();

        public List<EntUserResponsable> GetListUsers(int idIndicador, int idConfigProyecto, int IdUbicacion)
        {
            List<EntUserResponsable> ListaReturn = new List<EntUserResponsable>();

            EntUserResponsable elem = new EntUserResponsable();
            elem.Listaid = 1;
            elem.ListaNomnbre = "Usuarios";
            elem.User = getUsers(true, idIndicador, idConfigProyecto).Where(x=>x.idUbicacion==IdUbicacion).OrderBy(x=>x.Nombre).ToList();
            ListaReturn.Add(elem);

            elem = new EntUserResponsable();
            elem.Listaid = 2;
            elem.ListaNomnbre = "Responsables";
            elem.User = getUsers(false, idIndicador, idConfigProyecto).Where(x => x.idUbicacion == IdUbicacion).OrderBy(x=>x.Nombre).ToList();
            ListaReturn.Add(elem);


            return ListaReturn;
        }

        private List<EntUsuario> getUsers(bool isListUser, int Id, int idConfigProyecto)
        {
            List<EntUsuario> Usuarios = new List<EntUsuario>();
            List<Config_IndicadorResponsable> Responsables = db.Config_IndicadorResponsable.Where(x => x.IdIndicador == Id && x.IdConfigRelacionIndicadorProyecto == idConfigProyecto && x.Estatus == true).ToList();

            if (isListUser)
            {
                List<tbl_Responsables> users = db.tbl_Responsables.Where(x => x.Estatus == true).ToList();
                foreach (tbl_Responsables us in users)
                {
                    var intoResp = Responsables.Where(x => x.IdResponsable == us.idResponsable);
                    if (intoResp.Count() == 0)
                    {
                        EntUsuario user = new EntUsuario();
                        user.IdUsuario = us.idResponsable;
                        user.idUbicacion = us.IdUbicacion;
                        user.Nombre = us.NombreResponsable;
                        Usuarios.Add(user);
                    }
                }
            }
            else
            {
                List<tbl_Responsables> users = db.tbl_Responsables.Where(x => x.Estatus == true && x.Estatus != null).ToList();
                foreach (tbl_Responsables us in users)
                {
                    var intoResp = Responsables.Where(x => x.IdResponsable == us.idResponsable);
                    if (intoResp.Count() > 0)
                    {
                        EntUsuario user = new EntUsuario();
                        user.IdUsuario = us.idResponsable;
                        user.idUbicacion = us.IdUbicacion;
                        user.Nombre = us.NombreResponsable;
                        Usuarios.Add(user);
                    }

                }
            }

            return Usuarios;

        }

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
                    proyecto.proyecto_idresponsable = elem.IdResponsable;
                    proyecto.proyecto_idubicacion = elem.IdUbicacion;
                    proyecto.proyecto_fecha_ini = (elem.FechaInicio != null) ? elem.FechaInicio : null;
                    proyecto.proyecto_fecha_fin = (elem.FechaFin != null) ? elem.FechaFin : null;
                    proyecto.porcentaje_12MPI = (elem.Porcentaje12MPI != null) ? elem.Porcentaje12MPI : 0;
                    proyecto.porcentaje_SAA = (elem.PorcentajeSAA != null) ? elem.PorcentajeSAA : 0;
                    proyecto.porcentaje_SASP = (elem.PorcentajeSASP != null) ? elem.PorcentajeSASP : 0;
                    proyecto.porcentaje_SAST = (elem.PorcentajeSAST != null) ? elem.PorcentajeSAST : 0;
                    proyecto.semaforo_12MPI = (elem.Semaforo12MPI != null) ? elem.Semaforo12MPI : "Gris";
                    proyecto.semaforo_SAA = (elem.SemaforoSAA != null) ? elem.SemaforoSAA : "Gris";
                    proyecto.semaforo_SASP = (elem.SemaforoSASP != null) ? elem.SemaforoSASP : "Gris";
                    proyecto.semaforo_SAST = (elem.SemaforoSAST != null) ? elem.SemaforoSAST : "Gris";
                    lstProyectos.Add(proyecto);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return lstProyectos;
        }

        public List<EntProyecto> ProyectosPorEstatusAndRol(string estatus, int IdResponsable)
        {
            List<EntProyecto> lstProyectos = new List<EntProyecto>();
            try
            {
                var splstElementos = db.SP_SelectAllProyectosByEstatusAndRol(estatus, IdResponsable).ToList();
                foreach (var elem in splstElementos)
                {
                    //tbl_Areas area = db.tbl_Areas.FirstOrDefault(x=> x.idArea == elem.NombreArea);
                    EntProyecto proyecto = new EntProyecto();
                    proyecto.proyecto_id = elem.IdProyecto;
                    proyecto.proyecto_indicador_global = (elem.Semaforo != null) ? elem.Semaforo : "Gris";
                    proyecto.proyecto_nombre = elem.NombreProyecto;
                    proyecto.proyecto_responsable = elem.NombreArea;
                    proyecto.proyecto_idresponsable = elem.IdResponsable;
                    proyecto.proyecto_fecha_ini = (elem.FechaInicio != null) ? elem.FechaInicio : null;
                    proyecto.proyecto_fecha_fin = (elem.FechaFin != null) ? elem.FechaFin : null;
                    proyecto.porcentaje_12MPI = (elem.Porcentaje12MPI != null) ? elem.Porcentaje12MPI : 0;
                    proyecto.porcentaje_SAA = (elem.PorcentajeSAA != null) ? elem.PorcentajeSAA : 0;
                    proyecto.porcentaje_SASP = (elem.PorcentajeSASP != null) ? elem.PorcentajeSASP : 0;
                    proyecto.porcentaje_SAST = (elem.PorcentajeSAST != null) ? elem.PorcentajeSAST : 0;
                    proyecto.semaforo_12MPI = (elem.Semaforo12MPI != null) ? elem.Semaforo12MPI : "Gris";
                    proyecto.semaforo_SAA = (elem.SemaforoSAA != null) ? elem.SemaforoSAA : "Gris";
                    proyecto.semaforo_SASP = (elem.SemaforoSASP != null) ? elem.SemaforoSASP : "Gris";
                    proyecto.semaforo_SAST = (elem.SemaforoSAST != null) ? elem.SemaforoSAST : "Gris";
                    lstProyectos.Add(proyecto);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return lstProyectos;
        }

        public EntProyecto GetDataProyectobyId(int idproyecto)
        {
            HandlerDataProyecto.listaResponsables = db.SP_SelectAllResponsablesIndicadorXProyecto(idproyecto).ToList();
            HandlerDataProyecto.listaFechasSubEtapasxProyecto = db.SP_SelectAll_FechasSubEtapasxProyecto(idproyecto).ToList();
            HandlerDataProyecto.listaConfiguracionIndicadoresSubEtapasProyecto = db.SP_SelectAll_ConfiguracionIndicadoresSubEtapasxProyecto(idproyecto).ToList();
            tbl_Proyectos proyecto = db.tbl_Proyectos.FirstOrDefault(x => x.IdProyecto == idproyecto);
            EntProyecto entProyecto = new EntProyecto();

            if (proyecto != null)
            {
                entProyecto.proyecto_id = proyecto.IdProyecto;
                entProyecto.proyecto_nombre = proyecto.NombreProyecto;
                entProyecto.proyecto_idarea = proyecto.IdArea;
                entProyecto.proyecto_idresponsable = proyecto.IdResponsable;
                entProyecto.proyecto_fecha_ini = (proyecto.FechaInicio != null) ? proyecto.FechaInicio : null;
                entProyecto.proyecto_fecha_fin = (proyecto.FechaFin != null) ? proyecto.FechaFin : null;
                entProyecto.proyecto_idubicacion = proyecto.IdUbicacion;

                if (proyecto.IdTipoElementoOrganigrama == 3)
                {
                    entProyecto.proyecto_idCoordinacion = proyecto.IdElementoOrganigrama;
                    entProyecto.proyecto_idSuperintendencia = 0;
                }
                else
                {
                    entProyecto.proyecto_idCoordinacion = proyecto.IdElementoOrganigrama;//getNameCoordinacionByIdSuperInt(proyecto.IdElementoOrganigrama);
                    entProyecto.proyecto_idSuperintendencia = proyecto.IdTipoElementoOrganigrama;
                }

                entProyecto.proyecto_idTipoProyecto = proyecto.IdTipoProyecto;

            }

            return entProyecto;
        }

        private int? getNameCoordinacionByIdSuperInt(int? idSuperIntendencia)
        {
            return db.tbl_Superintendencias.FirstOrDefault(x => x.IdSuperintendencia == idSuperIntendencia).IdCoordinacion;
        }

        public List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> getDataGraficas(int idResponsable)
        {
            return db.SP_SelectAll_ValoresSemaforoSubsistemasEnProceso(idResponsable).ToList();
        }

        public List<EntEtapa> GetFechasEtapasProy(int idproyecto)
        {
            var splstElementos = HandlerDataProyecto.listaFechasSubEtapasxProyecto;
            List<EntEtapa> lstEtapas = new List<EntEtapa>();
            //List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            EntEtapa etapa = new EntEtapa();
            int? idetapaactual = 0;

            foreach (var elem in splstElementos)
            {
                if (idetapaactual != elem.IdEtapa)
                {
                    etapa = new EntEtapa();

                    idetapaactual = elem.IdEtapa;
                    etapa.etapa_id = elem.IdEtapa;
                    etapa.etapa_nombre = elem.NombreEtapa;
                    etapa.clave = elem.ClaveEtapa;
                    etapa.estatus = elem.EstatusEtapa;
                    etapa.SubEtapas = GetSubEtapas(idetapaactual, idproyecto);
                    etapa.FechaFin = (elem.FechaFinEtapa != null) ? elem.FechaFinEtapa : null;
                    etapa.FechaIni = (elem.FechaInicioEtapa != null) ? elem.FechaInicioEtapa : null;
                    lstEtapas.Add(etapa);

                    //var splstindicadores = db.SP_SelectAll_ConfiguracionIndicadoresSubEtapasxProyecto(idproyecto);

                    //lstsubetapa.Add(subetapa);
                }

                //EntProyecto etapa = new EntProyecto();
            }
            //etapa.SubEtapas = lstsubetapa;


            return lstEtapas;
        }

        private List<EntSubEtapa> GetSubEtapas(int? idetapa, int idproyecto)
        {
            var splstElementos = HandlerDataProyecto.listaFechasSubEtapasxProyecto;
            List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            foreach (var elem in splstElementos)
            {
                if (elem.IdEtapa == idetapa)
                {
                    EntSubEtapa subetapa = new EntSubEtapa();
                    subetapa.subetapa_id = elem.IdSubEtapa;
                    subetapa.clave = elem.ClaveSubEtapa;
                    subetapa.subetapa_nombre = elem.NombreSubEtapa;
                    subetapa.estatus = elem.Estatus;
                    subetapa.FechaIni = (elem.FechaInicio != null) ? elem.FechaInicio : null;
                    subetapa.FechaFin = (elem.FechaFin != null) ? elem.FechaFin : null;
                    subetapa.Subsistemas = GetSubsistemas(elem.IdEtapa, elem.IdSubEtapa, idproyecto);

                    //Obtener subsistemas

                    lstsubetapa.Add(subetapa);
                }
            }

            List<EntSubEtapa> listfiltrada = lstsubetapa.Distinct().ToList();

            return listfiltrada;
        }

        private List<EntSubsistema> GetSubsistemas(int? idetapa, int? idsubetapa, int idproyecto)
        {
            List<EntSubsistema> lstsubsistemas = new List<EntSubsistema>();
            var splstindicadores = HandlerDataProyecto.listaConfiguracionIndicadoresSubEtapasProyecto;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa)
                {
                    EntSubsistema subsistema = new EntSubsistema();
                    subsistema.id_subsistema = elem.IdSubsistema;
                    subsistema.subsistema = elem.NombreSubsistema;
                    subsistema.siglas = elem.NombreSubsistema;
                    //Obtenemos los elementos
                    subsistema.Procesos = GetElemento(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, idproyecto);
                    lstsubsistemas.Add(subsistema);
                }
            }

            List<EntSubsistema> listfiltrada = lstsubsistemas.GroupBy(d => new { d.id_subsistema, d.subsistema })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;

            //return lstsubsistemas;
        }

        private List<EntProcesos> GetElemento(int? idetapa, int? idsubetapa, int? idsubsistema, int idproyecto)
        {
            List<EntProcesos> lstprocesos = new List<EntProcesos>();
            var splstindicadores = HandlerDataProyecto.listaConfiguracionIndicadoresSubEtapasProyecto;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdSubsistema == idsubsistema)
                {
                    EntProcesos subsistema = new EntProcesos();
                    subsistema.proceso_id = elem.IdElemento;
                    subsistema.proceso_sigla = elem.NombreElemento;
                    subsistema.proceso_nombre = elem.DescripcionElemento;
                    subsistema.Indicadores = GetIndicadores(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, elem.IdElemento, idproyecto);

                    //Obtenemos los indicadores
                    lstprocesos.Add(subsistema);
                }
            }

            List<EntProcesos> listfiltrada = lstprocesos.GroupBy(d => new { d.proceso_id, d.proceso_nombre, d.proceso_sigla })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;

            //return lstprocesos;
        }

        private List<EntIndicador> GetIndicadores(int? idetapa, int? idsubetapa, int? idsubsistema, int? idelemento, int idproyecto)
        {
            List<EntIndicador> lstIndicadores = new List<EntIndicador>();
            var splstindicadores = HandlerDataProyecto.listaConfiguracionIndicadoresSubEtapasProyecto;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdSubsistema == idsubsistema && elem.IdElemento == idelemento)
                {
                    EntIndicador indicador = new EntIndicador();
                    indicador.indicador_id = elem.IdIndicador;
                    indicador.IdConfigIndicadorResponsable = elem.IdConfigRelacionIndicadorProyecto;
                    indicador.indicador_nombre = elem.DescripcionCorta;
                    indicador.indicador_estatus = elem.Estatus.ToString();
                    indicador.indicador_tipo = elem.TipoIndicador.ToLower();
                    indicador.indicador_meta = elem.Meta.ToString();
                    indicador.indicador_acttualizacion = elem.TipoFrecuencia.ToString();
                    indicador.indicador_clave = elem.Prefijo;
                    indicador.indicador_creacionproy = elem.CheckCreacionDesdeProyecto;
                    indicador.indicador_fechacompromiso = elem.FechaCompromisoParaEmpezarAMostrarse;
                    //Obtenemos los responsables
                    indicador.Responsables = GetResponsables(elem.IdEtapa, elem.IdSubEtapa, elem.IdIndicador, idproyecto);
                    lstIndicadores.Add(indicador);
                }
            }

            return lstIndicadores;
        }

        private List<EntResponsablesIndicador> GetResponsables(int? idetapa, int? idsubetapa, int? idindicador, int idproyecto)
        {
            List<EntResponsablesIndicador> lstResponsables = new List<EntResponsablesIndicador>();
            var splsresponsables = HandlerDataProyecto.listaResponsables;

            foreach (var elem in splsresponsables)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdIndicador == idindicador)
                {
                    EntResponsablesIndicador responsable = new EntResponsablesIndicador();
                    responsable.id_responsable = elem.IdResponsable;
                    responsable.IdEtapa = elem.IdEtapa;
                    responsable.IdSubetapa = elem.IdSubEtapa;
                    responsable.IdIndicador = elem.IdIndicador;
                    responsable.ficha = elem.Ficha;
                    responsable.nombre = elem.NombreResponsable;
                    responsable.correo = elem.Correo;
                    lstResponsables.Add(responsable);
                }
            }

            return lstResponsables;
        }

        public tbl_Indicadores GetIndicador(int idIndicador)
        {
            tbl_Indicadores indicador = db.tbl_Indicadores.FirstOrDefault(x => x.IdIndicador == idIndicador);

            if (indicador != null)
            {
                return indicador;
            }
            else
            {
                return new tbl_Indicadores();
            }

        }

        public List<tbl_HistorialCambioFechas> getHistorialCambiosFechas(int idProyecto, int idEtapa, int idSubetapa)
        {
            List<tbl_HistorialCambioFechas> list = new List<tbl_HistorialCambioFechas>();
            var elems = db.tbl_HistorialCambioFechas.Where(x=> x.idProyecto == idProyecto && x.idEtapa == idEtapa && x.idSubetapa==idSubetapa)
                .OrderBy(o => o.FechaCreacion).ToList();
            if (elems != null)
            {
                foreach (tbl_HistorialCambioFechas e in elems)
                {
                    tbl_HistorialCambioFechas d = new tbl_HistorialCambioFechas();
                    d.idCambioFecha = e.idCambioFecha;
                    d.idEtapa = e.idEtapa;
                    d.idProyecto = e.idProyecto;
                    d.IdResponsable = e.IdResponsable;
                    d.idSubetapa = e.idSubetapa;
                    d.Motivo = e.Motivo;
                    d.FechaInicioAnt = e.FechaInicioAnt;
                    d.FechaFinAnt = e.FechaFinAnt;
                    d.FechaInicio = e.FechaInicio;
                    d.FechaFin = e.FechaFin;
                    d.FechaCreacion = e.FechaCreacion;
                    list.Add(d);
                }
            }
            return list;
        }

        public int AddProyecto(tbl_Proyectos item)
        {
            int IdNewProyecto = 0;

            //tbl_Organigrama newOrganigrama = new tbl_Organigrama();
            try
            {
                if (item != null)
                {
                    db.tbl_Proyectos.Add(item);
                    db.SaveChanges();

                    IdNewProyecto = item.IdProyecto;

                    if (IdNewProyecto > 0)
                    {
                        var splstElementos = db.SP_InsertConfiguracionProyecto(IdNewProyecto);
                    }
                }
            }
            catch (Exception e)
            {

            }

            return IdNewProyecto;
        }

        public int EditProyecto(tbl_Proyectos item)
        {
            int IdIndicador = 0;
            try
            {
                var e = db.tbl_Proyectos.Single(x => x.IdProyecto == item.IdProyecto);
                if (e != null)
                {
                    e.IdArea = item.IdArea;
                    e.IdResponsable = item.IdResponsable;
                    e.IdUbicacion = item.IdUbicacion;
                    e.NombreProyecto = item.NombreProyecto;
                    e.FechaInicio = item.FechaInicio;
                    e.FechaFin = item.FechaFin;
                    e.UsuarioActualizacion = item.UsuarioActualizacion;
                    e.FechaActualizacion = item.FechaActualizacion;
                    db.SaveChanges();
                    IdIndicador = item.IdProyecto;
                }
            }
            catch (Exception)
            {

            }

            return IdIndicador;
        }

        public int EditProyecto(tbl_Proyectos item, int Coordinaciones, int TiposProyecto, int Superintendencias)
        {
            int IdIndicador = 0;
            try
            {
                var e = db.tbl_Proyectos.Single(x => x.IdProyecto == item.IdProyecto);
                if (e != null)
                {
                    e.IdArea = item.IdArea;
                    e.IdResponsable = item.IdResponsable;
                    e.IdUbicacion = item.IdUbicacion;
                    e.NombreProyecto = item.NombreProyecto;
                    e.FechaInicio = item.FechaInicio;
                    e.FechaFin = item.FechaFin;
                    e.UsuarioActualizacion = item.UsuarioActualizacion;
                    e.FechaActualizacion = item.FechaActualizacion;

                    e.IdElementoOrganigrama = Coordinaciones > 0 ? (Superintendencias > 0 ? Superintendencias : Coordinaciones) : 0;
                    e.IdTipoElementoOrganigrama = Coordinaciones > 0 ? (Superintendencias > 0 ? 4 : 3) : 0;

                    e.IdTipoProyecto = TiposProyecto;

                    db.SaveChanges();
                    IdIndicador = item.IdProyecto;
                }
            }
            catch (Exception)
            {

            }

            return IdIndicador;
        }

        public void DeleteProyecto(tbl_Proyectos item)
        {
            var ent = db.tbl_Proyectos;
            try
            {
                if (ent != null)
                {
                    var e = ent.FirstOrDefault(X => X.IdProyecto == item.IdProyecto);
                    if (e != null)
                    {
                        e.UsuarioActualizacion = item.UsuarioActualizacion;
                        e.FechaActualizacion = item.FechaActualizacion;
                        e.Estatus = false;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public string SaveResponsables(int idIndicador, string[] User, string[] NoUser, int idConfig, int idSubetapa, int idProyecto)
        {
            string Msj = string.Empty;

            try
            {
                if (User != null)
                {
                    var ent = db.Config_IndicadorResponsable;
                    foreach (string us in User)
                    {
                        int idresp = Int32.Parse(us);
                        db.SP_InsertConfigResponIndSubEProyectos(idConfig, idIndicador, idresp, true);

                    }
                }

                if (NoUser != null)
                {
                    var ent = db.Config_IndicadorResponsable;
                    foreach (string us in NoUser)
                    {
                        int idresp = Int32.Parse(us);
                        db.SP_InsertConfigResponIndSubEProyectos(idConfig, idIndicador, idresp, false);


                    }
                }

                db.SP_RecalculoRevisionesEnProyectos(idProyecto, idSubetapa);

            }
            catch (SqlException ex)
            {
                Msj = ex.Message;
            }

            return Msj;
        }

        public void DeleteReponsableIndicador(Config_IndicadorResponsable item)
        {
            var ent = db.Config_IndicadorResponsable;
            try
            {
                if (ent != null)
                {
                    var e = ent.FirstOrDefault(
                        X => X.IdResponsable == item.IdResponsable
                            && X.IdIndicador == item.IdIndicador
                            && X.IdConfigRelacionIndicadorProyecto == item.IdConfigRelacionIndicadorProyecto);
                    if (e != null)
                    {
                        e.Estatus = false;
                        //.UsuarioActualizacion = item.UsuarioActualizacion;
                        //e.FechaActualizacion = item.FechaActualizacion;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public int AddIndicador(tbl_Indicadores item, int idproyecto, int idsubetapa)
        {
            int IdIndicador = 0;
            try
            {
                if (item != null)
                {
                    if (item.TipoIndicador == "Check" || item.TipoIndicador == "Checkbox")
                    {
                        item.TipoIndicador = "Check";
                        item.TipoCalculo = null;
                    }
                    db.tbl_Indicadores.Add(item);
                    db.SaveChanges();
                    IdIndicador = item.IdIndicador;
                    if (IdIndicador > 0)
                    {
                        if (item.TipoCalculo == "Programa")
                        {
                            db.SP_InsertProgramaAsociadoNuevoIndicador(IdIndicador, item.UsuarioCreacion);
                        }
                        db.SP_InsertIndicadorDesdeProyecto(idproyecto, idsubetapa, IdIndicador);
                        db.SaveChanges();
                    }
                }
            }
            catch (SqlException ex)
            {
                var Msj = ex.Message;
            }

            return IdIndicador;
        }

        public int AddHistorialCambioFecha(tbl_HistorialCambioFechas item)
        {
            int id = 0;

            try
            {
                if (item != null)
                {
                    db.tbl_HistorialCambioFechas.Add(item);
                    db.SaveChanges();
                    id = item.idCambioFecha;
                }
            }
            catch (SqlException ex)
            {
                var Msj = ex.Message;
                throw new Exception(ex.Message);
            }

            return id;
        }

        public int EditInidicador(tbl_Indicadores item, string tipoEdit)
        {
            int IdIndicador = 0;
            try
            {
                var e = db.tbl_Indicadores.Single(x => x.IdIndicador == item.IdIndicador);
                if (e != null)
                {
                    if (tipoEdit == "sencillo")
                    {
                        e.Clave = item.Clave;
                        e.Prefijo = item.Prefijo;
                        e.DescripcionCorta = item.DescripcionCorta;
                        e.DescripcionLarga = item.DescripcionLarga;
                        e.UsuarioActualizacion = item.UsuarioActualizacion;
                        e.FechaActualizacion = item.FechaActualizacion;
                        db.SaveChanges();
                        IdIndicador = item.IdIndicador;
                    }
                }
            }
            catch (Exception)
            {

            }

            return IdIndicador;
        }

        public int EditEstatusIndicador(int idconfig, bool estatus)
        {
            try
            {
                db.SP_UpdateRelacionIndicadorSubEtapaProyecto(idconfig, estatus);
            }
            catch (SqlException ex)
            {
                var Msj = ex.Message;
            }

            return 1;
        }

        public void EditFrecuenciaIndicador(int idConfigRelacionIndicadorProyecto, int frecuencia, int idproyecto, int idsubetapa)
        {
            try
            {
                db.SP_UpdateFrecuenciaActualizacionInCRIP(idConfigRelacionIndicadorProyecto, frecuencia);

                var spData = db.Config_IndicadorResponsable.Where(x => x.IdConfigRelacionIndicadorProyecto == idConfigRelacionIndicadorProyecto).ToList();
                foreach (var row in spData)
                {
                    db.SP_InsertRevisionesIndicadoresSubEtapas(row.IdConfigIndicadorResponsable, idsubetapa, idproyecto);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Config_FechasEtapasProyecto EditFechaSubEtapa(int idproyecto, int idsubetapa, int idetapa, DateTime fechaini, DateTime fechafin)
        {
            int Id = 0;
            Config_FechasEtapasProyecto etapa = new Config_FechasEtapasProyecto();
            try
            {
                var e = db.Config_FechasSubEtapasProyecto.FirstOrDefault(x => x.IdProyecto == idproyecto && x.IdSubEtapa == idsubetapa);
                if (e != null)
                {
                    e.FechaFin = fechafin;
                    e.FechaInicio = fechaini;
                    db.SaveChanges();
                    Id = e.IdConfigFechasSubEtapasProyecto;

                    //Obtenemos la fechafin maxima
                    DateTime? fecini = db.Config_FechasSubEtapasProyecto
                        .Where(x => x.IdProyecto == idproyecto)
                        .Where(x => x.FechaInicio != null)
                        .OrderBy(x => x.FechaInicio)
                        .Select(x => x.FechaInicio)
                        .FirstOrDefault();

                    //Obtenemos la fechaFin maxima
                    DateTime? fecfin = db.Config_FechasSubEtapasProyecto
                        .Where(x => x.IdProyecto == idproyecto)
                        //.Where(x => x.IdSubEtapa == idsubetapa)
                        .OrderByDescending(x => x.FechaFin)
                        .Select(x => x.FechaFin)
                        .FirstOrDefault();

                    etapa = EditFechaEtapa(idproyecto, idetapa, fecini, fecfin);

                    db.SP_RecalculoRevisionesEnProyectos(idproyecto, idsubetapa);
                    db.SP_UpdateEstatusTiempoSubEtapaProyecto(idproyecto);
                    
                }
            }
            catch (Exception)
            {

            }

            return etapa;
        }

        public Config_FechasEtapasProyecto EditFechaEtapa(int idproyecto, int idetapa, DateTime? fechaini, DateTime? fechafin)
        {
            Config_FechasEtapasProyecto et = new Config_FechasEtapasProyecto();
            try
            {
                var e = db.Config_FechasEtapasProyecto.FirstOrDefault(x => x.IdProyecto == idproyecto && x.IdEtapa == idetapa);
                if (e != null)
                {
                    e.FechaInicio = fechaini;
                    e.FechaFin = fechafin;
                    et.IdConfigFechasEtapasProyecto = e.IdConfigFechasEtapasProyecto;
                    et.IdEtapa = e.IdEtapa;
                    et.IdProyecto = e.IdProyecto;
                    et.FechaFin = e.FechaFin;
                    et.FechaInicio = e.FechaInicio;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            return et;
        }

        public bool EditEstatusEtapasSubetapas(int id, int idproyecto, bool estatus, bool isetapa)
        {
            bool realizado = true;
            try
            {
                if (isetapa)
                {
                    db.SP_UpdateEstatusEtapaProyectos(id, idproyecto, estatus);
                    db.SP_UpdateEstatusTiempoSubEtapaProyecto(idproyecto);
                   
                }
                else
                {
                    db.SP_UpdateEstatusSubetapaProyectos(id, idproyecto, estatus);
                    db.SP_UpdateEstatusTiempoSubEtapaProyecto(idproyecto);
                   
                }
            }
            catch (Exception)
            {
                realizado = false;
            }

            return realizado;
        }

        public List<SP_SelectAll_Ubicaciones_Result> getAllUbicaciones()
        {
            List<SP_SelectAll_Ubicaciones_Result> ListResult = new List<SP_SelectAll_Ubicaciones_Result>();
            try
            {
                ListResult = db.SP_SelectAll_Ubicaciones().ToList();
            }
            catch (Exception)
            {

            }
            return ListResult;
        }

        public List<SP_SelectAll_CoordinacionesParaProyecto_Result> getAllCoordinaciones()
        {
            List<SP_SelectAll_CoordinacionesParaProyecto_Result> listresult = new List<SP_SelectAll_CoordinacionesParaProyecto_Result>();
            try
            {
                listresult = db.SP_SelectAll_CoordinacionesParaProyecto().ToList();
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return listresult;

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

        public List<SP_SelectAll_SuperintendenciasParaProyecto_Result> getAllSuperintendenciasXId(int IdCoordinacion)
        {
            List<SP_SelectAll_SuperintendenciasParaProyecto_Result> listresult = new List<SP_SelectAll_SuperintendenciasParaProyecto_Result>();
            try
            {
                listresult = db.SP_SelectAll_SuperintendenciasParaProyecto(IdCoordinacion).ToList();
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return listresult;
        }

        public void updateEstatusTiempoSubEtapaProyecto(int idProyecto)
        {
             try
            {
                db.SP_UpdateEstatusTiempoSubEtapaProyecto(idProyecto);
            }
             catch (SqlException ex)
             {
                 Trace.Write(ex.Message);
             }
        }

        public void UpdateFechaCompromisoEnProyecto(int IdConfigRelacionIndicadorProyecto, DateTime? FechaCompromisoParaEmpezarAMostrarse)
        {
            try
            {
                db.SP_UpdateFechaCompromisoInCRIP(IdConfigRelacionIndicadorProyecto, FechaCompromisoParaEmpezarAMostrarse);
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            } 
        }


    }
}
