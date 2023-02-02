using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvUsuarios
    {
        dbSDCEntities db = new dbSDCEntities();

        #region Services para Usuarios
        //Función para actualizar datos de Responsables/Usuario
        public int UpdateUsuario(tbl_Responsables item)
        {

            var e = db.tbl_Responsables.Single(x => x.idResponsable == item.idResponsable);

            //codificando contraseñas en caso de que cambien
            if(item.Contrasenia != "") item.Contrasenia = setPassword(item.Contrasenia);
            //item.PasswordC = chkPass(e.PasswordC, item.PasswordC) ? e.PasswordC : setPassword(item.PasswordC);
            int result=0;
            try {
                if (e != null)
                {
                    e.NombreResponsable = item.NombreResponsable;
                    e.Usuario = item.Usuario;
                    if (item.Contrasenia != "") e.Contrasenia = item.Contrasenia;
                    e.Correo = item.Correo;
                    e.Ficha = item.Ficha;
                    e.IdPuesto = item.IdPuesto;
                    e.IdArea = item.IdArea;
                    e.IdAquienReporta = item.IdAquienReporta;
                    e.Rol = item.Rol;
                    e.IdUbicacion = item.IdUbicacion;
                    e.FechaActualizacion = DateTime.Now;
                    e.UsuarioActualizacion = item.UsuarioActualizacion;
                    //e.Estatus = item.Estatus;
                    db.SaveChanges();
                }
                result = 1;
            }
            catch(Exception ex){
                result = 0;
            }
            return(result);
        }

        //Verificar mail
        public int ExisteMail(string mail)
        {
            int IdNewPers = 0;
            try
            {
                tbl_Responsables Responsables = new tbl_Responsables();
                Responsables = db.tbl_Responsables.Where(x=>x.Correo == mail && x.Estatus==true).FirstOrDefault();
                if (Responsables != null)
                {
                    IdNewPers = Responsables.idResponsable;
                }
            }
            catch (Exception e)
            {
            }

            return IdNewPers;
        }

        //Función 
        public int AddUsuario(tbl_Responsables item)
        {
            int IdNewPers = 0;
            try
            {
                if (item != null)
                {

                    item.Contrasenia = setPassword(item.Contrasenia);
                    db.tbl_Responsables.Add(item);
                    db.SaveChanges();
                    IdNewPers=item.idResponsable;
                }
            }
            catch (Exception e)
            {
            }

            return IdNewPers;
        }

        //Función borrado logico el Row de la BD en base a id
        public int DeleteUsuario(tbl_Responsables item)
        {
            var ent = db.tbl_Responsables;
            int Result = 0;
            try {
                if (ent != null)
                {
                    var e = ent.FirstOrDefault(X => X.idResponsable == item.idResponsable);
                    if (e != null)
                    {
                        e.Estatus = false;
                        e.FechaActualizacion = DateTime.Now;
                        e.UsuarioActualizacion = item.UsuarioActualizacion;
                        //db.tbl_Responsables.Remove(e);
                    }
                    db.SaveChanges();
                }
                Result = 1;
            
            }
            catch(Exception ex) {
                Result = 0;
            }
            return Result;
        }

        public bool ReactivarUsuario(int[] ids, int IdUserEdit)
        {
            var ent = db.tbl_Responsables;
            try{
                foreach(int id in ids){
                    var e = ent.FirstOrDefault(X => X.idResponsable == id);
                    if (e != null)
                    {
                        e.Estatus = true;
                        e.FechaActualizacion = DateTime.Now;
                        e.UsuarioActualizacion = IdUserEdit;
                    }
                    
                }
               db.SaveChanges();
                return true;
            }
            catch{
                return false;
            }
        }

        //Función que devuelve una lista de Roles
        //public List<Cat_Rol> GetRol()
        //{
        //    List<Cat_Rol> Cat = db.Cat_Rol.ToList();

        //    return Cat;
        //}


        //Función que regresa true o false si las contraseñan coninciden, convirtiendo una de ellas codificadas
        private bool chkPass(string passG, string passS)
        {
            bool result = string.Equals(passG, passS);
            return result;
        }

        //Codificar string para contraseña
        private string setPassword(string pass)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activo">0=Todos,1=activos</param>
        /// <returns></returns>
        public List<tbl_Responsables> GetALLUsuarios(bool activo)
        {
            if (activo)
                return db.tbl_Responsables.Where(x => x.Estatus == activo).ToList();
            else return null;
            //else return db.tbl_Responsables.ToList();
        }
        public List<SP_SelectAll_Responsables_Result> GetALLUsuariosSP(bool activo)
        {
            //if (activo)
            //{
            //    List<SP_SelectAll_Responsables_Result> Tx = db.SP_SelectAll_Responsables().ToList();
            //    return Tx;
            //}
            List<SP_SelectAll_Responsables_Result> Tx = db.SP_SelectAll_Responsables().ToList();
            return Tx;
        }

        public List<EntUsuariosEliminados> GetUsuariosEliminados()
        {
            List<EntUsuariosEliminados> result = new List<EntUsuariosEliminados>();
            var data = (from res in db.tbl_Responsables
                        join ubi in db.tbl_Ubicaciones on res.IdUbicacion equals ubi.idUbicacion
                        //join p in db.tbl_Puesto on res.IdPuesto equals p.idPuesto
                        join res2 in db.tbl_Responsables on res.UsuarioActualizacion equals res2.idResponsable
                        into G
                        where res.Estatus == false
                        select new
                        {
                            res.idResponsable,
                            res.Ficha,
                            res.NombreResponsable,
                            //TipoAcceso = p.NombrePuesto,
                            res.Usuario,
                            res.Correo,
                            res.Rol,
                            NombreArea = ubi.Ubicacion,
                            UsuarioElimino = G.Select(x=>x.NombreResponsable).FirstOrDefault(),
                            res.FechaActualizacion
                        }).ToList();

            foreach (var u in data)
            {
                EntUsuariosEliminados user = new EntUsuariosEliminados();
                user.idResponsable = u.idResponsable;
                user.Ficha = u.Ficha;
                user.NombreResponsable = u.NombreResponsable;
                user.Usuario = u.Usuario;
                user.Correo = u.Correo;
                user.Rol = u.Rol;
                user.NombreArea = u.NombreArea;
                user.UsuarioElimino = u.UsuarioElimino;
                user.FechaActualizacion = u.FechaActualizacion;
                result.Add(user);
            }

            return result;
        }



        public List<tbl_PuestoOrganigrama> llenarPuestos()
        {
            List<tbl_PuestoOrganigrama> query = db.tbl_PuestoOrganigrama.ToList();
            return query;

        }

        public List<SP_SelectTrace_Puesto_Result> getDependencias(int idpuesto)
        {
            List<SP_SelectTrace_Puesto_Result> lstdependencias = new List<SP_SelectTrace_Puesto_Result>();
            var spelements = db.SP_SelectTrace_Puesto(idpuesto);

            foreach (var e in spelements)
            {
                SP_SelectTrace_Puesto_Result data = new SP_SelectTrace_Puesto_Result();
                data.IdSubdireccion = e.IdSubdireccion;
                data.IdGerencia = e.IdGerencia;
                data.IdCoordinacion = e.IdCoordinacion;
                data.IdSuperintendencia = e.IdSuperintendencia;
                data.NombreSubdireccion = e.NombreSubdireccion;
                data.NombreGerencia = e.NombreGerencia;
                data.NombreCoordinacion = e.NombreCoordinacion;
                data.NombreSuperintendencia = e.NombreSuperintendencia;
                lstdependencias.Add(data);
            }

            return lstdependencias;
        }

        public List<EntAreas> llenarAreas()
        {
            EntAreas c = null;
            var query2 = db.tbl_Areas.ToList();
            List<EntAreas> l = new List<EntAreas>();
            foreach (var y in query2)
            {
                c = new EntAreas();
                c.idArea = y.idArea;
                c.NombreArea = y.NombreArea.ToUpper();

                l.Add(c);
            }
            return l;

        }

        public List<ClsJefes> llenarResponsablesxUbicacion(int? IdResponsable=null)
        {
            var userLogged = db.tbl_Responsables.FirstOrDefault(w => w.idResponsable == IdResponsable);
            ClsJefes c = null;
            List<ClsJefes> l = new List<ClsJefes>();
            List<tbl_Responsables> x = new List<tbl_Responsables>();
              if(userLogged.Rol=="SubAdministrador")  
                x= db.tbl_Responsables.Where(z => z.Estatus == true && z.IdUbicacion == userLogged.IdUbicacion).OrderBy(y => y.NombreResponsable.Trim()).ToList();
              else if(userLogged.Rol=="Administrador")
                x=  db.tbl_Responsables.Where(z => z.Estatus == true).OrderBy(y => y.NombreResponsable.Trim()).ToList();

              if (x != null)
              {
                  if (x.Count > 0)
                  {
                      foreach (var y in x)
                      {
                          c = new ClsJefes();
                          c.id = y.idResponsable;
                          c.idUbicacion = y.IdUbicacion.GetValueOrDefault();
                          c.idPuesto = y.IdPuesto.GetValueOrDefault();
                          c.Nombre = y.NombreResponsable.Trim().ToUpper();

                          l.Add(c);
                      }
                  }
              }
            return l;
        }

        public List<ClsJefes> llenarJefes()
        {
            ClsJefes c = null;
            List<ClsJefes> l = new List<ClsJefes>();
            var x = db.tbl_Responsables.Where(z=>z.Estatus==true).OrderBy(y=>y.NombreResponsable.Trim()).ToList();
            foreach (var y in x)
            {
                c = new ClsJefes();
                c.id = y.idResponsable;
                c.idUbicacion = y.IdUbicacion.GetValueOrDefault();
                c.idPuesto = y.IdPuesto.GetValueOrDefault();
                c.Nombre = y.NombreResponsable.Trim().ToUpper();
                
                l.Add(c);
            }
            return l;
        }

        public List<tbl_Ubicaciones> getUbicaciones()
        {
            List<tbl_Ubicaciones> lstGerencias = new List<tbl_Ubicaciones>();
            var elemens = db.tbl_Ubicaciones.Where(x => x.Estatus == true).OrderBy(y=>y.Ubicacion.Trim()).ToList();
            foreach (var gerencia in elemens)
            {
                tbl_Ubicaciones data = new tbl_Ubicaciones();
                data.idUbicacion = gerencia.idUbicacion;
                data.Ubicacion = gerencia.Ubicacion.Trim().ToUpper();
                lstGerencias.Add(data);
            }

            return lstGerencias;

        }
        public List<ClsRoles> llenarcmbRoles()
        {
            ClsRoles c = null;
            List<ClsRoles> l = new List<ClsRoles>();
            l.Add(new ClsRoles { id = "Administrador", Roles = "Administrador" });
            l.Add(new ClsRoles { id = "SubAdministrador", Roles = "SubAdministrador" });
            l.Add(new ClsRoles { id = "Carga", Roles = "Carga" });
            l.Add(new ClsRoles { id = "Consulta", Roles = "Consulta" });
            return l;
        }
        public EntUsuario BuscarUsuario(int id)
        {
            tbl_Responsables l = new tbl_Responsables();
            EntUsuario entUser = new EntUsuario();
            l = db.tbl_Responsables.FirstOrDefault(x => x.idResponsable==id);
            entUser.IdUsuario = l.idResponsable;
            entUser.Nombre = l.NombreResponsable;

            entUser.Email = l.Correo;
            entUser.Usuario = l.Usuario;
            entUser.Password = l.Contrasenia;
            entUser.ficha = l.Ficha;
            entUser.idPuesto = l.IdPuesto;
            entUser.idArea = l.IdArea;
            entUser.idAquienReporta = l.IdAquienReporta;
            entUser.Rol = l.Rol;
            entUser.idUbicacion = l.IdUbicacion;
            entUser.Activo = l.Estatus;

           return entUser;

        }
        public List<SP_SelectAll_Responsables_Result> BuscarUsuario(int IdUbicacion = 0, string Nombre = null, string Correo = null)
        {

            List<SP_SelectAll_Responsables_Result> l = new List<SP_SelectAll_Responsables_Result>();
            if(IdUbicacion > 0 || Nombre != null){
                List<SP_SelectAll_Responsables_Result> Responsables = db.SP_SelectAll_Responsables().ToList();
                if(IdUbicacion > 0 && Nombre == null){
                    l = Responsables.Where(x => x.IdUbicacion == IdUbicacion && x.NombreResponsable != null).ToList();
                }
                else if (IdUbicacion == 0 && Nombre != null)
                {
                    l = Responsables.Where(x => x.NombreResponsable.IndexOf(Nombre,StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                else if (IdUbicacion == 0 && Nombre == null && Correo != null)
                {
                    l = Responsables.Where(x => x.Correo.Contains(Correo)).ToList();
                }
                else
                {
                    l = Responsables.Where(x => x.IdUbicacion == IdUbicacion && x.NombreResponsable.IndexOf(Nombre,StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
            }
            return l;

        }
        #endregion
        #region Services para monitor de indicadores

        public List<SP_SelectAll_IndicadoresLlenosFaltantesGlobalesMonitoreo_Result> GetALLIndicadoresFaltante(int IdResponsable)
        {
            List<SP_SelectAll_IndicadoresLlenosFaltantesGlobalesMonitoreo_Result> Tx = db.SP_SelectAll_IndicadoresLlenosFaltantesGlobalesMonitoreo(IdResponsable).ToList();
            return Tx.OrderBy(x => x.IdSubsistema).ToList();
        }
        public List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> GetALLDataAvancesgraficas(int idResponsable)
        {
            List<SP_SelectAll_ValoresSemaforoSubsistemasEnProceso_Result> Tx = db.SP_SelectAll_ValoresSemaforoSubsistemasEnProceso(idResponsable).ToList();
            return Tx.OrderBy(x => x.IdSubsistema).ToList();
        }
        /// <summary>
        /// Metodo devuelve numero de proyectos segun su estatus
        /// </summary>
        /// <param name="Estatus">En Proceso</param>
        /// <returns></returns>
        public int GetContProyejecucion(string Estatus)
        {
            return db.SP_SelectAllProyectosByEstatus(Estatus).Count();
        }
        public int GetContProyejecucion(string Estatus, int IdResponsable)
        {
            return db.SP_SelectAllProyectosByEstatusAndRol(Estatus, IdResponsable).Count();
        }

        public string[] GetNombreProyEjecucion(string Estatus, int IdResponsable)
        {
            return db.SP_SelectAllProyectosByEstatusAndRol(Estatus, IdResponsable).Select(x => x.NombreProyecto).ToArray();
        }

        #endregion
    }
    public class ClsRoles
    {
        public string id { get; set; }
        public string Roles { get; set; }
    }
}
