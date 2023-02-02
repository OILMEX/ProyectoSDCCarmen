using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;


namespace RendicionCuentasServices.Services
{
    public class srvRendicionCuentas
    {
        dbSDCEntities db = new dbSDCEntities();
        srvConfigProyecto srvConfigProyecto = new srvConfigProyecto();

        private List<SP_VistaCargaIndicadoresProyectos_Result> ListIndicadoresSubsistemas;
        private List<SP_VistaCargaIndicadores12MPIxReponsable_Result> ListIndicadores12MPI;
        private List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> ListaIndicadoresAgrupados;
        private List<SP_VistaCargaIndicadores12MPIAgrupados_Result> ListaIndicadores12MPIAgrupados;
        private List<SP_VistaparaPivotGridProyectos_Result> ListaIndicadoresAnyEstatus;
        private List<tbl_Responsables> ListaResponsables;
        private int IdCargaResponsable;

        public srvRendicionCuentas(int IdResponsable)
        {
            ListIndicadores12MPI = db.SP_VistaCargaIndicadores12MPIxReponsable(0).ToList();
            //ListaIndicadoresAgrupados = db.SP_VistaCargaIndicadoresAgrupadosProyectos(IdResponsable).ToList();
            //ListIndicadoresSubsistemas = db.SP_VistaCargaIndicadoresProyectos(0).ToList();
            ListaIndicadoresAnyEstatus = db.SP_VistaparaPivotGridProyectos(0,"").ToList();
            //ListaIndicadores12MPIAgrupados = db.SP_VistaCargaIndicadores12MPIAgrupados(IdResponsable).ToList();
            ListaResponsables = db.tbl_Responsables.ToList();

            IdCargaResponsable = IdResponsable;
        }

        public List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> getListaIndicadoresAgrupados(string EstatusTiempo, int? IdUbicacion)
        {
            return db.SP_VistaCargaIndicadoresAgrupadosProyectos(IdCargaResponsable,EstatusTiempo, IdUbicacion).ToList();
        }

        public void setIndicadoresAgrupados(int IdResponsable)
        {
            //ListaIndicadoresAgrupados = db.SP_VistaCargaIndicadoresAgrupadosProyectos(IdResponsable).ToList();
            
        }

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

        public List<tbl_SoportesAsignadosaIndicador> getDocumentosIndicador(int? iddataindicador)
        {
            List<tbl_SoportesAsignadosaIndicador> list = new List<tbl_SoportesAsignadosaIndicador>();
            var elems = db.tbl_SoportesAsignadosaIndicador.Where(x => x.IdDataValoresIndicadores == iddataindicador).OrderBy(o => o.NombreDoc).ToList();
            if (elems != null)
            {
                foreach (tbl_SoportesAsignadosaIndicador e in elems)
                {
                    tbl_SoportesAsignadosaIndicador d = new tbl_SoportesAsignadosaIndicador();
                    d.IdSoportesAsignadosaIndicador = e.IdSoportesAsignadosaIndicador;
                    d.IdDataValoresIndicadores = e.IdDataValoresIndicadores;
                    d.NombreDoc = e.NombreDoc;
                    d.RutaDoc = e.RutaDoc;
                    d.FechaCreacion = e.FechaCreacion;
                    list.Add(d);
                }
            }
            return list;
        }

        #region Llenado de indicadores
        public List<EntEtapa> GetFechasEtapasProyAgrupado(int idproyecto, List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> data)
        {
            var splstElementos = data
                .Where(x => x.IdProyecto == idproyecto)
                .GroupBy(d => new { d.IdEtapa })
                .Select(d => d.First())
                .ToList();
            List<EntEtapa> lstEtapas = new List<EntEtapa>();
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
                        etapa.etapa_nombre = elem.NombreEtapa;
                        etapa.clave = elem.ClaveEtapa;
                        etapa.SubEtapas = GetSubEtapasAgrupadas(idetapaactual, idproyecto, data);
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

        private List<EntSubEtapa> GetSubEtapasAgrupadas(int? idetapa, int idproyecto, List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> data)
        {
            var splstElementos = data
                            .Where(x => x.IdEtapa == idetapa)
                            .Where(x => x.IdProyecto == idproyecto)
                            .GroupBy(d => new { d.IdSubEtapa })
                            .Select(d => d.First())
                            .ToList();
            List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            foreach (var elem in splstElementos)
            {
                if (elem.IdEtapa == idetapa && elem.IdProyecto == idproyecto)
                {
                    EntSubEtapa subetapa = new EntSubEtapa();
                    subetapa.subetapa_id = elem.IdSubEtapa;
                    subetapa.clave = elem.ClaveSubEtapa;
                    subetapa.subetapa_nombre = elem.NombreSubEtapa;
                    subetapa.FechaFin = (elem.FechaFinSubEtapa != null) ? elem.FechaFinSubEtapa : null;
                    subetapa.Subsistemas = GetSubsistemasAgrupados(elem.IdEtapa, elem.IdSubEtapa, idproyecto, data);//Obtener subsistemas

                    lstsubetapa.Add(subetapa);
                }
            }

            List<EntSubEtapa> listfiltrada = lstsubetapa.GroupBy(d => new { d.subetapa_id, d.subetapa_nombre })
                .Select(d => d.First())
                .OrderBy(d=>d.subetapa_id)
                .ToList();

            return listfiltrada;
        }

        private List<EntSubsistema> GetSubsistemasAgrupados(int? idetapa, int? idsubetapa, int idproyecto, List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> data)
        {
            List<EntSubsistema> lstsubsistemas = new List<EntSubsistema>();
            var splstindicadores = data
                .Where(x => x.IdEtapa == idetapa)
                .Where(x => x.IdSubEtapa == idsubetapa)
                .Where(x => x.IdProyecto == idproyecto)
                .GroupBy(d => new { d.IdSubsistema })
                .Select(d => d.First())
                .ToList(); 

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdProyecto == idproyecto)
                {
                    EntSubsistema subsistema = new EntSubsistema();
                    subsistema.id_subsistema = elem.IdSubsistema;
                    subsistema.subsistema = elem.NombreSubsistema;
                    subsistema.siglas = elem.NombreSubsistema;
                    //Obtenemos los elementos
                    subsistema.Procesos = GetElementoAgrupado(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, idproyecto, data);

                    lstsubsistemas.Add(subsistema);
                }
            }

            List<EntSubsistema> listfiltrada = lstsubsistemas.GroupBy(d => new { d.id_subsistema, d.subsistema })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;

            //return lstsubsistemas;
        }

        private List<EntProcesos> GetElementoAgrupado(int? idetapa, int? idsubetapa, int? idsubsistema, int idproyecto, List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> data)
        {
            List<EntProcesos> lstprocesos = new List<EntProcesos>();
            var splstindicadores = data
                .Where(x => x.IdEtapa == idetapa)
                .Where(x => x.IdSubEtapa == idsubetapa)
                .Where(x => x.IdSubsistema == idsubsistema)
                .Where(x => x.IdProyecto == idproyecto)
                .GroupBy(d => new { d.IdElemento })
                .Select(d => d.First())
                .ToList();

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdSubsistema == idsubsistema && elem.IdProyecto == idproyecto)
                {
                    EntProcesos subsistema = new EntProcesos();
                    subsistema.proceso_id = elem.IdElemento;
                    subsistema.proceso_sigla = elem.NombreElemento;
                    subsistema.proceso_nombre = elem.DescripcionElemento;
                    subsistema.Indicadores = GetIndicadoresAgrupados(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, elem.IdElemento, idproyecto, data);


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

        private List<EntIndicador> GetIndicadoresAgrupados(int? idetapa, int? idsubetapa, int? idsubsistema, int? idelemento, int idproyecto, List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> data)
        {
            List<EntIndicador> lstIndicadores = new List<EntIndicador>();
            var splstindicadores = data;
            var indicadores = splstindicadores.Where(x => x.IdEtapa == idetapa && x.IdSubEtapa == idsubetapa
                                                    && x.IdSubsistema == idsubsistema && x.IdElemento == idelemento
                                                    && x.IdProyecto == idproyecto).ToList()
                                                    .GroupBy(d => new { d.IdIndicador })
                                                    .Select(d => d.First())
                                                    .ToList();

            foreach(var elem in indicadores)
            {
                EntIndicador indicador = new EntIndicador();
                indicador.indicador_id = elem.IdIndicador;
                indicador.indicador_semaforo = elem.Semaforo;
                indicador.indicador_nombre = elem.DescripcionCorta;
                indicador.indicador_clave = elem.Prefijo;
                indicador.indicador_tipo = elem.TipoIndicador.ToLower();
                indicador.indicador_tipo_dato = elem.TipoCalculo;
                indicador.indicador_acttualizacion = elem.TipoFrecuencia.ToString();
                indicador.indicador_meta = elem.Meta.ToString();
                indicador.ValorProcesado = elem.ValorProcesado;
                
                indicador.indicador_notas = elem.SemaforoNotas;
                indicador.indicador_soporte = elem.SemaforoSoportes;
                indicador.Responsables = GetResponsables(elem.IdEtapa, elem.IdSubEtapa, elem.IdElemento, elem.IdIndicador, elem.IdSubsistema, idproyecto);
                lstIndicadores.Add(indicador);
            }

            List<EntIndicador> listfiltrada = lstIndicadores.GroupBy(d => new { d.indicador_id, d.indicador_nombre, d.indicador_clave })
                .Select(d => d.First())
                .ToList();


            return listfiltrada;
        }

        private List<EntResponsablesIndicador> GetResponsables(int? idetapa, int? idsubetapa, int? idelemento, int? idindicador, int? idsubsistema, int idproyecto)
        {
            List<EntResponsablesIndicador> lstResponsables = new List<EntResponsablesIndicador>();
            var splsresponsables = ListaIndicadoresAnyEstatus
                .Where(x => x.IdEtapa == idetapa)
                .Where(x => x.IdSubEtapa == idsubetapa)
                .Where(x=>x.IdSubsistema  == idsubsistema)
                .Where(x=> x.IdElemento == idelemento)
                .Where(x => x.IdIndicador == idindicador)
                .Where(x => x.IdProyecto == idproyecto)
                .ToList();

            foreach (var elem in splsresponsables)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdIndicador == idindicador && elem.IdProyecto == idproyecto)
                {
                    EntResponsablesIndicador responsable = new EntResponsablesIndicador();
                    responsable.id_responsable = elem.idResponsable;
                    responsable.IdEtapa = elem.IdEtapa;
                    responsable.IdSubetapa = elem.IdSubEtapa;
                    responsable.IdIndicador = elem.IdIndicador;

                    //get data responsable
                    var resp = ListaResponsables.Where(x => x.idResponsable == elem.idResponsable).FirstOrDefault();
                    responsable.ficha = resp.Ficha;
                    responsable.nombre = resp.NombreResponsable;
                    responsable.correo = resp.Correo;
                    responsable.DataIndicador = elem;
                    lstResponsables.Add(responsable);
                }
            }

            List<EntResponsablesIndicador> listfiltrada = lstResponsables.GroupBy(d => new { d.id_responsable, d.nombre, d.correo })
                .Select(d => d.First())
                .ToList();


            return listfiltrada;

        }


        public List<EntIndicador> getIndicadores12MPIAgrupados(int? idGerencia=null)
        {
            List<EntIndicador> lstIndicadores = new List<EntIndicador>();
            var splstindicadores = db.SP_VistaCargaIndicadores12MPIAgrupados(IdCargaResponsable, idGerencia).ToList(); 
            var indicadores = splstindicadores.GroupBy(d => new { d.IdIndicador })
                                                    .Select(d => d.First())
                                                    .ToList();

            var ListIndicadoresResponsables12MPI = db.SP_VistaCargaIndicadores12MPIxReponsableSubadministrador(IdCargaResponsable, idGerencia).ToList();
            List<SP_VistaCargaIndicadores12MPIxReponsable_Result> list12mpiresp = new List<SP_VistaCargaIndicadores12MPIxReponsable_Result>();
            foreach (var item in ListIndicadoresResponsables12MPI)
            {
                SP_VistaCargaIndicadores12MPIxReponsable_Result algo= new SP_VistaCargaIndicadores12MPIxReponsable_Result();
                algo.CheckComentarios = item.CheckComentarios;
                algo.CheckSoporte = item.CheckSoporte;
                algo.DescripcionCorta = item.DescripcionCorta;
                algo.DescripcionElemento = item.DescripcionElemento;
                algo.EstatusLlenado = item.EstatusLlenado;
                algo.IdConfigIndicadorResponsable = item.IdConfigIndicadorResponsable;
                algo.IdDataValoresIndicadores = item.IdDataValoresIndicadores;
                algo.IdElemento = item.IdElemento;
                algo.IdIndicador = item.IdIndicador;
                algo.IdResponsable = item.IdResponsable;
                algo.Meta = item.Meta;
                algo.NombreElemento = item.NombreElemento;
                algo.Prefijo = item.Prefijo;
                algo.Semaforo = item.Semaforo;
                algo.SemaforoComentarios = item.SemaforoSoportes;
                algo.SemaforoSoportes = item.SemaforoSoportes;
                algo.TipoCalculo = item.TipoCalculo;
                algo.TipoFrecuencia = item.TipoFrecuencia;
                algo.TipoIndicador = item.TipoIndicador;
                algo.Valor = item.Valor;
                list12mpiresp.Add(algo);
                
            }

            var dataResponsables = db.tbl_Responsables.ToList();
            foreach (var elem in indicadores)
            {
                EntIndicador indicador = new EntIndicador();
                indicador.Idelemento = elem.IdElemento;
                indicador.indicador_id = elem.IdIndicador;
                indicador.indicador_semaforo = elem.Semaforo;
                indicador.indicador_nombre = elem.DescripcionCorta;
                indicador.indicador_clave = elem.Prefijo;
                indicador.indicador_tipo = elem.TipoIndicador.ToLower();
                indicador.indicador_tipo_dato = elem.TipoCalculo;
                indicador.indicador_acttualizacion = elem.TipoFrecuencia.ToString();
                indicador.indicador_meta = elem.Meta.ToString();
                indicador.ValorProcesado = elem.ValorProcesado;
                indicador.Responsables = GetResponsables12MP1(elem.IdElemento, elem.IdIndicador, list12mpiresp, dataResponsables);
                lstIndicadores.Add(indicador);
            }

            return lstIndicadores;
        }

        private List<EntResponsablesIndicador> GetResponsables12MP1(int? idelemento, int? idindicador, List<SP_VistaCargaIndicadores12MPIxReponsable_Result> data, List<tbl_Responsables> dataResponsables)
        {
            List<EntResponsablesIndicador> lstResponsables = new List<EntResponsablesIndicador>();
            var splsresponsables = data //TODO HACER UNA CONSULTA EN BASE AL ROL Y NO PONERLE POR DEFAULT Q TRAIGA TODO
                .Where(x => x.IdElemento == idelemento)
                .Where(x => x.IdIndicador == idindicador)
                .ToList();

            foreach (var elem in splsresponsables)
            {
                if (elem.IdIndicador == idindicador && elem.IdElemento == idelemento)
                {
                    EntResponsablesIndicador responsable = new EntResponsablesIndicador();
                    responsable.id_responsable = elem.IdResponsable;
                    responsable.IdIndicador = elem.IdIndicador;

                    //get data responsable
                    var resp = dataResponsables.Where(x => x.idResponsable == elem.IdResponsable).FirstOrDefault();
                    responsable.ficha = resp.Ficha;
                    responsable.nombre = resp.NombreResponsable;
                    responsable.correo = resp.Correo;
                    responsable.DataIndicador12MPI = elem;
                    lstResponsables.Add(responsable);
                }
            }

            List<EntResponsablesIndicador> listfiltrada = lstResponsables.GroupBy(d => new { d.id_responsable, d.nombre, d.correo })
                .Select(d => d.First())
                .ToList();


            return listfiltrada;

        }

        private String getSemaforoAgrupado(int? valor, string tipoindicador, int meta){

            if(tipoindicador == "Seguimiento")
            {
                if(meta == 100)
                {
                    if (valor != null)
                    {
                        if (valor < 81)
                            return "Rojo";
                        else if (valor > 80 && valor < 96)
                            return "Amarillo";
                        else if (valor > 95)
                            return "Verde";
                        else
                            return "Rojo";
                    }
                    else
                        return "Rojo";
                }
                else
                {
                    if (valor != null)
                    {
                        if (valor > 0)
                            return "Verde";
                        else
                            return "Rojo";
                    }
                    else
                        return "Rojo";
                }
            }
            else
            {
                if (valor != null)
                {
                    if (valor > 0)
                        return "Verde";
                    else
                        return "Rojo";
                }
                else
                    return "Rojo";
            }
        }

        
        #endregion

    }
}
