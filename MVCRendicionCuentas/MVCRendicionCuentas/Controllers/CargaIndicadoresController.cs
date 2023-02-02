using RendicionCuentasServices.Services;
using RendicionCuentasServices.Model;
using RendicionCuentasServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;

namespace MVCRendicionCuentas.Controllers
{
    public class CargaIndicadoresController : ValidatorController
    {
       
        srvCargaIndicadores srv;
        srvConfigProyecto srvConfigProyecto = new srvConfigProyecto();
        srvCompromiso srvCompromisos = new srvCompromiso();
        GetsIndicadores objGetsIndicadores = new GetsIndicadores();
        
        public CargaIndicadoresController()
        {
            setsrvCargaIndicadores();  
        }

        public void setsrvCargaIndicadores()
        {
           
            srv = new srvCargaIndicadores(IdCargaResponsable);
        }

        public ActionResult Index()
        {
            if (myCookie != null)
            {
                List<SP_VistaCargaIndicadores12MPIxReponsable_Result> indicadores12mpi = objGetsIndicadores.getIndicadores12MPI(IdCargaResponsable);
                Object indicadores = new Object();

                indicadores = new { ind12mpi = indicadores12mpi };

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var json = new HtmlString(serializer.Serialize(indicadores));

                return View(json);
            }
            else
            {
                return RedireccionaLogin();
            }

        }

        public JsonResult getIndicadores12mpiFaltantes()
        {
            List<SP_VistaCargaIndicadores12MPIxReponsable_Result> indicadores12mpifaltantes = objGetsIndicadores.getIndicadores12MPI(IdCargaResponsable, true);

            Object indicadores = new Object();
            indicadores = new { ind12mpifaltantes = indicadores12mpifaltantes };

            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getIndicadores12mpi()
        {
            List<SP_VistaCargaIndicadores12MPIxReponsable_Result> indicadores12mpi = objGetsIndicadores.getIndicadores12MPI(IdCargaResponsable);

            Object indicadores = new Object();
            indicadores = new { ind12mpi = indicadores12mpi };

            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet); ;
        }

        public JsonResult getIndicadoresSubsistemas()
        {
            //setsrvCargaIndicadores();
            List<EntProyecto> proyectosproceso = srvConfigProyecto.ProyectosPorEstatus("En Proceso").ToList();
            List<EntProyecto> proyectosresp = new List<EntProyecto>();
            List<EntProyecto> proyectosfaltantes = new List<EntProyecto>();
            srv.setListIndicadoresSubsistemas();
            foreach (EntProyecto proi in proyectosproceso)
            {
                srvConfigProyecto.GetDataProyectobyId(proi.proyecto_id);
                proi.Etapas = srv.GetFechasEtapasProy(proi.proyecto_id);
                if (proi.Etapas.Count() > 0)
                {
                    proyectosresp.Add(proi);
                }
            }

            Object indicadores = new Object();
            indicadores = new { indicadoressubsistemas = proyectosresp };

            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getIndicadoresSubsistemasFaltantes()
        {
            //setsrvCargaIndicadores();
            Object indicadores = new Object();
            srv.setListIndicadoresSubsistemas();
            indicadores = srv.getIndicadoresSubsistemasFaltantes(IdCargaResponsable);

            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult infoIndicador(int idIndicador, int idconfig, int iddata)
        {
            //Se obtienen datos del indicaor
            EntIndicadorDetalle Indicador = srvCompromisos.GetInfoIndicador(idIndicador);


            //Obtenemos datos almacenados en la tabla Data_ValoresIndicadores
            Data_ValoresIndicador Data = srv.getDataRespIndicador(iddata, Indicador.TipoCalculo, idIndicador);

            Object indicadores = new Object();
            indicadores = new { detalle = Indicador, data = Data };

            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult infoNota(int iddata)
        {
            string nota = srv.getNota(iddata);
            Object indicadores = new Object();
            indicadores = new { nota = nota };

            return Json(new JavaScriptSerializer().Serialize(indicadores), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult getDocumentos(int iddataidentificador)
        {
            List<tbl_SoportesAsignadosaIndicador> files = srv.getDocumentosIndicador(iddataidentificador);
            Object documentos = new Object();
            documentos = new { documentos = files };
            var json = Json(new JavaScriptSerializer().Serialize(documentos));
            return json;
        }

        [HttpPost]
        public JsonResult editDataIndicadorCheckProy(int idIndicador, int idConfigRespInd, int valor, int IdDataValoresIndicadores, int idproy)
        {
            Data_ValoresIndicador item = new Data_ValoresIndicador();
            Predata_ValoresIndicadoresFormula predata = new Predata_ValoresIndicadoresFormula();
            Object documentos = new Object();
            SP_SelectAllProyectosByEstatus_Result SemaforoXProy;
            item.IdDataValoresIndicadores = (IdDataValoresIndicadores > 0) ? IdDataValoresIndicadores : 0;
            item.IdConfigIndicadorResponsable = idConfigRespInd;
            item.Valor = valor;

            try
            {
                int? result = srv.EditDataValoresIndicador(item, predata, "Check");

                if (result > 0)
                {
                    //return Json(new JavaScriptSerializer().Serialize(result), JsonRequestBehavior.AllowGet);
                    string semaforo = srv.DevuelveSemaforo(Convert.ToInt32(item.Valor), idConfigRespInd);
                    SemaforoXProy = srv.getProyectoxEstatusyIdProy("En Proceso", idproy);
                    documentos = new { accion = true, idIndicador = idIndicador, idResponsable = idConfigRespInd, valor = item.Valor, idresult = result, semaforo = semaforo, semaforoxproy = SemaforoXProy, idproy = idproy };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                    return json;
                }
                else
                {
                    //return Json("false", JsonRequestBehavior.AllowGet);
                    documentos = new { accion = false };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                    return json;
                }
            }
            catch (Exception ex)
            {
                //return Json("false", JsonRequestBehavior.AllowGet);
                //return Json("false", JsonRequestBehavior.AllowGet);
                documentos = new { accion = false };
                var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                return json;
            }

        }


        [HttpPost]
        public JsonResult editDataIndicadorCheck(int idIndicador, int idConfigRespInd, int valor, int IdDataValoresIndicadores)
        {
            Data_ValoresIndicador item = new Data_ValoresIndicador();
            Predata_ValoresIndicadoresFormula predata = new Predata_ValoresIndicadoresFormula();
            Object documentos = new Object();
            item.IdDataValoresIndicadores = (IdDataValoresIndicadores > 0) ? IdDataValoresIndicadores : 0;
            item.IdConfigIndicadorResponsable = idConfigRespInd;
            item.Valor = valor;

            try
            {
                int? result = srv.EditDataValoresIndicador(item, predata, "Check");

                if (result > 0)
                {
                    //return Json(new JavaScriptSerializer().Serialize(result), JsonRequestBehavior.AllowGet);
                    string semaforo = srv.DevuelveSemaforo(Convert.ToInt32(item.Valor), idConfigRespInd);
                    documentos = new { accion = true, idIndicador = idIndicador, idResponsable = idConfigRespInd, valor = item.Valor, idresult = result, semaforo = semaforo };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos));
                    return json;
                }
                else
                {
                    //return Json("false", JsonRequestBehavior.AllowGet);
                    documentos = new { accion = false };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                    return json;
                }
            }
            catch (Exception ex)
            {
                //return Json("false", JsonRequestBehavior.AllowGet);
                //return Json("false", JsonRequestBehavior.AllowGet);
                documentos = new { accion = false };
                var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                return json;
            }

        }


        [HttpPost]
        public JsonResult editDataIndicadorFormulaValor(
            int idIndicador, int idConfigRespInd, string TipoCalculo, double? ResultValor, double? dataProgramada, double? dataReal, double? ResultFormula, int IdDataValoresIndicadores, int? idproy)
        {
            Data_ValoresIndicador item = new Data_ValoresIndicador();
            Predata_ValoresIndicadoresFormula predata = new Predata_ValoresIndicadoresFormula();
            Object documentos = new Object();
            SP_SelectAllProyectosByEstatus_Result SemaforoXProy = new SP_SelectAllProyectosByEstatus_Result();

            item.IdDataValoresIndicadores = (IdDataValoresIndicadores > 0) ? IdDataValoresIndicadores : 0;

            if (TipoCalculo == "Valor" || TipoCalculo == "Programa")
            {
                item.Valor = ResultValor;
                item.IdConfigIndicadorResponsable = idConfigRespInd;
            }
            else
            {
                item.Valor = ResultFormula;
                item.IdConfigIndicadorResponsable = idConfigRespInd;
                predata.ValorProgramado = dataProgramada;
                predata.ValorReal = dataReal;
                predata.Resultado = ResultFormula;
            }

            try
            {

                int? result = srv.EditDataValoresIndicador(item, predata, TipoCalculo);

                if (result > 0)
                {
                    string semaforo = srv.DevuelveSemaforo(Convert.ToInt32(item.Valor), idConfigRespInd);
                    if(idproy != null) SemaforoXProy = srv.getProyectoxEstatusyIdProy("En Proceso", (int)idproy);
                    documentos = new { accion = true, idIndicador = idIndicador, idResponsable = idConfigRespInd, valor = item.Valor, idresult = result, semaforo = semaforo, semaforoxproy = SemaforoXProy, idproy = idproy };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos));
                    return json;
                }
                else
                {
                    //return Json("false", JsonRequestBehavior.AllowGet);
                    documentos = new { accion = false };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                    return json;
                }
            }
            catch (Exception ex)
            {
                //return Json("false", JsonRequestBehavior.AllowGet);
                documentos = new { accion = false };
                var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                return json;
            }
        }

        [HttpPost]
        public JsonResult editDataIndicadorPrograma(
            int IdIndicador, int idConfigRespInd, string TipoCalculo, double? ResultPrograma, Predata__ValoresIndicadoresPrograma predataform, int IdDataValoresIndicadores, int? idproy)
        {
            Data_ValoresIndicador item = new Data_ValoresIndicador();
            Predata__ValoresIndicadoresPrograma predata = new Predata__ValoresIndicadoresPrograma();
            Object documentos = new Object();
            SP_SelectAllProyectosByEstatus_Result SemaforoXProy = new SP_SelectAllProyectosByEstatus_Result();

            item.IdDataValoresIndicadores = (IdDataValoresIndicadores > 0) ? IdDataValoresIndicadores : 0;

            item.Valor = ResultPrograma;
            item.IdConfigIndicadorResponsable = idConfigRespInd;
            predataform.Resultado = ResultPrograma;

            try
            {

                int? result = srv.EditDataValoresIndicador(item, predataform, TipoCalculo);

                if (result > 0)
                {
                    //return Json(new JavaScriptSerializer().Serialize(result), JsonRequestBehavior.AllowGet);
                    string semaforo = srv.DevuelveSemaforo(Convert.ToInt32(item.Valor), idConfigRespInd);
                    if(idproy != null) SemaforoXProy = srv.getProyectoxEstatusyIdProy("En Proceso", (int)idproy);
                    documentos = new { accion = true, idIndicador = IdIndicador, idResponsable = idConfigRespInd, valor = item.Valor, idresult = result, semaforo = semaforo, semaforoxproy = SemaforoXProy, idproy = idproy };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos));
                    return json;
                }
                else
                {
                    //return Json("false", JsonRequestBehavior.AllowGet);
                    documentos = new { accion = false };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                    return json;
                }

            }
            catch (Exception ex)
            {
                //return Json("false", JsonRequestBehavior.AllowGet);
                documentos = new { accion = false };
                var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                return json;
            }
        }

        [HttpPost]
        public JsonResult editNotaIndicador(tbl_ComentariosAsignadosIndicador item)
        {
            try
            {

                int? result = srv.EditNotaIndicador(item);

                if (result > 0)
                {
                    return Json(new JavaScriptSerializer().Serialize(result), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult addRecurso(int IdDataValoresIndicadores, string NombreDoc, HttpPostedFileBase inputFiles)
        {
            try
            {

                if (inputFiles.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(inputFiles.FileName);
                    string extension = Path.GetExtension(inputFiles.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    var relative = "~/App_Data/uploads/" + fileName;
                    tbl_SoportesAsignadosaIndicador item = new tbl_SoportesAsignadosaIndicador();
                    item.IdDataValoresIndicadores = IdDataValoresIndicadores;
                    item.NombreDoc = NombreDoc;
                    item.RutaDoc = relative;
                    item.FechaCreacion = DateTime.Now;

                    int id = srv.AddDocumento(item);
                    if (id > 0)
                    {
                        inputFiles.SaveAs(path);
                        List<tbl_SoportesAsignadosaIndicador> files = srv.getDocumentosIndicador(IdDataValoresIndicadores);
                        Object documentos = new Object();
                        documentos = new { documentos = files };
                        var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                        return json;
                    }
                    else
                    {
                        return Json("false", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

            return Json("");
        }


        [HttpPost]
        public JsonResult deleteRecurso(int idConfig)
        {
            Object documentos = new Object();

            try
            {
                tbl_SoportesAsignadosaIndicador id = srv.DeleteDocumento(idConfig);
                if (id.IdSoportesAsignadosaIndicador > 0)
                {
                    int? iddata = id.IdDataValoresIndicadores;
                    List<tbl_SoportesAsignadosaIndicador> files = srv.getDocumentosIndicador(iddata);

                    documentos = new { documentos = files, accion = true };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                    return json;
                }
                else
                {
                    documentos = new { accion = false };
                    var json = Json(new JavaScriptSerializer().Serialize(documentos), JsonRequestBehavior.AllowGet);
                    return json;
                    //return Json({accion=false}, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                documentos = new { accion = false };
                var json = Json(new JavaScriptSerializer().Serialize(documentos));
                return json;
            }

            //return Json("false");
        }

        public FileResult Download(int idFile)
        {
            tbl_SoportesAsignadosaIndicador doc = srv.getDocument(idFile);
            var filepath = Server.MapPath(doc.RutaDoc);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);
            string extension = Path.GetExtension(filepath);
            string fileName = Path.GetFileName(filepath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }



    }
}