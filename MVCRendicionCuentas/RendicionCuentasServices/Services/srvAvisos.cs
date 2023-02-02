using GsgServicios.objetos;
using RendicionCuentasServices.DTO.Mail;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace RendicionCuentasServices.Services
{
    public class srvAvisos
    {
        dbSDCEntities db = new dbSDCEntities();
        public int SaveAlarma(Config_Avisos Avisos) {
            int IdAvisos = 0;
            try
            {
                var c = db.Config_Avisos.Count();
                if (c > 0)
                {   
                    var e = db.Config_Avisos.Single(x => x.IdConfigAvisos == 1);
                    e.DiadelMesPrimerAviso = Avisos.DiadelMesPrimerAviso;
                    e.DiadelMesSegundoAviso = Avisos.DiadelMesSegundoAviso;
                    e.DiadelMesTercerAviso = Avisos.DiadelMesTercerAviso;
                    db.SaveChanges();
                    IdAvisos = e.IdConfigAvisos;
                }
                else {
                    db.Config_Avisos.Add(Avisos);
                    db.SaveChanges();
                    IdAvisos = Avisos.IdConfigAvisos;
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
            return IdAvisos;
        }

        public Config_Avisos getConfigAvisos() {
            Config_Avisos cfgAvisos = new Config_Avisos();
            var c = db.Config_Avisos.Count();
            if(c > 0){
                cfgAvisos = db.Config_Avisos.SingleOrDefault();
            }
            return cfgAvisos;
        }

        public void SendAlarma(string ultDia){
            srvMail srvMail = new srvMail("~/siteconfig");
            try
            {
                var Responsables = db.SP_SelectAll_Responsables();
                if (Responsables != null)
                {
                    foreach(var resp in Responsables){
                        
                        List<SP_VistaCargaIndicadoresProyectos_Result> listSubsis = db.SP_VistaCargaIndicadoresProyectos(resp.idResponsable).Where(x=>x.Valor==null) .ToList();
                        List<SP_VistaCargaIndicadores12MPIxReponsable_Result> list12mpi = db.SP_VistaCargaIndicadores12MPIxReponsable(resp.idResponsable).Where(x => x.Valor == null).ToList();
                        if (listSubsis.Count > 0 || list12mpi.Count()>0)
                        {
                            int N12MPI = list12mpi!=null? list12mpi.Count(): 0;
                            int NSASP = listSubsis!=null? listSubsis.Where(x => x.IdSubsistema == 2).Count(): 0;
                            int NSAA = listSubsis != null? listSubsis.Where(x => x.IdSubsistema == 3).Count() : 0;
                            int NSAST = listSubsis != null? listSubsis.Where(x => x.IdSubsistema == 4).Count() : 0;
                            string Fecha = ultDia + " de " + FirstCharToUpper(DateTime.Now.ToString("MMMM", CultureInfo.CurrentCulture)) + " de " +DateTime.Now.Year.ToString();
                           

                            Dictionary<string, string> DatosMail = new Dictionary<string, string>() { 
                                {"RESPONSABLE",resp.NombreResponsable},
                                {"FECHAVENCIMIENTO", Fecha},
                                {"NMPI", N12MPI.ToString()},
                                {"NSASP", NSASP.ToString()},
                                {"NSAA", NSAA.ToString()},
                                {"NSAST", NSAST.ToString()}                            
                            };

                            //Preparamos correo para enviar
                            email correo = new email();
                            string PathMail = @"C:\WebSites\mail_indicadores\";

                            correo.Cuerpo = srvMail.GenerarCuerpo(PathMail + "content.html", DatosMail);
                            correo.De = "mailnotificacionesSDC@gsgpetroleum.com";
                            correo.Asunto = "Rendición de Cuentas: AVISO";
                            correo.Para = resp.Correo; //ingmiguelaguilarm@gmail.com //"ironluis.js@gmail.com"

                            List<attimage> listImages = new List<attimage>();
                            listImages.Add(new attimage { idimage = "image_1", ruta = PathMail + "image-1.jpg", tipo = "image/jpeg" });
                            listImages.Add(new attimage { idimage = "image_2", ruta = PathMail + "image-2.jpg", tipo = "image/jpeg" });
                            listImages.Add(new attimage { idimage = "image_3", ruta = PathMail + "image-3.png", tipo = "image/png" });
                            listImages.Add(new attimage { idimage = "image_4", ruta = PathMail + "image-4.png", tipo = "image/png" });
                            listImages.Add(new attimage { idimage = "image_19", ruta = PathMail + "image-19.png", tipo = "image/png" });
                            listImages.Add(new attimage { idimage = "image_20", ruta = PathMail + "background.jpg", tipo = "image/jpeg" });
                            listImages.Add(new attimage { idimage = "image_21", ruta = PathMail + "background-2.jpg", tipo = "image/jpeg" });
                            listImages.Add(new attimage { idimage = "image_22", ruta = PathMail + "background-5.jpg", tipo = "image/jpeg" });
                            listImages.Add(new attimage { idimage = "image_23", ruta = PathMail + "box-bg.jpg", tipo = "image/jpeg" });
                            listImages.Add(new attimage { idimage = "image_24", ruta = PathMail + "box-bg-3.png", tipo = "image/png" });
                            listImages.Add(new attimage { idimage = "image_25", ruta = PathMail + "page-bg.jpg", tipo = "image/jpeg" });
                            //listImages.Add(new attimage { idimage = "image_26", ruta = PathMail + "image-8.jpg", tipo = "image/jpeg" });
                            correo.AttImagenes = listImages;

                           srvMail.SendMail(correo);
                        }
                    }

                   
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

    }
}
