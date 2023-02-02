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
    public class srvCargaIndicadores
    {
        dbSDCEntities db = new dbSDCEntities();
        srvConfigProyecto srvConfigProyecto = new srvConfigProyecto();

        private List<SP_VistaCargaIndicadoresProyectos_Result> ListIndicadoresSubsistemas;
        private List<SP_VistaCargaIndicadores12MPIxReponsable_Result> ListIndicadores12MPI;
        private List<SP_VistaCargaIndicadoresAgrupadosProyectos_Result> ListaIndicadoresAgrupados;
        private int IdCargaResponsable;

        public srvCargaIndicadores(int IdResponsable)
        {
            
            IdCargaResponsable = IdResponsable;
        }

        public void setListIndicadoresSubsistemas()
        {
            ListIndicadoresSubsistemas = db.SP_VistaCargaIndicadoresProyectos(IdCargaResponsable).ToList();
            
        }
        public void setListIndicadores12MPI()
        {
            
            ListIndicadores12MPI = db.SP_VistaCargaIndicadores12MPIxReponsable(IdCargaResponsable).ToList();
            
        }
        public void setListaIndicadoresAgrupados()
        {
            //ListaIndicadoresAgrupados = db.SP_VistaCargaIndicadoresAgrupadosProyectos(IdCargaResponsable).ToList();
        }

        

        public void setListaIndicadoresSubsistema(int IdResponsable)
        {
            ListIndicadoresSubsistemas = db.SP_VistaCargaIndicadoresProyectos(IdResponsable).ToList();
        }

       public Object getIndicadoresSubsistemasFaltantes()
        {
           List<EntProyecto> proyectosproceso = srvConfigProyecto.ProyectosPorEstatus("En Proceso");
            List<EntProyecto> proyectosfaltantes = new List<EntProyecto>();

            foreach (EntProyecto proi in proyectosproceso)
            {
                proi.Etapas = this.GetFechasEtapasProyFaltantes(proi.proyecto_id);
                proyectosfaltantes.Add(proi);
            }

            
            return  new { faltantessubsis = proyectosfaltantes };
        }

       public Object getIndicadoresSubsistemasFaltantes(int idresponsable)
       {
           List<EntProyecto> proyectosproceso = srvConfigProyecto.ProyectosPorEstatus("En Proceso").ToList();
           List<EntProyecto> proyectosfaltantes = new List<EntProyecto>();

           foreach (EntProyecto proi in proyectosproceso)
           {
               proi.Etapas = this.GetFechasEtapasProyFaltantes(proi.proyecto_id);
               if (proi.Etapas.Count() > 0)
               {
                   proyectosfaltantes.Add(proi);
               }
           }


           return new { faltantessubsis = proyectosfaltantes };
       }

        public string DevuelveSemaforo(int Valor, int idConfigResponsableIndicador)
        {
          var Result=  db.SP_DevuelveSemaforoConsiderandoMeta(Valor, idConfigResponsableIndicador);
          string resultado = Result!=null? Result.SingleOrDefault(): null;
          return resultado;
        }

        public Data_ValoresIndicador getDataRespIndicador(int iddata, string tipocalculo, int? idindiacor)
        {
            Data_ValoresIndicador data = new Data_ValoresIndicador();

            var e = db.Data_ValoresIndicador.FirstOrDefault(x => x.IdDataValoresIndicadores == iddata);
            if (e != null)
            {
                data.IdDataValoresIndicadores = e.IdDataValoresIndicadores;
                data.IdConfigIndicadorResponsable = e.IdConfigIndicadorResponsable;
                data.Valor = e.Valor;
                data.FecCreacion = e.FecCreacion;
                data.Tipo = e.Tipo;

                if(tipocalculo =="Formula"){
                    List<Predata_ValoresIndicadoresFormula> predata = new List<Predata_ValoresIndicadoresFormula>();
                    Predata_ValoresIndicadoresFormula ele = new Predata_ValoresIndicadoresFormula();
                    var c = e.Predata_ValoresIndicadoresFormula.FirstOrDefault(x => x.IdDataValoresIndicadores == data.IdDataValoresIndicadores);
                    if (c != null)
                    {
                        ele.IdDataValoresIndicadores = c.IdDataValoresIndicadores;
                        ele.IdValoresIndicadoresFormula = c.IdValoresIndicadoresFormula;
                        ele.ValorProgramado = c.ValorProgramado;
                        ele.ValorReal = c.ValorReal;
                        ele.Resultado = c.Resultado;

                        predata.Add(ele);
                    }

                    data.Predata_ValoresIndicadoresFormula = predata;
                }

                if (tipocalculo == "Programa")
                {
                    List<Predata__ValoresIndicadoresPrograma> predata = new List<Predata__ValoresIndicadoresPrograma>();
                    Predata__ValoresIndicadoresPrograma ele = new Predata__ValoresIndicadoresPrograma();
                    var c = e.Predata__ValoresIndicadoresPrograma.FirstOrDefault(x => x.IdDataValoresIndicadores == data.IdDataValoresIndicadores);
                    if (c != null)
                    {
                        ele.IdPreVIP = c.IdPreVIP;
                        ele.IdDataValoresIndicadores = c.IdDataValoresIndicadores;
                        ele.EneroProg = c.EneroProg;
                        ele.FebreroProg = c.FebreroProg;
                        ele.MarzoProg = c.MarzoProg;
                        ele.AbrilProg = c.AbrilProg;
                        ele.MayoProg = c.MayoProg;
                        ele.JunioProg = c.JunioProg;
                        ele.JulioProg = c.JulioProg;
                        ele.AgostoProg = c.AgostoProg;
                        ele.SeptiembreProg = c.SeptiembreProg;
                        ele.OctubreProg = c.OctubreProg;
                        ele.NoviembreProg = c.NoviembreProg;
                        ele.DiciembreProg = c.DiciembreProg;
                        ele.EneroReal = c.EneroReal;
                        ele.FebreroReal = c.FebreroReal;
                        ele.MarzoReal = c.MarzoReal;
                        ele.AbrilReal = c.AbrilReal;
                        ele.MayoReal = c.MayoReal;
                        ele.JunioReal = c.JunioReal;
                        ele.JulioReal = c.JulioReal;
                        ele.AgostoReal = c.AgostoReal;
                        ele.SeptiembreReal = c.SeptiembreReal;
                        ele.OctubreReal = c.OctubreReal;
                        ele.NoviembreReal = c.NoviembreReal;
                        ele.DiciembreReal = c.DiciembreReal;

                        ele.ProyeccionAnualProg = c.ProyeccionAnualProg;
                        ele.ProyeccionAnualReal = c.ProyeccionAnualReal;
                        ele.AnioSiguienteProg = c.AnioSiguienteProg;
                        ele.AnioSiguienteReal = c.AnioSiguienteReal;
                        ele.AnioAnteriorProg = c.AnioAnteriorProg;
                        ele.AnioAnteriorReal = c.AnioAnteriorReal;

                        ele.FecCreacion = c.FecCreacion;
                        ele.Resultado = c.Resultado;

                        predata.Add(ele);
                    }
                    else
                    {
                        //aqui va el predata para el tipo programa
                        if (tipocalculo == "Programa")
                        {
                            var p = db.Rel_ProgramaAsociadoIndicador.FirstOrDefault(x => x.IdIndicador == idindiacor);
                            if (p != null)
                            {
                                //ele.IdPreVIP = p.IdPreVIP;
                                //ele.IdDataValoresIndicadores = p.IdDataValoresIndicadores;
                                ele.EneroProg = p.EneroProg;
                                ele.FebreroProg = p.FebreroProg;
                                ele.MarzoProg = p.MarzoProg;
                                ele.AbrilProg = p.AbrilProg;
                                ele.MayoProg = p.MayoProg;
                                ele.JunioProg = p.JunioProg;
                                ele.JulioProg = p.JulioProg;
                                ele.AgostoProg = p.AgostoProg;
                                ele.SeptiembreProg = p.SeptiembreProg;
                                ele.OctubreProg = p.OctubreProg;
                                ele.NoviembreProg = p.NoviembreProg;
                                ele.DiciembreProg = p.DiciembreProg;
                                ele.EneroReal = p.EneroReal;
                                ele.FebreroReal = p.FebreroReal;
                                ele.MarzoReal = p.MarzoReal;
                                ele.AbrilReal = p.AbrilReal;
                                ele.MayoReal = p.MayoReal;
                                ele.JunioReal = p.JunioReal;
                                ele.JulioReal = p.JulioReal;
                                ele.AgostoReal = p.AgostoReal;
                                ele.SeptiembreReal = p.SeptiembreReal;
                                ele.OctubreReal = p.OctubreReal;
                                ele.NoviembreReal = p.NoviembreReal;
                                ele.DiciembreReal = p.DiciembreReal;

                                ele.ProyeccionAnualProg = p.ProyeccionAnualProg;
                                ele.ProyeccionAnualReal = p.ProyeccionAnualReal;
                                ele.AnioSiguienteProg = p.AnioSiguienteProg;
                                ele.AnioSiguienteReal = p.AnioSiguienteReal;
                                ele.AnioAnteriorProg = p.AnioAnteriorProg;
                                ele.AnioAnteriorReal = p.AnioAnteriorReal;

                                predata.Add(ele);

                                //ele.FecCreacion = c.FecCreacion;
                            }
                        }
                    }

                    data.Predata__ValoresIndicadoresPrograma = predata;
                }
            }
            else
            {
                //aqui va el predata para el tipo programa
                if (tipocalculo == "Programa")
                {
                    List<Predata__ValoresIndicadoresPrograma> predata = new List<Predata__ValoresIndicadoresPrograma>();
                    Predata__ValoresIndicadoresPrograma ele = new Predata__ValoresIndicadoresPrograma();
                    var p = db.Rel_ProgramaAsociadoIndicador.FirstOrDefault(x => x.IdIndicador == idindiacor);
                    if (p != null)
                    {
                        //ele.IdPreVIP = p.IdPreVIP;
                        //ele.IdDataValoresIndicadores = p.IdDataValoresIndicadores;
                        ele.EneroProg = p.EneroProg;
                        ele.FebreroProg = p.FebreroProg;
                        ele.MarzoProg = p.MarzoProg;
                        ele.AbrilProg = p.AbrilProg;
                        ele.MayoProg = p.MayoProg;
                        ele.JunioProg = p.JunioProg;
                        ele.JulioProg = p.JulioProg;
                        ele.AgostoProg = p.AgostoProg;
                        ele.SeptiembreProg = p.SeptiembreProg;
                        ele.OctubreProg = p.OctubreProg;
                        ele.NoviembreProg = p.NoviembreProg;
                        ele.DiciembreProg = p.DiciembreProg;
                        ele.EneroReal = p.EneroReal;
                        ele.FebreroReal = p.FebreroReal;
                        ele.MarzoReal = p.MarzoReal;
                        ele.AbrilReal = p.AbrilReal;
                        ele.MayoReal = p.MayoReal;
                        ele.JunioReal = p.JunioReal;
                        ele.JulioReal = p.JulioReal;
                        ele.AgostoReal = p.AgostoReal;
                        ele.SeptiembreReal = p.SeptiembreReal;
                        ele.OctubreReal = p.OctubreReal;
                        ele.NoviembreReal = p.NoviembreReal;
                        ele.DiciembreReal = p.DiciembreReal;

                        ele.ProyeccionAnualProg = p.ProyeccionAnualProg;
                        ele.ProyeccionAnualReal = p.ProyeccionAnualReal;
                        ele.AnioSiguienteProg = p.AnioSiguienteProg;
                        ele.AnioSiguienteReal = p.AnioSiguienteReal;
                        ele.AnioAnteriorProg = p.AnioAnteriorProg;
                        ele.AnioAnteriorReal = p.AnioAnteriorReal;

                        predata.Add(ele);

                        //ele.FecCreacion = c.FecCreacion;
                    }

                    data.Predata__ValoresIndicadoresPrograma = predata;
                }
            }

            return data;
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
            var elems = db.tbl_SoportesAsignadosaIndicador.Where(x => x.IdDataValoresIndicadores == iddataindicador).OrderBy(o=>o.NombreDoc).ToList();
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

        private void SP_InsertIndicadoresProyecto(int IdConfigIndicadorResponsable)
        {
            Debug.Assert(IdConfigIndicadorResponsable != null);
            db.SP_InsertIndicadoresProyecto_v2(IdConfigIndicadorResponsable);
        }
        
        public int? EditDataValoresIndicador(Data_ValoresIndicador data, Predata_ValoresIndicadoresFormula predata, string tipocalculo)
        {
            int? IdElemento = 0;
            try{
            
                int? id;
                if (data.IdDataValoresIndicadores > 0)
                {
                    id = data.IdDataValoresIndicadores;
                }
                else
                {
                   id = null;
                }

                var result = db.SP_InsertValoresIndicadores(id, data.Valor, data.IdConfigIndicadorResponsable);
                IdElemento = result.FirstOrDefault();
                SP_InsertIndicadoresProyecto((int)data.IdConfigIndicadorResponsable);
                
                if (IdElemento > 0)
                {
                    //Se manda a guardar en predata para formula
                    if (tipocalculo == "Formula")
                    {
                        var pre = db.Predata_ValoresIndicadoresFormula.FirstOrDefault(x => x.IdDataValoresIndicadores == IdElemento);
                        if (pre == null)
                        {

                            predata.IdDataValoresIndicadores = IdElemento;
                            db.Predata_ValoresIndicadoresFormula.Add(predata);
                            db.SaveChanges();
                            
                        }
                        else
                        {
                            pre.ValorProgramado = predata.ValorProgramado;
                            pre.ValorReal = predata.ValorReal;
                            pre.Resultado = predata.Resultado;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                var msj = ex.Message;
            }

            return IdElemento;
        }

        public int? EditDataValoresIndicador(Data_ValoresIndicador data, Predata__ValoresIndicadoresPrograma predata, string tipocalculo)
        {
            int? IdElemento = 0;
            try
            {

                int? id;
                if (data.IdDataValoresIndicadores > 0)
                {
                    id = data.IdDataValoresIndicadores;
                }
                else
                {
                    id = null;
                }

                var result = db.SP_InsertValoresIndicadores(id, data.Valor, data.IdConfigIndicadorResponsable);
                IdElemento = result.FirstOrDefault();
                SP_InsertIndicadoresProyecto((int)data.IdConfigIndicadorResponsable);

                if (IdElemento > 0)
                {
                    //Se manda a guardar en predata para formula
                    if (tipocalculo == "Programa")
                    {
                        var pre = db.Predata__ValoresIndicadoresPrograma.FirstOrDefault(x => x.IdDataValoresIndicadores == IdElemento);
                        if (pre == null)
                        {

                            predata.IdDataValoresIndicadores = IdElemento;
                            predata.FecCreacion = DateTime.Now;
                            db.Predata__ValoresIndicadoresPrograma.Add(predata);
                            db.SaveChanges();
                            //IdElemento = result.FirstOrDefault();
                        }
                        else
                        {
                            //pre.IdPreVIP = predata.IdPreVIP;
                            //pre.IdDataValoresIndicadores = predata.IdDataValoresIndicadores;
                            pre.EneroProg = predata.EneroProg;
                            pre.FebreroProg = predata.FebreroProg;
                            pre.MarzoProg = predata.MarzoProg;
                            pre.AbrilProg = predata.AbrilProg;
                            pre.MayoProg = predata.MayoProg;
                            pre.JunioProg = predata.JunioProg;
                            pre.JulioProg = predata.JulioProg;
                            pre.AgostoProg = predata.AgostoProg;
                            pre.SeptiembreProg = predata.SeptiembreProg;
                            pre.OctubreProg = predata.OctubreProg;
                            pre.NoviembreProg = predata.NoviembreProg;
                            pre.DiciembreProg = predata.DiciembreProg;
                            pre.EneroReal = predata.EneroReal;
                            pre.FebreroReal = predata.FebreroReal;
                            pre.MarzoReal = predata.MarzoReal;
                            pre.AbrilReal = predata.AbrilReal;
                            pre.MayoReal = predata.MayoReal;
                            pre.JunioReal = predata.JunioReal;
                            pre.JulioReal = predata.JulioReal;
                            pre.AgostoReal = predata.AgostoReal;
                            pre.SeptiembreReal = predata.SeptiembreReal;
                            pre.OctubreReal = predata.OctubreReal;
                            pre.NoviembreReal = predata.NoviembreReal;
                            pre.DiciembreReal = predata.DiciembreReal;

                            pre.ProyeccionAnualProg = predata.ProyeccionAnualProg;
                            pre.ProyeccionAnualReal = predata.ProyeccionAnualReal;
                            pre.AnioSiguienteProg = predata.AnioSiguienteProg;
                            pre.AnioSiguienteReal = predata.AnioSiguienteReal;
                            pre.AnioAnteriorProg = predata.AnioAnteriorProg;
                            pre.AnioAnteriorReal = predata.AnioAnteriorReal;

                            pre.Resultado = predata.Resultado;

                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                var msj = ex.Message;
            }

            return IdElemento;
        }


        public int? EditNotaIndicador(tbl_ComentariosAsignadosIndicador item)
        {
            int? IdElemento = 0;
            try
            {
                var e = db.tbl_ComentariosAsignadosIndicador.FirstOrDefault(x => x.IdDataValoresIndicadores == item.IdDataValoresIndicadores);

                if (e == null)
                {
                    item.FechaCreacion = DateTime.Now;
                    db.tbl_ComentariosAsignadosIndicador.Add(item);
                    db.SaveChanges();
                    IdElemento = item.IdComentariosAsignadosaIndicador;
                }
                else
                {
                    e.Comentario = item.Comentario;
                    e.FechaCreacion = DateTime.Now;
                    db.SaveChanges();
                    IdElemento = e.IdComentariosAsignadosaIndicador;
                }
            }
            catch (Exception e)
            {
                var msj = e.Message;
            }
            

            return IdElemento;
        }

        public int AddDocumento(tbl_SoportesAsignadosaIndicador item)
        {
            int IdElemento = 0;
            try
            {
                if (item != null)
                {
                    db.tbl_SoportesAsignadosaIndicador.Add(item);
                    db.SaveChanges();
                    IdElemento = item.IdSoportesAsignadosaIndicador;
                }
            }
            catch (SqlException ex)
            {
                var msj = ex.Message;
            }

            return IdElemento;
        }

        public tbl_SoportesAsignadosaIndicador DeleteDocumento(int id)
        {
            tbl_SoportesAsignadosaIndicador ret = new tbl_SoportesAsignadosaIndicador();
            var ent = db.tbl_SoportesAsignadosaIndicador;
            try
            {
                if (ent != null)
                {
                    var e = ent.FirstOrDefault(X => X.IdSoportesAsignadosaIndicador == id);
                    if (e != null)
                    {
                        ret.IdSoportesAsignadosaIndicador = e.IdSoportesAsignadosaIndicador;
                        ret.IdDataValoresIndicadores = e.IdDataValoresIndicadores;
                        ret.RutaDoc = e.RutaDoc;
                        ret.NombreDoc = e.NombreDoc;

                        db.tbl_SoportesAsignadosaIndicador.Remove(e);
                        db.SaveChanges();
                        return ret;
                    }
                }
                
            }
            catch (Exception ex)
            {
                
            }
            return ret;
        }

        #region Llenado de indicadores
        public List<EntEtapa> GetFechasEtapasProy(int idproyecto)
        {
            var splstElementos = ListIndicadoresSubsistemas
                .Where(x=>x.IdProyecto == idproyecto)
                .GroupBy(d => new { d.IdEtapa })
                .Select(d => d.First())
                .ToList();
            List<EntEtapa> lstEtapas = new List<EntEtapa>();
            EntEtapa etapa = new EntEtapa();
            int? idetapaactual = 0;

            foreach (var elem in splstElementos)
            {
                //if (elem.IdProyecto == idproyecto)
                {
                    //if (idetapaactual != elem.IdEtapa)
                    {
                        etapa = new EntEtapa();

                        idetapaactual = elem.IdEtapa;
                        etapa.etapa_id = elem.IdEtapa;
                        etapa.etapa_nombre = elem.NombreEtapa;
                        etapa.clave = elem.ClaveEtapa;
                        etapa.SubEtapas = GetSubEtapas(idetapaactual, idproyecto);
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

        public List<EntEtapa> GetFechasEtapasProyFaltantes(int idproyecto)
        {
            var splstElementos = ListIndicadoresSubsistemas;
            List<EntEtapa> lstEtapas = new List<EntEtapa>();
            EntEtapa etapa = new EntEtapa();
            int? idetapaactual = 0;

            foreach (var elem in splstElementos)
            {
                if (elem.IdProyecto == idproyecto)
                {
                    if (idetapaactual != elem.IdEtapa && elem.IdDataValoresIndicadores == null)
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

        private List<EntSubEtapa> GetSubEtapas(int? idetapa, int idproyecto)
        {
            var splstElementos = ListIndicadoresSubsistemas
                .Where(x => x.IdProyecto == idproyecto)
                .Where(x => x.IdEtapa == idetapa)
                .GroupBy(d => new { d.IdSubEtapa })
                .Select(d => d.First())
                .ToList();

            List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            foreach (var elem in splstElementos)
            {
                //if (elem.IdEtapa == idetapa && elem.IdProyecto == idproyecto)
                {
                    EntSubEtapa subetapa = new EntSubEtapa();
                    subetapa.subetapa_id = elem.IdSubEtapa;
                    subetapa.clave = elem.ClaveSubEtapa;
                    subetapa.subetapa_nombre = elem.NombreSubEtapa;
                    subetapa.FechaFin = (elem.FechaFinSubEtapa != null) ? elem.FechaFinSubEtapa : null;
                    subetapa.Subsistemas = GetSubsistemas(elem.IdEtapa, elem.IdSubEtapa, idproyecto);//Obtener subsistemas

                        lstsubetapa.Add(subetapa);
                }
            }

            List<EntSubEtapa> listfiltrada = lstsubetapa.GroupBy(d => new { d.subetapa_id, d.subetapa_nombre })
                .Select(d => d.First())
                .ToList();

            return listfiltrada;
        }

        private List<EntSubEtapa> GetSubEtapasFaltantes(int? idetapa, int idproyecto)
        {
            var splstElementos = ListIndicadoresSubsistemas;
            List<EntSubEtapa> lstsubetapa = new List<EntSubEtapa>();
            foreach (var elem in splstElementos)
            {
                if (elem.IdEtapa == idetapa && elem.IdProyecto == idproyecto && elem.IdDataValoresIndicadores == null)
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

        private List<EntSubsistema> GetSubsistemas(int? idetapa, int? idsubetapa, int idproyecto)
        {
            List<EntSubsistema> lstsubsistemas = new List<EntSubsistema>();
            var splstindicadores = ListIndicadoresSubsistemas
                .Where(x => x.IdProyecto == idproyecto)
                .Where(x => x.IdEtapa == idetapa)
                .Where(x => x.IdSubEtapa == idsubetapa)
                .GroupBy(d => new { d.IdSubsistema })
                .Select(d => d.First())
                .ToList();

            foreach (var elem in splstindicadores)
            {
                //if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdProyecto == idproyecto)
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

        private List<EntSubsistema> GetSubsistemasFaltantes(int? idetapa, int? idsubetapa, int idproyecto)
        {
            List<EntSubsistema> lstsubsistemas = new List<EntSubsistema>();
            var splstindicadores = ListIndicadoresSubsistemas;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdProyecto == idproyecto && elem.IdDataValoresIndicadores == null)
                {
                    EntSubsistema subsistema = new EntSubsistema();
                    subsistema.id_subsistema = elem.IdSubsistema;
                    subsistema.subsistema = elem.NombreSubsistema;
                    subsistema.siglas = elem.NombreSubsistema;
                    //Obtenemos los elementos
                    subsistema.Procesos = GetElementoFaltantes(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, idproyecto);

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
            var splstindicadores = ListIndicadoresSubsistemas
                .Where(x => x.IdProyecto == idproyecto)
                .Where(x => x.IdEtapa == idetapa)
                .Where(x => x.IdSubEtapa == idsubetapa)
                .Where(x => x.IdSubsistema == idsubsistema)
                .GroupBy(d => new { d.IdElemento })
                .Select(d => d.First())
                .ToList();

            foreach (var elem in splstindicadores)
            {
                //if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdSubsistema == idsubsistema && elem.IdProyecto == idproyecto)
                {
                    EntProcesos subsistema = new EntProcesos();
                    subsistema.proceso_id = elem.IdElemento;
                    subsistema.proceso_sigla = elem.NombreElemento;
                    subsistema.proceso_nombre = elem.DescripcionElemento;
                    subsistema.IndicadoresProy = GetIndicadores(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, elem.IdElemento, idproyecto);

                    
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

        private List<EntProcesos> GetElementoFaltantes(int? idetapa, int? idsubetapa, int? idsubsistema, int idproyecto)
        {
            List<EntProcesos> lstprocesos = new List<EntProcesos>();
            var splstindicadores = ListIndicadoresSubsistemas;

            foreach (var elem in splstindicadores)
            {
                if (elem.IdEtapa == idetapa && elem.IdSubEtapa == idsubetapa && elem.IdSubsistema == idsubsistema && elem.IdProyecto == idproyecto && elem.IdDataValoresIndicadores == null)
                {
                    EntProcesos subsistema = new EntProcesos();
                    subsistema.proceso_id = elem.IdElemento;
                    subsistema.proceso_sigla = elem.NombreElemento;
                    subsistema.proceso_nombre = elem.DescripcionElemento;
                    subsistema.IndicadoresProy = GetIndicadoresFaltantes(elem.IdEtapa, elem.IdSubEtapa, elem.IdSubsistema, elem.IdElemento, idproyecto);


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

        private List<SP_VistaCargaIndicadoresProyectos_Result> GetIndicadores(int? idetapa, int? idsubetapa, int? idsubsistema, int? idelemento, int idproyecto)
        {
            List<SP_VistaCargaIndicadoresProyectos_Result> lstIndicadores = new List<SP_VistaCargaIndicadoresProyectos_Result>();
            var splstindicadores = ListIndicadoresSubsistemas;
            lstIndicadores= splstindicadores.Where(x=>x.IdEtapa == idetapa && x.IdSubEtapa == idsubetapa
                                                    && x.IdSubsistema == idsubsistema && x.IdElemento == idelemento
                                                    && x.IdProyecto == idproyecto).ToList()
                                                    .GroupBy(d => new { d.IdConfigIndicadorResponsable })
                                                    .Select(d => d.First())
                                                    .ToList();
            return lstIndicadores;
        }

        private List<SP_VistaCargaIndicadoresProyectos_Result> GetIndicadoresFaltantes(int? idetapa, int? idsubetapa, int? idsubsistema, int? idelemento, int idproyecto)
        {
            List<SP_VistaCargaIndicadoresProyectos_Result> lstIndicadores = new List<SP_VistaCargaIndicadoresProyectos_Result>();
            var splstindicadores = ListIndicadoresSubsistemas;
            lstIndicadores= splstindicadores.Where(x=>x.IdEtapa == idetapa && x.IdSubEtapa == idsubetapa && x.IdSubsistema == idsubsistema && 
                    x.IdElemento == idelemento && x.IdProyecto == idproyecto && x.IdDataValoresIndicadores == null).ToList()
                    .GroupBy(d => new { d.IdConfigIndicadorResponsable })
                    .Select(d => d.First())
                    .ToList();

            return lstIndicadores;
        }
        #endregion

       public tbl_SoportesAsignadosaIndicador getDocument(int idDocument)
        {
            return db.tbl_SoportesAsignadosaIndicador.FirstOrDefault(x => x.IdSoportesAsignadosaIndicador == idDocument);
        }


       public SP_SelectAllProyectosByEstatus_Result getProyectoxEstatusyIdProy(string estatus, int IdProy)
       {
           SP_SelectAllProyectosByEstatus_Result Result = new SP_SelectAllProyectosByEstatus_Result();
           try{
               List<SP_SelectAllProyectosByEstatus_Result> listResult = db.SP_SelectAllProyectosByEstatus(estatus).ToList();

               if (listResult.Count > 0)
               {
                   Result = listResult.FirstOrDefault(x => x.IdProyecto == IdProy);
                   Result.Porcentaje12MPI = (Result.Porcentaje12MPI == null) ? 0 : Result.Porcentaje12MPI;
                   Result.PorcentajeSAA = (Result.PorcentajeSAA == null) ? 0 : Result.PorcentajeSAA;
                   Result.PorcentajeSASP = (Result.PorcentajeSASP == null) ? 0 : Result.PorcentajeSASP;
                   Result.PorcentajeSAST = (Result.PorcentajeSAST == null) ? 0 : Result.PorcentajeSAST;

                   Result.Semaforo12MPI = (Result.Semaforo12MPI == null) ? "Gris" : Result.Semaforo12MPI;
                   Result.SemaforoSAA = (Result.SemaforoSAA == null) ? "Gris" : Result.SemaforoSAA;
                   Result.SemaforoSASP = (Result.SemaforoSASP == null) ? "Gris" : Result.SemaforoSASP;
                   Result.SemaforoSAST = (Result.SemaforoSAST == null) ? "Gris" : Result.SemaforoSAST;
               }
           }
           catch(SqlException ex)
           {
               Trace.Write(ex.Message);
           }

           return Result;
       }
    }
}
