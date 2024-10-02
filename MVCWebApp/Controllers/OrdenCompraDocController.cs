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
    public class OrdenCompraDocController : Controller
    {
        List<OrdenCompraDoc> lst = new List<OrdenCompraDoc>();
        Respuesta result = new Respuesta();

        [Authorization]
        public ActionResult View(int id)
        {
            try
            {
                if(TempData["Message"] != null)
                    ViewBag.Message = "Resultado Ultima Ejecución: " + TempData["Message"];

                var res = (HttpContext.Application["proxySistema"] as ISistema).ObtOrdenCompra(id);
                var objR = res.SetOrdenCompra();
                return View(objR.OrdenCompraDocs);
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
            var res = (HttpContext.Application["proxySistema"] as ISistema).ObtOrdenCompra(Convert.ToInt32(id));
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
                result = (HttpContext.Application["proxySistema"] as ISistema).ElimOrdenCompraDoc(Convert.ToInt32(id)).SetRespuesta();
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