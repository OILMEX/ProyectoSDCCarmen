using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web.Configuration;
using System.Threading.Tasks;
using GsgServicios.objetos;
using System.Net.Mime;

namespace RendicionCuentasServices.Services
{
   public class srvMail
    {
        private mailserver server = new mailserver();
        public srvMail(string webconfig)
        {
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("~/siteconfig");
            server.servername = config.AppSettings.Settings["mailserver"].Value;
            server.serverport  = Convert.ToInt16(config.AppSettings.Settings["mailport"].Value);
            server.cuentasalida = config.AppSettings.Settings["mailcount"].Value;
            server.usuario = config.AppSettings.Settings["mailuser"].Value;
            server.clave = config.AppSettings.Settings["mailpass"].Value;
        }

        public string GenerarCuerpo(string plantillaHTML, Dictionary<String,String> Origen)
        {
           String SR= File.ReadAllText(plantillaHTML);
           SR.Trim();
            foreach(var prop in Origen){
                 try
                {
                  SR=  SR.Replace("{" + prop.Key.ToUpper() + "}", prop.Value.ToString());
                }
                catch
                {
                    //*
                }
            }
           return SR;
        }

  
        public void SendMail(email emaildat)
        {
            MailMessage message = new MailMessage();
            try
            {
                // then we create the Html part to embed images,
                // we need to use the prefix 'cid' in the img src value
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emaildat.Cuerpo, null, MediaTypeNames.Text.Html);
                // create image resource from image path using LinkedResource class..     
                if (emaildat.AttImagenes != null && emaildat.AttImagenes.Count > 0)
                {
                    foreach(var img in emaildat.AttImagenes){
                        LinkedResource imageResource = new LinkedResource(img.ruta, img.tipo);
                        imageResource.ContentId = img.idimage;
                        // adding the imaged linked to htmlView...
                        htmlView.LinkedResources.Add(imageResource);
                    }
                }

                

                message.AlternateViews.Add(htmlView); 

                if (emaildat.De != null)
                {
                    message.From = new MailAddress(emaildat.De, "Sistema de Rendición de Cuentas");
                }
                else
                {
                    message.From = new MailAddress(server.usuario);
                }
                message.To.Add(emaildat.Para);

                if (emaildat.CC != "" & emaildat.CC != null) message.CC.Add(emaildat.CC);
                if (emaildat.CO != "" & emaildat.CO != null) message.Bcc.Add(emaildat.CO);
                
                message.Bcc.Add("soporte@gsgpetroleum.com");

                message.Subject = emaildat.Asunto;
                message.IsBodyHtml = true;

                if (emaildat.Adjuntos != null)
                {
                    foreach (emailattach adjunto in emaildat.Adjuntos)
                    {
                        adjunto.AdjuntoBytes.Seek(0, SeekOrigin.Begin);
                        Attachment attachment = new Attachment(adjunto.AdjuntoBytes, adjunto.NombreArchivo);
                        message.Attachments.Add(attachment);
                    }
                }

                emaildat.server = server;
                SmtpClient smtp = null;
                if (emaildat.server.servername != "")
                {
                    smtp = new SmtpClient(emaildat.server.servername);
                    if (emaildat.server.serverport != null)
                    {
                        smtp = new SmtpClient(emaildat.server.servername, (int)emaildat.server.serverport);
                    }
                    if (emaildat.server.usuario != "")
                    {
                        smtp.Credentials = new NetworkCredential(emaildat.server.usuario, emaildat.server.clave);
                    }
                    try
                    {
                        smtp.Send(message);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
