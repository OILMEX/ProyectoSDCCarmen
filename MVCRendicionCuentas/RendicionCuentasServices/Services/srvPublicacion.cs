using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvPublicacion
    {
        dbSDCEntities db = new dbSDCEntities();

        public Dashboard_Publicaciones GetPublicacionActual(string Tipo, int IdUbicacion){

            Dashboard_Publicaciones Publicado = new Dashboard_Publicaciones();
            var p = db.SP_Select_Publicacion(Tipo).Where(x=>x.IdUbicacion==IdUbicacion).OrderByDescending(x => x.FechaPublicacion).FirstOrDefault();
            if(p != null){
                Publicado.IdPublicacion = p.IdPublicacion;
                Publicado.TipoPublicacion = p.TipoPublicacion;
                Publicado.FechaPublicacion = p.FechaPublicacion;
            }
            return Publicado;
            
        }

        public EntTablelrosPublicadosGlobales GetTablerosPublicadosActuales(int IdPublicacion) {

            EntTablelrosPublicadosGlobales Tablero = new EntTablelrosPublicadosGlobales();
            List<EntTablerosSubsistemasPublicados> Subsistemas = new List<EntTablerosSubsistemasPublicados>();
            var pro = db.SP_SelectAllPublicacionProyectos(IdPublicacion).ToList();          
            var sbs = db.SP_Select_PublicacionValoresSemaforosXSubsistemas(IdPublicacion).ToList();
            var avm = db.SP_Select_PublicacionSubsistemasxMesTabular(IdPublicacion).ToList();

            Tablero.TotalProyectos = pro.Count();

            List<EntTablerosProyectosPublicados> ListProy = new List<EntTablerosProyectosPublicados>();
            srvTableros srvTbl = new srvTableros();
            foreach(var proyecto in pro){
                EntTablerosProyectosPublicados NProyecto = new EntTablerosProyectosPublicados();
                NProyecto.IdProyecto = proyecto.IdProyecto;
                NProyecto.NombreProyecto = proyecto.NombreProyecto;
                NProyecto.Responsable = proyecto.Responsable;
                NProyecto.FechaInicio = proyecto.FechaInicio;
                NProyecto.FechaFin = proyecto.FechaFin;
                NProyecto.Valor12MPI = proyecto.Valor12MPI;
                NProyecto.ValorSAA = proyecto.ValorSAA;
                NProyecto.ValorSASP = proyecto.ValorSASP;
                NProyecto.ValorSAST = proyecto.ValorSAST;
                NProyecto.Semaforo = proyecto.Semaforo;
                NProyecto.Semaforo12MPI = proyecto.Semaforo12MPI;
                NProyecto.SemaforoSAA = proyecto.SemaforoSAA;
                NProyecto.SemaforoSASP = proyecto.SemaforoSASP;
                NProyecto.SemaforoSAST = proyecto.SemaforoSAST;
                NProyecto.Etapas = srvTbl.GetEtapasxProyecto(proyecto.IdPublicacionProyecto);
                ListProy.Add(NProyecto);
            }
            Tablero.Proyectos = ListProy;

            List<EntTablerosSubsistemasPublicados> ListSubsistemas = new List<EntTablerosSubsistemasPublicados>();
            foreach (var Subsistema in sbs) {
                EntTablerosSubsistemasPublicados NSubsistema = new EntTablerosSubsistemasPublicados();
                NSubsistema.IdPublicacion = Subsistema.IdPublicacion;
                NSubsistema.IdSubsistema = Subsistema.IdSubsistema;
                NSubsistema.EstatusMejora = Subsistema.EstatusMejora;
                NSubsistema.NombreSistema = db.tbl_Subsistemas.Where(x=>x.IdSubsistema == Subsistema.IdSubsistema).FirstOrDefault().NombreSubsistema;
                NSubsistema.DescripcionLargaSubsistema = db.tbl_Subsistemas.Where(x => x.IdSubsistema == Subsistema.IdSubsistema).FirstOrDefault().DescripcionLargaSubsistema;
                NSubsistema.Semaforo = Subsistema.Semaforo;
                NSubsistema.Valor = Subsistema.Valor;
                List<Dashboard_PublicacionTabular> ListAvanceMes = new List<Dashboard_PublicacionTabular>();
                var AvanceMes = avm.Where(x => x.IdSubsistema == Subsistema.IdSubsistema).ToList();
                foreach (var avancemes in AvanceMes)
                {
                    Dashboard_PublicacionTabular NAvanceMes = new Dashboard_PublicacionTabular();
                    NAvanceMes.Mes = avancemes.Mes;
                    NAvanceMes.Semaforo = avancemes.Semaforo;
                    NAvanceMes.Valor = avancemes.Valor;
                    ListAvanceMes.Add(NAvanceMes);
                }
                NSubsistema.AvancesMes = ListAvanceMes;
                List<SP_Select_PublicacionElementoxSubsistema_Result> ListElemento = new List<SP_Select_PublicacionElementoxSubsistema_Result>();
                var elementos = db.SP_Select_PublicacionElementoxSubsistema(IdPublicacion, Subsistema.IdSubsistema).ToList();
                foreach (var elem in elementos)
                {
                    SP_Select_PublicacionElementoxSubsistema_Result NElemento = new SP_Select_PublicacionElementoxSubsistema_Result();
                    NElemento.IdElemento = elem.IdElemento.Value;
                    NElemento.NombreElemento = elem.NombreElemento;
                    NElemento.DescripcionElemento = elem.DescripcionElemento;
                    NElemento.EstatusMejora = elem.EstatusMejora;
                    NElemento.Semaforo = elem.Semaforo;
                    NElemento.Valor = elem.Valor;
                    ListElemento.Add(NElemento);
                }
                NSubsistema.Elementos = ListElemento;
                ListSubsistemas.Add(NSubsistema);
            }
            Tablero.Subsistemas = ListSubsistemas;
            return Tablero;
        }
    }
}
