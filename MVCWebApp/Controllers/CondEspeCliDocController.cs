using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace com.msc.frontend.mvc.Controllers
{
    public class CondEspeCliDocController : Controller
    {
        List<CondEspeCliDoc> lst = new List<CondEspeCliDoc>();
        Respuesta result = new Respuesta();

        private void SetBitacora(BitacoraActionCode code, string id, string message)
        {
            
            try
            {
                BitacoraDTO objBit = new BitacoraDTO
                {
                    Accion = MessagesApp.BitacoraActionMessage(code),
                    IdInterno = Convert.ToInt32(id),
                    Tabla = "CondEspeCli",
                    User = (Session["Usuario"] as ExternoDTO).Usuario,
                    Tipo = MessagesApp.BitacoraModeMessage(BitacoraModeCode.Manual),
                    Detalle = message
                };
                var bita = (HttpContext.Application["proxySistema"] as ISistema).EditBitacora(objBit).SetRespuesta();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
            }
        }

        [Authorization]
        public ActionResult View(int id)
        {
            try
            {
                if(TempData["Message"] != null)
                    ViewBag.Message = "Resultado Ultima Ejecución: " + TempData["Message"];

                var res = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCli(id);
                var objR = res.SetCondEspeCli();
                return View(objR.CondEspeCliDocs);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                TempData["Message"] = MessagesApp.BackAppMessage(MessageCode.InternalError).Descripcion;
                return RedirectToAction("ErrorJson", "Home");
            }
        }

        [Authorization]
        [HttpPost]
        public JsonResult ViewDocs(string id)
        {
            List<DocumentoDropZone> lstRes = new List<DocumentoDropZone>();
            var res = (HttpContext.Application["proxySistema"] as ISistema).ObtCondEspeCli(Convert.ToInt32(id));
            foreach (var item in res.Documentos)
            {
                lstRes.Add(new DocumentoDropZone {
                    Id = item.IdTarifaCEDoc,
                    name = item.Nombre,
                    size = Convert.ToInt64(item.TamanoMB*1024*1024),
                    type = item.Type
                });
            }
            return Json(lstRes);
        }

        [Authorization]
        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                result = (HttpContext.Application["proxySistema"] as ISistema).ElimCondEspeCliDoc(Convert.ToInt32(id)).SetRespuesta();
                SetBitacora(BitacoraActionCode.Eliminacion, result.Metodo, "Se eliminó el documento " + result.Message);
                return Json(result);
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                result = MessagesApp.BackAppMessage(MessageCode.InternalError);
                return Json(result);
            }
        }

    }
}