using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvCompromiso
    {
        private string Rol;
        //Contexto de Base de datos dbSDC
        dbSDCEntities db = new dbSDCEntities();

        public void setRol(string rol)
        {
            Rol = rol;
        }

        public List<tbl_Subsistemas> Subsistemas(bool is12mpi)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tbl_Subsistemas> lstSubsistemas = null;
            try
            {
                lstSubsistemas = db.tbl_Subsistemas.Where(x => x.CheckAplicaProyecto != is12mpi).ToList();
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            return lstSubsistemas;
        }

        public List<DTO.EntEtapaC> getEtapas()
        {
            List<DTO.EntEtapaC> lstEtapas = new List<DTO.EntEtapaC>();
            try
            {
                List<tbl_Etapas> splstElementos = db.tbl_Etapas.Where(x => x.Estatus == true).ToList();
                foreach (tbl_Etapas elem in splstElementos)
                {
                    DTO.EntEtapaC etapa = new DTO.EntEtapaC();
                    etapa.etapa_id = elem.IdEtapa;
                    etapa.etapa_nombre = elem.NombreEtapa;
                    etapa.etapa_clave = elem.Clave;
                    etapa.etapa_fechaactualizacion = elem.FechaActualizacion != null ? elem.FechaActualizacion.Value.ToShortDateString() : "";
                    etapa.etapa_fechacreacion = elem.FechaCreacion != null ? elem.FechaCreacion.Value.ToShortDateString() : "";
                    etapa.etapa_usuarioactualizacion = elem.UsuarioActualizacion;
                    etapa.etapa_usuariocreacion = elem.UsuarioCreacion;
                    etapa.subetapas = getSubEtapas(elem.IdEtapa);
                    lstEtapas.Add(etapa);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return lstEtapas;
        }

        private string getColor(int? ss)
        {
            string str = string.Empty;
            switch (ss)
            {
                case 1:
                    str = "Verde";
                    break;
                case 2:
                    str = "Amarillo";
                    break;
                case 3:
                    str = "Rojo";
                    break;
            }

            return str;
        }

        private List<DTO.EntSubEtapaC> getSubEtapas(int idEtapa)
        {
            List<DTO.EntSubEtapaC> listSEtapas = new List<DTO.EntSubEtapaC>();

            try
            {
                List<tbl_SubEtapas> lse = db.tbl_SubEtapas.Where(x => x.IdEtapa == idEtapa).ToList();
                foreach (tbl_SubEtapas se in lse)
                {
                    DTO.EntSubEtapaC ese = new DTO.EntSubEtapaC();
                    ese.etapa_id = se.IdSubEtapa;
                    ese.etapa_nombre = se.NombreSubEtapa;
                    listSEtapas.Add(ese);
                }
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }

            return listSEtapas;
        }

        public List<DTO.EntElementosCompromiso> getElementos(int idSubSistema)
        {
            List<DTO.EntElementosCompromiso> Elementos = new List<DTO.EntElementosCompromiso>();
            var lstElementos = db.SP_SelectAll_ElementosPorSubsistema(idSubSistema);
            foreach (var elem in lstElementos)
            {
                DTO.EntElementosCompromiso elec = new DTO.EntElementosCompromiso();
                elec.elemento_id = elem.IdElemento;
                elec.elemento_nombre = elem.NombreElemento;
                elec.elemento_descripcionelemento = elem.DescripcionElemento;
                elec.elemento_estatus = elem.Estatus.ToString();
                elec.elemento_fechaactualizacion = elem.FechaActualizacion != null ? elem.FechaActualizacion.Value.ToShortDateString() : "";
                elec.elemento_fechacreacion = elem.FechaCreacion != null ? elem.FechaCreacion.Value.ToShortDateString() : "";
                elec.elemento_usuarioactualizcion = elem.UsuarioActualizacion != null ? elem.UsuarioActualizacion.Value : 0;
                elec.elemento_usuariocreacion = elem.UsuarioCreacion != null ? elem.UsuarioCreacion.Value : 0;
                elec.indicadores = getIndicadores(elem.IdElemento);
                Elementos.Add(elec);
            }

            return Elementos;
        }

        public List<EntElementosCompromiso> getElementos(int idSubsistema, int idSubetapa)
        {
            List<DTO.EntElementosCompromiso> Elementos = new List<DTO.EntElementosCompromiso>();
            var lstElementos = db.SP_SelectAll_ElementosPorSubsistema(idSubsistema);
            foreach (var elem in lstElementos)
            {
                DTO.EntElementosCompromiso elec = new DTO.EntElementosCompromiso();
                elec.elemento_id = elem.IdElemento;
                elec.elemento_nombre = elem.NombreElemento;
                elec.elemento_descripcionelemento = elem.DescripcionElemento;
                elec.elemento_estatus = elem.Estatus.ToString();
                elec.elemento_fechaactualizacion = elem.FechaActualizacion != null ? elem.FechaActualizacion.Value.ToShortDateString() : "";
                elec.elemento_fechacreacion = elem.FechaCreacion != null ? elem.FechaCreacion.Value.ToShortDateString() : "";
                elec.elemento_usuarioactualizcion = elem.UsuarioActualizacion != null ? elem.UsuarioActualizacion.Value : 0;
                elec.elemento_usuariocreacion = elem.UsuarioCreacion != null ? elem.UsuarioCreacion.Value : 0;
                elec.indicadores = getIndicadores(elem.IdElemento, idSubetapa);
                Elementos.Add(elec);
            }

            return Elementos;
        }

        private List<DTO.EntIndicadorC> getIndicadores(int idIndicador)
        {
            List<DTO.EntIndicadorC> Indicadores = new List<DTO.EntIndicadorC>();
            var lstIndicadores = db.SP_SelectAll_IndicadoresPorElemento(idIndicador);
            foreach (var item in lstIndicadores)
            {
                DTO.EntIndicadorC Indicador = new DTO.EntIndicadorC();
                Indicador.indicador_IdIndicador = item.IdIndicador;
                Indicador.indicador_Clave = item.Clave;
                Indicador.indicador_DescripcionCorta = item.DescripcionCorta;
                Indicador.indicador_DescripcionLarga = item.DescripcionLarga;
                Indicador.indicador_CheckSoporte = item.CheckSoporte;
                Indicador.indicador_CheckReqSoporte = item.CheckReqSoporte;
                Indicador.indicador_CheckAplica = item.CheckAplica;
                Indicador.indicador_CheckComentarios = item.CheckComentarios;
                Indicador.indicador_CheckCreacionDesdeProyecto = item.CheckCreacionDesdeProyecto;
                Indicador.indicador_CheckReqComentario = item.CheckReqComentario;
                Indicador.indicador_DescripcionValorFormula = item.DescripcionValorFormula;
                Indicador.indicador_Estatus = item.CheckAplica;
                Indicador.indicador_EtiquetaValorProgramado = item.EtiquetaValorProgramado;
                Indicador.indicador_EtiquetaValorReal = item.EtiquetaValorReal;
                Indicador.indicador_FechaActualizacion = item.FechaActualizacion != null ? item.FechaActualizacion.Value.ToShortDateString() : "";
                Indicador.indicador_FechaCreacion = item.FechaCreacion != null ? item.FechaCreacion.Value.ToShortDateString() : "";
                Indicador.indicador_Meta = item.Meta;
                Indicador.indicador_Prefijo = item.Prefijo;
                Indicador.indicador_TipoCalculo = item.TipoCalculo;
                Indicador.indicador_TipoFrecuencia = item.TipoFrecuencia.ToString();
                Indicador.indicador_TipoIndicador = item.TipoIndicador;
                Indicador.indicador_UsuarioActualizacion = item.UsuarioActualizacion;
                Indicador.indicador_UsuarioCreacion = item.UsuarioCreacion;
                Indicadores.Add(Indicador);
            }

            return Indicadores;
        }

        private List<DTO.EntIndicadorC> getIndicadores(int idElemento, int idSubetapa)
        {
            List<DTO.EntIndicadorC> Indicadores = new List<DTO.EntIndicadorC>();
            var lstIndicadores = db.SP_SelectAll_IndicadoresPorElemento(idElemento);
            foreach (var item in lstIndicadores)
            {
                DTO.EntIndicadorC Indicador = new DTO.EntIndicadorC();
                Indicador.indicador_IdIndicador = item.IdIndicador;
                Indicador.indicador_Clave = item.Clave;
                Indicador.indicador_DescripcionCorta = item.DescripcionCorta;
                Indicador.indicador_DescripcionLarga = item.DescripcionLarga;
                Indicador.indicador_CheckSoporte = item.CheckSoporte;
                Indicador.indicador_CheckReqSoporte = item.CheckReqSoporte;
                Indicador.indicador_CheckAplica = item.CheckAplica;
                Indicador.indicador_CheckComentarios = item.CheckComentarios;
                Indicador.indicador_CheckCreacionDesdeProyecto = item.CheckCreacionDesdeProyecto;
                Indicador.indicador_CheckReqComentario = item.CheckReqComentario;
                Indicador.indicador_DescripcionValorFormula = item.DescripcionValorFormula;
                Indicador.indicador_Estatus = getStatusIndicadorxSubEtapa(item.IdIndicador, idSubetapa);
                Indicador.indicador_EtiquetaValorProgramado = item.EtiquetaValorProgramado;
                Indicador.indicador_EtiquetaValorReal = item.EtiquetaValorReal;
                Indicador.indicador_FechaActualizacion = item.FechaActualizacion != null ? item.FechaActualizacion.Value.ToShortDateString() : "";
                Indicador.indicador_FechaCreacion = item.FechaCreacion != null ? item.FechaCreacion.Value.ToShortDateString() : "";
                Indicador.indicador_Meta = item.Meta;
                Indicador.indicador_Prefijo = item.Prefijo;
                Indicador.indicador_TipoCalculo = item.TipoCalculo;
                Indicador.indicador_TipoFrecuencia = item.TipoFrecuencia.ToString();
                Indicador.indicador_TipoIndicador = item.TipoIndicador;
                Indicador.indicador_UsuarioActualizacion = item.UsuarioActualizacion;
                Indicador.indicador_UsuarioCreacion = item.UsuarioCreacion;
                Indicadores.Add(Indicador);
            }

            return Indicadores;
        }

        private bool? getStatusIndicadorxSubEtapa(int idIndicador, int idSubetapa)
        {
            bool? result = false;
            
            try
            {
                Config_SubEtapaIndicador inds = db.Config_SubEtapaIndicador.FirstOrDefault(x => x.IdIndicador == idIndicador && x.IdSubEtapa == idSubetapa && x.Estatus==true);
                if (inds != null)
                    result = inds.Estatus;
            }
            catch (SqlException ex)
            {
                Trace.Write("Error: " + ex.Message);
            }
            return result;
        }

        public string SetEstatusIndicador(int idIndicador, bool Status)
        {
            string MsjReturn = string.Empty;
            try
            {
                db.SP_InsertConfigIndicador12MPICompromisos(idIndicador, Status);
                MsjReturn = "Datos Guardados";
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return MsjReturn;
        }

        public string SetEstatusIndicador(int idIndicador, int idSubetapa, bool Status)
        {
            string MsjReturn = string.Empty;
            try
            {
                db.SP_InsertConfigSubEtapasIndicadorCompromisos(idIndicador, idSubetapa, Status);
                MsjReturn = "Datos Guardados";
            }
            catch (SqlException ex)
            {
                Trace.Write(ex.Message);
            }
            return MsjReturn;
        }


        public string SaveUsers(int idIndicador, string[] User, string[] NoUser)
        {
            string Msj = string.Empty;

            try
            {
                if (User != null)
                {
                    foreach (string us in User)
                    {
                        db.SP_InsertConfigResponsables12MPI(idIndicador, int.Parse(us), true);
                    }
                }

                if(NoUser != null)
                foreach (string us in NoUser)
                {
                    db.SP_InsertConfigResponsables12MPI(idIndicador, int.Parse(us), false);
                }

            } catch (SqlException ex)
            {
                Msj = ex.Message;
            }

            return Msj;
        }

        public List<EntUserResponsable> GetListUsers(int idIndicador, int IdUbicacion)
        {
            List<EntUserResponsable> ListaReturn = new List<EntUserResponsable>();
            EntUserResponsable elem = new EntUserResponsable();

            if(Rol=="Administrador")
            {
                elem.Listaid = 1;
                elem.ListaNomnbre = "Usuarios";
                elem.User = getUsers(true, idIndicador).OrderBy(x => x.Nombre).ToList();
                ListaReturn.Add(elem);

                elem = new EntUserResponsable();
                elem.Listaid = 2;
                elem.ListaNomnbre = "Responsables";
                elem.User = getUsers(false, idIndicador).OrderBy(x => x.Nombre).ToList();
                ListaReturn.Add(elem);
            }
            else
            {
                elem.Listaid = 1;
                elem.ListaNomnbre = "Usuarios";
                elem.User = getUsers(true, idIndicador).Where(x => x.idUbicacion == IdUbicacion).OrderBy(x => x.Nombre).ToList();
                ListaReturn.Add(elem);

                elem = new EntUserResponsable();
                elem.Listaid = 2;
                elem.ListaNomnbre = "Responsables";
                elem.User = getUsers(false, idIndicador).Where(x => x.idUbicacion == IdUbicacion).OrderBy(x => x.Nombre).ToList();
                ListaReturn.Add(elem);
            }
            
            


            return ListaReturn;
        }

        private List<EntUsuario> getUsers(bool isListUser, int Id)
        {
            List<EntUsuario> Usuarios = new List<EntUsuario>();
            List<Config_IndicadorResponsable> Responsables = db.Config_IndicadorResponsable.Where(x => x.IdIndicador == Id && x.Estatus == true).ToList();

            if(isListUser)
            {                
                List<tbl_Responsables> users = db.tbl_Responsables.Where(x => x.Estatus == true).ToList();
                foreach(tbl_Responsables us in users)
                {
                    var intoResp = Responsables.Where(x => x.IdResponsable == us.idResponsable);
                    if (intoResp.Count() == 0)
                    {
                        EntUsuario user = new EntUsuario();
                        user.IdUsuario = us.idResponsable;
                        user.Nombre = us.NombreResponsable.Trim();
                        user.idUbicacion = us.IdUbicacion;
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
                    if(intoResp.Count() > 0)
                    {
                        EntUsuario user = new EntUsuario();
                        user.IdUsuario = us.idResponsable;
                        user.Nombre = us.NombreResponsable.Trim();
                        user.idUbicacion = us.IdUbicacion;
                        Usuarios.Add(user);
                    }
                    
                }
            }

            return Usuarios;

        }


        /// <summary>
        /// Función que obtiene el detalle de un determinado Indicador en base a su id
        /// </summary>
        /// <param name="idIndicador">Id del Indicador a Consultar</param>
        /// <returns>tbl_Indicadores</returns>
        public EntIndicadorDetalle GetInfoIndicador(int idIndicador)
        {
            EntIndicadorDetalle ent = new EntIndicadorDetalle();
            tbl_Indicadores indicadores = db.tbl_Indicadores.SingleOrDefault(x => x.IdIndicador == idIndicador);
            if(indicadores != null)
            {
                ent.IdIndicador = indicadores.IdIndicador;
                ent.IdElemento = indicadores.IdElemento;
                ent.Clave = indicadores.Clave;
                ent.Prefijo = indicadores.Prefijo;
                ent.DescripcionCorta = indicadores.DescripcionCorta;
                ent.DescripcionLarga = indicadores.DescripcionLarga;
                ent.CheckSoporte = indicadores.CheckSoporte;
                ent.CheckComentarios = indicadores.CheckComentarios;
                ent.CheckReqSoporte = indicadores.CheckReqSoporte;
                ent.CheckReqComentario = indicadores.CheckReqComentario;
                ent.CheckCreacionDesdeProyecto = indicadores.CheckCreacionDesdeProyecto;
                ent.FechaCreacion = indicadores.FechaCreacion == null ? "" : indicadores.FechaCreacion.Value.ToShortDateString(); ;
                ent.UsuarioCreacion = indicadores.UsuarioCreacion == null ? "" : GetUsuarioxId(indicadores.UsuarioCreacion);
                ent.FechaActualizacion = indicadores.FechaActualizacion == null? "" : indicadores.FechaActualizacion.Value.ToShortDateString();
                ent.UsuarioActualizacion = indicadores.UsuarioActualizacion == null ? "" : GetUsuarioxId(indicadores.UsuarioActualizacion);
                ent.Estatus = indicadores.Estatus;
                ent.TipoIndicador = indicadores.TipoIndicador;
                ent.TipoFrecuencia = indicadores.TipoFrecuencia;
                ent.Meta = indicadores.Meta;
                ent.TipoCalculo = indicadores.TipoCalculo;
                ent.DescripcionValorFormula = indicadores.DescripcionValorFormula;
                ent.EtiquetaValorProgramado = indicadores.EtiquetaValorProgramado;
                ent.EtiquetaValorReal = indicadores.EtiquetaValorReal;
                ent.CheckAplica = indicadores.CheckAplica;
                ent.Ayuda = indicadores.Ayuda == null ? "-" : indicadores.Ayuda;
                ent.FechaCompromiso = indicadores.FechaCompromiso == null ? "-" : indicadores.FechaCompromiso.Value.ToString("dd/MM/yyyy");
            }
            return ent;
        }

        /// <summary>
        /// Función que regresa el nombre del usuario de acuerdo a un Id determinado
        /// </summary>
        /// <param name="IdUsuario">Id de usuario a buscar</param>
        /// <returns>string de Usuario</returns>
        private string GetUsuarioxId(int? IdUsuario)
        {
            string user = string.Empty;
            user = db.tbl_Responsables.SingleOrDefault(x => x.idResponsable == IdUsuario).NombreResponsable;
            return user;
        }

        public int setFechaCompromiso(int idInidicador, string FechaCompromiso) {
            int result = 0;
            DateTime? FechaNull = null;
            DateTime FechaCompromisoFinal = (FechaCompromiso != "") ? Convert.ToDateTime(FechaCompromiso) : FechaNull.Value;
            tbl_Indicadores Indicador = new tbl_Indicadores();

            Indicador = db.tbl_Indicadores.Where(x => x.IdIndicador == idInidicador).FirstOrDefault();
            if(Indicador != null){
               Indicador.FechaCompromiso = FechaCompromisoFinal;
               db.SaveChanges();
               result = 1;
            }
            return result;
        }
    }
}
