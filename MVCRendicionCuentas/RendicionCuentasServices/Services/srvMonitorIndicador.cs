using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RendicionCuentasServices.Services
{
    public class srvMonitorIndicador
    {
        dbSDCEntities db = new dbSDCEntities();
        
        private List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> ListIndicadoresSubsistemas;
        private List<SP_SelectAll_MonitoreoIndicadoresResponsables12MPI_Result> ListIndicadores12MPI;
        private List<SP_VistaCargaIndicadoresProyectos_Result> ListIndicadoresSubsistemasFaltantes;
        private int IdMonitorResponsable;
        GetsIndicadores objGetsIndicadores = new GetsIndicadores();

        public srvMonitorIndicador(int IdResponsable)
        {
            IdMonitorResponsable = IdResponsable;
            
           
        }

        public void setListIndicadoresSubsistemas()
        {
            ListIndicadoresSubsistemas = db.SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas(IdMonitorResponsable).ToList();
        }
        public void setListIndicadoresSubsistemasFaltantes()
        {
            ListIndicadoresSubsistemasFaltantes = db.SP_VistaCargaIndicadoresProyectos(IdMonitorResponsable).ToList();
        }
        public void setListIndicadores12MPI()
        {
            ListIndicadores12MPI = db.SP_SelectAll_MonitoreoIndicadoresResponsables12MPI(IdMonitorResponsable).ToList();
        }

        void setIdMonitorResponsable(int Idres)
        {
            IdMonitorResponsable = Idres;
        }

        public Object getIndicadoresSubsistemasFaltantes()
        {
            bool soporte = false;
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> ResponsablesSS = this.getAllResponsablesSSFiltro();
            List<EntResponsableIndicador> ListaIndicadoresSSxResponsables = getListIndicadoresSSxResponsables(ResponsablesSS, soporte);
            
            return new { faltantessubsis = ListaIndicadoresSSxResponsables };
        }

        public Object getSoportesSubsistemasFaltantes()
        {
            bool soporte = true;
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> ResponsablesSS = this.getAllResponsablesSSFiltro();
            List<EntResponsableIndicador> ListaIndicadoresSSxResponsables = getListIndicadoresSSxResponsables(ResponsablesSS, soporte);

            return new { faltantessubsis = ListaIndicadoresSSxResponsables };
        }

        public List<EntResponsableIndicador> getListIndicadoresSSxResponsables(List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> ResponsablesSS, bool soporte)
        {
            List<EntResponsableIndicador> ListEntResponsableIndicadorSS = new List<EntResponsableIndicador>();
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> ListIndicadoresSSfaltantes = new List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result>();
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> proyectosReponsableSS = this.getAllResponsablesSS();
            EntResponsableIndicador RI = new EntResponsableIndicador();

            if (ResponsablesSS.Count > 0)
                ResponsablesSS = ResponsablesSS.OrderBy(x => x.NombreResponsable).ToList();

            foreach (SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result Responsable in ResponsablesSS)
            {
                setIdMonitorResponsable((int) Responsable.idResponsable);
                ListIndicadoresSSfaltantes = !soporte ? this.getIndicadoresSSFaltantes(proyectosReponsableSS, (int)Responsable.idResponsable) : this.getSoportesSSFaltantes(proyectosReponsableSS, (int)Responsable.idResponsable);
                //HandlerIndicadoresProyectos.dataIndicadoresProyXResponsable = ListIndicadoresSSfaltantes;

                if (ListIndicadoresSSfaltantes.Count > 0)
                {

                    RI = new EntResponsableIndicador();
                    RI.responsable_id = Responsable.idResponsable;
                    RI.responsable_nombre = Responsable.NombreResponsable;
                    RI.responsable_count_sasp = this.GetCountIndicadoresFaltantes(ResponsablesSS, (int)Responsable.idResponsable, 2);
                    RI.responsable_count_saa = this.GetCountIndicadoresFaltantes(ResponsablesSS, (int)Responsable.idResponsable, 3);
                    RI.responsable_count_sast = this.GetCountIndicadoresFaltantes(ResponsablesSS, (int)Responsable.idResponsable, 4);
                    RI.proyectos = this.getProyectosxSubsistemas(Responsable);


                    ListEntResponsableIndicadorSS.Add(RI);
                }
            }

            return ListEntResponsableIndicadorSS;
        }

        public List<SP_VistaIndicadoresActivadosPeroNoAsignadosEnProyectos_Result> GetIndicadoresActivadosSinResponsables()
        {
            List<SP_VistaIndicadoresActivadosPeroNoAsignadosEnProyectos_Result> result = new List<SP_VistaIndicadoresActivadosPeroNoAsignadosEnProyectos_Result>();
            foreach (var item in db.SP_VistaIndicadoresActivadosPeroNoAsignadosEnProyectos().ToList())
            {
                item.NombreResponsable = item.NombreResponsable.Trim().ToUpper();
                result.Add(item);
            }
            return result.OrderBy(o=>o.Prefijo).ToList();
        }

        public List<SP_VistaIndicadores12MPIActivadosPeroAsignados_Result> GetIndicadores12mpiActivadosSinResponsables()
        {
            HttpCookie myCookie = System.Web.HttpContext.Current.Request.Cookies["myCookie"];
            int idresponsable = myCookie != null ? int.Parse(myCookie["IdUsuario"]) : 0;
            List<SP_VistaIndicadores12MPIActivadosPeroAsignados_Result> result = db.SP_VistaIndicadores12MPIActivadosPeroAsignados(idresponsable).ToList();
            return result;
        }

        public Object getSoportes12mpiFaltantes()
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsables12MPI_Result> Responsables12mpi = this.getAllResponsablesFiltroxReponsable12MPI();
            List<SP_VistaCargaIndicadores12MPIxReponsable_Result> indicadores12mpifaltantes = new List<SP_VistaCargaIndicadores12MPIxReponsable_Result>();
            List<EntResponsableIndicador> ListaIndicadores12mpixReponsables = new List<EntResponsableIndicador>();
            EntResponsableIndicador RI = new EntResponsableIndicador();

            if (Responsables12mpi.Count > 0)
                Responsables12mpi = Responsables12mpi.OrderBy(x => x.NombreResponsable).ToList();

            foreach (SP_SelectAll_MonitoreoIndicadoresResponsables12MPI_Result Responsable in Responsables12mpi)
            {

                indicadores12mpifaltantes = objGetsIndicadores.getSoportesFaltantesIndicadores12MPI((int)Responsable.IdResponsable);

                if (indicadores12mpifaltantes.Count > 0)
                {
                    RI = new EntResponsableIndicador();
                    RI.responsable_id = Responsable.IdResponsable;
                    RI.responsable_nombre = Responsable.NombreResponsable;
                    RI.Indicadores = indicadores12mpifaltantes;
                    ListaIndicadores12mpixReponsables.Add(RI);
                }
            }
            return new { ind12mpifaltantes = ListaIndicadores12mpixReponsables };
        }
       
        public Object getListaIndicadores12MPIFaltantes()
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsables12MPI_Result> Responsables12mpi = this.getAllResponsablesFiltroxReponsable12MPI().OrderBy(x=>x.NombreResponsable).ToList();
            List<SP_VistaCargaIndicadores12MPIxReponsable_Result> indicadores12mpifaltantes = new List<SP_VistaCargaIndicadores12MPIxReponsable_Result>();
            List<EntResponsableIndicador> ListaIndicadores12mpixReponsables = new List<EntResponsableIndicador>();
            EntResponsableIndicador RI = new EntResponsableIndicador();

            if (Responsables12mpi.Count > 0)
                Responsables12mpi = Responsables12mpi.OrderBy(x => x.NombreResponsable).ToList();

            foreach (SP_SelectAll_MonitoreoIndicadoresResponsables12MPI_Result Responsable in Responsables12mpi)
            {

                indicadores12mpifaltantes = objGetsIndicadores.getIndicadores12MPI((int)Responsable.IdResponsable, true);

                if (indicadores12mpifaltantes.Count > 0)
                {
                    RI = new EntResponsableIndicador();
                    RI.responsable_id = Responsable.IdResponsable;
                    RI.responsable_nombre = Responsable.NombreResponsable;
                    RI.Indicadores = indicadores12mpifaltantes;
                    ListaIndicadores12mpixReponsables.Add(RI);
                }
            }
            return new { ind12mpifaltantes = ListaIndicadores12mpixReponsables };
        }

        public JsRootObject GetJsonMonitoreoInd(int IdResponsable)
        {
            List<JsTableroSubsistema> tablero_subsistema = new List<JsTableroSubsistema>();
            JsTableroSubsistema SSPA = new JsTableroSubsistema();
            srvUsuarios srv = new srvUsuarios();
            List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> dtaIndic = srv.GetALLDataAvancesgraficas(IdResponsable);

            List<JsAvancesMes> avances_mes = new List<JsAvancesMes>();
            JsAvancesMes av = new JsAvancesMes();
            List<JsTableroSubsistema2> LSubs = new List<JsTableroSubsistema2>();
            JsTableroSubsistema2 TSubsistema = null;
            List<SP_SelectAll_IndicadoresLlenosFaltantesGlobalesMonitoreo_Result> LstIndFalt = srv.GetALLIndicadoresFaltante(IdResponsable);


            foreach (var x in dtaIndic)
            {
                if (x.IdSubsistema != 5)
                {
                    TSubsistema = new JsTableroSubsistema2();
                    TSubsistema.subsistema_id = x.IdSubsistema;
                    TSubsistema.subsistema = x.NombreSubsistema;
                    TSubsistema.estatus = x.Semaforo.ToLower();
                    TSubsistema.avance = x.Porcentaje;
                    TSubsistema.mejora = Convert.ToInt16(x.EstatusMejora);

                    var Rx = LstIndFalt.FirstOrDefault(c => c.IdSubsistema == TSubsistema.subsistema_id);
                    if (Rx != null)
                    {
                        TSubsistema.completados = Rx.IndicadoresLlenos.ToString() + " de " + Rx.IndicadoresFaltantes.ToString();
                    }

                    LSubs.Add(TSubsistema);

                }
                else
                {

                    SSPA.avance = x.Porcentaje;
                    SSPA.fecha = "15-Abr-2016";
                    SSPA.anio = "2016";
                    SSPA.estatus = x.Semaforo.ToLower();
                    SSPA.mejora = Convert.ToInt16(x.EstatusMejora);

                }
            }

            SSPA.tablero_subsistema = LSubs;
            tablero_subsistema.Add(SSPA);

            JsRootObject TJson = new JsRootObject();
            TJson.tablero_subsistema = tablero_subsistema;
            return TJson;
        }

        #region Notas

        public string getNota(int iddata)
        {
            String nota = "";

            var e = db.tbl_ComentariosAsignadosIndicador.FirstOrDefault(x => x.IdDataValoresIndicadores == iddata);
            if (e != null)
            {
                nota = e.Comentario;
            }

            return nota;
        }

        #endregion

        #region Responsables

        public List<SP_SelectAll_MonitoreoIndicadoresResponsables12MPI_Result> getAllResponsablesFiltroxReponsable12MPI()
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsables12MPI_Result> listResult = ListIndicadores12MPI;
            if (listResult.Count > 0)
            {
                listResult = listResult.GroupBy(d => new { d.IdResponsable, d.NombreResponsable })
                   .Select(d => d.First())
                   .ToList();
            }
            return listResult;
        }

        public List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> getAllResponsablesSSFiltro()
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> listResult = getAllResponsablesSS().OrderBy(x=>x.NombreResponsable).ToList();
            if (listResult.Count > 0)
            {
                listResult = listResult.GroupBy(d => new { d.idResponsable, d.NombreResponsable })
                   .Select(d => d.First())
                   .ToList();
            }
            return listResult;
        }

        public List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> getAllResponsablesSS()
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> listResult = ListIndicadoresSubsistemas;
            return listResult;
        }

        #endregion

        public List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> getIndicadoresSSFaltantes(List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> lista, int IdReponsable)
        {
            if(lista.Count > 0)
            {
                return lista.Where(x => x.idResponsable == IdReponsable && x.Valor == null).ToList();
            }
            else
            {
                return lista;
            }
            
        }

        public List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> getSoportesSSFaltantes(List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> lista, int IdReponsable)
        {
            if (lista.Count > 0)
            {
                return lista.Where(x => x.idResponsable == IdReponsable && x.SemaforoSoportes == "Rojo").ToList();
            }
            else
            {
                return lista;
            }

        }

        public List<EntEtapa> GetFechasEtapasProyFaltantes(int idproyecto)
        {
            List<SP_VistaCargaIndicadoresProyectos_Result> splstElementos = ListIndicadoresSubsistemasFaltantes;
            List<EntEtapa> lstEtapas = new List<EntEtapa>();
            //List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            EntEtapa etapa = new EntEtapa();
            int? idetapaactual = 0;

            foreach (var elem in splstElementos)
            {
                if (elem.IdProyecto == idproyecto)
                {
                    if (idetapaactual != elem.IdEtapa && elem.SemaforoSoportes == "Rojo")
                    {
                        etapa = new EntEtapa();

                        idetapaactual = elem.IdEtapa;
                        etapa.etapa_id = elem.IdEtapa;
                        etapa.etapa_nombre = elem.NombreEtapa;
                        etapa.clave = elem.ClaveEtapa;
                        etapa.SubEtapas = GetSubEtapasFaltantes(idetapaactual, idproyecto);
                        etapa.FechaFin = (elem.FechaFinEtapa != null) ? elem.FechaFinEtapa : null;

                        lstEtapas.Add(etapa);

                    }
                }
            }

            List<EntEtapa> listfiltrada = lstEtapas.GroupBy(d => new { d.etapa_id, d.etapa_nombre })
               .Select(d => d.First())
               .ToList();


            return listfiltrada;
        }

        private List<EntSubEtapa> GetSubEtapasFaltantes(int? idetapa, int idproyecto)
        {
            var splstElementos = ListIndicadoresSubsistemasFaltantes;
            List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            foreach (var elem in splstElementos)
            {
                if (elem.IdEtapa == idetapa && elem.IdProyecto == idproyecto && elem.SemaforoSoportes == "Rojo")
                {
                    EntSubEtapa subetapa = new EntSubEtapa();
                    subetapa.subetapa_id = elem.IdSubEtapa;
                    subetapa.clave = elem.ClaveSubEtapa;
                    subetapa.subetapa_nombre = elem.NombreSubEtapa;
                    subetapa.FechaFin = (elem.FechaFinSubEtapa != null) ? elem.FechaFinSubEtapa : null;
                    subetapa.Subsistemas = GetSubsistemasFaltantes(elem.IdEtapa, elem.IdSubEtapa, idproyecto);//Obtener subsistemas


                    lstsubetapa.Add(subetapa);

                }
            }

            List<EntSubEtapa> listfiltrada = lstsubetapa.GroupBy(d => new { d.subetapa_id, d.subetapa_nombre })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;
        }

        private List<EntSubsistema> GetSubsistemas(int? idetapa, int? idsubetapa, int? idproyecto, bool isSoporte = false)
        {
            List<EntSubsistema> lstsubsistemas = new List<EntSubsistema>();
            var splstindicadores = ListIndicadoresSubsistemas;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdProyecto == idproyecto && elem.idResponsable==IdMonitorResponsable)
                {
                    EntSubsistema subsistema = new EntSubsistema();
                    subsistema.id_subsistema = elem.IdSubsistema;
                    var ss = getSSDetalle(elem.IdSubsistema);
                    if (ss != null)
                    {
                        subsistema.subsistema = ss.NombreSubsistema;
                        subsistema.siglas = ss.NombreSubsistema;
                    }
                    
                    //Obtenemos los elementos
                    subsistema.Indicadores = GetIndicadoresxResponsableAlter(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, idproyecto);
                    subsistema.IndicadoresProy = GetIndicadoresxResponsable(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, idproyecto);

                    lstsubsistemas.Add(subsistema);
                }
            }

            List<EntSubsistema> listfiltrada = lstsubsistemas.GroupBy(d => new { d.id_subsistema, d.subsistema })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;

            //return lstsubsistemas;
        }

        private List<EntSubsistema> GetSubsistemasFaltantes(int? idetapa, int? idsubetapa, int idproyecto)
        {
            List<EntSubsistema> lstsubsistemas = new List<EntSubsistema>();
            var splstindicadores = ListIndicadoresSubsistemas;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdProyecto == idproyecto && elem.SemaforoSoportes == "Rojo")
                {
                    EntSubsistema subsistema = new EntSubsistema();
                    subsistema.id_subsistema = elem.IdSubsistema;
                    subsistema.subsistema = elem.NombreSubsistema;
                    subsistema.siglas = elem.NombreSubsistema;
                    //Obtenemos los elementos
                    subsistema.IndicadoresProy = GetIndicadoresFaltantes(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, idproyecto);

                    lstsubsistemas.Add(subsistema);
                }
            }

            List<EntSubsistema> listfiltrada = lstsubsistemas.GroupBy(d => new { d.id_subsistema, d.subsistema })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;

            //return lstsubsistemas;
        }
        
        private List<EntProcesos> GetElementoFaltantes(int? idetapa, int? idsubetapa, int? idsubsistema, int idproyecto, List<SP_VistaCargaIndicadoresProyectos_Result> ss)
        {
            List<EntProcesos> lstprocesos = new List<EntProcesos>();

            foreach (var elem in ss)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdSubsistema == idsubsistema && elem.IdProyecto == idproyecto && elem.IdDataValoresIndicadores == null)
                {
                    EntProcesos subsistema = new EntProcesos();
                    subsistema.proceso_id = elem.IdElemento;
                    subsistema.proceso_sigla = elem.NombreElemento;
                    subsistema.proceso_nombre = elem.DescripcionElemento;
                    subsistema.IndicadoresProy = GetIndicadoresFaltantes(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, elem.IdElemento, idproyecto, ss);


                    lstprocesos.Add(subsistema);

                    //lstprocesos.Add(subsistema);
                }
            }

            List<EntProcesos> listfiltrada = lstprocesos.GroupBy(d => new { d.proceso_id, d.proceso_nombre, d.proceso_sigla })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;

            //return lstprocesos;
        }

        private List<SP_VistaCargaIndicadoresProyectos_Result> GetIndicadoresxResponsable(int? idetapa, int? idsubetapa, int? idsubsistema, int? idproyecto)
        {
            List<SP_VistaCargaIndicadoresProyectos_Result> lstIndicadores = new List<SP_VistaCargaIndicadoresProyectos_Result>();
            var splstindicadores = ListIndicadoresSubsistemas;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdSubsistema == idsubsistema && elem.IdProyecto == idproyecto && elem.idResponsable==IdMonitorResponsable)
                {
                    SP_VistaCargaIndicadoresProyectos_Result data = new SP_VistaCargaIndicadoresProyectos_Result();
                    data.DescripcionCorta = elem.DescripcionCorta;
                    data.IdConfigIndicadorResponsable = elem.IdConfigIndicadorResponsable;
                    data.IdDataValoresIndicadores = elem.IdDataValoresIndicadores;
                    data.Prefijo = elem.Prefijo;
                    data.EstatusLlenado = elem.EstatusLlenado;
                    data.TipoIndicador = elem.TipoIndicador;
                    data.TipoCalculo = elem.TipoCalculo;
                    data.TipoFrecuencia = elem.TipoFrecuencia;
                    data.Meta = elem.Meta;
                    data.Valor = elem.Valor;
                    data.CheckComentarios = elem.CheckComentarios;
                    data.CheckSoporte = elem.CheckSoporte;
                    data.Semaforo = elem.Semaforo;
                    data.SemaforoComentarios = elem.SemaforoComentarios;
                    data.SemaforoSoportes = elem.SemaforoSoportes;
                    data.IdResponsable = elem.idResponsable;


                    lstIndicadores.Add(data);

                    //lstIndicadores.Add(data);
                }
            }

            List<SP_VistaCargaIndicadoresProyectos_Result> listfiltrada = lstIndicadores.GroupBy(d => new { d.IdConfigIndicadorResponsable })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;

            //return lstIndicadores;
        }

        private List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> GetIndicadoresxResponsableAlter(int? idetapa, int? idsubetapa, int? idsubsistema, int? idproyecto)
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> lstIndicadores = new List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result>();
            var splstindicadores = ListIndicadoresSubsistemas;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdSubsistema == idsubsistema && elem.IdProyecto == idproyecto && elem.idResponsable == IdMonitorResponsable)
                {
                    SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result data = new SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result();
                    data.DescripcionCorta = elem.DescripcionCorta;
                    data.IdConfigIndicadorResponsable = elem.IdConfigIndicadorResponsable;
                    data.IdDataValoresIndicadores = elem.IdDataValoresIndicadores;
                    data.Prefijo = elem.Prefijo;
                    data.EstatusLlenado = elem.EstatusLlenado;
                    data.TipoIndicador = elem.TipoIndicador;
                    data.TipoCalculo = elem.TipoCalculo;
                    data.TipoFrecuencia = elem.TipoFrecuencia;
                    data.Meta = elem.Meta;
                    data.Valor = elem.Valor;
                    data.CheckComentarios = elem.CheckComentarios;
                    data.CheckSoporte = elem.CheckSoporte;
                    data.Semaforo = elem.Semaforo;
                    data.SemaforoComentarios = elem.SemaforoComentarios;
                    data.SemaforoSoportes = elem.SemaforoSoportes;
                    data.idResponsable = elem.idResponsable;


                    lstIndicadores.Add(data);

                    //lstIndicadores.Add(data);
                }
            }

            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> listfiltrada = lstIndicadores.GroupBy(d => new { d.IdConfigIndicadorResponsable })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;

            //return lstIndicadores;
        }

        //TODO cambiar el valor devuelto por un List<T>
        private List<SP_VistaCargaIndicadoresProyectos_Result> GetIndicadoresFaltantes(int? idetapa, int? idsubetapa, int? idsubsistema, int? idelemento, int idproyecto)
        {
            List<SP_VistaCargaIndicadoresProyectos_Result> lstIndicadores = new List<SP_VistaCargaIndicadoresProyectos_Result>();
            var splstindicadores = ListIndicadoresSubsistemasFaltantes;

            lstIndicadores = splstindicadores.Where(x => x.IdEtapa == idetapa && x.IdSubEtapa == idsubetapa && x.IdSubsistema == idsubsistema &&
                    x.IdElemento == idelemento && x.IdProyecto == idproyecto && x.SemaforoSoportes == "Rojo").ToList().GroupBy(d => new { d.IdConfigIndicadorResponsable })
                .Select(d => d.First())
                .ToList();

            return lstIndicadores;
 
        }

        private List<SP_VistaCargaIndicadoresProyectos_Result> GetIndicadoresFaltantes(int? idetapa, int? idsubetapa, int? idsubsistema, int idproyecto)
        {
            List<SP_VistaCargaIndicadoresProyectos_Result> lstIndicadores = new List<SP_VistaCargaIndicadoresProyectos_Result>();
            var splstindicadores = ListIndicadoresSubsistemasFaltantes;

            lstIndicadores= splstindicadores.Where(x=>x.IdEtapa == idetapa && x.IdSubEtapa == idsubetapa && x.IdSubsistema == idsubsistema &&
                     x.IdProyecto == idproyecto && x.SemaforoSoportes == "Rojo").ToList().GroupBy(d => new { d.IdConfigIndicadorResponsable })
                .Select(d => d.First())
                .ToList();

            return lstIndicadores;
        }

        private List<SP_VistaCargaIndicadoresProyectos_Result> GetIndicadoresFaltantes(int? idetapa, int? idsubetapa, int? idsubsistema, int? idelemento, int idproyecto, List<SP_VistaCargaIndicadoresProyectos_Result> lstIndicadores)
        {
            lstIndicadores= lstIndicadores.Where(x=>x.IdEtapa == idetapa && x.IdSubEtapa == idsubetapa && x.IdSubsistema == idsubsistema &&
                    x.IdElemento == idelemento && x.IdProyecto == idproyecto && x.SemaforoSoportes == "Rojo").ToList().GroupBy(d => new { d.IdConfigIndicadorResponsable })
                .Select(d => d.First())
                .ToList();

            return lstIndicadores;
        }


        /// <summary>
        /// Función que devuelve los valores de los subsistemas de un proyecto determinado
        /// </summary>
        /// <param name="idProy">Id del proyecto</param>
        /// <returns>List<RunTime_PorcentajesProyecto></returns>
        private List<RunTime_PorcentajesProyecto> GetPorcentajeProyecto(int idProy)
        {
            List<RunTime_PorcentajesProyecto> list = db.RunTime_PorcentajesProyecto.Where(x => x.IdProyecto == idProy).ToList();
            return list;
        }



        public List<EntEtapa> getEtapasxProyecto(int? idproyecto, bool isSoporte = false)
        {
            var splstElementos = GetCountEtapas();
            List<EntEtapa> lstEtapas = new List<EntEtapa>();
            //List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            EntEtapa etapa = new EntEtapa();
            int? idetapaactual = 0;

            foreach (var elem in splstElementos)
            {
                if (elem.IdProyecto == idproyecto)
                {
                    if (idetapaactual != elem.IdEtapa)
                    {
                        etapa = new EntEtapa();

                        idetapaactual = elem.IdEtapa;
                        etapa.etapa_id = elem.IdEtapa;

                        var Etapa = getEtapaDetalle(elem.IdEtapa);
                        if(Etapa != null)
                        {
                            etapa.etapa_nombre = Etapa.NombreEtapa;
                            etapa.FechaFin = (elem.FechaFin != null) ? elem.FechaFin : null;
                        }

                        etapa.SubEtapas = GetSubEtapas(idetapaactual, idproyecto);

                        lstEtapas.Add(etapa);

                    }
                }
            }
            
            return lstEtapas;
        }

        private List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> GetCountEtapas()
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> listafiltrar = ListIndicadoresSubsistemas.Where(x=>x.idResponsable==IdMonitorResponsable).GroupBy(x => new { x.IdEtapa })
                .Select(x => x.First())
                .ToList();
            return listafiltrar;
        }


        private List<EntSubEtapa> GetSubEtapas(int? idetapa, int? idproyecto, bool isSoporte = false)
        {
            var splstElementos = GetCountSubEtapas();
            List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            foreach (var elem in splstElementos)
            {
                if (elem.IdEtapa == idetapa && elem.IdProyecto == idproyecto)
                {
                    EntSubEtapa subetapa = new EntSubEtapa();
                    subetapa.subetapa_id = elem.IdSubEtapa;
                    var SubEtapa = getSubEtapaDetalle(elem.IdSubEtapa);
                    if (SubEtapa != null)
                    {
                        subetapa.subetapa_id = SubEtapa.IdSubEtapa;
                        subetapa.subetapa_nombre = SubEtapa.NombreSubEtapa;
                        subetapa.clave = SubEtapa.Clave;
                        subetapa.FechaFin = (elem.FechaFin != null) ? elem.FechaFin : null;
                    }
                    subetapa.Subsistemas = GetSubsistemas(elem.IdEtapa, elem.IdSubEtapa, idproyecto);//Obtener subsistemas

                    lstsubetapa.Add(subetapa);
                }
            }

            return lstsubetapa;

        }

        private List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> GetCountSubEtapas()
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> listafiltrar = ListIndicadoresSubsistemas.Where(x=>x.idResponsable==IdMonitorResponsable).GroupBy(x => new { x.IdSubEtapa })
                .Select(x => x.First())
                .ToList();
            return listafiltrar;
        }


        private tbl_SubEtapas getSubEtapaDetalle(int? idsubetapa)
        {
            return db.tbl_SubEtapas.SingleOrDefault(x => x.IdSubEtapa == idsubetapa);
        }

        private tbl_Etapas getEtapaDetalle(int? idetapa)
        {
            return db.tbl_Etapas.SingleOrDefault(x => x.IdEtapa == idetapa);
        }

        private tbl_Subsistemas getSSDetalle(int? idss)
        {
            return db.tbl_Subsistemas.SingleOrDefault(x => x.IdSubsistema == idss);
        }

        public List<EntProyecto> getProyectosxSubsistemas(SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result Responsable, bool isSoporte = false)
        {
            var splstElementos = GetCountProyectos();
            List<EntProyecto> listaresult = new List<EntProyecto>();
            EntProyecto ent;

            foreach (var elem in splstElementos)
            {
                ent = new EntProyecto();
                ent.proyecto_id = (int)elem.IdProyecto;
                ent.proyecto_idresponsable = elem.idResponsable;
                ent.proyecto_nombre = elem.NombreProyecto;
                ent.proyecto_responsable = elem.NombreResponsable;
                ent.proyecto_fecha_ini = elem.FechaInicio;
                ent.proyecto_fecha_fin = elem.FechaFin;

                //Obtenemos los porcentajes de los subsistemas
                List<RunTime_PorcentajesProyecto> PorcentajeProy = GetPorcentajeProyecto(ent.proyecto_id);
                if (PorcentajeProy.Count > 0)
                {

                    //Obtenemos valores para SASP
                    RunTime_PorcentajesProyecto PProy = PorcentajeProy.SingleOrDefault(x => x.IdSubsistema == 2);
                    if (PProy != null)
                    {
                        ent.semaforo_SASP = PProy.Semaforo!=null? PProy.Semaforo.Length > 0 ? PProy.Semaforo : "" :"";
                        ent.porcentaje_SASP = PProy.Porcentaje;
                    }
                    else
                    {
                        ent.semaforo_SASP = "Gris";
                        ent.porcentaje_SASP = 0;
                    }

                    //Obtenemos valores para SAA
                    PProy = PorcentajeProy.SingleOrDefault(x => x.IdSubsistema == 3);
                    if (PProy != null)
                    {
                        ent.semaforo_SAA = PProy.Semaforo != null ? PProy.Semaforo.Length > 0 ? PProy.Semaforo : "" : "";
                        ent.porcentaje_SAA = PProy.Porcentaje;
                    }
                    else
                    {
                        ent.semaforo_SAA = "Gris";
                        ent.porcentaje_SAA = 0;
                    }

                    //Obtenemos valores para SASP
                    PProy = PorcentajeProy.SingleOrDefault(x => x.IdSubsistema == 4);
                    if (PProy != null)
                    {
                        ent.semaforo_SAST = PProy.Semaforo != null ? PProy.Semaforo.Length > 0 ? PProy.Semaforo : "" : "";
                        ent.porcentaje_SAST = PProy.Porcentaje;
                    }
                    else
                    {
                        ent.semaforo_SAST = "Gris";
                        ent.porcentaje_SAST = 0;
                    }

                }

                //Obtenemos las Etapas de los proyectos
                if (isSoporte)
                {
                    ent.Etapas = GetFechasEtapasProyFaltantes(ent.proyecto_id);
                }
                else
                {
                    ent.Etapas = getEtapasxProyecto(ent.proyecto_id, isSoporte);
                }

                listaresult.Add(ent);
            }


            return listaresult;
        }

        private List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> GetCountProyectos()
        {
            List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> listafiltrada = ListIndicadoresSubsistemas.Where(x=>x.idResponsable==IdMonitorResponsable).GroupBy(x => new { x.IdProyecto, x.NombreProyecto })
                .Select(x => x.First())
                .ToList();
            return listafiltrada;
        }

        public int GetCountIndicadoresFaltantes(List<SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas_Result> ListIndicadoresxResponsablesProyectos, int idresponsable, int idSubsistema)
        {
            int result = ListIndicadoresSubsistemas.Where(x => x.Valor == null && x.idResponsable == idresponsable && x.IdSubsistema == idSubsistema).ToList().Count();
           
            return result;
        }

        public int GetCountSoporteIndicadoresFaltantes(int idresponsable, int idss)
        {
            int returnvalue = 0;
            var result = db.SP_VistaCargaIndicadoresProyectos(idresponsable);
            returnvalue = result.Where(x=>x.SemaforoSoportes == "Rojo")
                .Where(x=>x.IdSubsistema == idss)
                .Select(x=>x.IdConfigIndicadorResponsable)
                .Count();

            return returnvalue;
        }        
    }
}
