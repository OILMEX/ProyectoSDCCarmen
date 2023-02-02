using RendicionCuentasServices.Model;
using RendicionCuentasServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCRendicionCuentas.Controllers
{
    public class AvisosController : ValidatorController
    {
        srvAvisos srvAviso = new srvAvisos();
        //
        // GET: /Avisos/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Alarmas/
        public ActionResult Alarmas()
        {
            if (myCookie != null)
            {
                Config_Avisos cnfAvisos = srvAviso.getConfigAvisos();
                if (cnfAvisos != null)
                {
                    ViewData["DiadelMesPrimerAviso"] = cnfAvisos.DiadelMesPrimerAviso;
                    ViewData["DiadelMesSegundoAviso"] = cnfAvisos.DiadelMesSegundoAviso;
                    ViewData["DiadelMesTercerAviso"] = cnfAvisos.DiadelMesTercerAviso;
                }
                else
                {
                    ViewData["DiadelMesPrimerAviso"] = "";
                    ViewData["DiadelMesSegundoAviso"] = "";
                    ViewData["DiadelMesTercerAviso"] = "";
                }
                return View();
            }
            else
            {
                return RedireccionaLogin();
            }
        }

        [HttpGet]
        public JsonResult saveAlarma(int DiadelMesPrimerAviso, int DiadelMesSegundoAviso, int DiadelMesTercerAviso)
        {
            try
            {
                Config_Avisos item = new Config_Avisos();
                item.DiadelMesPrimerAviso = DiadelMesPrimerAviso;
                item.DiadelMesSegundoAviso = DiadelMesSegundoAviso;
                item.DiadelMesTercerAviso = DiadelMesTercerAviso;

                int IdAviso = srvAviso.SaveAlarma(item);
                if (IdAviso > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(IdAviso), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EnviarAlarma()
        {
            int diaActual = DateTime.Now.Day;
            Config_Avisos item = srvAviso.getConfigAvisos();
            if(item != null){
                Dictionary<int, int> DiasAvisos = new Dictionary<int, int>(){
                    {1,item.DiadelMesPrimerAviso.Value},
                    {2,item.DiadelMesSegundoAviso.Value},
                    {3,item.DiadelMesTercerAviso.Value}
                };

                foreach (var dia in DiasAvisos) { 
                    if(dia.Value == diaActual){
                        if (item.DiadelMesTercerAviso.Value != null)
                        {
                            srvAviso.SendAlarma(item.DiadelMesTercerAviso.Value.ToString());
                        }
                        
                    }
                }
            }
            return View();
        }
	}
}