using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvIndicadores
    {
        dbSDCEntities db = new dbSDCEntities();

        public List<EntElementos> ElementosPorSubsistema(int IdSubsistema) 
        {
            List<EntElementos> lstElementos = new List<EntElementos>();
            try
            {
                var splstElementos = db.SP_SelectAll_ElementosPorSubsistema(IdSubsistema).ToList(); 
                foreach(var elem in splstElementos){
                    EntElementos elemN = new EntElementos();
                    elemN.IdSubsistema = elem.IdSubsistema.Value;
                    elemN.IdElemento = elem.IdElemento;
                    elemN.NombreElemento = elem.NombreElemento;
                    elemN.DescripcionElemento = elem.DescripcionElemento;
                    elemN.FechaActualizacion = (elem.FechaActualizacion != null)?elem.FechaActualizacion.Value.ToShortDateString():"";
                    elemN.FechaCreacion = (elem.FechaCreacion != null)?elem.FechaCreacion.Value.ToShortDateString():"";
                    elemN.UsuarioActualizacion = (elem.UsuarioActualizacion != null)?elem.UsuarioActualizacion.Value:0;
                    elemN.ResponsableCreacion = db.tbl_Responsables.FirstOrDefault(x=>x.idResponsable == elem.UsuarioCreacion).NombreResponsable;
                    elemN.ResponsableActualizacion = (elem.UsuarioActualizacion != null) ? db.tbl_Responsables.FirstOrDefault(x => x.idResponsable == elem.UsuarioActualizacion).NombreResponsable : "";
                    lstElementos.Add(elemN);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return lstElementos;
        }

        public List<tbl_Subsistemas> Subsistemas()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tbl_Subsistemas> lstSubsistemas = null;
            try
            {
                lstSubsistemas = db.tbl_Subsistemas.Where(x => x.Estatus == true).ToList();
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return lstSubsistemas;
        }

        public int AddElementos(tbl_Elementos item) 
        { 
            int IdElemento = 0;
            try
            {
                if (item != null)
                {
                    db.tbl_Elementos.Add(item);
                    db.SaveChanges();
                    IdElemento = item.IdElemento;
                }
            }
            catch(Exception)
            {

            }

            return IdElemento;
        }

        public int EditElementos(tbl_Elementos item)
        { 
            int IdElemento = 0;
            try
            {
                var e = db.tbl_Elementos.Single(x => x.IdElemento == item.IdElemento);
                if (e != null)
                {
                    e.NombreElemento = item.NombreElemento;
                    e.DescripcionElemento = item.DescripcionElemento;
                    e.FechaActualizacion = item.FechaActualizacion;
                    e.UsuarioActualizacion = item.UsuarioActualizacion;
                    db.SaveChanges();
                    IdElemento = item.IdElemento;
                }
            }
            catch(Exception)
            {

            }

            return IdElemento;
        }

        public void DeleteElementos(tbl_Elementos item)
        {
            try
            {
                var e = db.tbl_Elementos.Single(x => x.IdElemento == item.IdElemento);
                if (e != null)
                {
                    e.FechaActualizacion = item.FechaActualizacion;
                    e.UsuarioActualizacion = item.UsuarioActualizacion;
                    e.Estatus = false;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public List<EntElementos> GetIndicadoresElemento(int IdElemento)
        {
            List<EntElementos> lstElementos = new List<EntElementos>();
            List<EntIndicadoresElementos> lstIndicador = new List<EntIndicadoresElementos>();
            try
            {
                    var elem = db.tbl_Elementos.FirstOrDefault(x => x.IdElemento == IdElemento); //PorSubsistema(IdSubsistema).ToList();

                    EntElementos elemN = new EntElementos();
                    elemN.IdSubsistema = elem.IdSubsistema.Value;
                    elemN.IdElemento = elem.IdElemento;
                    elemN.NombreElemento = elem.NombreElemento;
                    elemN.DescripcionElemento = elem.DescripcionElemento;
                    elemN.FechaActualizacion = (elem.FechaActualizacion != null) ? elem.FechaActualizacion.Value.ToShortDateString() : "";
                    elemN.FechaCreacion = (elem.FechaCreacion != null) ? elem.FechaCreacion.Value.ToShortDateString() : "";
                    elemN.UsuarioActualizacion = (elem.UsuarioActualizacion != null) ? elem.UsuarioActualizacion.Value : 0;
                    elemN.ResponsableCreacion = db.tbl_Responsables.FirstOrDefault(x => x.idResponsable == elem.UsuarioCreacion).NombreResponsable;
                    elemN.ResponsableActualizacion = (elem.UsuarioActualizacion != null) ? db.tbl_Responsables.FirstOrDefault(x => x.idResponsable == elem.UsuarioActualizacion).NombreResponsable : "";
                    var lstInd = db.SP_SelectAll_IndicadoresPorElemento(elemN.IdElemento).ToList();
                    foreach(var Indicador in lstInd){
                        EntIndicadoresElementos IndN = new EntIndicadoresElementos();
                        IndN.indicador_IdIndicador = Indicador.IdIndicador;
                        IndN.indicador_IdElemento = Indicador.IdElemento;
                        IndN.indicador_Clave = Indicador.Clave;
                        IndN.indicador_Prefijo = Indicador.Prefijo;
                        IndN.indicador_DescripcionCorta = Indicador.DescripcionCorta;
                        IndN.indicador_DescripcionLarga = Indicador.DescripcionLarga;
                        IndN.indicador_Estatus = Indicador.Estatus;
                        IndN.indicador_FechaActualizacion = (Indicador.FechaActualizacion != null) ? Indicador.FechaActualizacion.Value.ToShortDateString() : "";
                        IndN.indicador_TipoFrecuencia = (Indicador.TipoFrecuencia!=null)?(Indicador.TipoFrecuencia + ((Indicador.TipoFrecuencia >1)?" días":" día")):"";
                        IndN.indicador_Meta = Indicador.Meta;
                        IndN.indicador_TipoIndicador = Indicador.TipoIndicador;
                        IndN.indicador_TipoCalculo = Indicador.TipoCalculo == null ? "Check" : Indicador.TipoCalculo;
                        IndN.indicador_Ayuda = Indicador.Ayuda;
                        lstIndicador.Add(IndN);
                    }
                    elemN.Indicadores = lstIndicador;
                    lstElementos.Add(elemN);
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return lstElementos;
        }

        public int EditInidicador(tbl_Indicadores item, string tipoEdit)
        {
            int IdIndicador = 0;
            try
            {
                var e = db.tbl_Indicadores.Single(x => x.IdIndicador == item.IdIndicador);
                if (e != null)
                {
                    if(tipoEdit == "sencillo"){
                        e.Clave = item.Clave;
                        e.Prefijo = item.Prefijo;
                        e.DescripcionCorta = item.DescripcionCorta;
                        e.DescripcionLarga = item.DescripcionLarga;
                        e.Ayuda = item.Ayuda;
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

        public void DeleteIndicador(tbl_Indicadores item)
        {
            try
            {
                var e = db.tbl_Indicadores.Single(x => x.IdIndicador == item.IdIndicador);
                if (e != null)
                {
                    e.UsuarioActualizacion = item.UsuarioActualizacion;
                    e.FechaActualizacion = item.FechaActualizacion;
                    e.Estatus = false;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public int AddIndicador(tbl_Indicadores item)
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
                    if (item.TipoCalculo == "Programa") {
                        db.SP_InsertProgramaAsociadoNuevoIndicador(IdIndicador,item.UsuarioCreacion);
                    }
                }
            }
            catch (Exception)
            {

            }

            return IdIndicador;
        }

        public EntProgramaAsociadoIndicador GetProgramaAsociadoIndicador(int IdIndicador)
        {
            EntProgramaAsociadoIndicador Programa = new EntProgramaAsociadoIndicador();
            var p = db.SP_Select_ProgramaAsociadoxIndicador(IdIndicador).SingleOrDefault();
            Programa.IdIndicador = p.IdIndicador;
            Programa.IdRelProgramaAsociado = p.IdRelProgramaAsociado;
            Programa.EneroProg = p.EneroProg;
            Programa.FebreroProg = p.FebreroProg;
            Programa.MarzoProg = p.MarzoProg;
            Programa.AbrilProg = p.AbrilProg;
            Programa.MayoProg = p.MayoProg;
            Programa.JunioProg = p.JunioProg;
            Programa.JulioProg = p.JulioProg;
            Programa.AgostoProg = p.AgostoProg;
            Programa.SeptiembreProg = p.SeptiembreProg;
            Programa.OctubreProg = p.OctubreProg;
            Programa.NoviembreProg = p.NoviembreProg;
            Programa.DiciembreProg = p.DiciembreProg;

            Programa.EneroReal = p.EneroReal;
            Programa.FebreroReal = p.FebreroReal;
            Programa.MarzoReal = p.MarzoReal;
            Programa.AbrilReal = p.AbrilReal;
            Programa.MayoReal = p.MayoReal;
            Programa.JunioReal = p.JunioReal;
            Programa.JulioReal = p.JulioReal;
            Programa.AgostoReal = p.AgostoReal;
            Programa.SeptiembreReal = p.SeptiembreReal;
            Programa.OctubreReal = p.OctubreReal;
            Programa.NoviembreReal = p.NoviembreReal;
            Programa.DiciembreReal = p.DiciembreReal;

            Programa.AnioAnteriorProg = p.AnioAnteriorProg;
            Programa.AnioAnteriorReal = p.AnioAnteriorReal;
            Programa.AnioSiguienteProg = p.AnioSiguienteProg;
            Programa.AnioSiguienteReal = p.AnioSiguienteReal;
            Programa.ProyeccionAnualProg = p.ProyeccionAnualProg;
            Programa.ProyeccionAnualReal = p.ProyeccionAnualReal;

            Programa.UsuarioCreacion = p.UsuarioCreacion;
            Programa.NombreUsuarioCreacion = p.NombreUsuarioCreacion;
            Programa.UsuarioActualizacion = p.UsuarioActualizacion;
            Programa.NombreUsuarioActualizacion = p.NombreUsuarioActualizacion;

            Programa.FechaCreacionStr = (p.FechaCreacion != null) ? p.FechaCreacion.Value.ToShortDateString() : "";
            Programa.FechaActualizacionStr = (p.FechaActualizacion != null) ? p.FechaActualizacion.Value.ToShortDateString() : "";

            return Programa;
        }

        public int EditProgramaAsociadoIndicador(EntProgramaAsociadoIndicador p)
        {
            int IdIndicador= 0;
            try
            {
                var Programa = db.Rel_ProgramaAsociadoIndicador.Where(x=> x.IdIndicador == p.IdIndicador).SingleOrDefault();
                if (Programa != null)
                {
                    Programa.EneroProg = p.EneroProg;
                    Programa.FebreroProg = p.FebreroProg;
                    Programa.MarzoProg = p.MarzoProg;
                    Programa.AbrilProg = p.AbrilProg;
                    Programa.MayoProg = p.MayoProg;
                    Programa.JunioProg = p.JunioProg;
                    Programa.JulioProg = p.JulioProg;
                    Programa.AgostoProg = p.AgostoProg;
                    Programa.SeptiembreProg = p.SeptiembreProg;
                    Programa.OctubreProg = p.OctubreProg;
                    Programa.NoviembreProg = p.NoviembreProg;
                    Programa.DiciembreProg = p.DiciembreProg;

                    Programa.EneroReal = p.EneroReal;
                    Programa.FebreroReal = p.FebreroReal;
                    Programa.MarzoReal = p.MarzoReal;
                    Programa.AbrilReal = p.AbrilReal;
                    Programa.MayoReal = p.MayoReal;
                    Programa.JunioReal = p.JunioReal;
                    Programa.JulioReal = p.JulioReal;
                    Programa.AgostoReal = p.AgostoReal;
                    Programa.SeptiembreReal = p.SeptiembreReal;
                    Programa.OctubreReal = p.OctubreReal;
                    Programa.NoviembreReal = p.NoviembreReal;
                    Programa.DiciembreReal = p.DiciembreReal;

                    Programa.AnioAnteriorProg = p.AnioAnteriorProg;
                    Programa.AnioAnteriorReal = p.AnioAnteriorReal;
                    Programa.AnioSiguienteProg = p.AnioSiguienteProg;
                    Programa.AnioSiguienteReal = p.AnioSiguienteReal;
                    Programa.ProyeccionAnualProg = p.ProyeccionAnualProg;
                    Programa.ProyeccionAnualReal = p.ProyeccionAnualReal;

                    Programa.UsuarioActualizacion = p.UsuarioActualizacion;
                    Programa.FechaActualizacion = p.FechaActualizacion;
                    db.SaveChanges();
                    IdIndicador = (int)p.IdIndicador;
                }
            }
            catch (Exception)
            {

            }

            return IdIndicador;
        }

        public List<tbl_Indicadores> GetIndicadoresFaltantes(int IdElemento, int IdSubEtapa, int IdProyecto)
        {
            List<tbl_Indicadores> lstIndicador = new List<tbl_Indicadores>();
            try
            {
                var lstInd = db.SP_SelectAll_IndicadoresPorElementoFaltantesEnSubEtapaProyecto(IdElemento, IdSubEtapa, IdProyecto).ToList();
                foreach (var Indicador in lstInd)
                {
                    tbl_Indicadores IndN = new tbl_Indicadores();
                    IndN.Clave = Indicador.Clave;
                    IndN.IdIndicador = Indicador.IdIndicador;
                    IndN.IdElemento = Indicador.IdElemento;
                    IndN.DescripcionCorta = Indicador.DescripcionCorta;
                    IndN.DescripcionLarga = Indicador.DescripcionLarga;
                    lstIndicador.Add(IndN);
                }

            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return lstIndicador;
        }

        public int AddIndicadoresFaltantes(int IdProyecto, int IdSubetapa, int[] Indicadores)
        {
            int result = 0;
            try
            {
                foreach(int IdIndicador in Indicadores){
                    db.SP_InsertIndicadorDesdeProyecto(IdProyecto, IdSubetapa, IdIndicador);    
                }
                result = 1;

            }
            catch (Exception)
            {
                result = 0;
            }

            return result;
        }

        public List<tbl_Subsistemas> allSubsistemas()
        {
            List<tbl_Subsistemas> lstSubsistemas = new List<tbl_Subsistemas>();
            try
            {
                var s = db.SP_SelectAll_Subsistemas().ToList();
                if (s!=null)
                {
                    foreach(var Subsistemas in s){
                        tbl_Subsistemas sub = new tbl_Subsistemas();
                        sub.IdSubsistema = Subsistemas.IdSubsistema;
                        sub.NombreSubsistema = Subsistemas.NombreSubsistema;
                        lstSubsistemas.Add(sub);
                    }
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return lstSubsistemas;
        }
    }
}
